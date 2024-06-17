using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.Widgets
{
	public class PerAPIRectangle
	{
		public int x,y,w,h;

		/// <summary>
		/// The X location Relative to the screen
		/// </summary>
		public int SX, SY;
		/// <summary>
		/// Creates a rectangle Object Relative to the Main Canvas Screen
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name=""></param>
		public PerAPIRectangle( int x, int y, int w, int h)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
			this.SX = x;
			this.SY = y;

		}

		/// <summary>
		/// Creates a rectagle that is realative to another rectangle area 
		/// 
		/// 
		/// </summary>
		/// 
		public PerAPIRectangle(int x,int y,int w,int h,PerAPIRectangle parentRectagnle)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
			this.SX = parentRectagnle.SX + x;
			this.SY = parentRectagnle.SY + y;
		}
		public bool ContainsRectanlge(PerAPIRectangle r)
		{
			return(r.SX+r.w >= x && r.SY+r.h >= y && r.SY+r.h <= h);
		}
		public bool ContainsPoint(int x, int y, bool UseRealative = false)
		{
			return (x >= this.x && x <= this.x + this.w && y >= this.y && y <= this.y + this.h);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p"></param>
		/// <param name="UseRealative">When true it will use the realative cords </param>
		/// <returns></returns>
		public bool ContainsPoint(Point p, bool UseRealative = false)
		{


			var x = p.X;
			var y = p.Y;

			if(UseRealative) { return (x >= this.x && x <= this.x + this.w && y >= this.y && y <= this.y + this.h); }
			return (x >= this.SX && x <= this.SX + this.w && y >= this.SY && y <= this.SY + this.h);

		}
	}
}
