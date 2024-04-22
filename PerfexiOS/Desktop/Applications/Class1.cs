using PerfexiOS.Desktop.PerfexiAPI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Desktop.Applications
{
	public class Gizmo : UiApplication
	{
		enum Moods
		{
			None,
			Pissed,
			Apologetic, 
			Happy,
			Mean,

		}

		int Hunger;
		int Fun;
		int Mood;

		Random random; 
		public bool Pissed = false;

		public Gizmo() : base("GizmoThePerfexiGear")
		{
			MainWindow = new(this, 200, 200, 128, 300, "Gizmo.pex", true);
			

		}

		public override void loop()
		{


			base.loop();
			Random rand = new Random();
			var UpdateHunger = rand.Next(1, 100);
			var UpdateFun = rand.Next(1, 100);
			var UpdateMood = rand.Next(1, 100);

			
		}

		private void RandomiseLine()
		{

		}
	}
}
