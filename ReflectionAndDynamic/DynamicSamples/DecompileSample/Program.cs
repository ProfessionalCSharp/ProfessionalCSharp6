using static System.Console;

namespace DecompileSample
{
    class Program
    {
        static void Main()
        {
            StaticClass staticObject = new StaticClass();
            DynamicClass dynamicObject = new DynamicClass();
            WriteLine(staticObject.IntValue);
            WriteLine(dynamicObject.DynValue);
            ReadLine();
        }
    }

    class StaticClass
    {
        public int IntValue = 100;
    }

    class DynamicClass
    {
        public dynamic DynValue = 100;
    }

}
