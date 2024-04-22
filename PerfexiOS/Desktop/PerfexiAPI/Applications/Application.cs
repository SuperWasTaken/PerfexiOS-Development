using PerfexiOS.Desktop.PerfexiAPI.Windows;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Applications
{
	public abstract class Application : process
	{
		public Window MainWindow;
		public List<Window> ChildWindows; 
		public Application(Window window,string name) : base(name)
		{
			ProcessManager.RegisterProcess(this);
			start();
		} 

	
	
	}
}
