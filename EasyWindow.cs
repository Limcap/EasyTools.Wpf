using System;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Limcap.EasyTools.PowerSyntax;
using Limcap.EasyTools.Wpf.DataBinding;

namespace Limcap.EasyTools.Wpf {
	public partial class EasyWindow {

		public static ResourceDictionary Colors;
		public static ResourceDictionary BaseResource;



		static EasyWindow() {
			LoadBaseResource();
			LoadColorResource();
		}




		private static void LoadColorResource() {
			var uri = new Uri( "pack://application:,,,/FP.WPF.Vintage;component/Parts/Colors.xaml", UriKind.RelativeOrAbsolute );
			Colors = new ResourceDictionary() { Source = uri };
		}





		private static void LoadBaseResource() {
			var uri = new Uri( "pack://application:,,,/FP.WPF.Vintage;component/Base.xaml", UriKind.RelativeOrAbsolute );
			BaseResource = new ResourceDictionary() { Source = uri };
		}





		public Window MainWindow;
		private Border ContentOutline;
		private Border ButtonsOutline;
		private StackPanel ButtonsPanel;
		public object DialogResult;






		public EasyWindow( string title = "Mensagem" ) : this( null, title ) { }
		public EasyWindow( Window window, string title = "Mensagem" ) {
			new Window().Update( ref MainWindow )
			.Title( title )
			.Owner( window )
			.WindowStartupLocation( window is null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner )
			.DataContext( style )
			.FontSizeBind( nameof( style.FontSize ) )
			.SizeToContent( SizeToContent.WidthAndHeight )
			.ResizeMode( ResizeMode.NoResize )
			.SnapsToDevicePixels( true )
			.Resources( BaseResource )
			.MinHeight( 150 )
			.Content(

				new DockPanel()
				.LastChildFill( true )
				.Children(

					new Border().Update( ref ContentOutline )
					.Child( null )
					.SetDock( Dock.Top ),

					new Border().Update( ref ButtonsOutline )
					.Background( Colors["MainColor"] as SolidColorBrush )
					.Child(

						new StackPanel().Update( ref ButtonsPanel )
						.Orientation( Orientation.Horizontal )
						.HorizontalAlignment( HorizontalAlignment.Right )
						.MarginBind( nameof( style.ButtonPanelPadding ) )
						.SetDock( Dock.Bottom )
					)
				)
			);
		}
		//public EasyWindow( Window centralizeOn, string title = "Mensagem" ) {
		//	MainWindow = new Window() {
		//		Title = title,
		//		SizeToContent = SizeToContent.WidthAndHeight,
		//		ResizeMode = ResizeMode.NoResize,
		//		SnapsToDevicePixels = true,
		//		Resources = BaseResource,
		//		MinHeight = 150,
		//		Content = new DockPanel() {
		//			Width = double.NaN,
		//			Height = double.NaN,
		//			LastChildFill = false,
		//			Children = { BuildContentPanel(), BuildButtonPanel() },
		//		}
		//	};
		//	MainWindow.DataContext = style;
		//	MainWindow.FontSize( nameof( style.FontSize ) );
		//	MainWindow.Loaded += ( o, a ) => WindowUtil.Centralize( MainWindow, centralizeOn );
		//}
		//private Border BuildContentPanel() {
		//	ContentPanel = new Border();
		//	DockPanel.SetDock( ContentPanel, Dock.Top );
		//	return ContentPanel;
		//}
		//private Border BuildButtonPanel() {
		//	ButtonPanel = new StackPanel() {
		//		Orientation = Orientation.Horizontal,
		//		HorizontalAlignment = HorizontalAlignment.Right,
		//	};
		//	ButtonPanel.Margin( nameof( style.ButtonPanelPadding ) );

		//	ButtonPanelBorder = new Border() {
		//		Background = Colors["MainColor"] as SolidColorBrush,
		//		Child = ButtonPanel
		//	};
		//	DockPanel.SetDock( ButtonPanelBorder, Dock.Bottom );

		//	return ButtonPanelBorder;
		//}



