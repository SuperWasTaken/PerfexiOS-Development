using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class rm : Command
    {
        public rm() : base("rm", "remove file from current directory or path")
        {
        }
        public override string[] Parse(GearSh parent, string[] args)
        {
            var item = args[0];

            if (!item.Contains(@":\"))
            {
                item = parent.workingdir + item;
            }
            if(File.Exists(item))
            {
                try
                {
                    File.Delete(item);

                    parent.Send($"Sucessfulyl deleted {item}");
                    return new string[] {};
                }
                catch(Exception ex)
                {
                    parent.Send($"Failed to delete File {ex.Message}");
                    return new string[] {};
                }
            }

            parent.Send("File Dosen't Exist");
            return new string[] { };

		}

	
		


	}
}
