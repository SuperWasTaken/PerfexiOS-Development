using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PINI
{
	public class Pini
	{

		/// <summary>
		/// Version of this PINI implimentation 
		/// </summary>
		public const string Version = "2.1";
		List<string> RawData = new();
		/// <summary>
		/// Changelog of this PINI implimentation
		/// </summary>
		public const string Changelog = "Rewrote Parser \n Added Structs \n Added Struct Library Importing \n Reduced to a single needed class to read";

		public int blockindex { get; set; } = 4;
		public PINISECTION _root;

		public Dictionary<string, PiniStructDefintion> DefinedStructs = new();


		public Pini(string path, int blockindex = 4)
		{
			this.blockindex = blockindex;
			var data = File.ReadAllText(path).Split("\n");
			this.RawData = data.ToList();
			_root = new PINISECTION(0, data.Length, data, this, "Root");
			_root.Lex();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Data"></param>
		/// <param name="blockindex">The amount of Spaces Between Blocks of Keys </param>
		public Pini(string[] Data, int blockindex = 4)
		{
			this.blockindex = blockindex;
			_root = new(0, Data.Length, Data, this, "Root");
			_root.Lex();
		}

		public void WriteKey(string keyname, string value, string[] args, string sectionpath)
		{
			if (this.GetSection(sectionpath, out var sec))
			{

				var tabcount = sectionpath.Split('/').Count();
				if (sec == _root) { tabcount = 0; }
				string FinalData = '\t' * tabcount + 1 + "KEY:{keyname}:{value}";
				foreach (var item in args) { FinalData += $":{item}"; }
				RawData.Insert(sec.endindex, FinalData);
				_root.endindex = RawData.Count;
				_root._Data = RawData.ToArray();
				_root.Lex();
			}

		}
		public void WriteSection(string sectionname, string sectionpath, string[] SectionData)
		{
			List<string> strings = new List<string>();
			if (this.GetSection(sectionpath, out var sec))
			{

				var tabcount = sectionpath.Split('/').Count();
				if (sec == _root) { tabcount = 0; }
				string Sectionlabel = '\t' * tabcount + 1 + $"SECTION-{sectionname}";
				int finalindex = sec.endindex + 3;
				RawData.Insert(finalindex, '\t' * tabcount + "END");
				_root.endindex = RawData.Count;
				_root._Data = RawData.ToArray();
				_root.Lex();
			}


		}
		public void Save(string path)
		{
			File.Create(path);
			File.WriteAllLines(path, RawData.ToArray());
		}


		public bool StructDefinitionExists(string name)
		{
			return DefinedStructs.ContainsKey(name);
		}
		/// <summary>
		/// Get a child Section in the PINI
		/// If you want to get the Root 
		/// It is already stored in the _root varible 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="section"></param>
		/// <returns></returns>
		/// 

		public bool GetSection(string path, out PINISECTION section)
		{

			section = _root;
			if (path == "/")
			{
				// Return true as this specifies the Root. 
				return true;
			}
			var paths = path.Split('/');
			var targetname = paths.Last();
			for (int i = 0; i < paths.Count(); i++)
			{

				var item = paths[i];
				if (section.GetSection(item, out var ns)) { section = ns; }

			}

			return true;
		}
	}

	public abstract class PINIBLOCK
	{


		public Pini parent;

		public PINIBLOCK(Pini parent, string[] data)
		{
			this.parent = parent;
			this._Data = data;

		}
		public string[] _Data;

		public abstract void Lex();
	}


	public struct PiniStructDefintion
	{
		public string name;
		public string[] Keys;

		public PiniStructDefintion(string name, string[] keys)
		{
			this.name = name;
			this.Keys = keys;
		}


	}
	public struct ActiveStructObject
	{
		public PiniStructDefintion definition;
		public List<Pinikey> ActiveKeys = new();

		public string name;
		public ActiveStructObject(string name, PiniStructDefintion defintion, string[] ConstructValues)
		{
			this.name = name;
			this.definition = defintion;
			for (int i = 0; i < ConstructValues.Length; i++)
			{
				// Check if theirs too many values 
				if (i > definition.Keys.Length) { break; }
				var k = new Pinikey(definition.Keys[i], ConstructValues[i]);
				ActiveKeys.Add(k);
			}
			// Done 
		}

		public bool GetKey(string name, out Pinikey key)
		{
			key = default;
			foreach (var item in ActiveKeys)
			{
				if (item.name == name) { key = item; return true; }
			}
			return false;
		}
	}

	public class PINISECTION : PINIBLOCK
	{
		enum ReadingTypes
		{
			None,
			Section,
			StuctDefinition,
		}
		public string name;

		public int startindex;
		public int endindex;
		public PINISECTION(int startindex, int endindex, string[] data, Pini parent, string name) : base(parent, data)
		{
			this.name = name;
		}



		public Dictionary<string, PINISECTION> Sections = new();

		public Dictionary<string, Pinikey> Keys = new();

		public Dictionary<string, ActiveStructObject> Structs = new();

		public bool GetKey(string name, out Pinikey key)
		{
			return Keys.TryGetValue(name, out key);
		}

		public override void Lex()
		{
			int readstartindex = 0;
			ReadingTypes type = ReadingTypes.None;
			string ReadName = "";
			bool ReadingBlock = false;
			// Clear the public lists to reset this section 
			Structs.Clear();
			Keys.Clear();
			Sections.Clear();
			List<string> _BlockData = new();
			for (int i = 0; i < _Data.Length; i++)
			{

				var line = _Data[i];
				if (ReadingBlock)
				{
					if (line.StartsWith("END"))
					{
						switch (type)
						{
							case ReadingTypes.Section:
								var sec = new PINISECTION(readstartindex, readstartindex + i - 1, _BlockData.ToArray(), parent, ReadName);
								Sections.Add(ReadName, sec);
								type = ReadingTypes.None;
								break;
							case ReadingTypes.StuctDefinition:
								// Check if the structtype already exists 
								type = ReadingTypes.None;


								// Create the new struct Definition 
								// Lex the Data to create the keys 

								List<string> _Keys = new();
								foreach (var item in _BlockData)
								{
									if (item.StartsWith("KEY:"))
									{

										_Keys.Add(item.Split(':')[1]);
									}
								}

								if (!parent.DefinedStructs.ContainsKey(ReadName.Trim()))
								{
									PiniStructDefintion _def = new(ReadName, _Keys.ToArray());
									parent.DefinedStructs.Add(ReadName.Trim(), _def);
								}

								break;

						}
						_BlockData.Clear();
						ReadingBlock = false;
						ReadName = " ";
						readstartindex = 0;
					}
					else
					{
						if (line.StartsWith("\t"))
						{
							var r = line.Substring(1);

							_BlockData.Add(r);
						}
						else
						{
							if (line.Length < parent.blockindex) { continue; }
							var r = line.Substring(parent.blockindex);
							_BlockData.Add(r);
						}

					}
				}
				else
				{

					// If the line starts with !C: witch is a comment Continue 
					if (line.StartsWith("!C:") || line.StartsWith("\t") || line.StartsWith(" ")) { continue; }

					if (line.StartsWith("IMPORT-"))
					{
						// Import the structs from the specified path 
						var args = line.Split('-').Skip(1).ToArray();
						var path = args[0].Trim();

						if (File.Exists(path))
						{
							Pini temp = new(path);
							foreach (var item in temp.DefinedStructs)
							{
								if (!parent.DefinedStructs.ContainsKey(item.Key))
								{
									parent.DefinedStructs.Add(item.Key, item.Value);
								}
							}
						}
					}


					if (line.StartsWith("KEY:"))
					{
						var argdata = line.Split(':');
						var keyname = argdata[1];
						var keyvalue = argdata[2];
						var keyargs = argdata.Skip(3).ToArray();
						// Append the key to the section.
						// Check if the section already has a key with the specified 
						if (Keys.ContainsKey(keyname)) { continue; }
						Keys.Add(keyname, new(keyname, keyvalue, keyargs));
						continue;
					}
					if (line.StartsWith("SECTION-"))
					{
						ReadingBlock = true;
						type = ReadingTypes.Section;
						ReadName = line.Split('-')[1];
						readstartindex = startindex + i + 1;
						continue;
					}
					if (line.StartsWith("STRUCT-"))
					{
						ReadingBlock = true;
						type = ReadingTypes.StuctDefinition;
						ReadName = line.Split('-')[1];
						readstartindex = startindex + i + 1;
						continue;
					}
					if (line.StartsWith("NEW-"))
					{
						var args = line.Split(':');
						var valueparams = args[0].Split('-');
						var valuetype = valueparams[1];
						var variblename = valueparams[2];
						if (parent.DefinedStructs.TryGetValue(valuetype, out var def))
						{
							if (Structs.ContainsKey(variblename)) { continue; }
							ActiveStructObject obj = new(variblename, def, args.Skip(1).ToArray());
							Structs.Add(variblename, obj);
						}

					}



				}

			}



		}

		public bool GetInstace(string Variblename, out ActiveStructObject obj)
		{
			return Structs.TryGetValue(Variblename, out obj);
		}

		public bool GetSection(string name, out PINISECTION sec)
		{
			Lex();


			foreach (var item in Sections)
			{
				name = name.Trim();
				var itemname = item.Key.Trim();

				
				if (name == itemname)
				{
					sec = item.Value;
					return true;
				}
			}
			sec = this;
			return false;
		}

		

	}
	public struct Pinikey
	{
		public string name;
		public string value;
		public string[] args { get; set; } = new string[0];

		/// <summary>
		/// This constructor uses the args- syntax 
		/// From sturct Constructors in the Pini Code
		/// 
		/// </summary>
		/// <param name="parsedata"></param>
		public Pinikey(string name, string parsedata)
		{


			if (parsedata.Contains("args-"))
			{
				this.name = name;
				var argsection = parsedata.Split("args-");
				var argvalues = argsection[1].Split('-');
				this.value = argsection[0];
				this.args = argvalues;


			}
			else
			{
				this.name = name;
				this.value = parsedata;
				// Set the args to a dummy value of Null incase of a error
				this.args = new string[] { "Null" };
			}


		}
		/// <summary>
		/// This constructor allows you to manualy set the values
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="args"></param>
		public Pinikey(string name, string value, string[] args)
		{
			this.name = name;
			this.value = value;
			this.args = args;
		}
	}
}