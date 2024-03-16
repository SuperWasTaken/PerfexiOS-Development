using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Geomitry
{
	
	public static class GeomitryForumlas
	{
		public static double Distance(Point p1,Point p2)
		{
			return Math.Sqrt(Math.Pow((p2.X-p1.X),2)+Math.Pow(p2.Y-p1.Y,2));
		}

		public static double Slope(Point p1,Point p2)
		{
			return p2.X - p1.X / p2.Y - p1.Y;
		}


		/// <summary>
		/// Yoinked this function from https://www.jeffreythompson.org/collision-detection/poly-poly.php
		/// Thanks Jeffry Thomsan :) Also I will be yoinking the rest of your collision functions 

		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <param name="p3"></param>
		/// <param name="p4"></param>
		/// <returns></returns>
		public static bool LineLine(PointF p1,PointF p2,PointF p3,PointF p4)
		{

			// calculate the distance to intersection point
			float uA = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X)) / ((p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y));
			float uB = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X)) / ((p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y));
			// if uA and uB are between 0-1, lines are colliding
			if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
	}
}
