using System.Windows;

namespace TemplatesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnShowButtonTemplates(object sender, RoutedEventArgs e)
        {
            var window = new StyledButtons();
            window.Show();
        }

        private void OnShowListBoxTemplates(object sender, RoutedEventArgs e)
        {
            var window = new StyledLists();
            window.Show();
        }
    }
}
