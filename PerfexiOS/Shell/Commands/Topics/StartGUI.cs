using Cosmos.System;
using PerfexiOS.Desktop.PerfexiAPI;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics
{
    public class StartGUI : Command
    {
        public StartGUI() : base("StartGUI", "Starts GUI while in Terminal Mode")
        {
        }
        public override string[] Execute(commandManager parent, string[] args)
        {
            if(Globals.Conf != null)
            {
				GUI.Initalise();
				return args;
			}
            return new string[] { "System Configuration File has not been loaded cannot start GUI" };
        }

    }
}
