using System;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace PINI
{
    
    class PiniReader {
        private  pini ini;

        public PiniReader(pini ini) {
            this.ini = ini;
        }

        public pini.key? GetKey(string path) 
        {
            return null;
        }
        public pini.section? GetSection(string path)
        {
            pini.section currentsection = ini.root;

            List<string> paths = path.Split("/").ToList();
            if(paths[1] == string.Empty) {return currentsection;}
            for(int i = 1; i < path.Count()-1; i++ ) 
            {
                currentsection.LoadItems();
                var line = paths[i];
               
                if(i < paths.Count()-1) {
                    foreach(var section in currentsection.sections) {
                      
                        if(section.name == line) 
                        {
                            currentsection = section;    
                        }
                    }
                }
                if(i ==  paths.Count()-1) {
                    currentsection.LoadItems();
                    foreach(var item in currentsection.sections) 
                    {
                        if(item.name == line)
                        {
                            currentsection = item;
                        }
                    }

                    return currentsection;
                }
            
            }
            return null;
        }
    }
}