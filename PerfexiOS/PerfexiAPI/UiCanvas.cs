using Cosmos.System.Graphics;
using CosmosTTF;
using PerfexiOS.PerfexiAPI.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI
{
	public class UiCanvas : ITTFSurface
	{
		Bitmap Buffer;
		/// <summary>
		/// The Full Size of the pannel
		/// </summary>
		uint ExplcitWidth,ExplcitHeight;

		/// <summary>
		/// The Viewpport Dimensions pof the pannel 
		/// </summary>
		uint Width, Height;
		public uint viewportx { get; set; } = 0;
		public uint viewporty { get; set; } = 0; 
		public UiCanvas(uint width,uint height)
		{
			Buffer = new(width, height, ColorDepth.ColorDepth32);
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Clears the canvas and makes it bigger or smaller
		/// </summary>
		/// <param name="newwidth"></param>
		/// <param name="newheight"></param>
		public void ResizeBuffer(uint newwidth,uint newheight)
		{
			Buffer = new Bitmap(newwidth, newheight, ColorDepth.ColorDepth32);
			Clear(Color.White);
		}
		public void Clear(Color c)
		{
			int value = c.ToArgb();
			Array.Fill(Buffer.RawData, value);
		}
		public bool Plot(int x,int y,Color color)
		{
			// If the cord is invalid return 
			if(y*Width+x > Buffer.RawData.Length) { return false; }
			if(color.A <= 0) { return true; }
			Buffer.RawData[y * Width + x] = color.ToArgb();
			return true;
		}

		public void DrawToScreen(int x,int y)
		{

			// Get Points from viewport 
			var data = Buffer.RawData;
			for(var i = 0; i < Height; i++)
			{
				for(var ii = 0; ii < Width; ii++)
				{

					var c = Color.FromArgb(data[i+viewporty * Height + ii+viewportx]);
					if (c.A <= 0) { continue; }
					if(c.A > 0 && c.A < 255)
					{
						c = Canvas.AlphaBlend(c, Globals.Canvas.GetPointColor(x + ii, y + i),c.A);
					}
					Globals.Canvas.DrawPoint(c, x + i, y + ii);
				}
			}
		}


	


		public void DrawBitmap(Bitmap bmp, int x, int y)
		{
			for(int Y = 0; Y < bmp.Width; Y++)
			{
				for(int  X = 0; X < bmp.Height; X++)
				{
					Plot(X + x, Y + y, Color.FromArgb(bmp.RawData[y * bmp.Width * x]));
				}
			}
		}
		public void DrawFilledCircle(Color color, int x0, int y0, int radius)
		{
			int num = radius;
			int num2 = 0;
			int num3 = 1 - (radius << 1);
			int num4 = 0;
			int num5 = 0;
			while (num >= num2)
			{
				for (int i = x0 - num; i <= x0 + num; i++)
				{

					Plot(i, y0 + num2, color);
					Plot(i, y0 - num2, color);
				}

				for (int j = x0 - num2; j <= x0 + num2; j++)
				{
					Plot(j, y0 + num, color);
					Plot(j, y0 - num, color);
				}

				num2++;
				num5 += num4;
				num4 += 2;
				if ((num5 << 1) + num3 > 0)
				{
					num--;
					num5 += num3;
					num3 += 2;
				}
			}
		}
		public void DrawCircle(int cx,int cy,int r,Color c)
		{
			
			int num = r;
			int num2 = 0;
			int num3 = 0;
			while (num >= num2)
			{
				Plot(cx + num, cy + num2,c);
				Plot(cx + num2, cy + num, c);
				Plot( cx - num2, cy + num,c);
				Plot( cx - num, cy + num2,c);
				Plot( cx - num, cy - num2,c);
				Plot(cx - num2, cy - num, c);
				Plot( cx + num2, cy - num,c);
				Plot(cx + num, cy - num2, c);
				num2++;
				if (num3 <= 0)
				{
					num3 += 2 * num2 + 1;
				}

				if (num3 > 0)
				{
					num--;
					num3 -= 2 * num + 1;
				}
			}
		}

		

		public void DrawRectangle(int x,int y,int w,int h,Color c)
		{
			int num = x + w;
			int num2 = y + h;
			int x2 = x + w;
			int y2 = y + h;
			DrawLine(c, x, y, num, y);
			DrawLine(c, x, y, x, num2);
			DrawLine(c, num, y, x2, y2);
			DrawLine(c, x, num2, x2, y2);
		}

		public void DrawFilledRectangle(int x,int y,int w,int h,Color c)
		{
			if (h == -1)
			{
				h = w;
			}

			for (int i = x; i < x + h; i++)
			{
			
				DrawLine(c, x, i, x + w - 1, i);
			}
		}

		public void DrawTextFragment(TextFragment fragment,int x,int y)
		{
		
			for(int i = 0; i < fragment.text.Length; i++)
			{
				var c = fragment.text[i];
				var glypth = fragment.font.RenderGlyphAsBitmap(new(c), fragment.Color, fragment.fontsize);
				if(glypth.HasValue)
				{
					DrawBitmap(glypth.Value.bmp, fragment.fontsize * i + x, y);
				}
			}
			
		}

		


		public void DrawHorizontalLine(Color color, int dx, int x1, int y1)
		{
			for (int i = 0; i < dx; i++)
			{
				Plot(x1 + i, y1, color);
			}
		}

		//
		// Summary:
		//     Draw a vertical line.
		//
		// Parameters:
		//   color:
		//     The color to draw with.
		//
		//   dy:
		//     The line of the line.
		//
		//   x1:
		//     The starting point X coordinate.
		//
		//   y1:
		//     The starting point Y coordinate.
		internal void DrawVerticalLine(Color color, int dy, int x1, int y1)
		{
			for (int i = 0; i < dy; i++)
			{
				Plot(y1 + i, x1, color);
			}
		}

		//
		// Summary:
		//     Draws a diagonal line.
		//
		// Parameters:
		//   color:
		//     The color to draw with.
		//
		//   dx:
		//     The line length on the X axis.
		//
		//   dy:
		//     The line length on the Y axis.
		//
		//   x1:
		//     The starting point X coordinate.
		//
		//   y1:
		//     The starting point Y coordinate.
		internal void DrawLine(Color color, int dx, int dy, int x1, int y1)
		{
			int num = Math.Abs(dx);
			int num2 = Math.Abs(dy);
			int num3 = Math.Sign(dx);
			int num4 = Math.Sign(dy);
			int num5 = num2 >> 1;
			int num6 = num >> 1;
			int num7 = x1;
			int num8 = y1;
			if (num >= num2)
			{
				for (int i = 0; i < num; i++)
				{
					num6 += num2;
					if (num6 >= num)
					{
						num6 -= num;
						num8 += num4;
					}

					num7 += num3;
					Plot(num8, num7,color);
				}

				return;
			}

			for (int i = 0; i < num2; i++)
			{
				num5 += num;
				if (num5 >= num2)
				{
					num5 -= num2;
					num7 += num3;
				}

				num8 += num4;
				Plot( num7, num8,color);
			}
		}

		//
		// Summary:
		//     Draws a line between the given points.
		//
		// Parameters:
		//   color:
		//     The color to draw the line with.
		//
		//   x1:
		//     The starting point X coordinate.
		//
		//   y1:
		//     The starting point Y coordinate.
		//
		//   x2:
		//     The end point X coordinate.
		//
		//   y2:
		//     The end point Y coordinate.
		













	}
}
