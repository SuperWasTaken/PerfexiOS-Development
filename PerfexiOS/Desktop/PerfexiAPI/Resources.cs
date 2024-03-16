using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI
{
	public static class resources
	{
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.Mouse.bmp")]
		private static readonly byte[] _mn; public static readonly Bitmap Mouse = new(_mn);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.MouseCarret.bmp")]
		private static readonly byte[] _mc; public static readonly Bitmap MouseCarret = new(_mn);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.MouseHResize.bmp")]
		private static readonly byte[] _mrh; public static readonly Bitmap HMouseResize = new(_mn);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.MouseMove.bmp")]
		private static readonly byte[] _mm; public static readonly Bitmap MouseMove = new(_mm);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.MouseSelect.bmp")]
		private static readonly byte[] _ms; public static readonly Bitmap MouseSelect = new(_ms);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.MouseVResize.bmp")]
		private static readonly byte[] _mvr; public static readonly Bitmap VMouseResize = new(_mvr);
		// Loading Animntion Frames 
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.LoadingFrame1.bmp")]
		private static readonly byte[] _lf1; public static readonly Bitmap LoadingFrame1 = new(_lf1);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.LoadingFrame2.bmp")]
		private static readonly byte[] _lf2; public static readonly Bitmap LoadingFrame2 = new(_lf2);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.LoadingFrame3.bmp")]
		private static readonly byte[] _lf3; public static readonly Bitmap LoadingFrame3 = new(_lf3);
		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.LoadingFrame3.bmp")]
		private static readonly byte[] _lf4; public static readonly Bitmap LoadingFrame4 = new(_lf4);

		[ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.PerfexiOS.bmp")]
		private static readonly byte[] _logo; public static readonly Bitmap Logo = new(_logo);
	}
}
