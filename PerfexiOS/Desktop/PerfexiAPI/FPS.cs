using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI
{
    public static class FPS
    {
        private static int Frames = 0;
        public static int Fps = 0;
        private static int Currentsecond = DateTime.Now.Second;
        private static int LastSecond;
        public static void CountFPS()
        {
            Frames++;
            Currentsecond = DateTime.Now.Second;
            if(Currentsecond != LastSecond)
            {
                Fps = Frames;
                Frames = 0;
                LastSecond = Currentsecond;
            }
        }
    }
}
