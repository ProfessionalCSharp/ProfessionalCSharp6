using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPFChatClient.ViewModels;

namespace WPFChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ChatViewModel ViewModel { get; } = 
            (App.Current as App).Container.GetService<ChatViewModel>();

        private void OnGroupChat(object sender, RoutedEventArgs e)
        {
            var groupChatWindow = new GroupChatWindow();
            groupChatWindow.Show();
        }
    }
}
