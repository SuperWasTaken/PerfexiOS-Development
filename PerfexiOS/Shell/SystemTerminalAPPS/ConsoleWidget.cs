using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.SystemTerminalAPPS
{
    public abstract class ConsoleWidget
    {
        public int x , y;
        public int w,h;
        public List<ConsoleWidget> children;
        ConsoleWidget parent;
        public ConsoleWidget(ConsoleWidget parent,int x,int y,int w,int h)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public ConsoleWidget(int x, int y, int w, int h)
        {
            
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public virtual void Print()
        {

        }

    }
}
