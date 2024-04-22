using Cosmos.System.Graphics;
using PerfexiOS.Data;
using PerfexiOS.Desktop.PerfexiAPI;
using PINI;
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COROS;
using CosmosTTF;
using System.IO;
using Cosmos.HAL.Drivers.Audio;
namespace PerfexiOS
{
    public static class Globals
    {
        public static CGSSurface CGSSurface { get; set; }
        public static TTFFont PointerFont { get; set; }
        public static AC97 AudioDriver { get; set; }
        public static TTFFont DefaultFont { get;  set; }
        public static COROS.AHCI_DISK SataDriver { get; set; }
        public static int SystemDrive { get; set; }
        public static pini Conf { get; set; } = null;

        public static WindowManager WM { get; set; }
        public static string Version { get; set; }
        public static string RootPath { get; set; } 
        public static Canvas Canvas { get; set; }
        public static bool Installer { get; set;} = false;

        public static bool GUI { get; set; } = false;

       
    }
}
