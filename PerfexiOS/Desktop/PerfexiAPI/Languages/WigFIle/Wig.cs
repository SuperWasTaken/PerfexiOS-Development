using Cosmos.HAL;
using PerfexiOS.Data;
using PerfexiOS.Data.Signal;
using PerfexiOS.Desktop.PerfexiAPI.Widgets;
using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.PerfexiAPI.Languages.WigFIle
{
	/// <summary>
	/// This is the translator that convert perfexiScript into widget objects 
	/// </summary>
	internal class Wig 
	{
		public Wig(string[] data,string name)
		{

		}

		public bool UsesKeyboard { get; set; } = false;
		public bool UsesMouse { get; set; } = false;

		
		public virtual void render()
		{
			
		}
	}
}
