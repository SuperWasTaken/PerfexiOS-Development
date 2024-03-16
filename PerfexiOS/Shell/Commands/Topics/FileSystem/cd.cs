using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class cd : Command
    {
        public cd() : base("cd","Change Directory")
        {
        }

        private string GetPreviousDirectory(commandManager parent)
        {
            var newdirs = parent.workingdir.Split('/');

            parent.workingdir = parent.previousdir;
            var Split = parent.workingdir.Split(@"\").ToList();
            Split.Remove(Split.Last());

            string NewPrevDir = "";
            foreach(var item  in Split)
            {
                NewPrevDir.Union(item.ToString() + @"\");

            }
            return NewPrevDir;
        }
    
        public override string[] Execute(commandManager parent, string[] args)
        {
            var nextdir = args[0];

            if(nextdir == "..")
            {

                parent.workingdir = GetPreviousDirectory(parent);
                parent.previousdir = GetPreviousDirectory(parent);
                return new string[] { "Sucessfully Changed to previous directory" };
            }

            if (!nextdir.Contains(@"\"))
            {
                
                nextdir = $@"{parent.workingdir}{nextdir}\";
            }

            if(Directory.Exists(nextdir)) 
            {
                parent.previousdir = parent.workingdir;
                parent.workingdir = nextdir;
                return new string[] {$"Sucessfully Changed to {nextdir}" }; 
            }
            return new string[] { $"Directory Dosen't exist" };

        }
    }
}
