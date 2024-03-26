
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using PerfexiOS.Desktop.PerfexiAPI.Collision;
using PerfexiOS.Data;
using PerfexiOS.Data.Signal;
namespace PerfexiOS.Desktop.PerfexiAPI.Widgets.UserControls
{
	public abstract class Button : Widget
	{
	
		public int x;
		public int y;
		public int width;
		public int height;
		public Polygon ColisionMask;

		public List<Widget> Children { get; set; } = new();
		public Window RootWindow { get; set; }
		public Signal<MouseArgs> OnMouseHover { get; set; } = new();
		public Signal<MouseArgs> OnMouseLeave { get; set; } = new();
		public Signal<MouseArgs> OnMouseDrag { get; set; }
		public Signal<MouseArgs> OnKeyTyped { get; set; } 
		public Signal<MouseArgs> OnMouseClick { get; set; } = new();

		public Button(Window parent,int x,int y,int w,int h,Polygon Collision) 
		{
		
			this.x = x;
			this.y = y;
			this.ColisionMask = Collision;
			this.RootWindow = parent;
		}

		/// <summary>
		/// 
		/// </summary>
		public abstract void render();
				
	
	



	}
}
