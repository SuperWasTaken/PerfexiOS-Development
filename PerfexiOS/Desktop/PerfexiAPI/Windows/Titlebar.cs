using Cosmos.Core.Memory;
using Cosmos.Core_Plugs.System;
using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using CosmosTTF;
using PerfexiOS.Data;
using PerfexiOS.Data.Signal;

using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
    public class Titlebar
    {
        Window parent;
        private bool MouseEntered = false;
        public Rectangle Collision;
        public Titlebar(Window parent) 
        {
            this.parent = parent;
            OnMouseEnter.Bind(HandleMouseEnter);
            OnCLicked.Bind(HandleMouseInput);
            OnMouseEnter.Bind(HandleMouseEnter);
            Collision = new(parent.x, parent.y - 32, parent.w, 32);
        }

        /// <summary>
        /// You can Cutomize how the titlebar draws itself in this void
        /// But by default it draws a filled solid blue rectangle with
        /// It's title in Comic Sans Because Yes
        /// </summary>
        public virtual void Draw()
        {
            Globals.Canvas.DrawFilledRectangle(Color.Blue, parent.x, parent.y - 32, parent.w, 32);
            Globals.Canvas.DrawString(parent.title, PCScreenFont.Default, Color.Black, parent.x+10, parent.y-16);           
        }
        /// <summary>
        ///
        /// </summary>
       
        public void HandleMouseInput(SignalArgs args)
        {
			parent.OnMove.Fire(new());
		}
        

       
        public void HandleMouseEnter(MouseArgs args)
        {
            Pointer.State = Pointer.UiMouseStates.Clickable;
            MouseEntered = true;
        }

        public void HandleMouseLeave(MouseArgs args)
        {
            Pointer.State = Pointer.UiMouseStates.Normal;
            MouseEntered = false;
        }
        public Signal<MouseArgs> OnCLicked = new();

        public Signal<MouseArgs> OnMouseEnter = new();

        public Signal<MouseArgs> OnMouseLeave = new();

       
    }

}
