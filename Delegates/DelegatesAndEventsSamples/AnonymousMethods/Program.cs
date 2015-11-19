using System;
using static System.Console;

namespace Wrox.ProCSharp.Delegates
{
    public class Program
    {
        public static void Main()
        {
            string mid = ", middle part,";

            Func<string, string> anonDel = delegate (string param)
            {
                param += mid;
                param += " and this was added to the string.";
                return param;
            };
            WriteLine(anonDel("Start of string"));

        }
    }
}
