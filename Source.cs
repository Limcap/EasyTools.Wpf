using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Limcap.WPF.EasyBinder {
	public class EasyBind_Double : EasyBind<double> {

		protected readonly Ruler Ruler;
		
		public EasyBind_Double( double baseValue ) : base( baseValue ) {
			Ruler = new Ruler( Base, Value );
		}
	}









	public class EasyBind<V> : EasyBind {
		private V _Value;
		private readonly V _Base;

		public V Base => _Base;

		public V Value {
			get { return _Value; }
			set { _Value = value; NotifyChanges(); }
		}

		public EasyBind( V baseValue ) : base( baseValue ) {
			_Base = baseValue;
			_Value = baseValue;
		}

		//public new T Bind( FrameworkElement control, DependencyProperty dp, string name ) {
		//	control.SetBinding( dp, Get( name ) );
		//	//bindings.Add( name, Get( name ) );
		//	return default( T );
		//}
	}








	public partial class EasyBind : INotifyPropertyChanged {
		private readonly Dictionary<string, Binding> _Bindings;
		private List<Lambda> _Lambdas = new List<Lambda>();
		public event PropertyChangedEventHandler PropertyChanged;

		public EasyBind( dynamic _base ) {}


		protected void NotifyChanges() {
			var props = GetType().GetProperties().Where( p => !p.Name.Contains( "_" ) && p.Name != "Value" && p.Name != "Base" );
			foreach (var entry in _Bindings) OnPropertyChanged( entry.Key );
			foreach (var entry in _Lambdas) entry.NotifyChange();
		}


		public void AddLambda( Lambda bind ) { _Lambdas.Add( bind ); }


		protected void OnPropertyChanged( string info ) {
			if (PropertyChanged != null)
				PropertyChanged( this, new PropertyChangedEventArgs( info ) );
		}


		public static E Set<E>( E el, DependencyProperty dp, string path ) where E : FrameworkElement {
			el.SetBinding( dp, new Binding( path ) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } );
			return el;
		}


		public static E Set<E>( E el, DependencyProperty dp, EasyBind source, string path ) where E : FrameworkElement {
			var bd = new Binding( path ) { Source = source, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
			source._Bindings.Add( path, bd );
			el.SetBinding( dp, bd );
			return el;
		}
	}



	//public partial class EasyBind_old : INotifyPropertyChanged {
		
	//	private readonly Dictionary<string, Binding> bindings;
	//	public event PropertyChangedEventHandler PropertyChanged;

	//	public EasyBind_old( dynamic _base ) {
	//		var props = GetType().GetProperties().Where( p => !p.Name.Contains( "_" ) && p.Name != "Value" && p.Name != "Base" );
	//		bindings = new Dictionary<string, Binding>();
	//		foreach (var p in props)
	//			bindings.Add( p.Name, new Binding( p.Name ) {
	//				Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
	//			} );
	//	}

	//	public Binding Get( string name ) => bindings[name];

	//	protected void NotifyChanges() {
	//		var props = GetType().GetProperties().Where( p => !p.Name.Contains( "_" ) && p.Name != "Value" && p.Name != "Base" );
	//		foreach (var prop in props) OnPropertyChanged( prop.Name );
	//		foreach (var ev in SingleBinds) ev.NotifyChange();
	//	}

	//	public List<EasyBind.Lambda> SingleBinds = new List<EasyBind.Lambda>();

	//	public void AddSingleBind( EasyBind.Lambda bind ) { SingleBinds.Add( bind ); }

	//	protected void OnPropertyChanged( string info ) {
	//		if (PropertyChanged != null) PropertyChanged( this, new PropertyChangedEventArgs( info ) );
	//	}

	//	public K Bind<K>( FrameworkElement control, DependencyProperty dp, string name, K value = default( K ) ) {
	//		BindingOperations.SetBinding( control, dp, Get( name ) );
	//		bindings.Add( name, Get( name ) );
	//		return value;
	//	}

	//	public dynamic Bind( FrameworkElement control, DependencyProperty dp, string name ) {
	//		BindingOperations.SetBinding( control, dp, Get( name ) );
	//		bindings.Add( name, Get( name ) );
	//		return null;
	//	}
	//}








	public static class EasyBindExt_Layout {
		// Dock methods
		public static T SetDock<T>( this T elem, Dock value ) where T : UIElement { DockPanel.SetDock( elem, value ); return elem; }

		// Make it easier to get child panel of a decorator and then the children of the child panel
		//public static UIElementCollection NextChildren<T>( this T elem ) where T : Decorator { return (elem.Child as Panel)?.Children; }
		public static void Add( this UIElementCollection collection, params UIElement[] items ) { foreach (var item in items) collection.Add( item ); }
		
		public static T AppendChild<T>( this T elem, params UIElement[] items ) where T : Decorator {
			Add( NextChildren( elem ), items );
			return elem;
		}
		
		public static UIElementCollection NextChildren<K>( this K elem ) {
			if (elem is ContentControl elem1) return elem1.Content.NextChildren();
			else if (elem is Decorator elem2) return elem2.Child.NextChildren();
			else if (elem is Panel elem3) return elem3.Children;
			return null;
		}

		public static UIElementCollection ForEach( this UIElementCollection collection, Action<UIElement> action ) { foreach (UIElement item in collection) action( item ); return collection; }
		
		public static void ForEach<T>( this UIElementCollection collection, Action<T> action ) where T : UIElement { foreach (UIElement e in collection) if (e is T t) action( t ); }
	}








	public static class EasyBindExt_Binding {
		// Methodos base de binding
		//public static T Bind<T>( this T el, DependencyProperty dp, BindingBase binding ) where T : FrameworkElement {
		//	el.SetBinding( dp, binding );
		//	return el;
		//}
		//public static T Bind<T>( this T el, DependencyProperty dp, EasyBind ds, string valueName ) where T : FrameworkElement {
		//	el.SetBinding( dp, ds.Get( valueName ) );
		//	return el;
		//}
		public static E Bind<E, V>( this E el, DependencyProperty dp, EasyBind ds, Func<V> fn ) where E : FrameworkElement {
			return EasyBind.Lambda<V>.Set( el, dp, ds, fn );
		}
		public static E Bind<E, V>( this E el, DependencyProperty dp, Func<V> fn ) where E : FrameworkElement {
			return EasyBind.Lambda<V>.Set( el, dp, fn );
		}
		public static E Bind<E>( this E el, DependencyProperty dp, EasyBind ds, string pn ) where E : FrameworkElement {
			return EasyBind.Set( el, dp, ds, pn );
		}
		public static E Bind<E>( this E el, DependencyProperty dp, string pn ) where E : FrameworkElement {
			return EasyBind.Set( el, dp, pn );
		}
	}








	//public partial class EasyBind {
		
	//	public static E Set<E>( E el, DependencyProperty dp, string propName ) where E : FrameworkElement {
	//		el.SetBinding( dp, new Binding( propName ) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } );
	//		//el.Loaded += ( obj, arg ) => {
	//		//	var eb = ((obj as FrameworkElement).DataContext as EasyBind);
	//		//	el.SetBinding( dp, new Binding( propName ) { Source = eb, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } );
	//		//};
	//		return el;
	//	}




	//	public static E Set<E>( E el, DependencyProperty dp, EasyBind dataSource, string propName ) where E : FrameworkElement {
	//		var bd = new Binding( propName ) { Source = dataSource, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
	//		dataSource.bindings.Add( propName, bd );
	//		el.SetBinding( dp, bd );
	//		return el;
	//	}
	//}







	public partial class EasyBind {

		public class Lambda : INotifyPropertyChanged {
			public void NotifyChange() => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( "Value" ) );
			public event PropertyChangedEventHandler PropertyChanged;
		}




		public class Lambda<V> : Lambda {
			
			public Lambda( EasyBind eb, Func<V> func ) { _func = func; _eb = eb; eb.AddLambda( this ); }
			
			private readonly Func<V> _func;
			
			private readonly EasyBind _eb;
			
			public V Value => _func();
			
			public static Binding To( EasyBind eb, Func<V> func ) => new Binding( "Value" ) { Source = new Lambda<V>( eb, func ), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
			
			public static E Set<E>( E element, DependencyProperty dp, EasyBind dataContext, Func<V> func ) where E : FrameworkElement {
				var sb = new Lambda<V>( dataContext, func );
				var bd = new Binding( "Value" ) { Source = sb, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
				element.SetBinding( dp, bd );
				return element;
			}

			public static E Set<E>( E el, DependencyProperty dp, Func<V> func ) where E : FrameworkElement {
				el.Loaded += ( obj, arg ) => {
					var eb = ((obj as FrameworkElement).DataContext as EasyBind);
					var bd = new Lambda<V>( eb, func );
					el.SetBinding( dp, new Binding( "Value" ) { Source = bd, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } );
				};
				return el;
			}
		}
	}








	public partial class EasyBind {
		public class Ruler {
			readonly double Base;
			readonly double Value;
			public Ruler( double _base, double _value ) {
				Base = _base;
				Value = _value;
			}
			public double Ratio1 => Value / Base;
			public double OneThird => Base / 3.0 * Ratio1;
			public double TwoThirds => Base / 3.0 * 2.0 * Ratio1;
		}
	}
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	//asdasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
	public static class EasyBindExt_Props {
		// Eventos
		public static T OnClick<T>( this T elem, RoutedEventHandler handler ) where T : ButtonBase { if (handler != null) elem.Click += handler; return elem; }

		// Propriedades
		public static T Content<T>( this T elem, object value ) where T : ContentControl { elem.Content = value; return elem; }
		public static T Content<T>( this T elem, EasyBind eb, string valueName ) where T : ContentControl { elem.Bind( ContentControl.ContentProperty, eb, valueName ); return elem; }
		public static T Tag<T>( this T elem, object value ) where T : FrameworkElement { elem.Tag = value; return elem; }
		public static T Tag<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.TagProperty, eb, valueName ); return elem; }
		public static T Width<T>( this T elem, double value ) where T : FrameworkElement { elem.Width = value; return elem; }
		public static T Width<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.WidthProperty, eb, valueName ); return elem; }
		public static T MinWidth<T>( this T elem, double value ) where T : FrameworkElement { elem.MinWidth = value; return elem; }
		public static T MinWidth<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.MinWidthProperty, eb, valueName ); return elem; }
		public static T MaxWidth<T>( this T elem, double value ) where T : FrameworkElement { elem.MaxWidth = value; return elem; }
		public static T MaxWidth<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.MaxWidthProperty, eb, valueName ); return elem; }
		public static T Height<T>( this T elem, double value ) where T : FrameworkElement { elem.Height = value; return elem; }
		public static T Height<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.HeightProperty, eb, valueName ); return elem; }
		public static T MinHeight<T>( this T elem, double value ) where T : FrameworkElement { elem.MinHeight = value; return elem; }
		public static T MinHeight<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.MinHeightProperty, eb, valueName ); return elem; }
		public static T MaxHeight<T>( this T elem, double value ) where T : FrameworkElement { elem.MaxHeight = value; return elem; }
		public static T MaxHeight<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.MaxHeightProperty, eb, valueName ); return elem; }
		public static T Padding<T>( this T elem, Thickness value ) where T : Control { elem.Padding = value; return elem; }
		public static T Padding<T>( this T elem, EasyBind eb, string valueName ) where T : Control { elem.Bind( Control.PaddingProperty, eb, valueName ); return elem; }
		public static T Margin<T>( this T elem, Thickness value ) where T : FrameworkElement { elem.Margin = value; return elem; }
		public static T Margin<T>( this T elem, EasyBind eb, string valueName ) where T : FrameworkElement { elem.Bind( FrameworkElement.MarginProperty, eb, valueName ); return elem; }

		// MegaEasyBind
		public static E FontSize<E, V>( this E elem, EasyBind eb, Func<V> func ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, eb, func );
		public static E FontSize<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, func );
		public static E FontSize<E>( this E elem, EasyBind dataSource, string path ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, dataSource, path );
		public static E FontSize<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, path );

		public static T Margin<T, V>( this T elem, EasyBind eb, Func<V> func ) where T : FrameworkElement {
			var binding = new Binding( "Value" ) { Source = new EasyBind.Lambda<V>( eb, func ), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
			elem.SetBinding( FrameworkElement.MarginProperty, binding );
			return elem;
		}
	}








	//public class EasyBind<T> : INotifyPropertyChanged {
	//	private T _masterValue;
	//	private readonly T _refValue;
	//	private Dictionary<string, Binding> bindings;
	//	public T Base => _refValue;
	//	public T Value {
	//		get => _masterValue;
	//		set {
	//			_masterValue = value;
	//			var props = GetType().GetProperties().Where( p => !p.Name.Contains( "_" ) && p.Name != "Value" && p.Name != "Base" );
	//			foreach (var prop in props) OnPropertyChanged( prop.Name );
	//		}
	//	}

	//	public EasyBind( T referenceValue ) {
	//		_refValue = referenceValue;
	//		_masterValue = referenceValue;
	//		var props = GetType().GetProperties().Where( p => !p.Name.Contains( "_" ) && p.Name != "Value" && p.Name != "Base" );
	//		bindings = new Dictionary<string, Binding>( props.Count() );
	//		foreach (var p in props)
	//			bindings.Add( p.Name, new Binding( p.Name ) {
	//				Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
	//			} );
	//	}

	//	public event PropertyChangedEventHandler PropertyChanged;
	//	private void OnPropertyChanged( string info ) {
	//		PropertyChangedEventHandler handler = PropertyChanged;
	//		if (handler != null) {
	//			handler( this, new PropertyChangedEventArgs( info ) );
	//		}
	//	}

	//	public Binding Get( string name ) => bindings[name];
	//	public T Bind( Control control, DependencyProperty dp, string name ) {
	//		control.SetBinding( dp, Get( name ) );
	//		//bindings.Add( name, Get( name ) );
	//		return default(T);
	//	}

	//}







	public class EasyBinds_ori : INotifyPropertyChanged {
		//public double FontSize { get => _size; set { _size = value; OnPropertyChanged( "FontSize" );	}}
		//public Binding FontSize_Binding = new Binding( "FontSize_Value" );
		//public Binding ButtonMarging_Binding = new Binding( "ButtonMargin_Value" );
		//public Binding ButtonPadding_Binding = new Binding( "ButtonMargin_Value" );

		public dynamic FontSize => Size;
		public dynamic FontSize_Value => Size;
		public dynamic ButtonMargin_Value => new Thickness( _originalSize / 2 + Math.Round( Size / (_originalSize) ) );
		public dynamic ButtonPadding_Value => new Thickness( _originalSize / 2 + Math.Round( Size / (_originalSize) ) );

		private double _size = 16;
		const double _originalSize = 16;
		public Dictionary<string, Binding> bindings;
		public double Size {
			get => _size;
			set {
				_size = value;
				var props = GetType().GetProperties().Where( p => p.Name.EndsWith( "_Value" ) );
				foreach (var prop in props) OnPropertyChanged( prop.Name );
			}
		}

		public EasyBinds_ori() {
			//var fields = GetType().GetFields().Where( f => f.Name.EndsWith( "_Binding" ) );
			//foreach (var field in fields) {
			//	(field.GetValue( this ) as Binding).Source = this;
			//	(field.GetValue( this ) as Binding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			//}
			var props = GetType().GetProperties().Where( p => p.Name.EndsWith( "_Value" ) );
			bindings = new Dictionary<string, Binding>( props.Count() );
			foreach (var p in props)
				bindings.Add( p.Name, new Binding( p.Name ) {
					Source = this, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
				} );
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged( string info ) {
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler( this, new PropertyChangedEventArgs( info ) );
			}
		}

		public Binding Get( string name ) => bindings[name];
	}
}

