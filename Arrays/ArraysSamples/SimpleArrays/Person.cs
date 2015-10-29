namespace Wrox.ProCSharp.Arrays
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
