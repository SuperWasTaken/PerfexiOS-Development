using Cosmos.Core.Memory;
using PerfexiOS.PerfexiAPI.Text;
using PerfexiOS.PerfexiAPI.WindowManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.Sysapps
{
	public class WelcomeWindow : Window
	{
		public WelcomeWindow() : base(300,300)
		{
			WindowManager.WindowManager.MouseState = WindowManager.WindowManager.UIMouseStates.Busy;
			Titlebar = new(this, new(Globals.DefaultFont,16,Color.Black,"HelloWorld"), 24);
			Heap.Collect();
			WindowManager.WindowManager.MouseState = WindowManager.WindowManager.UIMouseStates.None;
			canvas.Clear(Color.Aqua);
			canvas.DrawCircle(100,100, 12, Color.Brown);
		}
		
	}
}
