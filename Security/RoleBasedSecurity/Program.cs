using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading.Tasks;
using static System.Console;

namespace RoleBasedSecurity
{
    public class Program
    {
        public void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            try
            {
                ShowMessage();
            }
            catch (SecurityException exception)
            {
                WriteLine($"Security exception caught ({exception.Message})");
                WriteLine("The current principal must be in the local Users group");
            }

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "BUILTIN\\Users")]
        static void ShowMessage()
        {
            WriteLine("The current principal is logged in locally ");
            WriteLine("(member of the local Users group)");
        }

    }
}
