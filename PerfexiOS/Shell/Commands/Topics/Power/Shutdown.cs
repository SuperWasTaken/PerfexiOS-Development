using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.Power
{
    public class shutdown : Command
    {
        public shutdown() : base("shutdown", "shuts down the system")
        {
        }

        public override string[] Parse(GearSh parent, string[] args)
        {
            
            Cosmos.System.Power.Shutdown();
            return new string[] {};
        }

    }
}
