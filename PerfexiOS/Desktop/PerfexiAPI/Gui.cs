
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
using Cosmos.System.Graphics.Fonts;
using IL2CPU.API.Attribs;
using PerfexiOS.Desktop.Extensions;
using Cosmos.Core.Memory;
using PerfexiOS.Desktop.Applications;
using System.Security.Cryptography;
using PerfexiOS.Shell;
using System.IO.Compression;

namespace PerfexiOS.Desktop.PerfexiAPI
{
    public static class GUI 
    {
       
        public static void Initalise()
        {
            try
            {
                
               
                System.Console.WriteLine("Starting the GUI");
                System.Console.WriteLine("Registering System Fonts...");
                Globals.DefaultFont = new(File.ReadAllBytes($@"{Globals.RootPath}PerfexiOS\Default.ttf"));
                // Globals.PointerFont = new(File.ReadAllBytes($@"{Globals.RootPath}PerfexiOS\Cursors.ttf"));
                
                System.Console.WriteLine("Registering Installed Fonts...");
               
                PiniReader FontRegisty = new(new pini($@"{Globals.RootPath}PerfexiOS\UiConf\Fonts.pini"));
                foreach (var item in FontRegisty.GetSection("/FONTS/").keys)
                {
                    var fontname = item.name;
                    var drive = item.value;
                    var path = item.args[0];

                    if (drive == "$R") { drive = Globals.RootPath; }
                    if (!File.Exists($@"{drive}{path}")) { System.Console.WriteLine($"Cannot find {fontname}"); continue; }
                    FontManager.RegisterFont(fontname, new(File.ReadAllBytes($@"{drive}{path}")));
                    System.Console.WriteLine($"Registered {item.name}");

                }
				
				Globals.Canvas = FullScreenCanvas.GetFullScreenCanvas();
                
                Globals.Canvas.Clear(Color.DodgerBlue);
				Globals.GUI = true;
				Widgets.Pointer.Initalise();
				Globals.CGSSurface = new(Globals.Canvas);
				var wm = new WindowManager();
                Globals.WM = wm;
                Memservice service = new();
                FpsWindow win = new();
                               
				
                
			}
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
    }
}
