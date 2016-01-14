using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace Wrox.ProCSharp.Arrays
{
    class HelloCollection
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "Hello";
            yield return "World";
        }
    }

    class Program
    {
        static void Main()
        {
            HelloWorld();
            MusicTitles();

            var game = new GameMoves();

            IEnumerator enumerator = game.Cross();
            while (enumerator.MoveNext())
            {
                enumerator = enumerator.Current as IEnumerator;
            }
        }

        static void MusicTitles()
        {
            var titles = new MusicTitles();
            foreach (var title in titles)
            {
                WriteLine(title);
            }
            WriteLine();

            WriteLine("reverse");
            foreach (var title in titles.Reverse())
            {
                WriteLine(title);
            }
            WriteLine();

            WriteLine("subset");
            foreach (var title in titles.Subset(2, 2))
            {
                WriteLine(title);
            }

        }

        static void HelloWorld()
        {
            var helloCollection = new HelloCollection();
            foreach (string s in helloCollection)
            {
                WriteLine(s);
            }
        }
    }
}
