
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using PerfexiOS.Data.PINI;
using System.IO;
using System.Threading;
using Cosmos.HAL;
using PerfexiOS.Shell;
using PerfexiOS.Shell.Commands;

using Cosmos.System;
using Cosmos.System.ScanMaps;
using Cosmos.System.Graphics;

using PerfexiOS.Shell.TaskManager;
using PerfexiOS.Desktop;
using COROS;
using PerfexiOS.Desktop.PerfexiAPI;
using PINI;
using Cosmos.HAL.BlockDevice.Ports;

namespace PerfexiOS.Data.PINI
{
    public class Kernel : Sys.Kernel
    {
        public const string version = "HardChair2";
        
        bool listeningMode = true;   // Works only 
        private static commandManager cm;
        public static USStandardLayout us;
        public static US_Dvorak Dvorak;
        public static Cosmos.System.ScanMaps.TRStandardLayout TRS;
        public static Cosmos.System.ScanMaps.GBStandardLayout GB;
        public static Cosmos.System.ScanMaps.FRStandardLayout FR;
        public static Cosmos.System.ScanMaps.ESStandardLayout ES;

        protected override void BeforeRun()     
        {

            try
            {


                Globals.Version = version;
                System.Console.WriteLine($"Perfexi OS {version}");
                System.Console.WriteLine("Initalising filesystem....");
				var fs = new CosmosVFS();
				VFSManager.RegisterVFS(fs);
				System.Console.WriteLine("Filesystem Initalised...");
                System.Console.WriteLine("Looking For System Drive...");
                var partitons = VFSManager.GetVolumes();
                    
                var foundrootdrive = false;
                for (int i = 0; i < partitons.Count; i++)
                {
                    var p = partitons[i];
					if (File.Exists($@"{p.mName}PerfexiOS\Sys.pini"))
					{
						Globals.SystemDrive = i;
                        Globals.RootPath = p.mFullPath;
						System.Console.WriteLine($"Found Root Path as {p.mFullPath} ");
                        foundrootdrive = true;
                        break;
					}
                }
                if (!foundrootdrive)
                {
                    System.Console.WriteLine("Failed to find Root Drive... Gui has been disabled\n due to the innability to acess Sys.pini");
                  
                   
                    KeyboardManager.SetKeyLayout(us);
                    System.Console.WriteLine("Keyboard layout set to US by default ");
                    System.Console.WriteLine("Run SetLayout <layout> to change it");
                    System.Console.WriteLine("Root Drive Number set to 0 by default");
                    System.Console.WriteLine("Run SetRoot <drive> to change the root drive");
					System.Console.WriteLine("Starting Command Manager");
					cm = new($@"{Globals.RootPath}");
					foreach (var item in cm.commands)
					{
						System.Console.WriteLine($"Sucessfully Registered {item.name}");
					}
					System.Console.WriteLine("Prompting...");
                 
				}
                else
                {
                    
					System.Console.WriteLine("Loading System Pini...");
					Globals.Conf = new($@"{Globals.RootPath}PerfexiOS\Sys.pini");
                    var reader = new PiniReader(Globals.Conf);
					var bootkey = reader.GetSection("/BOOT/").GetKey("BOOTMODE");
					System.Console.WriteLine($"Bootmoode: {bootkey.value}");
					System.Console.WriteLine("Checking Keyboard Layout...");
					var layout = reader.GetSection("/CONF/").GetKey("LAYOUT");
					switch (layout.value)
					{
						case "US":

							KeyboardManager.SetKeyLayout(us);
							System.Console.WriteLine("Layout set to US qwerty");
							break;
						case "USDV":
							KeyboardManager.SetKeyLayout(Dvorak);
							System.Console.WriteLine("Layout set to Dvorak");
							break;
						case "ES":
							KeyboardManager.SetKeyLayout(ES);
							System.Console.WriteLine("Layout set to Spanish");
							break;
						case "FR":
							KeyboardManager.SetKeyLayout(FR);
							System.Console.WriteLine("Layout set to French");
							break;
						case "GB":
							KeyboardManager.SetKeyLayout(GB);
							System.Console.WriteLine("Layout set to UK standard");
							break;
						case "TRS":
							KeyboardManager.SetKeyLayout(TRS);
							System.Console.WriteLine("Layout set to Turkish");
							break;
						default:
							KeyboardManager.SetKeyLayout(us);
							System.Console.WriteLine("Failed to read the keyboard layout defaulted to US qwerty");
							break;

					}


					switch (bootkey.value)
					{
						case "TER":
							System.Console.WriteLine("Starting Command Manager");
							cm = new($@"{Globals.SystemDrive}:\");
							foreach (var item in cm.commands)
							{
								System.Console.WriteLine($"Sucessfully Registered {item.name}");
							}
							System.Console.WriteLine("Prompting...");
							break;
						case "GUI":
							GUI.Initalise();
							break;
						default:
							System.Console.WriteLine("Bootmode Key was invalid check the SYS pini");
							System.Console.WriteLine("Starting Terminal....");
							System.Console.WriteLine("Starting Command Manager");
							cm = new($@"{Globals.SystemDrive}:\");
							foreach (var item in cm.commands)
							{
								System.Console.WriteLine($"Sucessfully Registered {item.name}");
							}
							System.Console.WriteLine("Prompting...");
							break;
					}
				}
                
            }
            catch (Exception ex)
            {
                Crash(ex);
            }
        }

        protected override void Run()
        {
            try
            {
                Memservice.Tick();
                if(!Globals.GUI && listeningMode)
                {
                    System.Console.Write($@"{cm.workingdir} @PERFEXI:READY>");
                    var input = System.Console.ReadLine();
                    foreach(var item in cm.parse(input))
                    {
                        System.Console.WriteLine(item);
                    }
                }

                if(Globals.GUI)
                {
                    
                    ProcessManager.Yeild();
                    FPS.CountFPS();
                }
            }
            catch(Exception ex)
            {
                Crash(ex);
            }
        
            
        }

        public static void Crash(Exception e)
        {
            if(FullScreenCanvas.IsInUse) { Globals.Canvas.Disable(); }
            System.Console.BackgroundColor = System.ConsoleColor.DarkRed;
            System.Console.Clear();
            System.Console.WriteLine("A Problom was detected and PerfexiOS had to shutdown to prevent damage to your system");
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine("Shutting down in 10 Seconds...");
            Thread.Sleep(10000);
            Sys.Power.Shutdown();
          

        }
        /// <summary>
        /// If Sys.pini hasan't been found on boot a command will execute this to reload Sys.pini 
        /// 
        /// </summary>
        /// <param name="path"></param>
       
    }
}
