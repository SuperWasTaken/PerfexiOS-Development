using Cosmos.System.Graphics;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Widgets
{
	public class TextSurface : ITTFSurface
	{

		public Pannel Parent;

		public int x,y,width,height;
		public TextSurface(Pannel Parent,int x,int y,int w,int h)
		{
			this.Parent = Parent;
			this.x = x;
			this.y = y;

		}

		public void DrawBitmap(Bitmap bmp,int x,int y) 
		{
			Parent.DrawBitmap(bmp, x, y);
		}
	}
}
