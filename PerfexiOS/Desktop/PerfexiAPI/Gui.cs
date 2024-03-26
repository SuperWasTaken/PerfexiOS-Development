
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

namespace PerfexiOS.Desktop.PerfexiAPI
{
    public  static class GUI 
    {

        [ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.prefixilogo.bmp")]
        static byte[] _PrefixiLogo;
        static Bitmap PrefixiLogo = new Bitmap(_PrefixiLogo);
        [ManifestResourceStream(ResourceName = "PerfexiOS.Desktop.PerfexiAPI.Resources.Applogo.bmp")]
        static byte[] _AppLogo;
        static Bitmap AppLogo = new Bitmap(_AppLogo);
        static bool IsStartOpen = false;
        static MouseState PrevMState = MouseState.None;

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
                    FontManager.RegisterFont(fontname, new(File.ReadAllBytes($@"{drive}{path}")));
                    System.Console.WriteLine($"Registered {item.name}");

                }
				Globals.GUI = true;
				Globals.Canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode (800,600,ColorDepth.ColorDepth32));
                MouseManager.ScreenWidth = Globals.Canvas.Mode.Width;
                MouseManager.ScreenHeight = Globals.Canvas.Mode.Height;
                Globals.Canvas.Clear(Color.DodgerBlue);
                Globals.WM = new WindowManager();
                Widgets.Pointer.Initalise();
                ProcessManager.RegisterProcess(Globals.WM);
				Globals.Canvas.Display();
				Globals.WM.start();

                var win = new FpsWindow();
                Globals.WM.AppendWindow(win.MainWindow);
                win.start();
                
			}
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }

        

    }
}
