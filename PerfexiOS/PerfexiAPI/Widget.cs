using Cosmos.System;
using PerfexiOS.PerfexiAPI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI
{
	public abstract class Widget
	{
		public PerAPIRectangle Area;


		public Widget(int x,int y,int w,int h)
		{
			Area = new(x, y, w, h);

			OnMouseInteraction = (args) =>
			{
				if(args.dx != 0 || args.dy != 0) { }
			};
		}

		public Action<KeyEvent> OnKeyboardInteraction { get; set; }

		public Action<MouseInfo> OnMouseInteraction { get; set; }

		private Action<MouseInfo> OnMouseMove { get; set; }

	
		public abstract void Draw(); 

	}
}
