using System.Windows;
using System.Windows.Media;

namespace ShowFontsDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Fonts.SystemFontFamilies;
        }
    }
}
