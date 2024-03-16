using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class ls : Command
    {
        public ls() : base("ls", "List all files in the current directory")
        {
        }
        public override string[] Execute(commandManager parent,string[] args)
        {
            var output = new List<string>();
            try
            {
                foreach (var item in Directory.GetDirectories(parent.workingdir))
                {
                    output.Add($"DIR> {item}");
                }
                foreach (var item in Directory.GetFiles(parent.workingdir))
                {
                    output.Add(item);
                }
            }
            catch(Exception e)
            {
                output.Add($"Failed to list files: {e.Message}");
            }
            return output.ToArray();
        }
    }
}
