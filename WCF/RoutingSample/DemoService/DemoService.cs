using static System.Console;

namespace Wrox.ProCSharp.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DemoService : IDemoService
    {
        public static string Server { get; set; }

        public string GetData(string value)
        {
            string message = $"Message from {Server}, You entered: {value}";
            WriteLine(message);
            return message;
        }
    }
}
