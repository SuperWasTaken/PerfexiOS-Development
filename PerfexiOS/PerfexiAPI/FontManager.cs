using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI
{
	public static class FontManager
	{
		public static Dictionary<string, TTFFont> Fonts = new();

		public static void RegisterFont(TTFFont font,string name)
		{
			Fonts.Add(name, font);
		}

		public static TTFFont GetFont(string name)
		{
			return Fonts[name];
		}
	}
}
