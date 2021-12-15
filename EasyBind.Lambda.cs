using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Limcap.EasyTools.Wpf.DataBinding {

	public partial class EasyBind {

		public abstract class SingleBind : INotifyPropertyChanged {
			public SingleBind(string path = "Value" ) { _path = path; }
			protected readonly string _path;
			public void NotifyChange() => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( _path ) );
			public event PropertyChangedEventHandler PropertyChanged;
		}


		public class Lambda<V> : SingleBind {
			public Lambda( Func<V> value, EasyBind parent = null ) { _value = value; parent.AddLambda( this ); }
			private readonly Func<V> _value;
			public V Value => _value();
			
			public static E Set<E>( E target, DependencyProperty prop, EasyBind parent, Func<V> value ) where E : FrameworkElement {
				if (parent is null) return Set( target, prop, value );
				var lambda = new Lambda<V>( value, parent );
				var binding = new Binding( lambda._path ) { Source = lambda, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
				target.SetBinding( prop, binding );
				return target;
			}

			/// <summary>
			/// This method needs to be executed only after the element has loaded because of how the Lambda works. There must
			/// be a parent EasyBind object that will notify its Lambda children, so since the signature of this method does
			/// not contain a parameter for the EasyBind parent, it must be resolved after the DataContext of the UI tree
			/// have resolved. (The DataContext of an element, if null will default to the DataContext of the parent object) 
			/// </summary>
			/// <typeparam name="E">FrameworkElement</typeparam>
			/// <param name="target">the invoking this method</param>
			/// <param name="prop">the target dependency property</param>
			/// <param name="value">the function that returns the dependent value</param>
			/// <returns></returns>
			public static E Set<E>( E target, DependencyProperty prop, Func<V> value ) where E : FrameworkElement {
				target.Loaded += ( obj, arg ) => {
					var parent = (obj as FrameworkElement).DataContext;
					if (parent is null) throw new NullReferenceException( $"'EasyBind.Lambda' was invoked with null DataContext" );
					//else if (!(parent is EasyBind)) throw new InvalidCastException( $"'EasyBind.Lambda' was invoked with DataContext of type different than 'EasyBind'. Type: {parent.GetType().Name}" );
					else if (!(parent is EasyBind)) throw new InvalidCastException( $"'DataContext' object inherited by 'EasyBind.Lambda' object must be of type 'EasyBind'. Attempted type: '{parent.GetType().Name}'" );
					else {
						var lambda = new Lambda<V>( value, parent as EasyBind );
						target.SetBinding( prop, new Binding( lambda._path ) { Source = lambda, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } );
					}
				};
				return target;
			}
		}





		/// <summary>
		/// Describes a reaction that occurs when a dependency property on the target FrameworkElement changes.
		/// </summary>
		/// <typeparam name="V">Subtype of FrameworkElement</typeparam>
		public class Reaction<V> : SingleBind {
			private Reaction( FrameworkElement target, Action<V> func ) { _action = func; _target = target; }
			private readonly Action<V> _action;
			private readonly FrameworkElement _target;
			private V _value;
			private bool _created;

			public V Value {
				get => _value;
				set {	_value = value; if (_created) _action( _value ); else _created = true;  }
			} 

			public static E OnTargetChange<E>( E target, DependencyProperty dprop, Action<V> func ) where E : FrameworkElement {
				var src = new Reaction<V>( target, func );
				var binding = new Binding( nameof( Value ) ) { Source = src, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Mode = BindingMode.OneWayToSource };
				BindingOperations.SetBinding( target, dprop, binding );
				return target;
			}
		}






		public class Single<V, C> : INotifyPropertyChanged {
			public Single( Func<V, C> convert, Func<C, V> convertBack ) { _convert = convert; _convertBack = convertBack; _prop = nameof( ConvertedValue ); }
			public Single() { _prop = nameof( BaseValue ); }
			string _prop;
			V _value;
			Func<V, C> _convert;
			Func<C, V> _convertBack;
			public C ConvertedValue { get => _convert( _value ); set { _value = _convertBack( value ); PropertyChanged( this, new PropertyChangedEventArgs( _prop ) ); } }
			public V BaseValue { get => _value; set { _value = value; PropertyChanged( this, new PropertyChangedEventArgs( _prop ) ); } }
			public void NotifyChange() => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( _prop ) );
			public event PropertyChangedEventHandler PropertyChanged;
			public Binding BaseBinding() { return new Binding( _prop ) { Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }; }
			public Binding ConvertedBinding() { return new Binding( nameof(ConvertedValue) ) { Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Mode = BindingMode.OneWay }; }
		}


		//public class Single<E, V, C> : INotifyPropertyChanged where E : FrameworkElement{
		//	public Single( E element Func<V, C> convert, Func<C, V> convertBack )  {
		//		_convert = convert; _convertBack = convertBack; _prop = nameof( ConvertedValue ); }
		//	public Single() { _prop = nameof( BaseValue ); }
		//	string _prop;
		//	V _value;
		//	Func<V, C> _convert;
		//	Func<C, V> _convertBack;
		//	public C ConvertedValue { get => _convert( _value ); set { _value = _convertBack( value ); PropertyChanged( this, new PropertyChangedEventArgs( _prop ) ); } }
		//	public V BaseValue { get => _value; set { _value = value; PropertyChanged( this, new PropertyChangedEventArgs( _prop ) ); } }
		//	public void NotifyChange() => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( _prop ) );
		//	public event PropertyChangedEventHandler PropertyChanged;
		//	public Binding BaseBinding() { return new Binding( _prop ) { Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }; }
		//	public Binding ConvertedBinding() { return new Binding( nameof( ConvertedValue ) ) { Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Mode = BindingMode.OneWay }; }
		//}


		public class Converter<V,C> : IValueConverter {
			public Converter( Func<V, C> convert, Func<C, V> convertBack ) {
				_convert = convert; _convertBack = convertBack;
			}

			Func<V, C> _convert;
			Func<C, V> _convertBack;

			public Binding GetBinding(FrameworkElement source, string path) {
				return new Binding(path) {
					Source = source,
					UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
					Mode = BindingMode.OneWay,
					Converter = this
				};
			}

			public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
				return value is V v ? (object)_convert( v ) : null;
			}

			public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
				return value is C c ? (object)_convertBack( c ) : null;
			}

			//public static Binding ConvertibleBinding( this FrameworkElement e, string path, Func<V, C> convert, Func<C, V> convertBack )
			//	=> new ASd( e, path, convert, convertBack ).Binding( e, path );
			public Converter( params Tuple<V, C>[] conversions ) {

			}
		}
	}
}

