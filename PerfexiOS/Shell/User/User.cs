using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.User
{
    public class user
    {
        public string name { get; set; }

        public Image icon;

        public string password { get; set; }

        public string rootpath { get; set; }

        public enum UserPermissions
        {
            Admin,
            Standard,
            Restricted,
        }

        
    }
}
