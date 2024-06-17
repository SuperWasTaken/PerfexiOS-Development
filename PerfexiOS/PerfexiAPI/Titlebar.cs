using Cosmos.System.Graphics;
using CosmosTTF;
using LunarLabs.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfexiOS.PerfexiAPI.Text;
using PerfexiOS.PerfexiAPI.Widgets;
namespace PerfexiOS.PerfexiAPI
{
	
	public class Titlebar : UiCanvas
	{
		public Window parent;
		public int Height;
		public TextFragment fragment;
		public PerAPIRectangle Area; 
		public Titlebar(Window Parent, TextFragment Fragment, int height) : base((uint)Parent.Area.w, (uint)height)
		{
			this.Height = height;
			this.parent = Parent;
			this.Clear(Color.Blue);
			this.DrawTextFragment(Fragment,4,4);
			Area = new(parent.x, parent.y-height, parent.Area.w, height);
		}

		public void Render()
		{
			this.DrawToScreen(parent.x,parent.y-Height);
		}
	}
	
	
}