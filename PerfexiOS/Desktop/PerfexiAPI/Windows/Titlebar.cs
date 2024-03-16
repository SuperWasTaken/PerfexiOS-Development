using Cosmos.Core_Plugs.System;
using Cosmos.System;
using CosmosTTF;
using PerfexiOS.Data;
using PerfexiOS.Data.Signal;
using PerfexiOS.Desktop.PerfexiAPI.Collision;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
    public class Titlebar
    {
        Window parent;
        private bool MouseEntered = false;
       
        public Titlebar(Window parent) 
        {
            this.parent = parent;
            OnMouseEnter.Bind(HandleMouseEnter);
            OnCLicked.Bind(HandleMouseInput);
            OnMouseEnter.Bind(HandleMouseEnter);
        }

        /// <summary>
        /// You can Cutomize how the titlebar draws itself in this void
        /// But by default it draws a filled solid blue rectangle with
        /// It's title in Comic Sans Because Yes
        /// </summary>
        public virtual void Draw()
        {
            Globals.Canvas.DrawFilledRectangle(Color.Blue, parent.x, parent.y - 32, parent.w, 32);
           
        }
        /// <summary>
        ///
        /// </summary>
        public virtual void Update()
        {
            #region MouseCollision
            // Store a instance of MouseArgs and Check if the mouse is colliding and store them in values
            
            #endregion
        }
        public void HandleMouseInput(SignalArgs args)
        {            
            Drag();
        }


        public void Drag()
        {
            parent.x += MouseManager.DeltaX;
            parent.y += MouseManager.DeltaY;
        }

        public void HandleMouseEnter(MouseArgs args)
        {
            Pointer.State = Pointer.UiMouseStates.Clickable;
        }

        public void HandleMouseLeave(MouseArgs args)
        {
            Pointer.State = Pointer.UiMouseStates.Normal;
        }
        public Signal<MouseArgs> OnCLicked = new();

        public Signal<MouseArgs> OnMouseEnter = new();

        public Signal<MouseArgs> OnMouseLeave = new();

       
    }

}
