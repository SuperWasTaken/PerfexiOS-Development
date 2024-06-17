using Cosmos.System;
using PerfexiOS.PerfexiAPI;
using PerfexiOS.PerfexiAPI.WindowManager;
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
        public override string[] Parse(GearSh parent, string[] args)
        {


            try
            {
                if (args.Length <= 2 && uint.TryParse(args[0],out var rows) && uint.TryParse(args[1],out var cols)) { GUI.StartGUI(rows, cols); } else
                {
					GUI.StartGUI();
				}
			}
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            return new string[] { };
        }

    }
}
