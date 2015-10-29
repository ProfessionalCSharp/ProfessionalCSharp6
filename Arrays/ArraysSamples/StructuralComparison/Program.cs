using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace Wrox.ProCSharp.Arrays
{
    class TupleComparer : IEqualityComparer
    {
        #region IEqualityComparer Members

        public new bool Equals(object x, object y) => x.Equals(y);

        public int GetHashCode(object obj) => obj.GetHashCode();

        #endregion
    }


    class Program
    {
        static void Main()
        {
            var janet = new Person { FirstName = "Janet", LastName = "Jackson" };
            Person[] persons1 = { new Person { FirstName = "Michael", LastName = "Jackson" }, janet };
            Person[] persons2 = { new Person { FirstName = "Michael", LastName = "Jackson" }, janet };
            if (persons1 != persons2)
            {
                WriteLine("not the same reference");
            }

            if (!persons1.Equals(persons2))
            {
                WriteLine("equals returns false - not the same reference");
            }

            if ((persons1 as IStructuralEquatable).Equals(persons2, EqualityComparer<Person>.Default))
            {
                WriteLine("the same content");
            }


            var t1 = Tuple.Create<int, string>(1, "Stephanie");
            var t2 = Tuple.Create<int, string>(1, "Stephanie");
            if (t1 != t2)
            {
                WriteLine("not the same reference to the tuple");
            }

            if (t1.Equals(t2))
            {
                WriteLine("equals returns true");
            }

            var tc = new TupleComparer();

            if ((t1 as IStructuralEquatable).Equals(t2, tc))
            {
                WriteLine("yes, using TupleComparer");
            }

        }
    }
}
