using Cosmos.System.Graphics;
using PINI;
using COROS;
using CosmosTTF;
using System.IO;
using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.HAL.Drivers.Video;
using PerfexiOS.PerfexiAPI;
using System.Collections.Generic;
using Cosmos.System;


namespace PerfexiOS
{
    public static class Globals
    {


        public static CosmosVFS fs; 
      
        public static Canvas Canvas;
        public static TTFFont DefaultFont { get; set; } 
        public static AudioManager AudioManager { get; set; }

        public static bool UseAlpha { get; set; } = true;
       
        public static AudioMixer AudioMixer { get; set; }
        
        public static AudioDriver AudioDriver { get; set; }
        public static Queue<KeyEvent> KeyPresses = new();
        public static COROS.AHCI_DISK SataDriver { get; set; }
        public static int SystemDrive { get; set; }
        public static Pini Conf { get; set; } = null;

        public static string Version { get; set; }
        public static string RootPath { get; set; } 
        public static bool Installer { get; set;} = false;

        public static bool GUI { get; set; } = false;

        public static VGADriver _VgaDriver { get; set; }

        public static VBEDriver _Vbe { get; set; }

        
       
    }
}
