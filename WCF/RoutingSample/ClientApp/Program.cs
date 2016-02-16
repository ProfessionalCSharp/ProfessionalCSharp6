using static System.Console;

namespace ClientApp
{
    public class Program
    {
        public static void Main()
        {
            WriteLine("client - wait for services");
            ReadLine();
            DemoService.DemoServiceClient service = new DemoService.DemoServiceClient();

            WriteLine(service.GetData("HelloB"));
            ReadLine();
        }
    }
}
