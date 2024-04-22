using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.RichText.TextObjects
{
	public struct TextFragment
	{
		public string Text;
		public int FontSize;
		public Color TextColor;
		public Color BackgroundColor;
		public TTFFont font;
	}

	public class RichTextPannel
	{
		public int CarretIndex;
		public int CarretX; 
		public int CarretY;
		public uint FullWidth;
		public uint FullHeight;
		public TextFragment[] Fragments; 
		
	}
}
