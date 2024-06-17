using Cosmos.System.Graphics;
using CosmosTTF;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI
{
	public static class Resources
	{
		[ManifestResourceStream(ResourceName = "PerfexiOS.PerfexiAPI.Resources.PerfexiOS.png")]
		private static readonly byte[] _lgo; public static Bitmap Logo = new(_lgo);
	}
}
