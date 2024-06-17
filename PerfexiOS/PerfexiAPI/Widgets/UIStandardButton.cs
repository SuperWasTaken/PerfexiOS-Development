using PerfexiOS.PerfexiAPI.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.Widgets
{
	public class UiStandardButton : Widget
	{
		public TextFragment Fragment;
		public UiCanvas Canvas;
		public int x, y, w, h;
		public Color Color { get; set; } = Color.LightBlue;
		public Color HoverColor { get; set; } = Color.Green;
		public Color ForbiddenColor { get; set; } = Color.Red;

		public ButtonStates CurrentState { get; set; } = ButtonStates.None;
		public enum ButtonStates
		{
			Hovered,
			None,
			Forbidden, 
		}

		public UiStandardButton(int x, int y, int w, int h, TextFragment fragment, UiCanvas canvas) : base(x,y,w,h)
		{
			Fragment = fragment;
			Canvas = canvas;
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
			
		}

	

		public override void Draw()
		{
			Color DrawColor = this.Color;
			switch(CurrentState)
			{
				case ButtonStates.Hovered: DrawColor = this.HoverColor; break;
				case ButtonStates.Forbidden: DrawColor = this.ForbiddenColor; break;
			}
			Canvas.DrawFilledRectangle(x, y, w,h,DrawColor);
			Canvas.DrawTextFragment(Fragment, 4, this.h / 2);

		}
	}
}
