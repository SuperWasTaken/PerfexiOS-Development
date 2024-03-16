using PerfexiOS.Shell.SystemTerminalAPPS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.ConsoleWidgets
{
    class objectbox : ConsoleWidget
    {
        Color background { get; set; } = Color.White;
        public objectbox(int x,int y,int w,int h) : base(x,y,w,h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public override void Print()
        {
            base.Print();
            Console.SetCursorPosition(this.x,this.y);
            for(int i = 0; i < this.w; i++)
            {

            }
        }


    }
}
