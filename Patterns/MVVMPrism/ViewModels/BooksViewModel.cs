using Contracts;
using Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.Events;

namespace ViewModels
{
    public class BooksViewModel : BindableBase
    {
        private IBooksRepository _booksRepository;
        private IEventAggregator _eventAggregator;

        public BooksViewModel(IBooksRepository booksRepository, IEventAggregator eventAggregator)
        {
            _booksRepository = booksRepository;
            _eventAggregator = eventAggregator;

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
                    _eventAggregator.GetEvent<BookEvent>().Publish(new BookInfo { BookId = _selectedBook.BookId });
                }
            }
        }

        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        public IEnumerable<Book> Books => _books;
        public ICommand GetBooksCommand { get; }

        public async void OnGetBooks()
        {
            (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
            var books = await _booksRepository.GetItemsAsync();
            _books.Clear();
            foreach (var book in books)
            {
                _books.Add(book);
            }
            _canGetBooks = true;
           (GetBooksCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        }

        private bool _canGetBooks = true;

        public bool CanGetBooks() => _canGetBooks;

        private void OnAddBook()
        {
            _eventAggregator.GetEvent<BookEvent>().Publish(new BookInfo { BookId = 0 });

        }

        public ICommand AddBookCommand { get; }
    }
}
