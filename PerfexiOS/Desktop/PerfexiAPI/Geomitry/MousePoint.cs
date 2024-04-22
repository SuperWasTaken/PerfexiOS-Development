using Cosmos.System;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Geomitry
{
	/// <summary>
	/// This class contains methods that check Mouse Collision With simple shapes 
	/// using Simple Formulas 
	/// </summary>
	public static class MousePoint
	{
		/// <summary>
		/// Checks if the mouse is colliding with a rectangle with a simple condition
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public static bool Rect(Rectangle rect)
		{
			return (MouseManager.X >= rect.X && MouseManager.X <= rect.X + rect.Width && MouseManager.Y >= rect.Y && MouseManager.Y <= rect.X + rect.Height);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cx"></param>
		/// <param name="cy"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public static bool Circle(int cx,int cy,int radius)
		{
			return GeomitryForumlas.Distance(Pointer.GetLocation(),new Point(cx,cy)) >= radius;
		}
		
		
	}
}
