using Cosmos.System;
using Cosmos.System.Graphics;
using CosmosTTF;
using PerfexiOS.PerfexiAPI.Events;
using PerfexiOS.PerfexiAPI.Widgets;
using PerfexiOS.PerfexiAPI.WindowManager;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI
{
	public class Window
	{
		public Titlebar Titlebar;
		public int x, y;
		public PerAPIRectangle Area;
		public List<Widget> ActiveWidgets = new();
		public UiCanvas canvas;
		public bool Selected = false;
		public Titlebar tb = null;
		public Window(int x,int y,int w,int h)
		{
			canvas = new((uint)w, (uint)h);
			this.x = x;
			this.y = y;
		}
		/// <summary>
		/// Randomises the location of the window
		/// </summary>
		/// <param name="w"></param>
		/// <param name="h"></param>
		public Window(int w,int h)
		{
			var location = WindowManager.WindowManager.GetRandomisedWindowLocation();
			canvas = new((uint)w, (uint)h);
			this.x = location.X;
			this.y = location.Y;
			Area = new(this.x, this.y, w, h);
		}
		/// <summary>
		/// This gets the mouse location realative to the window
		/// </summary>
		/// <returns></returns>
		public Point GetMouseLocationRelative(MouseEvent Event)
		{
			
			int mx = x+(int)canvas.viewportx - Event.MX;
			int my = y+(int)canvas.viewporty - Event.MY;
			return new Point(mx, my);

		}

		public void ProcessSignals()
		{
			Point p = new((int)MouseManager.X, (int)MouseManager.Y); 
			if(!Selected)
			{
				if (Area.ContainsPoint(p.X,p.Y) && MouseManager.MouseState == MouseState.Left) ;
				{
					WindowManager.WindowManager.SetSelectedWindow(this);
				}
				return;
			}
			if(Globals.KeyPresses.TryDequeue(out var key))
			{
				HandldeKeyTyped(key);
			}
			if(MouseManager.DeltaX  != 0 || MouseManager.DeltaY != 0 || MouseManager.MouseState != MouseState.None ) { HandleMouseInteraction(new()); } 
		}
		public static void Render(Window win)
		{
			if(win.Titlebar != null)
			{
				win.Titlebar.DrawToScreen(win.x, win.y - win.tb.Height);
			}
			
			win.canvas.DrawToScreen(win.x, win.y);
		}


		public void HandldeKeyTyped(KeyEvent key)
		{
			foreach (var item in ActiveWidgets) { item.OnKeyboardInteraction.Invoke(key); }
		}
		public void HandleMouseInteraction(MouseInfo info)
		{
			

			foreach(var item in ActiveWidgets)
			{
				item.OnMouseInteraction(info);
			}

		}
		
	}
	public class MouseInfo
	{
		public Point Location; 
		public MouseState MouseState;
		public MouseState LastMouseState;
		public int dx;
		public int dy;
		public MouseInfo()
		{
			Location = new((int)MouseManager.X, (int)MouseManager.Y);
			MouseState = MouseManager.MouseState;
			LastMouseState = MouseManager.LastMouseState;
			dx = MouseManager.DeltaX; dy = MouseManager.DeltaY;
		}
		/// <summary>
		/// Allows Manual Setting if you want to Virtualy Input 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="mouseState"></param>
		/// <param name="lastMouseState"></param>
		/// <param name="dx"></param>
		/// <param name="dy"></param>
		public MouseInfo(Point location, MouseState mouseState, MouseState lastMouseState, int dx, int dy)
		{
			Location = location;
			MouseState = mouseState;
			LastMouseState = lastMouseState;
			this.dx = dx;
			this.dy = dy;
		}
	}
}
