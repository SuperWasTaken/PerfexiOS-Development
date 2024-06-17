using Cosmos.System.Graphics;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.Text
{
	

	/// <summary>
	/// This is a Text Fragment 
	/// This is a part of text using a GLypth sheet 
	/// </summary>
	public struct TextFragment
	{
		public TTFFont font;
		public string text;
		public int fontsize;
		public Color Color;
		public TextFragment(TTFFont font,int Fontsize,Color c, string text)
		{
			this.font = font;
			this.text = text;
			this.fontsize = Fontsize;
			this.Color = c;
		}
	}
	
}
