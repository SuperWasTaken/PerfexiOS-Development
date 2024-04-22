using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PINI;
using System.IO;
namespace PerfexiOS.Desktop.PerfexiAPI.RichText
{
	/// <summary>
	/// This class just manages the installed fonts in one place
	/// 
	/// </summary>
	public static class FontManager
	{
		private static Dictionary<string, TTFFont> fonts = new();
		/// <summary>
		/// THis just adds
		/// </summary>
		/// <param name="font"><	/param>
		public static void RegisterFont(string name,TTFFont font)
		{
			fonts.Add(name,font);
		}
		/// <summary>
		/// This Method writes the fonts data to the Font Pini file and Registeres it 
		/// 
		/// </summary>
		public static void InstallFont(string path,string name)
		{
			if(!File.Exists(path))
			{
				return;
			}
			File.Move(path, $@"{Globals.RootPath}PerfexiOS\UiAssests\{name}.ttf");
			PiniReader reader = new(new($@"{Globals.RootPath}PerfexiOS\UiConf\Fonts.pini"));
			var sec = reader.GetSection("/FONTS/");
			sec.WriteKey(@$"KEY:$R:PerfexiOS\UiAssests\{name}.ttf");
			var font = new TTFFont(File.ReadAllBytes($@"{Globals.RootPath}PerfexiOS\UiAssests\{name}.ttf"));
			RegisterFont(name,font);
		}

		public static TTFFont GetFont(string name)
		{
			if (fonts.ContainsKey(name)) return fonts[name]; else { return Globals.DefaultFont; }
		}
	}
}