		public EasyWindow CenterOn( int processId ) {
			MainWindow.OnLoad( ( o, a ) => WindowUtil.Centralize( o as Window, processId ) );
			MainWindow.WindowStartupLocation( WindowStartupLocation.Manual );
			//MainWindow.OnRender( ( o, a ) => WindowUtil.Centralize( o as Window, processId ) );
			//MainWindow.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Loaded, 
			//	new Action( () => { WindowUtil.Centralize( MainWindow, processId ); } )
			//);
			return this;
		}
		public EasyWindow CenterOn( Window window ) {
			MainWindow.Owner = window;
			MainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			return this;
		}
		public EasyWindow SetContent( FrameworkElement contentElement ) {
			ContentOutline.Child = contentElement;
			//contentPanel.FontSize( () => style.FontSize );
			return this;
		}
		public EasyWindow SetContent( string message ) {
			ContentOutline.Child =
				new TextBox()
				.BorderThickness( 0 )
				.IsReadOnly( true )
				.Text( message )
				.MarginBind( () => style.MessageBoxContentPadding );
			return this;
		}






		public EasyWindow AddButton( string caption, RoutedEventHandler action = null ) {
			return AddButton( caption, null, action );
		}
		public EasyWindow AddButton( string caption, object tag, RoutedEventHandler action = null ) {
			ButtonsOutline.AppendChild(
				new Button()
				.Content( caption )
				.Tag( tag )
				.MarginBind( style, () => new Thickness( style.ButtonDistance, 0, 0, 0 ) )
				.PaddingBind( style, () => style.ButtonPadding )
				.OnClick( action )
			);
			return this;
		}






		public bool? ShowDialog( MessageBoxButton buttons, bool closeOnClick = true, bool showInTaskbar = true, bool topmost = true) {
			ButtonsPanel.Children.Clear();
			if (buttons == MessageBoxButton.YesNo) { Yes(); No(); }
			else if (buttons == MessageBoxButton.YesNoCancel) { Yes(); No(); Cancel(); }
			else if (buttons == MessageBoxButton.OK) { Ok(); }
			else if (buttons == MessageBoxButton.OKCancel) { Ok(); Cancel(); }
			//foreach(Button btn in ButtonPanelBorder.NextChildren() ) btn.MinWidth = 100;
			ButtonsOutline.NextChildren()?.ForEach<Button>( btn => btn.MinWidthBind( style, nameof( style.ButtonMinWidth ) ) );
			void Yes() { AddButton( "Sim", true ); }
			void No() { AddButton( "Não", false ); }
			void Cancel() { AddButton( "Cancelar", null ); }
			void Ok() { AddButton( "Ok", true ); }
			return (bool?)ShowDialog(closeOnClick, showInTaskbar, topmost);
		}






		public void Show() {
			MainWindow?.Show();
		}






		public object ShowDialog( bool closeOnClick = true, bool showInTaskbar = true, bool topmost = true ) {
			// Warning: If the Visibility of the window is Hidden, the method ShowDialog will return immediately
			// and will not wait for user input.
			MainWindow.ShowInTaskbar = showInTaskbar;
			MainWindow.Topmost = topmost;
			if (closeOnClick)
				foreach (Button btn in ButtonsPanel.Children)
					btn.Click += ( o, a ) => { DialogResult = btn.Tag; MainWindow.Close(); };
			SystemSounds.Question.Play();
			MainWindow.ShowDialog();
			return DialogResult;
		}






		public void Hide() => MainWindow?.Hide();
		public void Close() => MainWindow?.Close();








		public DefaultStyle style = new DefaultStyle();

		public class DefaultStyle : EasyBind_Double, EasyWindowStyle {
			public DefaultStyle() : base( 15 ) { }

			public dynamic FontSize => Value;
			public dynamic MessageBoxContentPadding => new Thickness( Base * 2, Base, Base * 2, Base * 2 * Ruler.Ratio1 );
			public dynamic ButtonPanelPadding => new Thickness( Base * Ruler.Ratio1 );
			public dynamic ButtonDistance => Base * Ruler.Ratio1;
			public dynamic ButtonMinWidth => Base * 6.5 * Ruler.Ratio1;

			public dynamic ButtonPadding => new Thickness( ButtonPadding_Horiz, ButtonPadding_Top, ButtonPadding_Horiz, ButtonPadding_Bottom );
			private dynamic ButtonPadding_Top => Ruler.OneThird * Ruler.Ratio1;
			private dynamic ButtonPadding_Bottom => (Ruler.OneThird + 1) * Ruler.Ratio1;
			private dynamic ButtonPadding_Horiz => Base * Ruler.Ratio1;
		}




		public interface EasyWindowStyle {
			dynamic FontSize { get; }
			dynamic MessageBoxContentPadding { get; }
			dynamic ButtonPanelPadding { get; }
			dynamic ButtonMinWidth { get; }
			dynamic ButtonPadding { get; }
			dynamic ButtonDistance { get; }
		}
	}
}

