using Cosmos.HAL;
using PINI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
	public class piniread : Command
	{
		public piniread() : base("pini","Allows Viewing Information of PINI objects")
		{
		}

		

		public override string[] Parse(GearSh parent, string[] args)
		{
			var filepath = args[0];
			var rootcommand = args[1];

			if(!filepath.Contains(':'))
			{
				filepath = parent.workingdir + filepath;

			}
			if(File.Exists(filepath))
			{
				Pini ini = new(filepath);
				var sectionpathh = args[2];
				if(!ini.GetSection(sectionpathh,out var sec)) { return new string[] { "Section Does not exist" }; }
				sec.Lex();
				switch (rootcommand)
				{
					case "secinfo":
						
						parent.SendLine("NAME: " + sec.name);
						parent.SendLine("----Local Keys----");
						foreach(var item in sec.Keys)
						{
							parent.Send(item.Key);
						}
						parent.SendLine("----Child Sections-----");
						foreach(var item in sec.Sections)
						{
							parent.Send(item.Key);
						}
						parent.SendLine("----Child Instances----");
						foreach(var item in sec.Structs)
						{
							parent.Send("ValueName: " + item.Key);
							parent.Send("Instanceof: " + item.Value.name);

						}
						

						break;
					case "keyinfo":
						if (sec.GetKey(args[3],out var key))
						{
							parent.Send("Keyname: " + key.name);
							parent.Send("Value: " + key.value);
							parent.Send("---Args---");
							for(int i = 0; i < key.args.Length; i++)
							{
								parent.Send($"args: {i}" + key.args[i]);

							}
						}
						else { return new string[] { "Key Does NOT exist" }; }
						break;
				}
			}
			else
			{
				return new string[] { "File Not Valid Error" };
			}
			return new string[] { };
		}
	}
}
