using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Arrays
{
    public enum PersonCompareType
    {
        FirstName,
        LastName
    }

    public class PersonComparer : IComparer<Person>
    {
        private PersonCompareType _compareType;

        public PersonComparer(PersonCompareType compareType)
        {
            _compareType = compareType;
        }


        #region IComparer<Person> Members

        public int Compare(Person x, Person y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            switch (_compareType)
            {
                case PersonCompareType.FirstName:
                    return x.FirstName.CompareTo(y.FirstName);
                case PersonCompareType.LastName:
                    return x.LastName.CompareTo(y.LastName);
                default:
                    throw new ArgumentException(
                          "unexpected compare type");
            }
        }

        #endregion
    }

}
