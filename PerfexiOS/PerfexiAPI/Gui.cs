using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CosmosTTF;
using Cosmos.System;
using PerfexiOS.PerfexiAPI.Sysapps;
using Cosmos.System.Graphics.Fonts;
using PINI;
using System.IO;
using Cosmos.HAL.Drivers.USB;
using PerfexiOS.PerfexiAPI.WindowManager;
using System.Linq.Expressions;

namespace PerfexiOS.PerfexiAPI
{
	public static class GUI
	{
		public static void StartGUI(uint Rows = 640,uint Cols = 480)
		{
			try
			{
				
				System.Console.WriteLine("Starting GUI...");
				Globals.DefaultFont = new(File.ReadAllBytes($@"{Globals.RootPath}PerfexiOS\Default.ttf"));
				Globals.Canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(Rows,Cols,ColorDepth.ColorDepth32));
				MouseManager.ScreenWidth = Globals.Canvas.Mode.Width;
				MouseManager.ScreenHeight = Globals.Canvas.Mode.Height;
				Globals.GUI = true;
				Globals.Canvas.Clear(Color.Blue);
				Globals.Canvas.Display();
				WindowManager.WindowManager.Update();
				
				
			}
			catch(Exception e)
			{
				System.Console.WriteLine("GUI START FAILED");
				System.Console.WriteLine(e.Message);
			}


		}
	}


}
