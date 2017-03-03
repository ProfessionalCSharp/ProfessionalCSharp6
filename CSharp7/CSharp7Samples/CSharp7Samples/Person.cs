using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp7Samples
{
    public class Person
    {
        private EventHandler _someEvent;
        public event EventHandler SomeEvent
        {
            add => _someEvent += value;
            remove => _someEvent -= value;
        }

        private string _firstName;
        private string _lastName;
        public Person(string name) => 
            name.Split(' ').MoveElementsTo(out _firstName, out _lastName);

        public string FirstName => _firstName;
        public string LastName => _lastName;

        private int _age;

        public int Age
        {
            get => _age;
            set => _age = value;
        }


    }

    public static class PersonExtensions
    {
        public static void Deconstruct(this Person p, out string firstname, out string lastname, out int age)
        {
            firstname = p.FirstName;
            lastname = p.LastName;
            age = p.Age;
        }

        public static void Deconstruct(this Person p, out string firstname, out string lastname)
        {
            firstname = p.FirstName;
            lastname = p.LastName;
        }
    }

    public static class StringCollectionExtension
    {
        public static void MoveElementsTo(this IList<string> coll, out string s1, out string s2)
        {
            if (coll.Count != 2) throw new ArgumentException("wrong collection count", nameof(coll));

            s1 = coll[0];
            s2 = coll[1];
        }
    }
}
