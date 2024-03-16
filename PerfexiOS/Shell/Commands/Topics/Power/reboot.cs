using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.Power
{
    public class reboot : Command
    {
        public reboot() : base("reboot","Reboots your system")
        {
           
            
        }
        public override string[] Execute(commandManager parent, string[] args)
        {
            Cosmos.System.Power.Reboot();
            return base.Execute(parent, args);
        }
    }
}
