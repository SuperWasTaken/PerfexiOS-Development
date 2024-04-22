using Cosmos.System;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands
{ 
    public abstract class Command : process
    {
        public string name { get; set; }
        public string description { get; set; }



        public Command(string name, string description) : base(name)
        {
            this.name = name;
            this.description = description;
        }
        public virtual string[] Execute(commandManager parent,string[] args)
        {
           
            
            return args;
        }

        

		
	}
}
