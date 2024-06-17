using Cosmos.System;
using Cosmos.System.ScanMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class SetLayout : Command
    {
        public SetLayout() : base("SetLayout", "Sets the keyboard layout manualy")
        {
        }

        public override string[] Parse(GearSh parent, string[] args)
        {
            var layout = args[0];

            switch (layout)
            {
                case "US":
                    USStandardLayout us = new();
                    KeyboardManager.SetKeyLayout(us);
                    parent.Send("Sucessfully changed Keyboard layout to US");
                    return new string[] {};
                case "DV":
                    US_Dvorak dv = new();
                    KeyboardManager.SetKeyLayout(dv);
                    parent.Send("Sucessfully changed Keyboard layout to Dvorak");
                    return new string[] {};
                case "GB":
                    GBStandardLayout GB = new();
                    KeyboardManager.SetKeyLayout(GB);
                    parent.Send("Sucessfully Changed keyboard layout to British");
                    return new string[] {};
                case "TR":
                    TRStandardLayout TR = new();
                    KeyboardManager.SetKeyLayout(TR);
                    parent.Send("Sucessfully Changed keyboard layout to Turkish");
                    return new string[] {};
                case "FR":
                    FRStandardLayout FR = new();
                    KeyboardManager.SetKeyLayout(FR);
                    parent.Send("Sucessfully Changed keyboard layout to French");
                    return new string[] {};
                case "ES":
                    ESStandardLayout ES = new();
                    KeyboardManager.SetKeyLayout(ES);
                    parent.Send("Sucessfully changed layout to Spanish");
                    return new string[] {};
                case "DE":
                    DEStandardLayout DE = new();
                    KeyboardManager.SetKeyLayout(DE);
                    parent.Send("Sucessfully changed layout to German");
                    return new string[] {};
                default:
                    parent.Send("Invalid Layout");
                    return new string[] {};

            }
            return new string[] { };
        }
        
    }
}
