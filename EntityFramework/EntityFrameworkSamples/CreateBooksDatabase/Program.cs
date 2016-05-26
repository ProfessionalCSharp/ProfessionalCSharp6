using BooksSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateBooksDatabase
{
    class Program
    {
        static void Main()
        {
            using (var context = new BooksContext())
            {
                context.Database.EnsureCreated();

                if (context.Books.Count() == 0)
                {
                    var b1 = new Book { Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press" };
                    var b2 = new Book { Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press" };
                    var b3 = new Book { Title = "JavaScript for Kids", Publisher = "Wrox Press" };
                    var b4 = new Book { Title = "Web Design with HTML and CSS", Publisher = "For Dummies" };
                    context.Books.AddRange(b1, b2, b3, b4);
                }
            }
        }
    }
}
