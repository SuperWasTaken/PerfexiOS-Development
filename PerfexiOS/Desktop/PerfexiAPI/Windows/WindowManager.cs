using System.Drawing;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using Cosmos.System;

namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
    public class WindowManager : process
    {
      
        
        public WindowManager() : base("System Explorer")
        {
        }
        public List<Window> windows = new();
        public void Render()
        {   
            DrawWallaper();
          
            foreach(var item in windows)
            {
                item.Render();
            }

            Pointer.Render();
           
            Globals.Canvas.Display();
        }

	
		public void CloseWindow(Window win)
        {
            foreach (var item in windows)
            {
                if(item == win) { windows.Remove(item); break; }
            }
        }


		public override void loop()
		{
            Render();
            if(MouseManager.MouseState == MouseState.Left)
            {
                var win = new Window(300, 300, 400, 400, "HelloWorld", false);
               
				win.Canvas.Clear(Color.White);
				win.Canvas.DrawLine(10, 10, 100, 120, Color.Pink);
				win.Canvas.DrawFilledCircle(Color.HotPink, 10, 100, 20);
				win.Canvas.DrawString(150, 90, "Default", "Hello!", 20, Color.White);
				windows.Add(win);
                win.Render();


                
            }
		}
		public void Prioritise(Window win)
        {
            foreach(var item in windows)
            {
                if(item == win)
                {
                    var c = windows.Last();
                    var index = windows.IndexOf(win);
                    windows[index] = c;
                    windows.Add(win);
                }
            }
        }
        /// <summary>
        /// Appends a subwindow to the Application 
        /// 
        /// </summary>
        /// <param name="win"></param>
        public void AppendWindow(Window win)
        {
            windows.Add(win);
        }
        /// <summary>
        /// This changes the MAIN window of the application if you
        /// want to add a sub window like a popup use AppendWindow() instead
        /// </summary>
        /// <param name="win"></param>
        public void DrawWallaper()
        {
            Globals.Canvas.DrawFilledRectangle(Color.DarkBlue, 0, 0, (int)Globals.Canvas.Mode.Width, (int)Globals.Canvas.Mode.Height);
        }
    }
}
