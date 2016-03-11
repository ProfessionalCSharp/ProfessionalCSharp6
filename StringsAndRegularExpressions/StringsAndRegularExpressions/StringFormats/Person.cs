using System;

namespace StringFormats
{
    public class Person : IFormattable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString() => FirstName + " " + LastName;

        public virtual string ToString(string format) => ToString(format, null);


        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "A":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                default:
                    throw new FormatException($"invalid format string {format}");
            }
        }
    }
}