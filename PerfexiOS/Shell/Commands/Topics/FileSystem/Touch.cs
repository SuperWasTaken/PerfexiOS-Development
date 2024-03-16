using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class Touch : Command
	{
		public Touch() : base("touch", "Creates a file")
		{
		}

		public override string[] Execute(commandManager parent, string[] args)
		{
			try
			{
				if (File.Exists($@"{parent.workingdir}{args[0]}"))
				{
					return new string[] { "Item already exists" };
				}

				VFSManager.CreateFile($@"{parent.workingdir}{args[0]}");
				return new string[] { $"Created{args[0]} sucessfully." };
				

			}
			catch (Exception ex)
			{
				return new string[] { "Failed to make file", ex.Message };
			}
		}
	}

}
