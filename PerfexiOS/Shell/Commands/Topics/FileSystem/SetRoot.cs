using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class SetRoot : Command 
    {
        public SetRoot() : base("SetRoot","Manualy sets the root drive if it failed to do so on boot")
        {
        }

        public override string[] Execute(commandManager parent, string[] args)
        {
            if (Directory.Exists($@"{args[0]}PerfexiOS\Sys.pini"))
            {
                Globals.RootPath = $@"{args[0]}";
                return new string[] { @$"Changed Root Directory Sucessfully to {args[0]}:\" };   
            }
            return new string[] {"Failed to Change RootDir" };
        }
    }
}
