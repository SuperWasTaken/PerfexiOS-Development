using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.PerfexiAPI.Events
{
	public interface perfAPIevent
	{
	}

	public class KeyboardEvent : perfAPIevent
	{
		KeyEvent MainEvent;
		
		public bool shift
		{
			get
			{
				return MainEvent.Modifiers == ConsoleModifiers.Shift;
			}
		}
		public bool ctrl
		{
			get
			{
				return MainEvent.Modifiers == ConsoleModifiers.Control;
			}
		}
		public bool alt
		{
			get
			{
				return MainEvent.Modifiers == ConsoleModifiers.Alt;
			}
		}
		
		
		public ConsoleKeyInfo info = new();
		public KeyboardEvent(KeyEvent MainEvent)
		{
			this.MainEvent = MainEvent;
		}
	}
	public class MouseEvent : perfAPIevent
	{
		public int MX, MY;
		public MouseState Sate; 
		public MouseEvent(int mX, int mY, MouseState sate)
		{
			MX = mX;
			MY = mY;
			Sate = sate;
		}
	}
}
