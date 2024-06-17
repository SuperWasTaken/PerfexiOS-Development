using Cosmos.Core_Plugs;
using PerfexiOS.Shell.Commands.Topics;
using PerfexiOS.Shell.Commands.Topics.FileSystem;
using PerfexiOS.Shell.Commands.Topics.Power;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PerfexiOS.Shell.Commands
{
    /// <summary>
    /// The Main Command Shell Interface for Perfexi OS 
    /// 
    /// </summary>
    public class GearSh 
    {

        public ConsoleColor Forground;
        public ConsoleColor Background;
        public Queue<Cosmos.System.KeyEvent> KeyBuffer = new();
        private Command Job;
        public Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        public string workingdir;
        public string previousdir { get; set; } = Globals.RootPath;
        public Command[] CurrentJobs = null;
        public bool ParsingCommand = false;
        public int CX, CY;
        public int Rows, Cols;

        public GearSh(string workingdir)
        {
            this.workingdir = workingdir;
            RegisterSystem();
            Prompt();
        }

        public Action<int, int> SetCursor { get; set; } = (cx, cy) =>
        {
            Console.SetCursorPosition(cx, cy);
        };

        public Action<ConsoleColor> SetBackgroundColor { get; set; } = (Color) =>
        {
            Console.BackgroundColor = Color;
        };

        public Action<ConsoleColor> SetForegroundColor { get; set; } = (Color) =>
        {
            Console.ForegroundColor = Color;
        };

        public Action<string> SendLine { get; set; } = (line) =>
        {
            Console.WriteLine(line);
        };

        public Action<string> Send { get; set; } = (text) =>
        {
            Console.WriteLine(text);
        };

        
        public void RegisterCommand(Command command)
        {
            Commands.Add(command.name, command);
        }
        public void UnregisterCommand(Command command)
        {
            Commands.Remove(command.name);
        }
        public void RegisterSystem()
        {
            var ls = new ls();
            RegisterCommand(ls);
            var cd = new cd();
            RegisterCommand(cd);
            var diskpart = new Diskpart();
            RegisterCommand(diskpart);
            var rm = new rm();
            RegisterCommand(rm);
            var reboot = new reboot();
            RegisterCommand(reboot);
            var shutdown = new shutdown();
            RegisterCommand(shutdown);
            var clear = new Clear();
            RegisterCommand(clear);
            var startgui = new StartGUI();
            RegisterCommand(startgui);
            var SetRoot = new SetRoot();
            RegisterCommand(SetRoot);
            var SetLayout = new SetLayout();
            RegisterCommand(SetLayout);
            var touch = new Touch();
            RegisterCommand(touch);
            var mdir = new Mkdir();
            RegisterCommand(mdir);
            var SetConf = new SetConfig();
            RegisterCommand(SetConf);
            var PrintText = new PrintText();
            RegisterCommand(PrintText);
            var pini = new piniread();
            RegisterCommand(pini);
        }

        

        public void DeregisterAll()
        {
            foreach(var item in Commands.Values)
            {
                UnregisterCommand(item);
            }
        }
        public virtual void Prompt()
        {
            Console.Write(workingdir + "@PERFEXI:READY>");
        }
        public void parse(string input)
        {




            var splitted = input.Split(' ');
            var commandname = splitted[0];
            commandname = commandname.Trim();
            var args = splitted.Skip(1).ToArray();

            if(commandname == "printreg")
            {
                Send("Registered Keywords");
                foreach(var item in Commands.Keys)
                {
                    Send(item);
                }
                return;
            }
            if(commandname == "ver")
            {
                
                Send($"You are Running PerfexiOS {Globals.Version} ");
            }

            if(!ParsingCommand)
            {
				if(Commands.TryGetValue(commandname, out var command))
                {
					command.Parse(this, args);
                    
				}
                else
                {
					Send("Command Dosen't Exist type in 'printreg' for help");
				}

			}
        }
            
            
        

        public Action Clear { get; set; } = () => { Console.Clear(); };

        public virtual string Ask(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            return input;
        }
    
       
        /// <summary>
        /// This allows commands to print to the shell 
        /// 
        /// By Default it writes to the system console 
        /// But for Gui Terminals you can delgate this void to 
        /// Writing lines on the console
        /// </summary>
    }
}
