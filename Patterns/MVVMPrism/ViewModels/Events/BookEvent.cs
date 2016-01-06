using Prism.Events;

namespace ViewModels.Events
{
    public class BookInfo
    {
        public int BookId { get; set; }
    }

    public class BookEvent : PubSubEvent<BookInfo>
    {
    }
}
