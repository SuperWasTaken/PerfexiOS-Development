using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class Mkdir : Command
	{
		public Mkdir() : base("mkdir","creates a directory")
		{
		}

		public override string[] Execute(commandManager parent, string[] args)
		{
			try
			{
				if(Directory.Exists(parent.workingdir + args[0]+ @"\")) { return new string[] { "Directory Already Exists" }; }
				VFSManager.CreateDirectory(parent.workingdir + args[0] + @"\");
				return new string[] { "Sucessfully created " + args[0] };
			}
			catch(Exception e)
			{
				return new string[] { "Failed to make directory", e.Message };
			} 
		}
	}
}
