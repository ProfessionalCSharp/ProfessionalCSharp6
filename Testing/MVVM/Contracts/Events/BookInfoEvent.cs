using System;

namespace Contracts.Events
{
    public class BookInfoEvent : EventArgs
    {
        public int BookId { get; set; }
    }
}
