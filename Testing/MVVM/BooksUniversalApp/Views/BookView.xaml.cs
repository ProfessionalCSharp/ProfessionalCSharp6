using Microsoft.Extensions.DependencyInjection;
using ViewModels;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BooksUniversalApp.Views
{
    public sealed partial class BookView : UserControl
    {
        public BookView()
        {
            this.InitializeComponent();
        }

        // public BookViewModel ViewModel { get; } = new BookViewModel((App.Current as App).BooksService);
        public BookViewModel ViewModel { get; } = (App.Current as App).Container.GetService<BookViewModel>();
    }
}
