using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class Mkdir : Command
	{
		public Mkdir() : base("mkdir","creates a directory")
		{
		}

		public override string[] Parse(GearSh parent, string[] args)
		{
			try
			{
				if(Directory.Exists(parent.workingdir + args[0]+ @"\")) { parent.Send("Diretory Already Exists"); return new string[] {}; }
				Directory.CreateDirectory(parent.workingdir + args[0] + @"\");
				parent.Send( "Sucessfully created " + args[0] );
			}
			catch(Exception e)
			{
				parent.Send( "Failed to make directory "+ e.Message );
			}
			return new string[] { };
		}
		
		
	}
}
