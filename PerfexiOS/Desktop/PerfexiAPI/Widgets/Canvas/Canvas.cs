using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Cosmos.Core_Plugs.System;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using CosmosTTF;
using PerfexiOS.Desktop.PerfexiAPI.RichText;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using PerfexiOS.Desktop.PerfexiAPI.Windows;

namespace PerfexiOS.Desktop
{
    /// <summary>
    /// This class is just a adaptation of the Canvas Class from the cosmos
    /// Source code to work as a Widget That is compatible with FullScreenCanvas().
    /// 
    /// Basicly just a copy and paste of the functions from Canvas.cs from the cosmos Source Code
    /// With some slight alterations to make it a widget. 
    /// 
    /// </summary>
    public class Pannel : Widget
    {
        /// <summary>
        /// The Dimensions of the pannel Realitive to the screen. 
        /// </summary>
        public int x, y, w, h;
        
        
        private Bitmap Buffer;

        public uint Hviewport { get; set; } = 0;
        public uint Vviewport { get; set; } = 0; 
        /// <summary>
        /// Allows automatic Scaling of the Buffers size 
        /// Note the Buffer Bitmap is the FULL image of the pannel and the
        /// Viewport Bitmap is what is actually drawn on the screen
        /// </summary>
        public bool Scalable { get; set; } = false;
        private Bitmap Viewport { get; set; }

        private TextSurface TextWriter;

        public Pannel(Window parent,int x,int y,int w,int h) : base(parent,x,y)
        {
            this.x = x;
            this.y = y;
            this.w = w; 
            this.h = h;

            Buffer = new((uint)w, (uint)h, ColorDepth.ColorDepth32);
            TextWriter = new(this,x,y,w,h);
            Clear(Color.White);
        }
        /// <summary>
        /// This Method allows you to Plot Points in a certain rectangular area of the pannel 
        /// So if you want to draw text for example with a text surface and you Don't want to have to 
        /// 
        /// </summary>
        /// <param name="color">Color of the point</param>
        /// <param name="ax">The X Location of the point realative to the Pannels full Buffer</param>
        /// <param name="ay">The Y Location of the point realative to the Pannels full Buffer</param>
        /// <param name="aw">The Width of the area that can be plotted too</param>
        /// <param name="ah">The Height of the area that can be plotted to</param>
        /// <param name="x"> The x Value of the point in this area</param>
        /// <param name="y">The y Value of the point in this area</param>
        public void AreaPlot(Color color,int ax,int ay,int aw,int ah,int x,int y)
        {
            // Check if the rectangle area to plot in is actually valid in the pannel Buffer 
			if(ax+aw > Buffer.Width || ay+ah > Buffer.Height) { return; }
			// Check if the Point is valid in the area 
			if(x > aw || y > ah) { return; }
            // Plots the Point on the main Buffer 
            Plot(color, ax + x, ay + y);
		}

        public void Plot(Color color, int x,int y)
        {
			// Checks if the Cord is valid if it isn't valid it with check if the Buffer's size can be expanded 
			// If it can then it will store the buffer in a cache Bitmap and then reinitalise the buffer with the new scal
			// If not then it will just return.
			if (x < 0 || y < 0) { return; }
			if (x > Buffer.Width || y > Buffer.Height)
            {
                // Return if the Buffers size cannot be increased 
                if(!Scalable) { return; }
                // Cache the current buffer and reinitalise it. 
                var Cache = Buffer;
                var xfactor = 0; // Calculate the scaling factors 
                var yfactor = 0;
                if(x > Cache.Width) { xfactor = x - (int)Cache.Width; };
                if(y > Cache.Height) { yfactor = y - (int)Cache.Height; }

                // Create the new buffer with the new size; 
                Buffer = new(Cache.Width + (uint)xfactor, Cache.Height +(uint)yfactor, ColorDepth.ColorDepth32);
                // Draw the pixels from the Cache to the new Buffer 
                DrawBitmap(Cache, 0, 0);
                
            }
            
            Buffer.RawData[(y*Buffer.Width)+x] = color.ToArgb();
        }
        /// <summary>
        /// Draw a Bitmap Image in a certain area of the Buffer this Method Utilises the AreaPlot() Method
        /// </summary>
        /// <param name="bmp">Bitmap image to be drawn</param>
        /// <param name="ax">the x value of the area Realative to the Main Buffer of the Pannel</param>
        /// <param name="ay">The y value of the area Realative to the Main Buffer of the Pannel</param>
        /// <param name="aw">The width of the area </param>
        /// <param name="ah">The Height of the area</param>
        /// <param name="x">the x value realtive to the Specfied area to draw the Image</param>
        /// <param name="y">the y value realative to the Specifed area to draw the Image</param>
        public void DrawBitmapChecked(Bitmap bmp,int ax,int ay,int aw,int ah,int x,int y)
        {
			var data = bmp.RawData;
			for (int i = 0; i < bmp.Height; i++)
			{
				for (int ii = 0; ii < bmp.Width; i++)
				{
					var pixel = data[(i * Buffer.Width) + ii];
					AreaPlot(Color.FromArgb(pixel),ax,ay,aw,ah,x,y);
				}
			}
		}
        
