using System;

namespace Wrox.ProCSharp.Arrays
{
    public class Person : IEquatable<Person>
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString() => $"{Id} {FirstName} {LastName}";


        //public override bool Equals(object obj)
        //{
        //    if (obj == null) 
        //        return base.Equals(obj);
        //    return Equals(obj as Person);
        //}

        //public override int GetHashCode()
        //{
        //    return Id.GetHashCode();
        //}

        #region IEquatable<Person> Members

        public bool Equals(Person other)
        {
            if (other == null)
                return base.Equals(other);

            return Id == other.Id && FirstName == other.FirstName && LastName == other.LastName;
        }

        #endregion
    }

}
