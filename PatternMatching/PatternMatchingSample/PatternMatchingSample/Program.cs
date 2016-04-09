using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PatternMatchingSample
{
    // using compilation symbols __DEMO_EXPERIMENTAL__ and __DEMO__
    // which is equivalent to /features:patterns, /features:localFunctions, /features:refLocalsAndReturns
    // https://www.visualstudio.com/en-us/news/vs15-preview-vs.aspx
    class Program
    {
        static void Main()
        {
            Patterns1();
            IsOperatorExtension();
            SwitchStatement();
            TypePattern();
            TypePattern2();
            ConstantPattern();
            VarPattern();
            WildcardPattern();
            //RecursivePattern();
            //PropertyPattern();
            //ScopeOfPatternVariables();
            //UserDefinedIsOperator();
        }

        private static void IsOperatorExtension()
        {
            WriteLine(nameof(IsOperatorExtension));
            int? x1 = 3;
            if (x1 is int v)  // new local variable v that is definitely assigned after the is operator is true
            {
                WriteLine($"{nameof(x1)} is type int, this is the value: {v}");
            }
            int? x2 = null;
            if (x2 is int v)
            {
                WriteLine($"{nameof(x2)} is type int, this is the value: {v}");
            }
            else
            {
                WriteLine($"{nameof(x2)} is not an int");
            }
            WriteLine();
        }

        private static void SwitchStatement()
        {
            WriteLine(nameof(SwitchStatement));
            int? x = 4;
            switch (x)
            {
                case int val:
                    WriteLine($"it's an int with the value {val}");
                    break;
                default:
                    WriteLine("something else");
                    break;
            }
            WriteLine();
        }

        private static void UserDefinedIsOperator()
        {
            WriteLine(nameof(UserDefinedIsOperator));

            WriteLine();
        }

        private static void ScopeOfPatternVariables()
        {
            WriteLine(nameof(ScopeOfPatternVariables));

            WriteLine();
        }

        private static void PropertyPattern()
        {
            WriteLine(nameof(PropertyPattern));

            WriteLine();
        }

        private static void RecursivePattern()
        {
            WriteLine(nameof(RecursivePattern));
            //object x1 = 3;
            //if (x1 is Bird(*))  // new local variable v that is definitely assigned after the is operator is true
            //{
            //}
            //else if (x1 is string v)
            //{

            //}

            WriteLine();
        }

        private static void WildcardPattern()
        {
            int x1 = 3;
            if (x1 is *)
            {
                WriteLine("x is *");
            }
        }

        private static void VarPattern()
        {
            int x1 = 3;
            if (x1 is var v)
            {
                WriteLine("it's always a var");
            }
        }

        private static void TypePattern2()
        {
            Animal a = new Bird();
            var v = a as Bird;
            if (v != null)
            {
                WriteLine("v is a bird");
            }
        }

        static void TypePattern()
        {
            WriteLine(nameof(TypePattern));
            object[] numbers = { 42, 0b1010, new object[] { 1, 2, 3 }, 0x1234_5678 };
            foreach (var n in numbers)
            {
                switch (n)
                {
                    case int i when i > 40:
                        WriteLine($"n is an int with the value {i} that is larger than 40");
                        break;
                    case int i:
                        WriteLine($"n is an int with the value {i}");
                        break;
                    case IEnumerable<object> list when list.Any():
                        break;
                    default:
                        break;
                }
            }
            int x1 = 3;
            if (x1 is int v)
            {
                WriteLine($"{nameof(x1)} is type int, this is the value: {v}");
            }
            WriteLine();
        }

        static void ConstantPattern()
        {
            int x = 3;
            if (x is 3)
            {
                WriteLine($"constant x is 3: {x}");
            }
        }

        // current version doesn't implement positional parameters with pattern matching
        //private static Expr Deriv(Expr e)
        //{
        //    switch (e)
        //    {
        //        case X() : return new Const(1);
        //        case Const(*): return new Const(0);
        //        case Add(var left, var right): return new Add(Deriv(left), Deriv(right));
        //        case Mult(var left, var right):
        //            return new Add(Deriv(left), Deriv(right), Mult(left, Deriv(right)));
        //        case Neg(var value):
        //            return Neg(Deriv(value));
        //        default:
        //            break;
        //    }
        //}

        //private static Expr Simplify(Expr e)
        //{
        //    switch (e)
        //    {
        //        case Mult(Const(0), *): return new Const(0);
        //        case Mult(*, Const(0)): return new Const(0);
        //        case Mult(Const(1), var x): return Simplify(x);
        //        case Mult(var x, Const(1)): return Simplify(x);
        //        case Mult(Const(var l), Const(var r)): return new Const(l * r);
        //        case Add(Const(0), var x): return Simplify(x);
        //        case Add(var x, Const(0)): return Simplify(x);
        //        case Add(Const(var l, Const(var r)): return new Const(l + r);
        //        case Neg(Const(var k)) : return Const(-k); 
        //        default: return e;
        //    }
        //}

        static void Patterns1()
        {
            foreach (var book in GetBooks())
            {
                if (book is ProBook { Title is var t, Publisher is "Wrox Press" })
                {
                    WriteLine($"{t} is a book from Wrox Press");   
                }
                //switch (book)
                //{
                //    case book is ProBook { Title is var t, Publisher is "Wrox Press" }:
                //        WriteLine($"{t} is a book from Wrox Press");                    
                //        break;
                //    default:
                //        break;
                //}
            }
        }


        static IEnumerable<Book> GetBooks() =>
            new List<Book>()
            {
                new ProBook("Professional C# 5.0 and .NET 4.5.1", "Wrox Press", "Christian Nagel", "Jay Glynn", "Morgan Skinner"),
                new ProBook("Professional C# 6 and .NET Core 1.0", "Wrox Press", "Christian Nagel"),
                new ProBook("Professional C# 7 and .NET Core 1.1", "Wrox Press", "Christian Nagel"),
                new BeginningBook("Beginning C# 6.0 Programming with Visual Studio 2015", "Wrox Press", "Jacob Vibe Hammer", "Jon D. Reid")
            };
    }
}
