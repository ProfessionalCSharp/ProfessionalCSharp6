using PInvokeSampleLib;
using System.IO;
using static System.Console;

namespace PInvokeSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                WriteLine("usage: PInvokeSample " +
                  "existingfilename newfilename");
                return;
            }
            try
            {
                FileUtility.CreateHardLink(args[0], args[1]);
            }
            catch (IOException ex)
            {
                WriteLine(ex.Message);
            }
        }


    }
}
