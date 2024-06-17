
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.SystemTerminalAPPS
{
    public class Diskpart
    {
        bool recivingvalue = false;
        List<Disk> disks = Globals.fs.GetDisks();
        int pagecount;
        int currentpage;
        public bool running = true;
        enum CurrentView
        {
            DiskSelector,
            Diskinfo,
            PartEditor,
            DiskFormater,

        }
        public Diskpart()
        {
            
            Console.BackgroundColor = ConsoleColor.White;
            pagecount = disks.Count;
            currentpage = disks.Count;
        }


       
        public void handlekeytyped()
        {
           
        }
       
        public void DrawDiskPage()
        {

        }

        
    }
}
