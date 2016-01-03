using System.ServiceProcess;

namespace Wrox.ProCSharp.WinServices
{
    public class Program
    {
        public static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[]
            {
                new QuoteService()
            };
            ServiceBase.Run(servicesToRun);
            //ServiceBase.Run(new QuoteService());
        }
    }
}
