using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Reflection;
using static System.Console;


#if DOTNETCORE
using System.Runtime.Loader;
#endif

namespace ClientApp
{
    class Program
    {
        private const string CalculatorLibPath = @"c:\AddIns\CalculatorLib.dll";
        private const string CalculatorLibName = "CalculatorLib";
        private const string CalculatorTypeName = "CalculatorLib.Calculator";

        static void Main()
        {
            ReflectionOld();
            ReflectionNew();
        }

        private static void ReflectionNew()
        {
            double x = 3;
            double y = 4;
            dynamic calc = GetCalculator();
            double result = calc.Add(x, y);
            WriteLine($"the result of {x} and {y} is {result}");

            try
            {
                result = calc.Multiply(x, y);
            }
            catch (RuntimeBinderException ex)
            {
                WriteLine(ex);
            }
        }

        private static void ReflectionOld()
        {
            double x = 3;
            double y = 4;
            object calc = GetCalculator();

            // previous to using the NuGet package System.Reflection.TypeExtensions
            // object result = calc.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, calc, new object[] { x, y });
            object result = calc.GetType().GetMethod("Add").Invoke(calc, new object[] { x, y });
            WriteLine($"the result of {x} and {y} is {result}");
        }

#if NET461
        private static object GetCalculator()
        {
            Assembly assembly = Assembly.LoadFile(CalculatorLibPath);
            return assembly.CreateInstance(CalculatorTypeName);
        }
#endif

#if DOTNETCORE
        private static object GetCalculator()
        {
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(CalculatorLibPath);
            Type type = assembly.GetType(CalculatorTypeName);
            return Activator.CreateInstance(type);
        }
#endif
    }
}
