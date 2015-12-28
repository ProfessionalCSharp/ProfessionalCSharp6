using System;
using System.Globalization;
using static System.Console;

namespace CustomCultures
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Create a Styria culture
                var styria = new CultureAndRegionInfoBuilder("de-AT-ST", CultureAndRegionModifiers.None);
                var cultureParent = new CultureInfo("de-AT");
                styria.LoadDataFromCultureInfo(cultureParent);
                styria.LoadDataFromRegionInfo(new RegionInfo("AT"));
                styria.Parent = cultureParent;
                styria.RegionNativeName = "Steiermark";
                styria.RegionEnglishName = "Styria";
                styria.CultureEnglishName = "Styria (Austria)";
                styria.CultureNativeName = "Steirisch";

                styria.Register();
            }
            catch (UnauthorizedAccessException ex)
            {
                WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                WriteLine(ex.Message);
            }
            ReadLine();
        }
    }
}
