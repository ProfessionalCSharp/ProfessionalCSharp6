using static System.Console;

namespace VariableScopeSample
{
    class Program
    {
        private int j;

        static int Main()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    WriteLine(i);
            //}  // i goes out of scope here
            //   // We can declare a variable named i again, because
            //   // there's no other variable with that name in scope
            //for (int i = 9; i >= 0; i--)
            //{
            //    WriteLine(i);
            //}  // i goes out of scope here.
            //return 0;

            //int j = 20;
            //for (int i = 0; i < 10; i++)
            //{
            //    int j = 30; // Can't do this — j is still in scope
            //    WriteLine(j + i);
            //}
            //return 0;

            int j = 30;
            WriteLine(j);
            return 0;


        }

    }
}
