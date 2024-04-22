using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COROS;
using Cosmos.HAL.BlockDevice.Ports;
namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class Diskpart : Command
	{
		public Diskpart() : base("diskpart","Command Line Tool for Paritioning disks")
		{
		}
		private string[] SyntaxError = new string[] { "Syntax Error" };
		private string[] DiskNotFound = new string[] { "Drive not connected" };
		private string[] PartNotFound = new string[] { "Partition Not Found" };
		public override string[] Execute(commandManager parent, string[] args)
		{
			var rootcommand = args[0];
			switch(rootcommand)
			{
				case "ls":
					return ls();
				case "mkpart":
					if (!int.TryParse(args[1], out var disk)) { break; }
					if(!int.TryParse(args[2],out var size)) { break; }
					return mkpart(disk, size);
				case "inf":
					if (!int.TryParse(args[1],out disk)) { break; }
					return inf(disk);
				case "rmpart":
					if (!int.TryParse(args[1],out disk)) { break; }
					if (!int.TryParse(args[2],out var partition)) { break; }
					return rmpart(disk, partition);	
				case "mkfs":
					if (!int.TryParse(args[1],out disk)) { return new string[] { "Disk Not connected" }; }
					if (!int.TryParse(args[2],out var par)) { return new string[] { "Partition Dosent Exist" }; }
					bool q;
					if (args[3] != null) { q = true; } else { q = false; }
					return mkfs(disk, par,q);
				case "mnt":
					if (!int.TryParse(args[1],out disk)) { return SyntaxError; }
					if (!int.TryParse(args[2],out par)) { return SyntaxError; }
					return mount(disk, par);
				case "clr":
					if (!int.TryParse(args[1], out disk)) { return new string[] { "Invalid Syntax" }; }
					var a = parent.Ask("Are you sure you want to clear all paritions Y/N");
					if (a == "Y") { return clr(disk); }
					if(a == "M") { return new string[] { "Operation Canceled" }; }

					break;
				case "mnta":
					if (!int.TryParse(args[1], out disk)) { return new string[] { "Invalid Syntax" }; }
					return MountAll(disk);

					
			}

			return new string[] { "Invalid Syntax" };
		}

		private string[] mkpart(int drive, int size)
		{
			var disks = VFSManager.GetDisks();
			if(disks.Count < drive) { return new string[] { "Drive dosen't Exist" }; }
			try
			{
				var disk = disks[drive];
				disk.CreatePartition(size);
				return new string[] { $"Created {size} MB Parition on Drive {drive}",$"Run the command >diskpart mkfs <drive> <partion> to format it" };
			}
			catch(Exception e)
			{
				return new string[] { e.Message };
			}
			
		}

		private string[] ls()
		{
	
			List<string> output = new();
			var disks = VFSManager.GetDisks();
			Console.WriteLine($"Drives Connected:{disks.Count}");
			for(int i = 0; i < disks.Count;i++)
			{
				var disk = disks[i];
				output.Add($"DISK:{i}");
				if(disk.Partitions.Count > 0)
				{
					for (int ii = 0; ii < disk.Partitions.Count - 1; i++)
					{
						var par = disk.Partitions[ii];
						output.Append($"- Partition{ii}");
					}
				}
				output.Append("No Paritions found on this disk");

				
			}
			return output.ToArray();
		}

		private string[] inf(int drive)
		{
			try
			{


				List<string> output = new();
				var disks = VFSManager.GetDisks();
				Disk? disk = disks[drive];
				if(disk == null) { return DiskNotFound; }


				
				output.Add($"Info for disk{drive}");
				output.Add($"Full Size:{disk.Size/1024/1024} MB");
				output.Add($"PartitionCount:{disk.Partitions.Count}");

				if(disk.Partitions.Count > 0)
				{
					
					output.Add("Partitions-");
					for (var i = 0; i <= disk.Partitions.Count - 1; ++i)
					{
						var par = disk.Partitions[i];
						output.Add($" Partition: {i}");
						output.Add($" Total Size: {par.Host.BlockCount * par.Host.BlockSize / 1024 / 1024} MB");
						output.Add($" RootPath: {par.RootPath}");
						output.Add($"FileSystem: {par.MountedFS}");
						if (!par.HasFileSystem) { output.Add("No File System Found"); }else
						{
							output.Add("Has File System");
						}
						
					}
				}
				
				return output.ToArray();
			}
			catch (Exception ex)
			{
				return new string[] {"Failed to get info "+ ex.Message };
			}
			
		}

		public string[] rmpart(int disk,int partition)
		{
			var disks = VFSManager.GetDisks();
			if(disk > disks.Count-1) { return new string[] { "Disk Dosen't Exist" }; }
			if(partition > disks[disk].Partitions.Count-1) { return new string[] { "Partiton Dosent Exist" }; }
			try
			{
				var d = disks[disk];
				d.DeletePartition(partition);
				return new string[] { $"Deleted partition#{partition} sucessfully" };
			}
			catch(Exception e)
			{
				return new string[] { $"{e.Message}" };
			}
		}

		public string[] mkfs(int disk,int partition,bool quick,string format = "FAT32")
		{
			var disks = VFSManager.GetDisks();
			try
			{

				var d = disks[disk];
				d.FormatPartition(partition, format, quick);
			}
			catch(Exception e)
			{
				return new string[] { $"{e.Message}" };
			}
			return new string[] { "Syntax Error" };
		}
		public string[] mount(int disk,int partition)
		{
			var disks = VFSManager.GetDisks();
			if(disks.Count < disk) { return DiskNotFound; }
			if (VFSManager.GetDisks()[disk].Partitions.Count-1 < partition) { return PartNotFound; }
			try
			{
				var d = VFSManager.GetDisks()[disk];
				d.MountPartition(partition);
				
				return new string[] { $" Sucessfully Mounted Partition at {d.Partitions[partition].RootPath}" };
			}
			catch(Exception e)
			{
				return new string[] { $"Failed to Mount Parition {e.Message}" };
			}
		}
		public string[] MountAll(int disk)
		{
			var disks = VFSManager.GetDisks();
			if (disks.Count < disk) { return DiskNotFound; }
			var d = disks[disk];
			try
			{
				d.Mount();
				return new string[] { $"Mounted all paritions on disk# {disk}" };
			}
			catch(Exception e) { return new string[] {e.Message};}
		}
		public string[] clr(int disk)
		{
			var d = VFSManager.GetDisks()[disk];
			d.Clear();
			return new string[] { $"Cleared all paritions on disk {disk}" };
		}
	
		
	}
}
