using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Limcap.EasyTools.Wpf {

	public class WindowUtil {

		public static Position? GetMainWindowPosition( int processId ) {
			if (processId == 0) return null;
			Process p = Process.GetProcessById( processId );
			IntPtr ptr = p.MainWindowHandle;
			Position pos = new Position();
			GetWindowRect( ptr, ref pos );
			return pos;
		}



		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr FindWindow( string strClassName, string strWindowName );




		[DllImport( "user32.dll" )]
		public static extern bool GetWindowRect( IntPtr hwnd, ref Position rectangle );



		public struct Position {
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }
		}



		public static void Centralize( Window thisWindow, Window overThisWindow ) {
			thisWindow.Top = overThisWindow.Top + overThisWindow.Height / 2 - thisWindow.Height / 2;
			thisWindow.Left = overThisWindow.Left + (overThisWindow.Width / 2) - (thisWindow.Width / 2);
		}



		public static void Centralize( Window thisWindow, int overThisWindowProcessId ) {
			try {
				var pc = Process.GetProcessById(overThisWindowProcessId);
				var pos = GetMainWindowPosition( pc.Id );
				if (pos.HasValue) {
					var p = pos.Value;
					var p_Height = p.Bottom - p.Top;
					var p_Width = p.Right - p.Left;
					thisWindow.Top = p.Top + p_Height / 2 - thisWindow.Height / 2;
					thisWindow.Left = p.Left + (p_Width / 2) - (thisWindow.Width / 2);
				}
			}
			catch { }
		}
	}
}
