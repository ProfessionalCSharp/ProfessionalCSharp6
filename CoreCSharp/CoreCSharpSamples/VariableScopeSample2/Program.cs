﻿using static System.Console;

namespace VariableScopeSample2
{
    public class Program
    {
        public static int Main(string[] args)
        {

            int j = 20;
            for (int i = 0; i < 10; i++)
            {
                int j = 30; // Can't do this — j is still in scope
                WriteLine(j + i);
            }
            return 0;
        }
    }
}
