using Cosmos.System;

using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
    
    public class Window 
    {
        
        
        public bool Focused { get; set; }
        public bool HasTitlebar { get; set; } = true;
        public string title { get; set; }

        public bool CloseButton { get; set; } = true;
        public bool MinimiseButton { get; set; } = true;



        public Pannel Canvas;
        public List<Widget> Widgets = new List<Widget>();
        
        public int x, y,w,h;
       
        public Titlebar Titlebar;

        
        public Window(int x, int y,int width,int height,string title,bool titlebar = true)
        {
            this.x = x;
            this.y = y;
            this.w = width;
            this.h = height;
            this.HasTitlebar = titlebar;
            this.title = title;
            if(titlebar)
            {
                this.Titlebar = new(this);
            }
            Canvas = new(this, this.x, this.y, this.w, this.h);
            
        }

        public virtual void Update()
        {
            foreach(var item in Widgets)
            {
                item.update();
            }

            
        }

        public virtual void Render()
        {
            Canvas.render();
            foreach(var item in Widgets)
            {
                item.render();
            }
            if(HasTitlebar)
            {
                Titlebar.Draw();
            }
        }
            
        
    }
}