        /// <summary>
        /// Clears the buffer but keeps it's current full size 
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color)
        {
            for(int i = 0; i < Buffer.RawData.Length; i++)
            {
                Buffer.RawData[i] = color.ToArgb();
            }
        }
        /// <summary>
        /// Clears and resets the buffer to the size of the current Viewport 
        /// </summary>
        public void ResetBuffer(Color color)
        {
            Buffer = new(Viewport.Width, Viewport.Height, ColorDepth.ColorDepth32);
			Clear(color);
		}

        /// <summary>
        /// Draws a line on the Buffer
        /// Algorith was taken from the Cavas.cs source code and th
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// 
       
        public void DrawString(int x,int y,string font,string text,int size,Color Color)
        {
            Globals.DefaultFont.DrawToSurface(TextWriter, size, x, y,text, Color);
        }     


        public void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            int i, dx, dy, sdx, sdy, dxabs, dyabs, x, y, px, py;

            dx = x2 - x1;      /* the horizontal distance of the line */
            dy = y2 - y1;      /* the vertical distance of the line */
            dxabs = Math.Abs(dx);
            dyabs = Math.Abs(dy);
            sdx = Math.Sign(dx);
            sdy = Math.Sign(dy);
            x = dyabs >> 1;
            y = dxabs >> 1;
            px = x1;
            py = y1;



            if (dxabs >= dyabs) /* the line is more horizontal than vertical */
            {
                for (i = 0; i < dxabs; i++)
                {
                    y += dyabs;
                    if (y >= dxabs)
                    {
                        y -= dxabs;
                        py += sdy;
                    }
                    px += sdx;
                    Plot(color,px, py);
                }
            }
            else /* the line is more vertical than horizontal */
            {
                for (i = 0; i < dyabs; i++)
                {
                    x += dxabs;
                    if (x >= dyabs)
                    {
                        x -= dyabs;
                        px += sdx;
                    }
                    py += sdy;
                    Plot(color, px,py);
                }
            }
        }

		public void DrawCircle(Color color, int xCenter, int yCenter, int radius)
		{
			
			int x = radius;
			int y = 0;
			int e = 0;

			while (x >= y)
			{
				Plot(color, xCenter + x, yCenter + y);
				Plot(color, xCenter + y, yCenter + x);
				Plot(color, xCenter - y, yCenter + x);
				Plot(color, xCenter - x, yCenter + y);
				Plot(color, xCenter - x, yCenter - y);
				Plot(color, xCenter - y, yCenter - x);
				Plot(color, xCenter + y, yCenter - x);
				Plot(color, xCenter + x, yCenter - y);

				y++;
				if (e <= 0)
				{
					e += (2 * y) + 1;
				}
				if (e > 0)
				{
					x--;
					e -= (2 * x) + 1;
				}
			}
		}
		public void DrawFilledCircle(Color color,int x0, int y0, int radius)
		{
			int x = radius;
			int y = 0;
			int xChange = 1 - (radius << 1);
			int yChange = 0;
			int radiusError = 0;

			while (x >= y)
			{
				for (int i = x0 - x; i <= x0 + x; i++)
				{
					Plot(color,i, y0 + y);
					Plot(color,i, y0 - y);
				}
				for (int i = x0 - y; i <= x0 + y; i++)
				{
				    Plot(color,i, y0 + x);
				    Plot(color,i, y0 - x);
				}

				y++;
				radiusError += yChange;
				yChange += 2;
				if (((radiusError << 1) + xChange) > 0)
				{
					x--;
					radiusError += xChange;
					xChange += 2;
				}
			}
		}
		public void DrawBitmap(Bitmap bmp,int x,int y)
        {
            var data = bmp.RawData;
            for(int i = 0; i < bmp.Height; i++)
            {
                for(int ii = 0; ii < bmp.Width; i++)
                {
                    var pixel = data[(i * Buffer.Width) + ii];
                    Plot(Color.FromArgb(pixel), ii+y, i+x);
                }
            }
        }
        public override void render()
        {
            
            Globals.Canvas.DrawImage(Buffer,x,y);
        }

        /// <summary>
        /// Allows you to snip a rectangle area of pixels from a bitmap and returns it into
        /// a new bitmap;
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static Bitmap GetArea(Bitmap bmp, uint x, uint y, uint w, uint h)
        {
           
   
            
            if (x > bmp.Width || y > bmp.Height) return bmp;
            if (x < 0 || y < 0) return bmp;
            if (x + w > bmp.Width) { w -= (x + w) - bmp.Width; }
            if (y + h > bmp.Height) { h -= (y + w) - bmp.Height; }
			var area = new Bitmap((uint)w, (uint)h, ColorDepth.ColorDepth32);

			for (uint i = 0; i < h; i++)
            {
                for (uint ii = 0; ii < w; i++)
                {
                    
                    var pixel = GetPointFromBitmap(bmp,x+ii,y+i);
                    area.RawData[(i * area.Width) + ii] = pixel; 



                }
            
            }
            return area;
        }
        /// <summary>
        /// Allows you to Grab a point from a bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetPointFromBitmap(Bitmap bmp,uint x,uint y)
        {
            return bmp.RawData[(y * bmp.Width) + x];

		}

    }
  
}