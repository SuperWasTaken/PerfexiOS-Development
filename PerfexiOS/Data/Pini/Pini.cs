
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PINI
{
    public class pini
    {
        public int tabmultiplier = 4;
        public string[] data;
        public section root;
        public List<section> returnsections = new();
        public List<key> returnkeys = new();

        public pini(string rootpath)
        {
            returnsections.Clear();
            returnkeys.Clear();
            data = File.ReadAllLines(rootpath);
            root = new("ROOT", 0, data.Length, 0, this);
            root.LoadItems();

        }

        

        public class section
        {
            public string name;
            public string value;
            public int startindex;
            public int endindex;
            public int tabindex;
            readonly pini parent;
            public section(string name, int startindex, int endindex, int tabindex, pini parent)
            {
                this.startindex = startindex;
                this.name = name;
                this.endindex = endindex;
                this.tabindex = tabindex;
                this.parent = parent;
            }

            public key? GetKey(string name) 
            {
                foreach(var item in keys)
                {
                    if(item.name == name) 
                    {
                        return item;
                    }
                }
                return null;
            }

            public List<section> sections = new List<section>();
            public List<key> keys = new List<key>();

            public void WriteKey(string data)
            {
                var root = new List<string>(parent.data);
                root.Insert(endindex, new string(' ',tabindex*4)+data);
                parent.data = root.ToArray();
                LoadItems();
            }

            public void MakeSection(string name)
            {
                var root = new List<string>(parent.data.ToList());
                root.Insert(endindex, new string(' ',tabindex*4)+$"SECTION-{name}");
                root.Insert(endindex + 1, new string(' ', (tabindex * 4)) + " ");
                root.Insert(endindex+4,new string(' ', tabindex * 4)+"END");
                LoadItems();
            }
            public void LoadItems()
            {
                
                int readstartindex = 0;
                int readendindex; 
                bool readingsection = false;
                string readname = "";
                sections.Clear();
                keys.Clear();
                string[] data = parent.data;
                string line = string.Empty;
                for (int i = startindex; i < endindex; i++)
                {   
                    var baseline = data[i];
                    
                    if(baseline.Contains("\t" )) {
                        var spaced = baseline.Replace("\t","    ");
                        if(tabindex *4 <= spaced.Length) {
                            spaced = spaced.Substring(tabindex*4);
                            line = spaced;
                        }

                    }
                    if(baseline.StartsWith("    "))
                    {
                        line = baseline.Substring(tabindex*4);
                    }
                    else {
                        line = baseline;
                    }
                    
                    if (readingsection)
                    {
                        if (line.StartsWith("END"))
                        {
                            
                            readendindex = i;
                            sections.Add(new(readname,readstartindex, readendindex,tabindex+1,parent));
                            readname = " ";
                            readingsection = false;
                        }
                        else 
                        {
                            continue;
                        }

                        
                    }

                    
                    if(line.StartsWith("!C:") || line.StartsWith(" ") || line.StartsWith("\t")) {  continue; }
                    if (line.StartsWith("SECTION-"))
                    {
                        var s = line.Split('-');
                        readname = s[1].Trim();
                        readingsection = true;
                        readstartindex = i+1;
                        continue;
                    }

                    if(line.StartsWith("KEY:")) 
                    {
                        string[] s = line.Split(":");
                        if(s.Length > 3) {
                            keys.Add(new(s));
                        }
                        else {
                            keys.Add(new(s));
                        }
                        
                    }


                   

                    

                }

              

            }
            
                
                


                
            

            
        }
        public class key
        {

            public string name {get; set;}
            public string value {get;  set;}
            public string[] args {get; set;}


            public key(string[] args)
            {
                this.name = args[1];
                this.value = args[2];
                if(args.Length > 3) {
                    this.args = args.Skip(3).ToArray();
                }
            }
        }
    }

   
    

    
}
