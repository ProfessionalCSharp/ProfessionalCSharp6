using System;
using System.Collections.Generic;

namespace CSharp7Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitSeparators();
            BinaryLiterals();
            RefLocalAndRefReturn();
            OutVars();
            //LocalFunctionsWithDelegates();
            LocalFunctions();
            TuplesAndDeconstruction();
            PatternMatching();
        }

        private static void DigitSeparators()
        {
            Console.WriteLine(nameof(DigitSeparators));
            ulong l1 = 0xfedcba9876543210;
            ulong l2 = 0xfedc_ba98_7654_3210;
            ulong l3 = 0xf_ed_cba_9876_54321_0;
            Console.WriteLine($"{l1:X} {l2:X} {l3:X}");

            Console.WriteLine();
        }
        private static void BinaryLiterals()
        {
            Console.WriteLine(nameof(BinaryLiterals));
            ushort b1 = 0b1111_0000_1010_1010;
            ShowBinary(nameof(b1), b1);
            ushort b2 = 0b0000_1111_0101_1010;
            ShowBinary(nameof(b2), b2);
            int b3 = b1 & b2;
            int b4 = b1 | b2;
            int b5 = b1 ^ b2;

            ShowBinary(nameof(b3), b3);
            ShowBinary(nameof(b4), b4);
            ShowBinary(nameof(b5), b5);

            Console.WriteLine();
        }

        private static void ShowBinary(string name, int n)
        {
            Console.WriteLine($"{name} hex: {n:X8}, binary: {Convert.ToString(n, 2)}");
        }

        private static void RefLocalAndRefReturn()
        {
            Console.WriteLine(nameof(RefLocalAndRefReturn));
            int[] data = { 1, 2, 3, 4 };
            int a1 = GetArrayElement1(data, 2);
            Console.WriteLine($"received a1: {a1}");
            a1 = 42;
            Console.WriteLine($"a1: {a1}, data[2]: {data[2]}");

            int a2 = GetArrayElement2(data, 2);
            Console.WriteLine($"received a2: {a2}");
            a2 = 42;
            Console.WriteLine($"a2: {a2}, data[2]: {data[2]}");

            ref int a3 = ref GetArrayElement2(data, 2);
            Console.WriteLine($"received a3: {a3}");
            a3 = 42;
            Console.WriteLine($"a3: {a3}, data[2]: {data[2]}");
            Console.WriteLine();
        }

        private static int GetArrayElement1(int[] arr, int index)
        {
            int x = arr[index];
            return x;
        }

        private static ref int GetArrayElement2(int[] arr, int index)
        {
            ref int x = ref arr[index];
            return ref x;
        }

        private static void OutVars()
        {
            Console.WriteLine(nameof(OutVars));
            Console.WriteLine("enter a number:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out var result))
            {
                Console.WriteLine($"this number: {result}");
            }
            else
            {
                Console.WriteLine("NaN");
            }
            Console.WriteLine();
        }

        private int ParseIt(string input) =>
            int.TryParse(input, out int result) ? result : -1;

        private static void LocalFunctionsWithDelegates()
        {
            Console.WriteLine($"{nameof(LocalFunctions)} part 1");

            Func<int, int, int> add = (x, y) => x + y;

            int result = add(1, 2);
            Console.WriteLine($"the result of {nameof(add)} is {result}");
            Console.WriteLine();
        }

        private static void LocalFunctions()
        {
            Console.WriteLine(nameof(LocalFunctions));
            int z = 3;
            int Add(int x, int y) => x + y + z;
            //{
            //    return x + y + z;
            //}

            int result = Add(1, 2);
            Console.WriteLine(result);
            Console.WriteLine();
        }

        private static void ThrowExpressions()
        {
            Console.WriteLine(nameof(ThrowExpressions));

            int x = 42;

            //int y = 0;
            //if (x <= 42)
            //{
            //    y = x;
            //}
            //else
            //{
            //    throw new Exception("bad value");
            //}
            int y = x <= 42 ? x : throw new Exception("bad value");

            Console.WriteLine($"y: {y}");

            Console.WriteLine();
        }

        private static void TuplesAndDeconstruction()
        {
            Console.WriteLine(nameof(TuplesAndDeconstruction));
            (var s, var i) = ("magic", 42);
            Console.WriteLine(s);
            Console.WriteLine(i);

            (string s, int i) t1 = ("magic", 42);
            Console.WriteLine(t1.s);
            Console.WriteLine(t1.i);

            (var result, var remainder) = Divide(9, 2);
            Console.WriteLine($"result: {result}, remainder: {remainder}");
            var t2 = Divide(7, 3);
            Console.WriteLine($"result: {t2.Result}, remainder: {t2.Remainder}");

            var book = new Book(1, "Professional C# 6 and .NET Core 1.0", "Wrox Press");
            (var id, var title, var publisher) = book;
            Console.WriteLine($"id: {id}, title: {title}, publisher: {publisher}");

            (_, var title1, _) = book;
            Console.WriteLine($"title: {title1}");

            (var title2, var publisher2) = book;
            Console.WriteLine($"title: {title2}, publisher: {publisher}");

            var books = new List<Book>
            {
                book,
                new Book(2, "Enterprise Services with the .NET Framework", "Addison Wesley"),
                new Book(3, "Professional C# 5.0 and .NET 4.5.1", "Wrox Press")
            };

            foreach ((string t, string p) in books)
            {
                Console.WriteLine($"title: {t}, publisher: {p}");
            }
        }

        public static (int Result, int Remainder) Divide(int val1, int val2)
        {
            int result = val1 / val2;
            int remainder = val1 % val2;
            return (result, remainder);
        }

        private static void PatternMatching()
        {
            Console.WriteLine(nameof(PatternMatching));
            object[] data = { null, 42, new Person("Matthias Nagel"), new Person("Katharina Nagel") };

            foreach (var item in data)
            {
                IsPattern(item);
            }
            foreach (var item in data)
            {
                SwitchPattern(item);
            }
            Console.WriteLine();
        }

        public static void IsPattern(object o)
        {
            if (o is null) Console.WriteLine("it's a const pattern");

            if (o is 42) Console.WriteLine("it's 42"); // const pattern

            if (o is var x) Console.WriteLine($"it's a var pattern with the type {x?.GetType()?.Name}");

            if (o is int i) Console.WriteLine($"it's a type pattern with an int and the value {i}");

            if (o is Person p) Console.WriteLine($"it's a person: {p.FirstName}");

            if (o is Person p2 && p2.FirstName.StartsWith("Ka")) Console.WriteLine($"it's a person starting with Ka {p2.FirstName}");
        }

        public static void SwitchPattern(object o)
        {
            switch (o)
            {
                case 42:
                    Console.WriteLine("it's 42 - a const pattern");
                    break;
                case null:
                    Console.WriteLine("it's a const pattern");
                    break;
                case int i:
                    Console.WriteLine("it's an int");
                    break;
                case Person p when p.FirstName.StartsWith("Ma"):
                    Console.WriteLine($"a Ma person {p.FirstName}");
                    break;
                case var x:
                    Console.WriteLine("it's a var pattern");
                    break;
                default:
                    break;
            }
        }
    }
}