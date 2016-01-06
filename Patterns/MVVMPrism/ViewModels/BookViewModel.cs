using Contracts;
using Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;
using ViewModels.Events;

namespace ViewModels
{
    public enum EditBookMode
    {
        Edit,
        AddNew
    }

    public class BookViewModel : BindableBase
    {
        private IBooksRepository _booksRepository;
        private IEventAggregator _eventAggregator;

        public BookViewModel(IBooksRepository booksRepository, IEventAggregator eventAggregator)
        {
            _booksRepository = booksRepository;
            _eventAggregator = eventAggregator;

            SaveBookCommand = new DelegateCommand(OnSaveBook);

            _eventAggregator.GetEvent<BookEvent>().Subscribe(SetBook);
        }

        public ICommand SaveBookCommand { get; }

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { SetProperty(ref _book, value); }
        }

        private EditBookMode _mode;
        public EditBookMode Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        private async void SetBook(BookInfo bookInfo)
        {
            if (bookInfo.BookId == 0)
            {
                Mode = EditBookMode.AddNew;
            }
            else  // get an existing book from the repository
            {
                Mode = EditBookMode.Edit;
                Book = await _booksRepository.GetItemAsync(bookInfo.BookId);
            }
        }

        private async void OnSaveBook()
        {
            switch (Mode)
            {
                case EditBookMode.Edit:
                    await _booksRepository.UpdateAsync(Book);
                    break;
                case EditBookMode.AddNew:
                    await _booksRepository.AddAsync(Book);
                    break;
                default:
                    break;
            }
        }
    }
}
