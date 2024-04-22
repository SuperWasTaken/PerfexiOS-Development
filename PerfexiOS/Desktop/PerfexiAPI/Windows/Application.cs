using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
	public abstract class UiApplication : process
	{
		public Window MainWindow { get; set; }
		public List<Window> ChildWindows { get; set; }

		public UiApplication(string name) : base(name)
		{
		}
		public override void start()
		{
			base.start();
			
		}
		public void AddWindow(Window window)
		{
			ChildWindows.Append(window);
		}


		public void RemoveWindow(Window window)
		{
			ChildWindows.Remove(window);
		}
		public override void stop()
		{
			base.stop();
			foreach(var item in ChildWindows)
			{
				Globals.WM.CloseWindow(item);
			}
			
		}
		
		
		
	
		/// <summary>
		/// This void runs the basic loop functions of the application 
		/// </summary>
		public override void loop()
		{
			// If the GUI is not enabled it dosent need to loop and then Halts the process so it dosen't call the method again 
			if(!Globals.GUI) { base.State = state.Halt; return; }
			
		}

		public void SetMainWindow(Window win)
		{
			if(!ChildWindows.Contains(win))
			{
				return;
			}
			MainWindow = win;
			ChildWindows.Remove(win);
        }


		public void RegisterWindow(Window win)
		{
			ChildWindows.Add(win);
			Globals.WM.windows.Add(win);
		}

		public void CloseWindow(Window win)
		{
			ChildWindows.Remove(win);
		}

		
	
	}
}