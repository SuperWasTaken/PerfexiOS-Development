using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class SetRoot : Command 
    {
        public SetRoot() : base("SetRoot","Manualy sets the root drive if it failed to do so on boot")
        {
        }

        public override string[] Parse(GearSh parent, string[] args)
        {
            if (File.Exists($@"{args[0]}PerfexiOS\Sys.pin"))
            {
                Globals.RootPath = $@"{args[0]}";
                parent.Send(@$"Changed Root Directory Sucessfully to {args[0]}:\");
                
            }
            parent.Send("Failed to Change RootDir");
            return new string[] { };
        }
    }
}
