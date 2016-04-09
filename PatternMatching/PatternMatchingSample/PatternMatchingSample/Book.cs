using System.Collections.Generic;

namespace PatternMatchingSample
{
    abstract class Book
    {
        public Book(string title, string publisher, params string[] authors)
        {
            Title = title;
            Publisher = publisher;
            foreach (var author in authors)
            {
                Authors.Add(author);
            }
        }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public IList<string> Authors { get; } = new List<string>();
    }

    class BeginningBook : Book
    {
        public BeginningBook(string title, string publisher, params string[] authors)
            : base(title, publisher, authors)
        {
        }
    }

    class ProBook : Book
    {
        public ProBook(string title, string publisher, params string[] authors)
            : base(title, publisher, authors)
        {
        }
    }
}
