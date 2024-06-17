
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Shell.User
{
    public static class UserManager
    {
        public static user CurrentUser { get; private set; }


        enum perms
        {
            admin,
            standard,
            low,
        }
        public static void Initalise()
        {
            
        }




    }
}
