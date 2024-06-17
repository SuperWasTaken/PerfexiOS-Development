
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using PerfexiOS.Data;
using System.IO;

using Cosmos.HAL;
using PerfexiOS.Shell;
using PerfexiOS.Shell.Commands;

using Cosmos.System;
using Cosmos.System.ScanMaps;
using Cosmos.System.Graphics;
using Cosmos.HAL.Drivers;
using COROS;

using PINI;
using Cosmos.HAL.BlockDevice.Ports;
using Cosmos.HAL.Drivers.USB;
using Cosmos.Core;
using System.Threading;

using Cosmos.System.FileSystem.ISO9660;
using System.Linq;
using Cosmos.Core.Multiboot;
using System.Transactions;

using PerfexiOS.PerfexiAPI;
using PerfexiOS.PerfexiAPI.WindowManager;


namespace PerfexiOS
{
    public class Kernel : Sys.Kernel
    {
        public const string version = "HardChair2";
        
		bool listeningMode = true;   // Works only 
        private static GearSh Shell;
       
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
                // Debug
                
                

                Globals.Version = version;
                System.Console.WriteLine($"Perfexi OS {version}");
                System.Console.WriteLine("Initalising filesystem....");
                var fs = new CosmosVFS();
                VFSManager.RegisterVFS(fs);
                Globals.fs = fs;
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
                    Shell = new(@"0:\");

					System.Console.WriteLine("Prompting...");
                 
				}
                else
                {
                  
					System.Console.WriteLine("Loading System Pini...");
                    Globals.Conf = new(File.ReadAllText($@"{Globals.RootPath}PerfexiOS\Sys.pini").Split('\n'));

                    Globals.Conf.GetSection("BOOT", out var boot);
                    boot.GetKey("BOOTMODE", out var bootkey);
					System.Console.WriteLine($"Bootmoode: {bootkey.value}");
					System.Console.WriteLine("Checking Keyboard Layout...");
                    Globals.Conf.GetSection("CONF", out var conf);
                    conf.GetKey("LAYOUT", out var layout);

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
							System.Console.WriteLine("Starting Gearsh");
                            Shell = new(Globals.RootPath);
							System.Console.WriteLine("Prompting...");
                            Shell.Prompt();
							break;
						case "GUI":
                            System.Console.WriteLine("GUI not yet Supported");
							System.Console.WriteLine("Starting Gearsh");
							Shell = new(Globals.RootPath);
							System.Console.WriteLine("Prompting...");
							break;
						default:
							System.Console.WriteLine("Bootmode Key was invalid check the SYS pini");
							System.Console.WriteLine("Starting Gearsh");
							System.Console.WriteLine("Starting Command Manager");
							Shell = new($@"{Globals.SystemDrive}:\");
							foreach (var item in Shell.Commands.Keys)
							{
								System.Console.WriteLine($"Sucessfully Registered {item}");
							}
                            
							System.Console.WriteLine("Prompting...");
                            Shell.Prompt();
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
               


                if(listeningMode  && !Globals.GUI)
                {
                    
					var input = System.Console.ReadLine();
					Shell.parse(input);
                    Shell.Prompt();
                    
				}
                else
                {
                    if(KeyboardManager.TryReadKey(out var k))
                    {
                        Globals.KeyPresses.Enqueue(k);
                    }
                    WindowManager.Update();
                }
                
            }
            catch(Exception ex)
            {
                Crash(ex);
            }
        
            
        }

        public static void Crash(Exception e)
        {
            if(FullScreenCanvas.IsInUse) { Globals.Canvas.Disable(); Globals.GUI = false; }
            
            System.Console.BackgroundColor = System.ConsoleColor.DarkRed;
            System.Console.Clear();
            System.Console.WriteLine("A Problom was detected and PerfexiOS had to shutdown to prevent damage to your system");
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine("Shutting down in 10 seconds");
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
