using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using static System.Console;

namespace WindowsPrincipalSample
{
    class Program
    {
        static void Main()
        {
            WindowsIdentity identity = ShowIdentityInformation();

            WindowsPrincipal principal = ShowPrincipal(identity);

            ShowClaims(principal.Claims);

        }

        public static WindowsIdentity ShowIdentityInformation()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                WriteLine("not a Windows Identity");
                return null;
            }

            identity.AddClaim(new Claim("Age", "25"));


            WriteLine($"IdentityType: {identity}");
            WriteLine($"Name: {identity.Name}");
            WriteLine($"Authenticated: {identity.IsAuthenticated}");
            WriteLine($"Authentication Type: {identity.AuthenticationType}");
            WriteLine($"Anonymous? {identity.IsAnonymous}");
            WriteLine($"Access Token: {identity.AccessToken.DangerousGetHandle()}");
            WriteLine();
            return identity;
        }

        public static WindowsPrincipal ShowPrincipal(WindowsIdentity identity)
        {
            WriteLine("Show principal information");
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal == null)
            {
                WriteLine("not a Windows Principal");
                return null;
            }
            WriteLine($"Users? {principal.IsInRole(WindowsBuiltInRole.User)}");
            WriteLine($"Administrators? {principal.IsInRole(WindowsBuiltInRole.Administrator)}");
            WriteLine();
            return principal;
        }

        public static void ShowClaims(IEnumerable<Claim> claims)
        {
            WriteLine("Claims");
            foreach (var claim in claims)
            {
                WriteLine($"Subject: {claim.Subject}");
                WriteLine($"Issuer: {claim.Issuer}");
                WriteLine($"Type: {claim.Type}");
                WriteLine($"Value type: {claim.ValueType}");
                WriteLine($"Value: {claim.Value}");
                foreach (var prop in claim.Properties)
                {
                    WriteLine($"\tProperty: {prop.Key} {prop.Value}");
                }
                WriteLine();

            }
        }
    }
}
