using Cosmos.Core.Memory;
using Cosmos.System;
using PerfexiOS.PerfexiAPI.Sysapps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.WindowManager
{
	public static class WindowManager
	{
		static int tickamount = 20;
		static int ticks = 0;
		static int LastSecond = 0;
		static int CurrentSecond = DateTime.Now.Second;
		static int Frames = 0;
		public static int FPS = 0;
		public static List<Window> OpenWindows = new();
		public static Window FocusedWindow = null;
		public static UIMouseStates MouseState = UIMouseStates.None;
		public enum UIMouseStates : int
		{
			None = 0,
			Hover = 65280,
			Forbidden = 16711680,
			Busy = 16776960, 
		}
		public static Point GetRandomisedWindowLocation()
		{
			Random r = new();
			var x = r.Next(0, (int)Globals.Canvas.Mode.Width);
			var y = r.Next(0,(int)Globals.Canvas.Mode.Height);
			return new Point(x, y);
		}

		
		public static void Update()
		{
			Globals.Canvas.Clear(System.Drawing.Color.White);
			Globals.Canvas.DrawImageAlpha(Resources.Logo, 0, 0);
			ProcessSignals();
			foreach(var item in OpenWindows) { if (item == FocusedWindow) { continue; } Window.Render(item); }
			if(FocusedWindow != null) { Window.Render(FocusedWindow); }
			Globals.Canvas.DrawFilledRectangle(Color.FromArgb((int)MouseState), (int)MouseManager.X, (int)MouseManager.Y, 20, 20);
			CountFPS();
			MemService();
			Globals.Canvas.Display();
			if(!GetIntersectingWindow(out _) && MouseManager.MouseState == Cosmos.System.MouseState.Left)
			{
				WelcomeWindow win = new();
				OpenWindows.Add(win);
			}
		}

		private static bool GetIntersectingWindow(out Window window)
		{
			window = null;
			System.Drawing.Point MouseLocation = new((int)MouseManager.X, (int)MouseManager.Y);
			if (FocusedWindow.Area.ContainsPoint(MouseLocation))
			{
				window = FocusedWindow;
				return true;
			}

			foreach(var item in OpenWindows)
			{
				if (item.Area.ContainsPoint(MouseLocation))
				{
					window = item;
				}
			}

			if(window != null)
			{
				return true;
			}
			return false;
		}


		public static void ProcessSignals()
		{
			foreach(var item in OpenWindows) { item.ProcessSignals(); }
		}

		public static void SetSelectedWindow(Window win)
		{
			win.Selected = true;
			foreach(var item in OpenWindows) {  if(item.Selected && item != win) { item.Selected = false; } }
		}
		private static void CountFPS()
		{
			Frames++;
			CurrentSecond = DateTime.Now.Second;
			if(LastSecond != CurrentSecond)
			{
				LastSecond = CurrentSecond;
				FPS = Frames;
				Frames = 0;
			}
		}

		private static void MemService()
		{
			ticks++;
			if(ticks >= tickamount)
			{
				ticks = 0;
				Heap.Collect();
			}

		}
	}


}
