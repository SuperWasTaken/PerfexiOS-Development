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
        public override string[] Execute(commandManager parent, string[] args)
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
                    return new string[] { $"Sucessfulyl deleted {item}" };
                }
                catch(Exception ex)
                {
                    return new string[] { $"Failed to delete File {ex.Message}" };
                }
            }
            return new string[] { "File Dosen't Exist" };
        }
        

    }
}
