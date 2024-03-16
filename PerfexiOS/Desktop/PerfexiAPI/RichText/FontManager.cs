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
		private static List<fontdata> Fonts = new();

		public struct fontdata
		{
			public TTFFont font { get; set; }
			public string name { get; set; }
			public fontdata(TTFFont font,string name)
			{
				this.font = font;
				this.name = name;
			}
		}
		/// <summary>
		/// THis just adds
		/// </summary>
		/// <param name="font"><	/param>
		public static void RegisterFont(fontdata data )
		{
			Fonts.Add(data);
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
			sec.WriteKey(@$"KEY:{name}:$R:\PerfexiOS\UiConf\Fonts.pini");
			var d = new fontdata(new(File.ReadAllBytes($@"{Globals.RootPath}PerfexiOS\UiAssests\{name}.ttf")),name);
			RegisterFont(d);
		}

		public static TTFFont GetFont(string name)
		{
			foreach(var item in Fonts)
			{
				if(item.name == name) { return item.font; }
			}
			return null;
		}
	}
}
