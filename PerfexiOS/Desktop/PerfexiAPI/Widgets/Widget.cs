using PerfexiOS.Data;
using PerfexiOS.Data.Signal;

using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System.Collections.Generic;
using System.Drawing;

namespace PerfexiOS.Desktop.PerfexiAPI.Widgets
{
	public interface Widget
    {
        public List<Widget> Children { get; set; }
        public Window RootWindow { get; set; }


        public bool MouseEntered { get; set; }
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

        /// <summary>
        /// Called When the Mouse Enters the Widgets Boundry box
        /// </summary>
        public Signal<MouseArgs> OnMouseHover { get; set; }

        /// <summary>
        /// Calls when the Mouse Exits the Widgets Boundry box.
        /// </summary>
        /// 

        public Signal<MouseArgs> OnMouseLeave { get; set; }
        /// <summary>
        /// Calls when the mouse moves inside the boundry box
        /// </summary>
        public Signal<MouseArgs> OnMouseDrag { get; set; }
        /// <summary>
        /// Calls when a key is pressed with keyboard activated widgets Ex: Textboxes.
        /// </summary>
        /// 

        public Rectangle Mask { get; set; }
        public Signal<KeyboardArgs> OnKeyTyped { get; set; }
        /// <summary>
        /// Calls when the mouse is clicked inside the widgets boundry box 
        /// </summary>
        public Signal<MouseArgs> OnMouseClick { get; set; }
    }
}
