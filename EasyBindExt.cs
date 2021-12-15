using Limcap.EasyTools.Wpf.DataBinding;
using System;
using System.Windows;

namespace Limcap.EasyTools.Wpf.DataBinding {

	public static class EasyBindExt {
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
		public static E OnChange<E, V>( this E target, DependencyProperty property, Action<V> action ) where E : FrameworkElement {
			EasyBind.Reaction<V>.OnTargetChange( target, property, action );
			return target;
		}
	}
}

