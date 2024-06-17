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

		public override string[] Parse(GearSh parent, string[] args)
		{
			try
			{
				if (File.Exists($@"{parent.workingdir}{args[0]}"))
				{
					parent.Send("Item Already Exists");
				}
				File.Create($@"{parent.workingdir}{args[0]}");
				parent.Send($"Created{args[0]} sucessfully.");

			}
			catch (Exception ex)
			{
				parent.Send("Failed to make file " + ex.Message);

			}
			return new string[] {};
		}
	}

}
