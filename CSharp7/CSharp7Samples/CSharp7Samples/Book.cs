namespace CSharp7Samples
{
    public class Book
    {
        public Book(int id, string title, string publisher)
        {
            Id = id;
            Title = title;
            Publisher = publisher;
        }

        public int Id { get; }
        public string Title { get; }
        public string Publisher { get; }

        public void Deconstruct(out int id, out string title, out string publisher)
        {
            id = Id;
            title = Title;
            publisher = Publisher;
        }
    }

    public static class BookExtensions
    {
        public static void Deconstruct(this Book book, out string title, out string publisher) =>
            book.Deconstruct(out _, out title, out publisher);
    }
}
