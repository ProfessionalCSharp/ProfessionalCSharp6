using DataBindingSamples.Contracts;
using DataBindingSamples.Events;
using DataBindingSamples.Models;
using Framework;
using System.Collections.Generic;
using System.Windows.Input;

namespace DataBindingSamples.ViewModels
{
    public class BooksViewModel : ViewModelBase
    {
        private IBooksService _booksService;

        public BooksViewModel(IBooksService booksService)
        {
            _booksService = booksService;
            GetBooksCommand = new DelegateCommand(OnGetBooks, CanGetBooks);
            AddBookCommand = new DelegateCommand(OnAddBook);
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                if (SetProperty(ref _selectedBook, value))
                {
                    EventAggregator<BookInfoEvent>.Instance.Publish(this, new BookInfoEvent { BookId = _selectedBook.BookId });
                }
            }
        }

        public IEnumerable<Book> Books => _booksService.Books;
        public ICommand GetBooksCommand { get; }

        public async void OnGetBooks()
        {
            await _booksService.LoadBooksAsync();

            _canGetBooks = false;
           (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        }

        private bool _canGetBooks = true;

        public bool CanGetBooks() => _canGetBooks;

        private void OnAddBook()
        {
            EventAggregator<BookInfoEvent>.Instance.Publish(this, new BookInfoEvent { BookId = 0 });
        }

        public ICommand AddBookCommand { get; }
    }
}
