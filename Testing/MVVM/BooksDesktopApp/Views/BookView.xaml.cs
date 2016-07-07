using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using ViewModels;

namespace BooksDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookView : UserControl
    {
        public BookView()
        {
            InitializeComponent();
        }

        public BookViewModel ViewModel { get; } = new BookViewModel((App.Current as App).BooksService);
        // public BookViewModel ViewModel { get; } = (App.Current as App).Container.GetService<BookViewModel>();
    }
}
