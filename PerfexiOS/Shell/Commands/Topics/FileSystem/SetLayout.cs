using Cosmos.System;
using Cosmos.System.ScanMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.Commands.Topics.FileSystem
{
    public class SetLayout : Command
    {
        public SetLayout() : base("SetLayout","Sets the keyboard layout manualy")
        {
        }

        public override string[] Execute(commandManager parent, string[] args)
        {
            var layout = args[0];

            switch(layout)
            {
                case "US":
                    USStandardLayout us = new();
                    KeyboardManager.SetKeyLayout(us);
                    return new string[] { "Sucessfully changed Keyboard layout to US" };
                case "DV":
                    US_Dvorak dv = new();
                    KeyboardManager.SetKeyLayout(dv);
                    return new string[] { "Sucessfully changed Keyboard layout to Dvorak" };
                case "GB":
                    GBStandardLayout GB = new();
                    KeyboardManager.SetKeyLayout(GB);
                    return new string[] { "Sucessfully Changed keyboard layout to British" };
                case "TR":
                    TRStandardLayout TR = new();
                    KeyboardManager.SetKeyLayout(TR);
                    return new string[] { "Sucessfully Changed keyboard layout to Turkish" };
                case "FR":
                    FRStandardLayout FR = new();
                    KeyboardManager.SetKeyLayout(FR);
                    return new string[] { "Sucessfully Changed keyboard layout to French" };
                case "ES":
                    ESStandardLayout ES = new();
                    KeyboardManager.SetKeyLayout(ES);
                    return new string[] { "Sucessfully changed layout to Spanish" };
                case "DE":
                    DEStandardLayout DE = new();
                    KeyboardManager.SetKeyLayout(DE);
                    return new string[] { "Sucessfully changed layout to German" };
                default:
                    return new string[] { "Invalid layout" };
               
            }
        }
    }
}
