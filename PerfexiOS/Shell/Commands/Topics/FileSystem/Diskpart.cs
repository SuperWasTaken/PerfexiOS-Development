using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COROS;
using Cosmos.HAL.BlockDevice.Ports;
using Cosmos.Core.Memory;
using System.Runtime.InteropServices;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class Diskpart : Command
	{

		
		bool FormattingDisk;
		ulong FormatNum = 0ul;
		Disk targetDisk;
		int targetpart;
		ManagedPartition Formatting;
		List<Disk> Disks
		{
			get
			{
				var disks = VFSManager.GetDisks();
				return disks;
			}
		}
		public Diskpart() : base("diskpart","Manage Format and parition Disks with this tool")
		{

		}

		public override string[] Parse(GearSh parent, string[] args)
		{
			parentshell = parent;
			var commandname = args[0];
			var commandargs = args.Skip(1).ToArray();
			var disks = Disks;
			if(commandname == "mnt")
			{
				try
				{
					if (int.TryParse(commandargs[0], out var disk) && int.TryParse(commandargs[1], out var part))
					{
						return mnt(disk, part);
					}
					return new string[] { "Syntax Error" };
				}
				catch(Exception e)
				{
					return new string[] { "Error: " + e.Message };
				}
			}
			if(commandname == "ls") { ls(); return new string[] { }; }
			if(commandname == "inf")
			{
				if (int.TryParse(commandargs[0],out var d))
				{
					parentshell.Send($"INFORMATION FOR DISK: {d}");
					inf(disks[d]);
					return new string[] { };
				}
			}
			if(commandname == "mkpart")
			{
				if (int.TryParse(commandargs[0],out var disk) && int.TryParse(commandargs[1],out var size))
				{
					var input = parent.Ask($"Create a {size} MB partition on Disk {disk}? Y/N:");
					if(input == "Y")
					{
						mkpart(disks[disk], size);
					}
					return new string[] { };
				}
			}
			if(commandname == "rmpart")
			{
				if (int.TryParse(commandargs[0], out var disk) && int.TryParse(commandargs[1], out var part))
				{
					var input = parent.Ask($"Delete Partition {part} on Disk {disk}? All Data on this part will be lost Y/N:");
					if (input == "Y")
					{
						rmpart(disks[disk],part);
					}
					return new string[] { };
				}
			}
			if (commandname == "mkfs")
			{
				if (int.TryParse(commandargs[0], out var disk) && int.TryParse(commandargs[1], out var partition))
				{
					try
					{
						
					
						ulong cyclecount = 0;
						int LastSecond = 0;
						int CurrentSecond = DateTime.Now.Second;

						ulong writespd = 1ul;
						bool quick = false;
						if(commandargs.Contains("QUICK:TRUE")) { quick = true; }
						foreach(var item in commandargs)
						{
							if(item.StartsWith("WRITESPD:"))
							{
								if (ulong.TryParse(item.Split(':')[1].Trim(), out var spd))
								{
									writespd = spd;
								}
							}
						}
						parent.Clear();
						parent.Send("Begining Format phase 1 please wait...");
						Heap.Collect();
						var part = disks[disk].Partitions[partition];
						byte[] aData = new byte[512*writespd];
						for (ulong num = 0uL; num < part.Host.BlockCount; num+= writespd)
						{
							
							if(num + writespd > part.Host.BlockCount)
							{
								var fw = part.Host.BlockCount - num;
								byte[] fdata = new byte[512*fw];
								part.Host.WriteBlock(num,writespd,ref fdata);
								break;
							}
							else
							{

								part.Host.WriteBlock(num, writespd, ref aData);
								
								
							}
							cyclecount++;
							CurrentSecond = DateTime.Now.Second;

							if(CurrentSecond != LastSecond)
							{
								parent.Clear();
								parent.Send("Formatting Disk Please wait");
								parent.Send("Do not Close this terminal or power off your computer");
								parent.Send("As this can damage the disk");
								parent.Send($"{num}/{part.Host.BlockCount} Blocks Written");
								LastSecond = CurrentSecond;
								var bps = cyclecount * writespd;
								var remainingblocks = part.Host.BlockCount - num;
								parent.Send($"BPS: {bps}");
								cyclecount = 0;
								
								parent.Send("ETR: " +  remainingblocks/bps + " Seconds Remaining");
							}
						}
						part.MountedFS = null;
						part.RootPath = "";
						parent.Send("Creating Filesystem...");
						disks[disk].FormatPartition(partition, "FAT32", quick);
						return new string[] { "FORMAT COMPLETED! run diskpart mnt {disk} {partition} to mount it" };
					}
					catch(Exception ex)
					{
						parent.Send(" FORMAT FAILED");
						parent.Send(ex.Message);
						
					}
				

				}
				else
				{
					return new string[] { "INVALID SYNTAX" };
				}
			}

			
			return new string[] { };
		}

		private void mkpart(Disk d,int size)
		{
			try
			{
				d.CreatePartition(size);
				parentshell.Send($"Sucessfully Created {size} MB Partition");
				return;

			}
			catch(Exception e)
			{
				parentshell.Send("Failed To create Partion" + e.Message);
				return; 
			}
		}
	
		private void rmpart(Disk d,int index)
		{
			try
			{
				
				if (parentshell.Ask("Are you sure you want to Delete this partion?\nAll Data will be wiped and wont be recoverable y/n > ") == "y")
				{
					d.DeletePartition(index);
					parentshell.Send($"Parition {index} was Sucessfully Removed");
					return;
				}
				else
				{
					parentshell.Send("Operation Canceled");
					return;
				}
			}
			catch(Exception e)
			{
				parentshell.Send("ERROR: " + e.Message);
			}
		}



		private string[] mnt(int disk,int part)
		{
			var disks = Disks;
			var d = disks[disk];
			var par = d.Partitions[part];
			d.MountPartition(part);
			return new string[] { "Mounted at " + par.RootPath };

		}



		private void ls()
		{
			var disks = Disks;
			for(int i = 0; i < Disks.Count; i++)
			{
				parentshell.Send("DISK: " + i);
				var disk = disks[i];
				
				switch(disk.Type)
				{
					case Cosmos.HAL.BlockDevice.BlockDeviceType.HardDrive:
						parentshell.Send("TYPE: " + "HDD");
						break;
					case Cosmos.HAL.BlockDevice.BlockDeviceType.RemovableCD:
						parentshell.Send("TYPE: " + "CDR");
						break;
					case Cosmos.HAL.BlockDevice.BlockDeviceType.Removable:
						parentshell.Send("TYPE: " + "REM");
						break;
					default:
						parentshell.Send("TYPE: " + "UNK");
						break;
				}
				for(int p = 0; p < disk.Partitions.Count; p++)
				{
					var par = disk.Partitions[p];
					parentshell.Send("--------");
					parentshell.Send("PARTITION: " + p);
					parentshell.Send("BLOCKSIZE: " + par.Host.BlockSize);
					parentshell.Send("BLOCKCOUNT: " + par.Host.BlockCount);
					parentshell.Send("SIZE: " + par.Host.BlockSize * par.Host.BlockCount / 1024 / 1024 + " MB");
					parentshell.Send("FILESYSTEM: " + par.LimitFS);
					parentshell.Send("MOUNTPOINT: " + par.RootPath);

				}
				return;
			}
		}

		private void inf(Disk disk)
		{

			switch (disk.Type)
			{
				case Cosmos.HAL.BlockDevice.BlockDeviceType.HardDrive:
					parentshell.Send("TYPE: " + "HDD");
					break;
				case Cosmos.HAL.BlockDevice.BlockDeviceType.RemovableCD:
					parentshell.Send("TYPE: " + "CDR");
					break;
				case Cosmos.HAL.BlockDevice.BlockDeviceType.Removable:
					parentshell.Send("TYPE: " + "REM");
					break;
				default:
					parentshell.Send("TYPE: " + "UNK");
					break;
			}

			parentshell.Send($"SIZE: {disk.Size * 1024 * 1024} MB");

			parentshell.Send($"PARITIONCOUNT: {disk.Partitions.Count}");

			for (int p = 0; p < disk.Partitions.Count; p++)
			{
				var par = disk.Partitions[p];
				parentshell.Send("--------");
				parentshell.Send("PARTITION: " + p);
				parentshell.Send("BLOCKSIZE: " + par.Host.BlockSize);
				parentshell.Send("BLOCKCOUNT: " + par.Host.BlockCount);
				parentshell.Send("SIZE: " + par.Host.BlockSize * par.Host.BlockCount / 1024 / 1024 + " MB");
				parentshell.Send("FILESYSTEM: " + par.LimitFS);
				parentshell.Send("MOUNTPOINT: " + par.RootPath);

			}
			return;
		}
	}
}
