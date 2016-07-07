using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using ViewModels;

namespace BooksDesktopApp.Views
{

    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
        }

       // public BooksViewModel ViewModel { get; } = new BooksViewModel((App.Current as App).BooksService);
         public BooksViewModel ViewModel { get; } = (App.Current as App).Container.GetService<BooksViewModel>();
    }
}
