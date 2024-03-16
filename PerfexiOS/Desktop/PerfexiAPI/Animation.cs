using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI
{
	public class Animation
	{
		public List<Bitmap> Frames { get; set; }
		public int Fps { get; set; }
		private DateTime Updatetime;
		public Bitmap CurrentFrame;
		private int currentframeidex;
		private int FrameDelay;
		public bool loop { get; set; }

		public bool playing { get; set; } = true;
		public Animation(List<Bitmap> Frames,int FPS,bool loop)
		{
			this.Frames = Frames;
			this.Fps = FPS;
			this.loop = loop;
			
		}

		public void play()
		{
			playing = true;
			Update();
		}
		private void Update()
		{
			FrameDelay = FPS.Fps / Fps;
			Updatetime = DateTime.Now.AddMilliseconds(FrameDelay * 1000);
			UpdateFrame();
		}

		
		private void UpdateFrame()
		{

			CurrentFrame = Frames[currentframeidex];
			if(Updatetime >= DateTime.Now)
			{
				if(currentframeidex+1 > Frames.Count) 
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
		public void Stop()
		{
			playing = false;
		}
		
	}
}
