using Cosmos.Core.Memory;
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
    public static class Memservice 
    {
       
        public static int ticks = 0;

		
		
		public static void Tick()
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
