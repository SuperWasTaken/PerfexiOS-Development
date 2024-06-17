using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.HAL.Drivers.USB;
namespace PerfexiOS.Shell.IO
{
	public abstract class USBhost :Device
	{

	}


	public class USBDevice
	{
		

	}

	public class UsbPacket
	{
		public enum PacketType
		{
			Token,
			Data,
			Handshake,
			Specail,
		}

		public PacketType Type;
	
		


		public UsbPacket(byte[] data)
		{
			var PacketInformation = data[0];
			
		}
	}
}
