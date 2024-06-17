using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics
{
    public class Clear :Command
    {
        public Clear() : base("clear","clears the terminal")
        {

        }
		public override string[] Parse(GearSh parent, string[] args)
		{
			parent.Clear();
			return new string[] { };
		
		}
	}
}
