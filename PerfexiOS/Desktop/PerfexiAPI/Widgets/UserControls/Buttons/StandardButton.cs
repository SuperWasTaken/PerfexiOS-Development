
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using Cosmos.System;
using PerfexiOS.Data.Signal;
using CosmosTTF;
using PerfexiOS.Desktop.PerfexiAPI.RichText;
using PerfexiOS.Desktop.PerfexiAPI.Geomitry;
namespace PerfexiOS.Desktop.PerfexiAPI.Widgets.UserControls.Buttons
{
	public class StandardButton : Button
	{
		public Color Background { get; set; } = Color.Gray;
		private bool Hover = false;
		public Color BackgroundSelect { get; set; } = Color.LightGray;

		public int w, h;
		public int Fontsize { get; set; } = 18;
		public string Font { get; set; } = "Default";

		private Action Callback; 
		public Color ForbiddenBackground { get; set; } = Color.DarkGray;
		public Color Text { get; set; } = Color.Black;
		public string text { get; set; } = "Button";
		public enum StandardButtonStates
		{
			None,
			Hover,
			Forbidden,
			
		}

		public StandardButtonStates State { get; set; } = StandardButtonStates.None;
		public StandardButton(Window parent, int x, int y, int w, int h, string text,Action callback) : base(parent, x, y, w, h, new(x +parent.x,y+parent.y,w,h))
		{
			this.Callback = callback;
			var sx = parent.x + x;
			var sy = parent.y + y;
			// Finalise size of the button 
			if (sx + w > parent.x + parent.w)
			{
				base.width = sx + w - parent.w;
			}
			if(sy + h > parent.x+parent.y)
			{
				base.height = sy+h + h - parent.y;
			}
			base.Mask = new(x + parent.x, y + parent.y, w, h);
			
		
			OnMouseHover.Bind(HanldeMouseEnter);
			OnMouseLeave.Bind(HandleMouseLeave);
		}
		public override void render()
		{
			// Calculate the FontSize 
			
			
			Color Bg = Background;
			
			switch(State)
			{
				case StandardButtonStates.None:
					Bg = Background;
					
					break;
				case StandardButtonStates.Hover:
					Bg = BackgroundSelect;
					
					break;
				case StandardButtonStates.Forbidden:
					Bg = ForbiddenBackground;
					
					break;

			}
			var fontwidth = FontManager.GetFont(Font).CalculateWidth(text, Fontsize);
			var font = FontManager.GetFont(Font);
			var offset = (w / 2) - (fontwidth / 2);
			base.RootWindow.Canvas.DrawFilledRectangle(Bg, x, y, width, height);
			base.RootWindow.Canvas.DrawStringTTF(this.x+offset,this.y+h/2,text,Font,Fontsize, Text);

		}

		
		public void HanldeMouseEnter(MouseArgs args)
		{
			if(State == StandardButtonStates.Forbidden) { return; }
			State = StandardButtonStates.Hover;
			Pointer.State = Pointer.UiMouseStates.Clickable;
		}
		public void HandleMouseLeave(MouseArgs args)
		{
			if (State == StandardButtonStates.Forbidden) { return; }
			State = StandardButtonStates.None;
			Pointer.State = Pointer.UiMouseStates.Normal;
		}
		public void HandleMouseClicked(MouseArgs args)
		{
			Callback.Invoke();
		}
		
	}

	
}
