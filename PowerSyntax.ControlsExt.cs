using Limcap.EasyTools.Wpf.DataBinding;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Limcap.EasyTools.PowerSyntax {
	public static class ControlsExt {

		// Event
		public static E OnClick<E>( this E e, RoutedEventHandler handler ) where E : ButtonBase { if (handler != null) e.Click += handler; return e; }
		public static E OnLoad<E>( this E e, RoutedEventHandler handler ) where E : FrameworkElement { if (handler != null) e.Loaded += handler; return e; }
		public static E OnRender<E>( this E e, EventHandler handler ) where E : Window { if (handler != null) e.ContentRendered += handler; return e; }


		// Properties
		public static E SizeToContent<E>( this E e, SizeToContent value ) where E : Window { e.SizeToContent = value; return e; }
		public static E ResizeMode<E>( this E e, ResizeMode value ) where E : Window { e.ResizeMode = value; return e; }
		public static E SnapsToDevicePixels<E>( this E e, bool value ) where E : Window { e.SnapsToDevicePixels = value; return e; }
		public static E Resources<E>( this E e, ResourceDictionary value ) where E : Window { e.Resources = value; return e; }
		public static E Owner<E>( this E e, Window value ) where E : Window { e.Owner = value; return e; }
		public static E WindowStartupLocation<E>( this E e, WindowStartupLocation value ) where E : Window { e.WindowStartupLocation = value; return e; }
		public static E DataContext<E>( this E e, object value ) where E : FrameworkElement { e.DataContext = value; return e; }
		public static E HorizontalAlignment<E>( this E e, HorizontalAlignment value ) where E : FrameworkElement { e.HorizontalAlignment = value; return e; }
		public static E VerticalAlignment<E>( this E e, VerticalAlignment value ) where E : FrameworkElement { e.VerticalAlignment = value; return e; }
		public static E VerticalContentAlignment<E>( this E e, VerticalAlignment value ) where E : Control { e.VerticalContentAlignment = value; return e; }
		public static E Child<E>( this E e, UIElement child ) where E : Decorator { e.Child = child; return e; }
		public static E Children<E>( this E e, params UIElement[] children ) where E : Panel { e.Children.Add( children ); return e; }
		public static E LastChildFill<E>( this E e, bool value ) where E : DockPanel { e.LastChildFill = value; return e; }
		public static E Orientation<E>( this E e, Orientation value ) where E : StackPanel { e.Orientation = value; return e; }
		public static Border Background( this Border e, Brush value ) { e.Background = value; return e; }
		public static Border BorderThickness( this Border e, Thickness value ) { e.BorderThickness = value; return e; }
		public static Border BorderThickness( this Border e, int left, int top, int right, int bottom ) { e.BorderThickness = new Thickness( left, top, right, bottom ); return e; }
		public static Border BorderThickness( this Border e, int horiz, int vert ) { e.BorderThickness = new Thickness( horiz, vert, horiz, vert ); return e; }
		public static Border BorderBrush( this Border e, Brush value ) { e.BorderBrush = value; return e; }
		public static Control Background( this Control e, Brush value ) { e.Background = value; return e; }
		public static TextBox Text( this TextBox e, string value ) { e.Text = value; return e; }
		public static TextBlock Text( this TextBlock e, string value ) { e.Text = value; return e; }
		public static E BorderThickness<E>( this E e, Thickness value ) where E : TextBoxBase { e.BorderThickness = value; return e; }
		public static E BorderThickness<E>( this E e, int left, int top, int right, int bottom ) where E : TextBoxBase { e.BorderThickness = new Thickness( left, top, right, bottom ); return e; }
		public static E BorderThickness<E>( this E e, int horiz, int vert ) where E : TextBoxBase { e.BorderThickness = new Thickness( horiz, vert, horiz, vert ); return e; }
		public static E BorderThickness<E>( this E e, int value ) where E : TextBoxBase { e.BorderThickness = new Thickness( value, value, value, value ); return e; }
		public static E BorderBrush<E>( this E e, Brush value ) where E : TextBoxBase { e.BorderBrush = value; return e; }
		public static E ToolTip<E>( this E e, string value, bool showOnDisabled ) where E : FrameworkElement { e.ToolTip = value; ToolTipService.SetShowOnDisabled( e, showOnDisabled ); return e; }
		public static E IsEnabled<E>( this E e, bool value ) where E : UIElement { e.IsEnabled = value; return e; }
		public static E IsReadOnly<E>( this E e, bool value ) where E : TextBoxBase { e.IsReadOnly = value; return e; }
		public static E IsChecked<E>( this E e, bool value ) where E : ToggleButton { e.IsChecked = value; return e; }
		public static E FlowDirection<E>( this E e, FlowDirection value ) where E : FrameworkElement { e.FlowDirection = value; return e; }
		public static E Opacity<E>( this E e, double value ) where E : UIElement { e.Opacity = value; return e; }
		public static E Visibility<E>( this E e, Visibility value ) where E : UIElement { e.Visibility = value; return e; }

		public static E Title<E>( this E e, string value ) where E : Window { e.Title = value; return e; }
		public static E FontSize<E>( this E elem, double value ) where E : Control { elem.FontSize = value; return elem; }
		public static E Content<E>( this E elem, object value ) where E : ContentControl { elem.Content = value; return elem; }
		public static E Tag<E>( this E elem, object value ) where E : FrameworkElement { elem.Tag = value; return elem; }
		public static E Width<E>( this E elem, double value ) where E : FrameworkElement { elem.Width = value; return elem; }
		public static E MinWidth<E>( this E elem, double value ) where E : FrameworkElement { elem.MinWidth = value; return elem; }
		//public static E MaxWidth<E>( this E elem, double value ) where E : FrameworkElement { elem.MaxWidth = value; return elem; }
		public static E Height<E>( this E elem, double value ) where E : FrameworkElement { elem.Height = value; return elem; }
		public static E MinHeight<E>( this E elem, double value ) where E : FrameworkElement { elem.MinHeight = value; return elem; }
		public static E Margin<E>( this E elem, Thickness value ) where E : FrameworkElement { elem.Margin = value; return elem; }
		public static E Margin<E>( this E elem, double left, double top, double right, double bottom ) where E : FrameworkElement { elem.Margin = new Thickness( left, top, right, bottom ); return elem; }
		public static E Margin<E>( this E elem, double horizontal, double vertical ) where E : FrameworkElement { elem.Margin = new Thickness( horizontal, vertical, horizontal, vertical ); return elem; }
		public static E Padding<E>( this E elem, Thickness value ) where E : Control { elem.Padding = value; return elem; }
		public static E Padding<E>( this E elem, double left, double top, double right, double bottom ) where E : Control { elem.Padding = new Thickness( left, top, right, bottom ); return elem; }
		public static E Padding<E>( this E elem, double horizontal, double vertical ) where E : Control { elem.Padding = new Thickness( horizontal, vertical, horizontal, vertical ); return elem; }
	}
}

