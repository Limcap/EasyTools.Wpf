using System;
using System.Windows;
using System.Windows.Controls;

namespace Limcap.EasyTools.PowerSyntax {
	public static class PanelExt {
		// Dock methods
		public static T SetDock<T>( this T elem, Dock value ) where T : UIElement { DockPanel.SetDock( elem, value ); return elem; }

		// Make it easier to get child panel of a decorator and then the children of the child panel
		//public static UIElementCollection NextChildren<T>( this T elem ) where T : Decorator { return (elem.Child as Panel)?.Children; }
		//public static Panel AddChildIf( this Panel panel, bool condition, UIElement element ) { if (condition) panel.Children.Add( element ); return panel; }
		public static Decorator AppendChild( this Decorator elem, params UIElement[] items ) { Add( NextChildren( elem ), items ); return elem; }
		public static Panel AddChildIf( this Panel panel, bool condition, Func<UIElement> element ) { if (condition) panel.Children.Add( element() ); return panel; }
		public static Panel AddChild( this Panel panel, UIElement element ) { panel.Children.Add( element ); return panel; }
		public static Panel AddChildren( this Panel panel, params UIElement[] elements ) { foreach (var e in elements) panel.Children.Add( e ); return panel; }
		
		public static Panel AddChildrenIf( this Panel panel, bool condition, params UIElement[] elements ) { if (condition) foreach (var e in elements) panel.Children.Add( e ); return panel; }
		
		public static void Add( this UIElementCollection collection, params UIElement[] items ) { foreach (var item in items) collection.Add( item ); }
		
		public static UIElementCollection NextChildren<K>( this K elem ) {
			if (elem is ContentControl elem1) return elem1.Content.NextChildren();
			else if (elem is Decorator elem2) return elem2.Child.NextChildren();
			else if (elem is Panel elem3) return elem3.Children;
			return null;
		}

		// Make it easier to get child panel of a decorator and then the children of the child panel
		//public static UIElementCollection NextChildren<T>( this T elem ) where T : Decorator { return (elem.Child as Panel)?.Children; }

		public static UIElementCollection ForEach( this UIElementCollection collection, Action<UIElement> action ) { foreach (UIElement item in collection) action( item ); return collection; }

		public static void ForEach<T>( this UIElementCollection collection, Action<T> action ) where T : UIElement { foreach (UIElement e in collection) if (e is T t) action( t ); }
	}
}