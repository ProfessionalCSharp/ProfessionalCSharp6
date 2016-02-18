using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Reflection;
using static System.Console;

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

#if NET46
        private static object GetCalculator()
        {

            Assembly assembly = Assembly.LoadFile(CalculatorLibPath);
            return assembly.CreateInstance(CalculatorTypeName);
        }
#endif

#if DOTNETCORE
        private static object GetCalculator()
        {
            IAssemblyLoadContext loadContext = PlatformServices.Default.AssemblyLoadContextAccessor.Default;
            using (PlatformServices.Default.AssemblyLoaderContainer.AddLoader(new DirectoryLoader(CalculatorLibPath, loadContext)))
            {
                Assembly assembly = Assembly.Load(new AssemblyName(CalculatorLibName));
                Type type = assembly.GetType(CalculatorTypeName);
          
                return Activator.CreateInstance(type);
            }
        }
#endif

    }

    public class DirectoryLoader : IAssemblyLoader
    {
        private readonly IAssemblyLoadContext _context;
        private readonly string _path;

        public DirectoryLoader(string path, IAssemblyLoadContext context)
        {
            _path = path;
            _context = context;
        }

        public Assembly Load(AssemblyName assemblyName) =>
             _context.LoadFile(_path);


        public IntPtr LoadUnmanagedLibrary(string name)
        {
            throw new NotImplementedException();
        }
    }
}
