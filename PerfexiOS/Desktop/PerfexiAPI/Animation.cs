using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;

namespace PerfexiOS.Desktop.PerfexiAPI
{
	public class Animation
	{
		public Bitmap[] Frames { get; set; }
		public int Fps { get; set; }
		private DateTime Updatetime;
		public Bitmap CurrentFrame;
		private int currentframeidex;
		private int FrameDelay;
		public int x, y;
		public bool loop { get; set; }
		Pannel Parent;
		public bool playing { get; set; } = true;
		public Animation(Pannel parent,int x,int y,int w,int h,Bitmap[] Frames,int FPS,bool loop)
		{
			this.Frames = Frames;
			this.Fps = FPS;
			this.loop = loop;
			this.x = x;
			this.y = y;
			
			
		}

		public void play()
		{
			playing = true;
			Update();
		}
		private void Update()
		{
			if(!playing) { return; }
			FrameDelay = FPS.Fps / Fps;
			Updatetime = DateTime.Now.AddMilliseconds(FrameDelay * 1000);
			UpdateFrame();
		}

		
		private void UpdateFrame()
		{

			CurrentFrame = Frames[currentframeidex];
			if(Updatetime >= DateTime.Now)
			{
				if(currentframeidex+1 > Frames.Length) 
				{ 
					currentframeidex = 0; 
					if(!loop) { return; }
					CurrentFrame = Frames[currentframeidex];
					Update();
				} 
				else
				{
					currentframeidex++;
					CurrentFrame = Frames[currentframeidex];
					Update();
				}
				
			}
		}
		public void Draw()
		{
			Parent.DrawBitmap(CurrentFrame, x, y);
		}
		public void Stop()
		{
			playing = false;
		}
		
	}
}
