using PerfexiOS.Desktop.PerfexiAPI.Widgets.UserControls.Buttons;
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.Applications
{
	public class ButtonTest : UiApplication
	{
		
		public ButtonTest() : base("ButtonTest")
		{
			MainWindow = new(this, 100, 100, 400, 400, "ButtonTest", true);
			var btn = new StandardButton(MainWindow, 40, 40, 120, 64, "ClickMe", () => ClickAction());
			btn.Background = Color.Blue;
		}
		
		private void ClickAction()
		{
			MainWindow.Canvas.DrawString(200, 200, "ComicSans", "You Clicked Me!",26, Color.Red);
		}

		
	}
}
