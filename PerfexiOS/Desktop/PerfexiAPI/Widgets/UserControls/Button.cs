
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using PerfexiOS.Desktop.PerfexiAPI.Collision;
namespace PerfexiOS.Desktop.PerfexiAPI.Widgets.UserControls
{
	public abstract class Button : Widget
	{
		public Window parent;
		public int x;
		public int y;
		public int width;
		public int height;
		public Polygon ColisionMask;
		public Button(Window parent,int x,int y,int w,int h,Polygon Collision) : base(parent,x,y)
		{
			this.parent = parent;
			this.x = x;
			this.y = y;
			this.ColisionMask = Collision;
		}

		
		public override void render()
		{
			Draw();
		}
		public abstract void Draw();
	



	}
}
