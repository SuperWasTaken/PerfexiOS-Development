using PerfexiOS.Desktop.PerfexiAPI.Geomitry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PerfexiOS.Desktop.PerfexiAPI.Collision
{
	public class Polygon
	{
		public List<Point> points = new();

		public static Polygon MakeRectangle(int x1,int y1,int x2,int y2)
		{
			var p = new Polygon();
			var dx = x2 - x1;
			var dy = y2 - y1;
			p.AddVerticy(new(x1,y1));
			p.AddVerticy(new(x2,y1));
			p.AddVerticy(new(x2, y2));
			p.AddVerticy(new(x1 , y2));
			return p;
		}

		
		public void RemoveVerticy(int x, int y)
		{
			foreach (var item in points)
			{
				if (item.X == x && item.Y == y) { points.Remove(item); }
			}
		}

		public void AddVerticy(Point point)
		{
			points.Add(point);
		}

		/// <summary>
		/// Check if a Polygon collision mask is colliding with anohter polygonCollisionmask
		/// Also yoinked and adapted from https://www.jeffreythompson.org/collision-detection/poly-poly.php
		/// (Thanks again Jeffry Thomsan)
		/// </summary>
		/// <param name="polygon"></param>
		public bool Colliding(Polygon polygon )
		{
			return polyPoly(polygon);
		}
		/// <summary>
		/// Use this method to check collision between this parent polygon and a point 
		/// 
		/// </summary>
		/// <param name="Polygon"></param>
		/// <param name="px"></param>
		/// <param name="py"></param>
		/// <returns></returns>
		public bool polyPoint(Polygon Polygon, float px, float py)
		{
			bool collision = false;
			var vertices = Polygon.points;
			// go through each of the vertices, plus the next
			// vertex in the list
			int next = 0;
			for (int current = 0; current < vertices.Count; current++)
			{

				// get next vertex in list
				// if we've hit the end, wrap around to 0
				next = current + 1;
				if (next == vertices.Count) next = 0;

				// get the PVectors at our current position
				// this makes our if statement a little cleaner
				Point vc = vertices[current];    // c for "current"
				Point vn = vertices[next];       // n for "next"

				// compare position, flip 'collision' variable
				// back and forth
				if (((vc.Y > py && vn.Y < py) || (vc.Y < py && vn.Y > py)) &&
					 (px < (vn.X - vc.X) * (py - vc.Y) / (vn.Y - vc.Y) + vc.X))
				{
					collision = !collision;
				}
			}
			return collision;
		}
		// POLYGON/POLYGON
		bool polyPoly( Polygon p2)
		{
			var p1 = this.points;
			// go through each of the vertices, plus the next
			// vertex in the list
			int next = 0;
			for (int current = 0; current < p1.Count; current++)
			{

				// get next vertex in list
				// if we've hit the end, wrap around to 0
				next = current + 1;
				if (next == p1.Count) next = 0;

				// get the PVectors at our current position
				// this makes our if statement a little cleaner
				Point vc = p1[current];    // c for "current"
				Point vn = p1[next];       // n for "next"

				// now we can use these two points (a line) to compare
				// to the other polygon's vertices using polyLine()
				bool collision = polyLine(vc.X, vc.Y, vn.X, vn.Y);
				return collision;

				// optional: check if the 2nd polygon is INSIDE the first
				
			}

			return false;
		}

		
		bool polyLine( float x1, float y1, float x2, float y2)
		{
			var vertices = this.points;
			// go through each of the vertices, plus the next
			// vertex in the list
			int next = 0;
			for (int current = 0; current < vertices.Count; current++)
			{


				// get next vertex in list
				// if we've hit the end, wrap around to 0
				next = current + 1;
				if (next == vertices.Count) next = 0;

				// get the PVectors at our current position
				// extract X/Y coordinates from each
				float x3 = vertices[current].X;
				float y3 = vertices[current].Y;
				float x4 = vertices[next].X;
				float y4 = vertices[next].Y;

				// do a Line/Line comparison
				// if true, return 'true' immediately and
				// stop testing (faster)
				bool hit = GeomitryForumlas.LineLine(new(x1, y1),new( x2, y2),new( x3, y3),new( x4, y4));
				return hit;
			}

			// never got a hit
			return false;
		}



	}
}
