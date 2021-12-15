using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Limcap.EasyTools.Wpf.DataBinding {
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
	}








	public partial class EasyBind : INotifyPropertyChanged {
		private readonly Dictionary<string, Binding> _Bindings = new Dictionary<string, Binding>();
		private readonly List<SingleBind> _Lambdas = new List<SingleBind>();
		public event PropertyChangedEventHandler PropertyChanged;

		public EasyBind( dynamic _base ) {

		}


		protected void NotifyChanges() {
			//foreach (var entry in _Bindings) OnPropertyChanged( entry.Key );
			var props = GetType().GetProperties().Where( p => !p.Name.StartsWith( "_" ) && p.Name != "Value" && p.Name != "Base" );
			foreach (var entry in props) OnPropertyChanged( entry.Name );
			
			foreach (var entry in _Lambdas) entry.NotifyChange();
		}


		public void AddLambda( SingleBind bind ) { _Lambdas.Add( bind ); }


		protected void OnPropertyChanged( string info ) {
			if (PropertyChanged != null)
				PropertyChanged( this, new PropertyChangedEventArgs( info ) );
		}


		public static E Set<E>( E el, DependencyProperty dp, string path ) where E : FrameworkElement {
			Set( el, dp, null, path );
			//el.Loaded += ( obj, arg ) => {
			//	var source = ((obj as FrameworkElement).DataContext as EasyBind);
			//	Set( el, dp, source, path );
			//};
			return el;
		}


		public static E Set<E>( E el, DependencyProperty dp, EasyBind source, string path ) where E : FrameworkElement {
			if (source != null) {
				bool existAlready = source._Bindings.ContainsKey( path );
				Binding bd = existAlready
					? source._Bindings[path]
					: new Binding( path ) { Source = source, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
				if (!existAlready)
					source._Bindings.Add( path, bd );
				el.SetBinding( dp, bd );
			}
			else el.SetBinding( dp, new Binding( path ) );
			return el;
		}

		//public class Tes : IValueConverter {
		//	public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
		//		throw new NotImplementedException();
		//	}

		//	public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
		//		throw new NotImplementedException();
		//	}
		//}
	}
}

