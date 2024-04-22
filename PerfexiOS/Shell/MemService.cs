using Cosmos.Core.Memory;
using PerfexiOS.Shell.TaskManager;
using PINI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell
{
    /// <summary>
    /// This Collects the heap with a slight delay for Performance 
    /// </summary>
    public  class Memservice : process
    {
       
        public int ticks = 0;

		public Memservice() : base("MemService")
        {

        }
		public override void loop()
		{
			try
            {
                Tick();
            }
            catch (Exception ex)
            {
                Kernel.Crash(ex);
            }
		}
		private void Tick()
        {
            ticks++;
            if(ticks >= 20)
            {
                ticks = 0;
                Heap.Collect();
            }
        }
       
    }
}
