
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
                    var d = new FontManager.fontdata(new(File.ReadAllBytes($@"{drive}{path}")), fontname);
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

                var win = new Window(100, 100, 300, 400, "4", true);
                win.Canvas.Clear(Color.White);
                win.Canvas.DrawLine(10, 10, 100, 120, Color.Pink);
                win.Canvas.DrawFilledCircle(Color.HotPink, 10, 100, 20);
                win.Canvas.DrawString(10, 90, "ComicSans", "Hello!", 20, Color.Yellow);
                Globals.WM.AppendWindow(win);
                while (true)
                {
                    Update();
                }

			}
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }

        public static void Update()
        {
            Globals.Canvas.Clear(Color.DodgerBlue);

            Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 217, 217, 217), 0, 560, 800, 40);
            Globals.Canvas.DrawImageA(PrefixiLogo, 2, 562);
            if (new Rectangle((int)MouseManager.X, (int)MouseManager.Y, 10, 10).IntersectsWith(new Rectangle(2,562,36,36)))
            {

                if (PrevMState == MouseState.None && MouseManager.MouseState == MouseState.Left)
                {
                    PrevMState = MouseState.Left;
                }
                else if (PrevMState == MouseState.Left && MouseManager.MouseState == MouseState.None)
                {
                    PrevMState = MouseState.None;
                    IsStartOpen = !IsStartOpen;
                }

            }
            //Globals.Canvas.DrawImageA(AppLogo, 200, 564);
            //Globals.Canvas.DrawImageA(AppLogo, 240, 564);
            //Globals.Canvas.DrawImageA(AppLogo, 280, 564);
            //Globals.Canvas.DrawImageA(AppLogo, 320, 564);
            //Globals.Canvas.DrawImageA(AppLogo, 360, 564);
            //Globals.Canvas.DrawImageA(AppLogo, 400, 564);
            //Globals.Canvas.DrawImageA(AppLogo, 440, 564);
            Globals.Canvas.DrawString(DateTime.Now.ToString("hh:mm"), PCScreenFont.Default, Color.FromArgb(0, 0, 0), 742, 569);
            Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 255, 255, 255), 45, 568, 148, 23);
            Globals.Canvas.DrawRectangle(Color.FromArgb(255, 0, 0, 0), 45, 568, 148, 23);
            Globals.Canvas.DrawString("talk to prefexi", PCScreenFont.Default, Color.FromArgb(0, 0, 0), 47, 569);
            Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 217, 217, 217), 561, 9, 230, 257);
            Globals.Canvas.DrawRectangle(Color.FromArgb(255, 0, 0, 0), 561, 9, 230, 257);
            Globals.Canvas.DrawString("prefexi stats", PCScreenFont.Default, Color.FromArgb(0, 0, 0), 563, 11);
            Globals.Canvas.DrawString("Hunger", PCScreenFont.Default, Color.FromArgb(0, 0, 0), 574, 33);
            Globals.Canvas.DrawString("Mood", PCScreenFont.Default, Color.FromArgb(0, 0, 0), 574, 98);
            Globals.Canvas.DrawString("Fun", PCScreenFont.Default, Color.FromArgb(0, 0, 0), 575, 163);
            Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 255, 255, 255), 574, 55, 208, 43);
            Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 255, 255, 255), 574, 120, 208, 43);
            Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 255, 255, 255), 575, 185, 208, 43);
            Globals.Canvas.DrawString("prefexi beta ui.", PCScreenFont.Default, Color.FromArgb(255, 255, 255), 671, 514);
            Globals.Canvas.DrawString("this ui is subject to change", PCScreenFont.Default, Color.FromArgb(255, 255, 255), 570, 536);
            if (IsStartOpen)
            {
                Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 217, 217, 217), 2, 140, 360, 418);
                //Globals.Canvas.DrawImageA(AppLogo, 242, 518);
                //Globals.Canvas.DrawImageA(AppLogo, 282, 518);
                //Globals.Canvas.DrawImageA(AppLogo, 322, 518);
                Globals.Canvas.DrawFilledRectangle(Color.FromArgb(255, 255, 255, 255), 9, 152, 224, 400);
            }

            Globals.Canvas.DrawFilledRectangle(Color.Black,(int)MouseManager.X,(int)MouseManager.Y,10,10);

            Globals.Canvas.Display();
            Heap.Collect();
        }

    }
}
