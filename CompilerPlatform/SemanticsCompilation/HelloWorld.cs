using static System.Console;

namespace SemanticsCompilation
{
    // Hello World! Sample Program
    class Program
    {
        // Hello World! Sample Method with a variable
        public void Hello()
        {
            string hello = "Hello, World!";
            WriteLine(hello);
        }

        static void Main()
        {
            var p = new Program();
            p.Hello();
        }
    }
}
