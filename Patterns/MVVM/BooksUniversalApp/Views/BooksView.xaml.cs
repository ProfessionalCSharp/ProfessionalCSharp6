using Microsoft.Extensions.DependencyInjection;
using ViewModels;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BooksUniversalApp.Views
{
    public sealed partial class BooksView : UserControl
    {
        public BooksView()
        {
            this.InitializeComponent();
        }


        // public BooksViewModel ViewModel { get; } = new BooksViewModel((App.Current as App).BooksService);
        public BooksViewModel ViewModel { get; } = (App.Current as App).Container.GetService<BooksViewModel>();
    }
}
