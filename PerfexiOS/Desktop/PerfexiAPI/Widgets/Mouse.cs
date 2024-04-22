using Cosmos.System;
using Cosmos.System.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PerfexiOS.Desktop.Extensions;
using Cosmos.Core;
using CosmosTTF;
namespace PerfexiOS.Desktop.PerfexiAPI.Widgets
{
	public static class Pointer
	{
		/// <summary>
		/// This setting will change how the pointer is drawn being a simple rectangle instead of 
		/// A Bitmap image for Perforamce as less math has to be done to draw a rectangle over a bitmap pointer
		/// </summary>
		public static bool SimplePointer { get; set; } = true;
		public static int PointerSize { get; set; } = 12;


		public static char Mouse_Normal = 'w';

		public static char Clickable = 'O';
		public static char Move = 'p';
		public static char HScale = 'W';
		public static char VScale = 'n';
		public static char Carret = 'o';
		public static char MiddleFinger = 'b';
		public static char Peace_Sign = 'F';
		public enum UiMouseStates
		{
	
			Normal = 'w',
			Clickable = 'O',
			Move = 'p',
			HScale = 'W',
			VScale = 'n', 
			Carret = 'o',
			MiddleFinger = 'b',
			Peace_Sign = 'F',
		}


		
		public static UiMouseStates State { get; set; } = UiMouseStates.Normal;


		public static Point GetLocation()
		{
			return new Point((int)MouseManager.X, (int)MouseManager.Y);
		
		}
		public static void Initalise()
		{
			MouseManager.ScreenWidth = Globals.Canvas.Mode.Width;
			MouseManager.ScreenHeight = Globals.Canvas.Mode.Height;
			
		}
		public static void Render()
		{
			if(SimplePointer)
			{
				switch(State)
				{
					case UiMouseStates.Normal:
						Globals.Canvas.DrawFilledRectangle(Color.Black, (int)MouseManager.X, (int)MouseManager.Y, 16, 16);
						break;
					case UiMouseStates.Clickable:
						Globals.Canvas.DrawFilledRectangle(Color.Green, (int)MouseManager.X, (int)MouseManager.Y, 16, 16);
						break;
					case UiMouseStates.Carret:
						Globals.Canvas.DrawFilledRectangle(Color.Black, (int)MouseManager.X, (int)MouseManager.Y, 4, 16);
						break;
					default:
						Globals.Canvas.DrawFilledRectangle(Color.Black, (int)MouseManager.X, (int)MouseManager.Y, 16, 16);
						break;
				}
				return;
			}
			char drawchar;
			switch(State)
			{
				case UiMouseStates.Normal:
					drawchar = Mouse_Normal;
					break;
				case UiMouseStates.Clickable:
					drawchar = Clickable;
					break;
				case UiMouseStates.Move:
					drawchar = Move;
					break;
				case UiMouseStates.HScale:
					drawchar = HScale;
					break;
				case UiMouseStates.VScale:
					drawchar = VScale;
					break;
				case UiMouseStates.Carret:
					drawchar = Carret;
					break;
				case UiMouseStates.Peace_Sign:
					drawchar = Peace_Sign;
					break;
				case UiMouseStates.MiddleFinger:
					drawchar = MiddleFinger;
					break;
				default:
					drawchar = Mouse_Normal;
					break;
			}
			Globals.PointerFont.DrawToSurface(Globals.CGSSurface, PointerSize, (int)MouseManager.X, (int)MouseManager.Y, drawchar.ToString(), Color.Black); 
		}

	}
}
