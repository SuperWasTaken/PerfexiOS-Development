using Cosmos.Core;
using Cosmos.Core.Memory;
using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using PerfexiOS.Data;
using PerfexiOS.Data.Signal;
using PerfexiOS.Desktop.PerfexiAPI.Geomitry;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using PerfexiOS.Shell.TaskManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Windows
{
    
    public class Window 
    {
        /// <summary>
        /// This will show the amount of ram this window is using 
        /// </summary>
        
        /// <summary>
        /// This will get the load this instance is causing to the CPU
        /// </summary>
        /// 
        
        public bool Focused { get; set; }
        public bool HasTitlebar { get; set; } = true;
        public string title { get; set; }


        public UiApplication Parent{ get; set; }

        /// <summary>
        /// This checks if the mouse has previously entered the window Already 
        /// So it dosen't constatnly fire the onmouseleave signal 
        /// </summary>
        public bool MouseEntered { get; set; } = false;
        public bool CloseButton { get; set; } = true;
        public bool MinimiseButton { get; set; } = true;

        public Rectangle FullMask;
        
       

        public Pannel Canvas;
        public List<Widget> Widgets = new List<Widget>();
        
        public int x, y,w,h;
       
        public Titlebar Titlebar;


        public bool MouseIntersecting
        {
            get
            {
                // This
                return MousePoint.Rect(FullMask);
            }
        }

        public Window(UiApplication Parent,int x, int y,int width,int height,string title,bool titlebar = false)
        {
            this.Parent = Parent;
            this.x = x;
            this.y = y;
            this.w = width;
            this.h = height;
            this.HasTitlebar = titlebar;
            this.title = title;
			var maskx = x;
			var masky = y;
			var maskw = w;
			var maskh = h;
			if (titlebar)
            {
                this.Titlebar = new(this);

                masky = y - 32;
                maskh = h + 32;

            }
            Canvas = new(this, this.x, this.y, this.w, this.h);

            FullMask = new(x,masky, w, maskh);

            OnMouseClicked.Bind(HandleMouseClicked);
            OnMouseEnter.Bind(HanldeMouseEnter);
            OnMouseLeave.Bind(HandleMouseLeave);
            OnMove.Bind(HandleMove);
            OnFocus.Bind(HanldeFocus);
            OnDefocus.Bind(HanldeDefocus);
            OnKeyTyped.Bind(HandleKeyTyped);
            OnMouseMove.Bind(HandleMouseMoving);
        }

       
        public virtual void Render()
        {
            try
            {
				foreach (var item in Widgets)
				{
					item.render();
				}
				if (HasTitlebar)
				{
					// Render the titlebar
					Titlebar.Draw();

				}
				// Render the Pannel 
				Canvas.render();
			}
            catch
            {
                
            }
			
            
            
        }
            
     
        private void HandleMove(SignalArgs args)
        {
            // While the mouse is moving 
            if(MouseManager.MouseState == MouseState.Left )
            {
				Pointer.State = Pointer.UiMouseStates.Move;

                int newx;
                int newy;

                // Calculate the new x and y values of the window 

                // Calculate newx 

                newx = this.x + MouseManager.DeltaX;
                newy = this.FullMask.Y + (int)MouseManager.DeltaY;


     
                // reset the collision mask when it
                this.FullMask.Location = new(newx, newy);
				// Set the location of the windows canvas 
				Canvas.x = this.FullMask.X;
				Canvas.y = this.FullMask.Y;
                this.x = newx;
                
               
				if (HasTitlebar)
				{
                    Titlebar.Collision.Location = this.FullMask.Location;
                    this.y = FullMask.Y + Titlebar.Collision.Height;
				}
                else
                {
                    this.y = newy;
                }
			}
            else
            {
                // Return the 
                Pointer.State = Pointer.UiMouseStates.Normal;
                return; 
            }
        }
        /// <summary>
        /// Gets the Widget that the mouse is intersecting 
        /// </summary>
        /// <returns></returns>
        /// 
        private void HandleKeyTyped(KeyboardArgs args)
        {
            foreach(var item in Widgets) { item.OnKeyTyped.Fire(new(args.Key, args.Character, args.Modifiers)); }
        }
        private void HandleMouseClicked(MouseArgs args)
        {
            
            // Check if the mouse has clicked on the titlebar if it has then Fire the titlebars onclicked event 
            if(MousePoint.Rect(Titlebar.Collision)) { Titlebar.OnCLicked.Fire(args); return; }

            // Send the Mouse Signal to the appropriate Widget 
            var w = GetIntersectingWidget();
            if(w != null) { w.OnMouseClick.Fire(args); }
            
            

        } 
        private void HanldeMouseEnter(MouseArgs args)
        {
            MouseEntered = true;
		}
        private void HandleMouseLeave(MouseArgs args)
        {
            // Resets the Pointer
            Pointer.State = Pointer.UiMouseStates.Normal;
            MouseEntered = false;
            
        }
       


        private void HanldeDefocus(SignalArgs args)
        {
            Focused = false;
            
        }
        private void HanldeFocus(SignalArgs args)
        {
            Focused = true;
            
        }

        /// <summary>
        /// This method is used for when the mouse moved on the window
        /// </summary>
        private void HandleMouseMoving(MouseArgs args)
        {
            var wid = GetIntersectingWidget();
            foreach(var item in Widgets)
            {
                if(item.MouseEntered && item != wid)
                {
                    item.MouseEntered = false;
                    item.OnMouseLeave.Fire(args);
                }

            }
            if(!wid.MouseEntered)
            {
                wid.MouseEntered = true;
                wid.OnMouseHover.Fire(args);
            }
            else
            {
                wid.OnMouseDrag.Fire(args);
            }
        }
        private Widget GetIntersectingWidget()
        {
            Widget Cache = null;
            foreach(var item in Widgets)
            {
                // if the item has no collision mask return 
                if(item.Mask.Contains( (int)MouseManager.X, (int)MouseManager.Y))
                {
                    Cache = item;
                }
            }
            return Cache;
        }




        public Signal<KeyboardArgs> OnKeyTyped = new();
        public Signal<MouseArgs> OnMouseClicked = new();
        public Signal<SignalArgs> OnFocus = new();
        public Signal<SignalArgs> OnDefocus = new();
        public Signal<SignalArgs> OnClose = new();
        public Signal<SignalArgs> OnDoubleClick = new();
        public Signal<SignalArgs> OnMove = new();
        public Signal<MouseArgs> OnMouseEnter = new();
        public Signal<MouseArgs> OnMouseLeave = new();
        public Signal<MouseArgs> OnMouseMove = new();
    }
}
