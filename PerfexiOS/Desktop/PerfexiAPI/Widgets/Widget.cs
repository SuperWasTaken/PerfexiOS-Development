using PerfexiOS.Data;
using PerfexiOS.Data.Signal;

using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System.Collections.Generic;

namespace PerfexiOS.Desktop.PerfexiAPI.Widgets
{
	public abstract class Widget
    {
        public List<Widget> Children = new();
        public Window RootWindow;
       
        public int x, y;
        
        public Widget(Window RootWindow,int x,int y)
        {
            this.x = x;
            this.y = y;
          
        }

        public virtual void update()
        {
            foreach(var item in Children)
            {
                item.update();
            }
        }

        public abstract void render();

        public virtual void HandleMouseEnter() { }

        public virtual void HandleMouseLeave() { }

        public virtual void HandleKeyTyped() { }

        public virtual void HandleKeyPressed() { }


        public Signal<MouseArgs> OnMouseHover;
        public Signal<MouseArgs> OnMouseLeave;
        public Signal<MouseArgs> OnMouseDrag;
        public Signal<MouseArgs> OnKeyTyped;
        public Signal<MouseArgs> OnMouseClick;
    }
}
