using Limcap.EasyTools.Wpf.DataBinding;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Limcap.EasyTools.PowerSyntax {
	public static class EasyBindControlExt {
		// Property binding
		public static E TitleBind<E>( this E e, EasyBind src, string path ) where E : Window => e.Bind( Window.TitleProperty, src, path );
		public static E TitleBind<E>( this E e, string path ) where E : Window => e.Bind( Window.TitleProperty, path );
		public static E TitleBind<E, V>( this E e, EasyBind src, Func<V> func ) where E : Window => e.Bind( Window.TitleProperty, src, func );
		public static E TitleBind<E, V>( this E e, Func<V> func ) where E : Window => e.Bind( Window.TitleProperty, func );

		public static E ContentBind<E>( this E elem, EasyBind src, string path ) where E : ContentControl => elem.Bind( ContentControl.ContentProperty, src, path );
		public static E ContentBind<E>( this E elem, string path ) where E : ContentControl => elem.Bind( ContentControl.ContentProperty, path );
		public static E ContentBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : ContentControl => elem.Bind( ContentControl.ContentProperty, src, func );
		public static E ContentBind<E, V>( this E elem, Func<V> func ) where E : ContentControl => elem.Bind( ContentControl.ContentProperty, func );

		public static E TagBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.TagProperty, src, path );
		public static E TagBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.TagProperty, path );
		public static E TagBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.TagProperty, src, func );
		public static E TagBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.TagProperty, func );

		public static E WidthBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.WidthProperty, src, path );
		public static E WidthBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.WidthProperty, path );
		public static E WidthBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.WidthProperty, src, func );
		public static E WidthBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.WidthProperty, func );

		public static E MinWidthBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinWidthProperty, src, path );
		public static E MinWidthBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinWidthProperty, path );
		public static E MinWidthBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinWidthProperty, src, func );
		public static E MinWidthBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinWidthProperty, func );

		public static E MaxWidthBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxWidthProperty, src, path );
		public static E MaxWidthBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxWidthProperty, path );
		public static E MaxWidthBind<E, V>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxWidthProperty, src, path );
		public static E MaxWidthBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxWidthProperty, func );

		public static E HeightBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.HeightProperty, src, path );
		public static E HeightBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.HeightProperty, path );
		public static E HeightBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.HeightProperty, src, func );
		public static E HeightBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.HeightProperty, func );

		public static E MinHeightBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinHeightProperty, src, path );
		public static E MinHeightBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinHeightProperty, path );
		public static E MinHeightBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinHeightProperty, src, func );
		public static E MinHeightBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MinHeightProperty, func );

		public static E MaxHeight<E>( this E elem, double value ) where E : FrameworkElement { elem.MaxHeight = value; return elem; }
		public static E MaxHeightBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxHeightProperty, src, path );
		public static E MaxHeightBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxHeightProperty, path );
		public static E MaxHeightBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxHeightProperty, src, func );
		public static E MaxHeightBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( FrameworkElement.MaxHeightProperty, func );

		public static E PaddingBind<E>( this E elem, EasyBind src, string valueName ) where E : Control => elem.Bind( Control.PaddingProperty, src, valueName );
		public static E PaddingBind<E>( this E elem, string path ) where E : Control => elem.Bind( Control.PaddingProperty, path );
		public static E PaddingBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : Control => elem.Bind( Control.PaddingProperty, src, func );
		public static E PaddingBind<E, V>( this E elem, Func<V> func ) where E : Control => elem.Bind( Control.PaddingProperty, func );

		public static E MarginBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( Control.MarginProperty, src, path );
		public static E MarginBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( Control.MarginProperty, path );
		public static E MarginBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( Control.MarginProperty, src, func );
		public static E MarginBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( Control.MarginProperty, func );

		public static E FontSizeBind<E>( this E elem, EasyBind src, string path ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, src, path );
		public static E FontSizeBind<E>( this E elem, string path ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, path );
		public static E FontSizeBind<E, V>( this E elem, EasyBind src, Func<V> func ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, src, func );
		public static E FontSizeBind<E, V>( this E elem, Func<V> func ) where E : FrameworkElement => elem.Bind( Control.FontSizeProperty, func );
	}
}

