using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.Extensions
{
    public static class CanvasFix
    {

        public static void DrawImageA(this Canvas canv,Image img,int x,int y)
        {

            for (int X = 0; X < img.Width; X++)
            {
                for (int Y = 0; Y < img.Height; Y++)
                {
                    var c = Color.FromArgb(img.RawData[X + Y * img.Width]);
                    if (c.A > 0)
                    {
                        canv.DrawPoint(c, X + x, Y + y);
                    }
                }
            }

        }

    }
}
