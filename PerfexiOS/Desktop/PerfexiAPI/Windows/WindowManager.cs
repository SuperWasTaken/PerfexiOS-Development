using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using Cosmos.System;
using PerfexiOS.Data.Signal;
using Cosmos.System.Graphics.Fonts;
namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
	public class WindowManager : process
	{
		public WindowManager() : base("Window Manager")
		{
		}
		
		/// <summary>
		/// 
		/// </summary>
		public List<Window> windows = new();
	
		/// <summary>
		/// 
		/// </summary>
		private Window FocusedWindow { get; set; }
		/// <summary>
		/// 
		/// </summary>
		private Window IntersectingWindow;
		public void Render()
		{
			DrawWallpaper();
			foreach(var item in windows)
			{
				item.Render();
				if(item.MouseIntersecting)
				{
					IntersectingWindow = item;
				}
				if(item == FocusedWindow)
				{
					continue;
				}
		
			}
			if(FocusedWindow != null)
			{
				FocusedWindow.Render();
			}
			Pointer.Render();
			
		}

		
		private void DrawWallpaper()
		{
			Globals.Canvas.DrawFilledRectangle(Color.DarkCyan, 0, 0,(int) Globals.Canvas.Mode.Width,(int) Globals.Canvas.Mode.Height);
			Globals.Canvas.DrawString(FPS.Fps.ToString(), PCScreenFont.Default, Color.Black, 10, 10);
		}
		
		public override void loop()
		{
			Render();
			ProcessSignals();
			if(IntersectingWindow != null && MouseManager.MouseState == MouseState.Left)
			{
				Focus(IntersectingWindow);
			}
		}
		private  void ProcessSignals()
		{

			var win = FocusedWindow;
			MouseArgs args = new((int)MouseManager.X, (int)MouseManager.Y, MouseManager.MouseState);
			if (FocusedWindow != null)
			{

				if (KeyboardManager.TryReadKey(out var k))
				{
					win.OnKeyTyped.Fire(new(k.Key, k.KeyChar, k.Modifiers));
				}

				if (IntersectingWindow == win)
				{
					if (!win.MouseEntered)
					{
						win.OnMouseEnter.Fire(args);
					}
					if (args.State != MouseState.None)
					{
						win.OnMouseClicked.Fire(args);
					}
					if (MouseManager.DeltaX != 0 | MouseManager.DeltaY != 0)
					{
						win.OnMouseMove.Fire(args);
					}

				}
				else
				{
					if (win.MouseEntered)
					{
						win.OnMouseLeave.Fire(args);
					}
				}
			}
		}

		public void Focus(Window win)
		{

			if(win == FocusedWindow) { return; }
			var c = FocusedWindow;
			FocusedWindow = win;
			c.OnDefocus.Fire(new());
			windows.Add(c);
			windows.Remove(win);
			FocusedWindow = win;
			win.OnFocus.Fire(new());
		    
		}

		public void CloseWindow(Window win)
		{
			windows.Remove(win);
			win.OnClose.Fire(new());
		}

	}

}
