using Cosmos.System;
using PerfexiOS.Data;
using PerfexiOS.Data.Signal;
using PerfexiOS.Desktop.PerfexiAPI;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.TaskManager
{
    /// <summary>
    /// This class manages the Processes and is also the Operating Systems Window Manager 
    /// As my idiotic self decited to manage windows in a seprate rpocess due to it being taxxing 
    /// </summary>
    public static class ProcessManager
    {
       
        public static List<process> processes = new();
        public static int nextprocessid = 0;

        public static void RegisterProcess(process process)
        {
            processes.Add(process);
            process.id = nextprocessid;
            nextprocessid++;
            process.start();
        }

        public static void Yeild()
        {
            foreach(var item in processes)
            {
                if(item.State == process.state.Running)
                {
                    item.loop();
                }
                if(item.State == process.state.Stopped || item.State == process.state.Crashed)
                {
                    KillProcess(item);
                }
            }
        }


    

		private static void UnregisterProcess(process process)
        {
            process.stop();
            processes.Remove(process);
            nextprocessid = processes.Count;
            
        }

        public static void KillProcess(process process)
        {
            UnregisterProcess(process);
        }

        public static process GetProcess(int id)
        {
            return processes[id];
        }
      
	}
}
