using System;
using static System.Console;

namespace Wrox.ProCSharp.Arrays
{
    class Program
    {
        static void Main()
        {
            SimpleArrays();
            TwoDim();
            ThreeDim();
            Jagged();

            ArrayClass();
            CopyArrays();

        }

        static void CopyArrays()
        {
            Person[] beatles = {
                     new Person { FirstName="John", LastName="Lennon" },
                     new Person { FirstName="Paul", LastName="McCartney" }
                   };

            Person[] beatlesClone = (Person[])beatles.Clone();

        }

        static void ArrayClass()
        {
            Array intArray1 = Array.CreateInstance(typeof(int), 5);
            for (int i = 0; i < 5; i++)
            {
                intArray1.SetValue(33, i);
            }

            for (int i = 0; i < 5; i++)
            {
                WriteLine(intArray1.GetValue(i));
            }

            int[] lengths = { 2, 3 };
            int[] lowerBounds = { 1, 10 };
            Array racers = Array.CreateInstance(typeof(Person), lengths, lowerBounds);
            racers.SetValue(new Person { FirstName = "Alain", LastName = "Prost" }, 1, 10);
            racers.SetValue(new Person
            {
                FirstName = "Emerson",
                LastName = "Fittipaldi"
            }, 1, 11);
            racers.SetValue(new Person { FirstName = "Ayrton", LastName = "Senna" }, 1, 12);
            racers.SetValue(new Person { FirstName = "Michael", LastName = "Schumacher" }, 2, 10);
            racers.SetValue(new Person { FirstName = "Fernando", LastName = "Alonso" }, 2, 11);
            racers.SetValue(new Person { FirstName = "Jenson", LastName = "Button" }, 2, 12);

            Person[,] racers2 = (Person[,])racers;
            Person first = racers2[1, 10];
            Person last = racers2[2, 12];


        }

        static void Jagged()
        {
            int[][] jagged = new int[3][];
            jagged[0] = new int[2] { 1, 2 };
            jagged[1] = new int[6] { 3, 4, 5, 6, 7, 8 };
            jagged[2] = new int[3] { 9, 10, 11 };

            for (int row = 0; row < jagged.Length; row++)
            {
                for (int element = 0;
                   element < jagged[row].Length; element++)
                {
                    WriteLine(
                       $"row: {row}, element: {element}, value: {jagged[row][element]}");
                }
            }
        }

        static void ThreeDim()
        {
            int[,,] threedim = {
                    { { 1, 2 }, { 3, 4 } },
                    { { 5, 6 }, { 7, 8 } },
                    { { 9, 10 }, { 11, 12 } }
                   };


            WriteLine(threedim[0, 1, 1]);
        }

        static void TwoDim()
        {
            int[,] twodim = new int[3, 3];
            twodim[0, 0] = 1;
            twodim[0, 1] = 2;
            twodim[0, 2] = 3;
            twodim[1, 0] = 4;
            twodim[1, 1] = 5;
            twodim[1, 2] = 6;
            twodim[2, 0] = 7;
            twodim[2, 1] = 8;
            twodim[2, 2] = 9;
        }


        static void SimpleArrays()
        {
            Person[] myPersons = new Person[2];

            myPersons[0] = new Person { FirstName = "Ayrton", LastName = "Senna" };
            myPersons[1] = new Person { FirstName = "Michael", LastName = "Schumacher" };

            Person[] myPersons2 =
            {
                new Person { FirstName="Ayrton", LastName="Senna"},
                new Person { FirstName="Michael", LastName="Schumacher"}
            };
        }
    }
}
