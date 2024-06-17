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
		private GearSh parent;
		private string[] args; 
        public cd() : base("cd","Change Directory")
        {
        }

        private string GetPreviousDirectory(GearSh parent)
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

		public override string[] Parse(GearSh parent, string[] args)
		{
			var nextdir = args[0];

			if (nextdir == "..")
			{

				parent.workingdir = GetPreviousDirectory(parent);
				parent.previousdir = GetPreviousDirectory(parent);

				parent.Send("Sucessfully Changed to previous directory");


			}

			if (!nextdir.Contains(@"\"))
			{

				nextdir = $@"{parent.workingdir}{nextdir}\";
			}

			if (Directory.Exists(nextdir))
			{
				parent.previousdir = parent.workingdir;
				parent.workingdir = nextdir;

				parent.Send($"Sucessfully Changed to {nextdir}");
			}
			parent.Send("Directory Dosen't exist");
			parent.ParsingCommand = false;
			return new string[] { };
		}
	}
}
