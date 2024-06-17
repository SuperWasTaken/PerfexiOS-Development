using Cosmos.System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands
{ 
    public abstract class Command 
    {
        public string name { get; set; }
        public string description { get; set; }

        public GearSh parentshell;


        public Command(string name, string description) 
        {
            this.name = name;
            this.description = description;
            
        }

        /// <summary>
        /// Loops the Command 
        /// </summary>
        

        /// <summary>
        /// Runs ONCE when the command is parsed in the GearshShell
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="args"></param>
        public abstract string[] Parse(GearSh parent, string[] args);
        	
	}
}
