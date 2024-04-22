using Cosmos.Core;
using Cosmos.Core.Memory;
using Cosmos.Core_Plugs.System;
using PerfexiOS.Desktop.PerfexiAPI;
using PerfexiOS.Desktop.PerfexiAPI.Geomitry;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.Applications
{
	public class FpsWindow : UiApplication
	{
		public FpsWindow() : base("SysInfo")
		{
			MainWindow = new(this,100, 100, 300, 300, "SysInfo", true);
			Globals.WM.windows.Add(MainWindow);
		}
		public override void loop()
		{
			try
			{
				MainWindow.Canvas.Clear(Color.White);
				MainWindow.Canvas.DrawString(10, 40, "Default", "FPS:" + FPS.Fps, 20, Color.Pink);
				MainWindow.Canvas.DrawString(10, 70, "Default", $"AvalibleRam:{GCImplementation.GetAvailableRAM()} MB", 20, Color.Pink);
				MainWindow.Canvas.DrawString(10, 100, "Default", $"TotalRamUsed:{GCImplementation.GetUsedRAM() * 1024 * 1024} MB", 20, Color.Pink);
				MainWindow.Canvas.DrawString(10, 160, "Default", $"Version:{Globals.Version}", 20, Color.Pink);
				
			}
			catch(Exception e)
			{
				// If theres a problom it will just stop
				base.stop();
				PerfexiOS.Kernel.Crash(e);
			}
		}
	}
}
