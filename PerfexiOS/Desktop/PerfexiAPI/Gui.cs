
using Cosmos.System;
using Cosmos.System.Graphics;
using CosmosTTF;
using PINI;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;
using Cosmos.System.FileSystem;
using PerfexiOS.Desktop.PerfexiAPI.RichText;

namespace PerfexiOS.Desktop.PerfexiAPI
{
    public  static class GUI 
    {
     
  
        public static void Initalise()
        {
            try
            {
                
                
                System.Console.WriteLine("Starting the GUI");
                System.Console.WriteLine("Registering Default font...");
                Globals.DefaultFont = new(File.ReadAllBytes($@"{Globals.RootPath}PerfexiOS\Default.ttf"));

				
                System.Console.WriteLine("Registering Installed Fonts...");
               

                PiniReader FontRegisty = new(new pini($@"{Globals.RootPath}PerfexiOS\UiConf\Fonts.pini"));
                foreach (var item in FontRegisty.GetSection("/FONTS/").keys)
                {
                    var fontname = item.name;
                    var drive = item.value;
                    var path = item.args[0];

                    if (drive == "$R") { drive = Globals.RootPath; }
                    if (!File.Exists($@"{drive}{path}")) { System.Console.WriteLine($"Cannot find {fontname}"); continue; }
                    var d = new FontManager.fontdata(new(File.ReadAllBytes($@"{drive}{path}")), fontname);
                    System.Console.WriteLine($"Registered {item.name}");

                }
				Globals.GUI = true;
				Globals.Canvas = FullScreenCanvas.GetFullScreenCanvas();
				Globals.Canvas.Clear(Color.White);
				var wm = new WindowManager();
                Widgets.Pointer.Initalise();
                Globals.WM = wm;
                ProcessManager.RegisterProcess(Globals.WM);
				Globals.Canvas.Display();
				Globals.WM.start();

                var win = new Window(100, 100, 300, 400, "4", false);
                win.Canvas.Clear(Color.White);
                win.Canvas.DrawLine(10, 10, 100,120 , Color.Pink);
                win.Canvas.DrawFilledCircle(Color.HotPink, 10, 100, 20);
                win.Canvas.DrawString(10, 90, "ComicSans", "Hello!",20,Color.Yellow);
                wm.AppendWindow(win);

			}
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
           
            

        }
        

    }
}
