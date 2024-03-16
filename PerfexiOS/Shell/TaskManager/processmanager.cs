using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.TaskManager
{
    public static class ProcessManager
    {
        public static List<process> processes = new();
        public static int nextprocessid = 0;




        public static void RegisterProcess(process process)
        {
            processes.Add(process);
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
                    UnregisterProcess(item);
                }
            }
        }

        private static void UnregisterProcess(process process)
        {
            if(!processes.Contains(process)) { return; }
            processes.Remove(process);

            nextprocessid = processes.Count;
        }

        public static void KillProcess(process process)
        {
            if(!processes.Contains(process)) { return; }
            process.stop();
            UnregisterProcess(process);
        }

        public static process GetProcess(int id)
        {
            return processes[id];
        }
        internal static T GetProcess<T>()
        {
            foreach (process process in processes )
            {
                if (process is T processT) return processT;
            }
            return default;
        }

    }
}
