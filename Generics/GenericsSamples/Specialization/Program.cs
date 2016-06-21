﻿using static System.Console;

namespace Wrox.ProCSharp.Generics
{
    public class MethodOverloads
    {
        public void Foo<T>(T obj)
        {
            WriteLine($"Foo<T>(T obj), obj type: {obj.GetType().Name}");
        }

        public void Foo(int x)
        {
            WriteLine("Foo(int x)");
        }

        public void Foo<T1, T2>(T1 obj1, T2 obj2)
        {
            WriteLine($"Foo<T1, T2>(T1 obj1, T2 obj2); {obj1.GetType().Name} {obj2.GetType().Name}");
        }

        public void Foo<T>(int obj1, T obj2)
        {
            WriteLine($"Foo<T>(int obj1, T obj2); {obj2.GetType().Name}");
        }

        public void Bar<T>(T obj)
        {
            Foo(obj);
        }

        public void BarInt(int obj)
        {
            Foo(obj);
        }
    }

    class Program
    {
        static void Main()
        {
            var test = new MethodOverloads();
            test.Foo(33);
            test.Foo("abc");
            test.Foo("abc", 42);
            test.Foo(33, "abc");
            test.Bar(44);
            test.BarInt(55);
        }
    }
}
