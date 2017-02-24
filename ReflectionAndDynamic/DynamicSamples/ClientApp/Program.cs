using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Reflection;
using static System.Console;
using System.Runtime.Loader;

namespace ClientApp
{
    class Program
    {
        private const string CalculatorLibName = "CalculatorLib";
        private const string CalculatorTypeName = "CalculatorLib.Calculator";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            ReflectionOld(args[0]);
            ReflectionNew(args[0]);
        }

        private static void ShowUsage()
        {
            WriteLine($"Usage: {nameof(ClientApp)} path");
            WriteLine();
            WriteLine("Copy CalculatorLib.dll to an addin directory");
            WriteLine("and pass the absolute path of this directory when starting the application to load the library");
        }

        private static void ReflectionNew(string addinPath)
        {
            double x = 3;
            double y = 4;
            dynamic calc = GetCalculator(addinPath);
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

        private static void ReflectionOld(string addinPath)
        {
            double x = 3;
            double y = 4;
            object calc = GetCalculator(addinPath);

            // previous to using the NuGet package System.Reflection.TypeExtensions
            // object result = calc.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, calc, new object[] { x, y });
            object result = calc.GetType().GetMethod("Add").Invoke(calc, new object[] { x, y });
            WriteLine($"the result of {x} and {y} is {result}");
        }

#if NET461
        private static object GetCalculator(string addinPath)
        {
            Assembly assembly = Assembly.LoadFile(addinPath);
            return assembly.CreateInstance(CalculatorTypeName);
        }
#else
        private static object GetCalculator(string addinPath)
        {
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(addinPath);
            Type type = assembly.GetType(CalculatorTypeName);
            return Activator.CreateInstance(type);
        }
#endif
    }
}
