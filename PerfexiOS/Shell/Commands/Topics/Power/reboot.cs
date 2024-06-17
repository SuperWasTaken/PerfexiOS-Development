using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.Power
{
    public class reboot : Command
    {
        public reboot() : base("reboot","Reboots your system")
        {
           
            
        }
        public override string[] Parse(GearSh parent, string[] args)
        {
            parent.Send("Rebooting in 5 seconds");
            Thread.Sleep(5000);
            Cosmos.System.Power.Reboot();
            return new string[] { };
        }
    }
}
