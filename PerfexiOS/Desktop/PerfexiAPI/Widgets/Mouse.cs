using Cosmos.System;
using Cosmos.System.Graphics;
using PerfexiOS.Desktop.PerfexiAPI.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PerfexiOS.Desktop.PerfexiAPI.Widgets
{
	public static class Pointer
	{
		public static Bitmap Mouse_Normal { get; set; } = resources.Mouse;
		public static Bitmap Mouse_Press { get; set; } = resources.MouseSelect;

		public static Bitmap Mouse_Move { get; set; } = resources.MouseMove;

		public static Bitmap Mouse_HResize { get; set; } = resources.HMouseResize;

		public static Bitmap Mouse_VResize { get; set;} = resources.VMouseResize;

		public static Bitmap Mouse_Carret { get; set; } = resources.MouseCarret;



		public enum UiMouseStates
		{
			Normal,
			Clickable,
			Move,
			HScale,
			VScale, 
			Carret,

		}

		public static Polygon Collision;
		
		public static UiMouseStates State { get; set; } = UiMouseStates.Normal;
		public static void Initalise()
		{
			MouseManager.ScreenWidth = Globals.Canvas.Mode.Width;
			MouseManager.ScreenHeight = Globals.Canvas.Mode.Height;
			Collision = new();
			var mx = (int)MouseManager.X;
			var my = (int)MouseManager.Y;
			Collision.AddVerticy(new(mx,my));
			Collision.AddVerticy(new(mx, my + 4));
			Collision.AddVerticy(new(mx+4,my+4));
		}
		public static void Render()
		{
			// Update the Pointers Collision Mask
			var x = (int)MouseManager.X;
			var y = (int)MouseManager.Y;
			Collision.points[0] = new(x, y);
			Collision.points[1] = new(x, y + 4);
			Collision.points[2] = new(x + 4, y + 4);
			switch (State)
			{
				case UiMouseStates.Normal:
					Globals.Canvas.DrawPolygon(Color.Black, Collision.points.ToArray());
					break;
				case UiMouseStates.Clickable:
					Globals.Canvas.DrawPolygon(Color.Green, Collision.points.ToArray());
					break;
				case UiMouseStates.Move:
					Globals.Canvas.DrawImage(Mouse_Move,x,y);
					break;
				case UiMouseStates.VScale:
					Globals.Canvas.DrawImage(Mouse_VResize, x, y);
					break;
				case UiMouseStates.HScale:
					Globals.Canvas.DrawImage(Mouse_HResize, x, y);
					break;
				case UiMouseStates.Carret:
					Globals.Canvas.DrawImage(Mouse_Carret, x, y);
					break;
			}

			

		}




	}
}
