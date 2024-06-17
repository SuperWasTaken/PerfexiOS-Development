using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI
{
   public static class CanvasExtensions
	{
		public static void DrawImageA(this Canvas canv,int x,int y,Bitmap bitmap)
		{
			var data = bitmap.RawData;
			var Height = bitmap.Height;
			var Width = bitmap.Width;
			for (var i = 0; i < Height; i++)
			{
				for (var ii = 0; ii < Width; ii++)
				{

					var c = Color.FromArgb(data[i  * Height + ii ]);
					if (c.A <= 0) { continue; }
					if (c.A > 0 && c.A < 255)
					{
						c = Canvas.AlphaBlend(c, Globals.Canvas.GetPointColor(x + ii, y + i), c.A);
					}
					Globals.Canvas.DrawPoint(c, x + i, y + ii);
				}
			}
		}
	}
}
