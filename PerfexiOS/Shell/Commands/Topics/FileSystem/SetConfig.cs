using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class SetConfig : Command
	{
		public SetConfig() : base("SetConf","Sets the Configuration Files path manualy if it fails on boot.")
		{
		}

		public override string[] Parse(GearSh parent, string[] args)
		{
			if (Directory.Exists(@$"{Globals.RootPath}PerfexiOS\Sys.pini"))
			{
				try
				{
					Globals.Conf = new($@"{args[0]}:\PerfexiOS\Sys.pini");
					parent.Send("System Configuration File Loaded Sucessfully");
					
				}
				catch (Exception ex)
				{
					parent.Send("Failed to load System Configuration file " + ex.Message);
					
				}
			}
			parent.Send("Syntax Error");
			return new string[] { };
		}

	
	}
}
