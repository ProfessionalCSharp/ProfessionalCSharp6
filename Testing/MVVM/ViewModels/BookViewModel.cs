using System;
using Contracts;
using Contracts.Events;
using Framework;
using Models;
using System.Windows.Input;

namespace ViewModels
{
    public enum EditBookMode
    {
        Edit,
        AddNew
    }

    public class BookViewModel : ViewModelBase, IDisposable
    {
        private IBooksService _booksService;
        public BookViewModel(IBooksService booksService)
        {
            _booksService = booksService;

            SaveBookCommand = new DelegateCommand(OnSaveBook);

            EventAggregator<BookInfoEvent>.Instance.Event += LoadBook;
        }

        public ICommand SaveBookCommand { get; }

        private void LoadBook(object sender, BookInfoEvent bookInfo)
        {
            if (bookInfo.BookId == 0)
            {
                Book = new Book();
            }
            else
            {
                Book = _booksService.GetBook(bookInfo.BookId);
            }
        }

        public void Dispose()
        {
            EventAggregator<BookInfoEvent>.Instance.Event -= LoadBook;
        }

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { SetProperty(ref _book, value); }
        }

        private async void OnSaveBook()
        {
            Book = await _booksService.AddOrUpdateBookAsync(Book);
        }
    }
}
