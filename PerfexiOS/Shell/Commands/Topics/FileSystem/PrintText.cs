using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class PrintText : Command
	{
		public PrintText() : base("PrintFile","Prints the Text Of a File to the Terminal")
		{
	
		}

		public override string[]  Parse(GearSh parent, string[] args)
		{
			var path = args[0];
			if (path.Contains(':'))
			{
				foreach(var item in File.ReadAllText(path).Split('\n')){ parent.Send(item); }
				return new string[] {};
			}
			
			try
			{
				foreach(var item in File.ReadLines(parent.workingdir + path).ToArray())
				{
					parent.Send(item);
				}
			}
			catch(Exception e)
			{
				parent.Send("Read Failed: " + e.Message);
				
			}
			return new string[] { };
		}
	}
}
