using System;
using System.Runtime.CompilerServices;
using static System.Console;

namespace CallerInformation
{
    class Program
    {
        static void Main()
        {
            var p = new Program();
            p.Log();
            p.SomeProperty = 33;
            Action a1 = () => p.Log();
            a1();

            ReadLine();
        }

        private int _someProperty;
        public int SomeProperty
        {
            get { return _someProperty; }
            set
            {
                Log();
                _someProperty = value;
            }
        }


        public void Log([CallerLineNumber] int line = -1,
            [CallerFilePath] string path = null,
            [CallerMemberName] string name = null)
        {
            WriteLine((line < 0) ? "No line" : "Line " + line);
            WriteLine((path == null) ? "No file path" : path);
            WriteLine((name == null) ? "No member name" : name);
            WriteLine();
        }

    }
}
