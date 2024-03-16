using Cosmos.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell
{
    public static class Memservice
    {
        private static int ticks = 0;

        public static void Tick()
        {
            if(ticks >=  5)
            {
                ticks = 0;
                Heap.Collect();
                
            }
           
        }
        

       
    }
}
