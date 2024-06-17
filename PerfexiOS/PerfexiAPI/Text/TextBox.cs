using Cosmos.System.Graphics;
using CosmosTTF;
using PerfexiOS.PerfexiAPI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.Text
{
	public class TextBox : Widget
	{
		public TextFragment[] Fragments;
		public int carretindex;
		public int cx;
		public int cy;
		public bool HScrollbar;
		public bool VScroolbar;
		public int viewportx;
		UiCanvas MainCanvas;
		public TextBox(PerAPIRectangle rectangle, UiCanvas target, TextFragment[] Fragments) : base(rectangle.x,rectangle.y,rectangle.w,rectangle.h)
		{
			this.Fragments = Fragments;
			MainCanvas = target;
		}
		public TextBox(PerAPIRectangle rectangle, UiCanvas target, string text,TTFFont font,int fontsize,Color c) : base(rectangle.x, rectangle.y, rectangle.w, rectangle.h)
		{
			TextFragment fragment = new(font, fontsize, c, text);
			Fragments.Append(fragment);
		}




		/// <summary>
		/// If text wrapping 
		/// </summary>
		/// <param name="wrapping"></param>
		/// <returns></returns>
		


		public void RenderText()
		{
			bool drawCarret = false;
			int dx = 0;
			int dy = 0;
			int cx = 0;
			int cy = 0;
			TextFragment CarretFragment = Fragments[0];
			int carretFragmentIndex = 0;
			int CharsLeft = 0;
			int ldw = 0;
			int ldh = 0;
			for(int i = 0; i < Fragments.Length; i++)
			{
				int linesdrawn = 0;
				var f = Fragments[i];
				int maxcharsperline = base.Area.w / f.fontsize;
				if(CharsLeft > 0)
				{
					CharsLeft = ldw / f.fontsize;
				}
				for(int ii = 0; ii < f.text.Length; ii++)
				{
					var c = f.text[ii];
					switch(c)
					{
						case '\n':
							dy += f.fontsize;
							linesdrawn++;
							break;
						case '\t':
							dx += f.fontsize * 4;
							CharsLeft -= 4;
							ldw += f.fontsize * 4;
							break;
						default:
							if(CharsLeft <= 0)
							{
								dx = 0;
								dy += f.fontsize;
								CharsLeft = maxcharsperline;
								linesdrawn++;
							}
							var glypth = f.font.RenderGlyphAsBitmap(new(c), f.Color, f.fontsize);
							if(glypth.HasValue) { MainCanvas.DrawBitmap(glypth.Value.bmp, dx, dy); }
							ldw += f.fontsize;
							if (cx >= dx && cx <= cx+f.fontsize)
							{
								cx = dx;
								CarretFragment = f;
								cy = dy;
								carretFragmentIndex = ii;
							}
							break;
					}
				};
			}
			// Draw the carret 
			if(!drawCarret) { drawCarret = true; return; }
			MainCanvas.DrawVerticalLine(Color.Black, CarretFragment.fontsize, cx, cy);
			drawCarret = false;
		}
		public int CalculateLines()
		{
			int lines = 0;
			for(int i = 0; i < Fragments.Length; i++)
			{
				var fragment = Fragments[i];
				lines += i + fragment.text.Split('\n').Length;
			}
			return lines;
		}

		public override void Draw()
		{
			RenderText();
		}
	}
}
