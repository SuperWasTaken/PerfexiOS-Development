using PerfexiOS.Shell.Commands.Topics;
using PerfexiOS.Shell.Commands.Topics.FileSystem;
using PerfexiOS.Shell.Commands.Topics.Power;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PerfexiOS.Shell.Commands
{
    public class commandManager
    {
        public List<Command> commands = new();
        public string workingdir;
        public string previousdir;
        public commandManager(string workingdir)
        {
            this.workingdir = workingdir;
            RegisterSystem();
        }

        public void RegisterCommand(Command command)
        {
            commands.Add(command);

        }
        public void UnregisterCommand(Command command)
        {
            if(commands.Contains(command))
            {
                commands.Remove(command);
            }
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
        }

        public void DeregisterAll()
        {
            foreach(var item in commands)
            {
                UnregisterCommand(item);
            }
        }

        public string[] parse(string args)
        {
            string[] splitted = args.Split(' ');
            string comandname = splitted[0].Trim();
            string[] commandargs = splitted.Skip(1).ToArray();
            
            foreach (var item in commands)
            {
                if(comandname == item.name)
                {
                    return item.Execute(this,commandargs);
                }
            }

            return new string[] { $"{args} is not a valid syntax " };
        }

        public string Ask(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            return input;
        }
        public void Clear()
        {
            Console.Clear();
        }
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }
}
