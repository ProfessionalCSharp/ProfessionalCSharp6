using static System.Console;

namespace SemanticsCompilation
{
    // Hello World! Sample Program
    public class Program
    {
        // Hello World! Sample Method with a variable
        public void Hello()
        {
            string hello = "Hello, World!";
            WriteLine(hello);
        }

        public static void Main()
        {
            var p = new Program();
            p.Hello();
        }
    }
}
