using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace FlowDocumentsDemo
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

        private void OnOpenDocument(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new OpenFileDialog();
                dlg.DefaultExt = "*.xaml";
                dlg.InitialDirectory = Environment.CurrentDirectory;
                if (dlg.ShowDialog() == true)
                {
                    using (FileStream xamlFile = File.OpenRead(dlg.FileName))
                    {
                        var doc = XamlReader.Load(xamlFile) as FlowDocument;
                        if (doc != null)
                        {
                            _activedocumentReader.Document = doc;
                            _activedocumentReader.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            catch (XamlParseException ex)
            {
                MessageBox.Show($"Check content for a Flow document: {ex.Message}");
            }
        }

        private void OnReaderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = (sender as ComboBox).SelectedItem;

            if (_activedocumentReader != null)
            {
                _activedocumentReader.Visibility = Visibility.Collapsed;
            }
            _activedocumentReader = item.Instance;
        }

        private dynamic _activedocumentReader = null;

        public IEnumerable<object> Readers => GetReaders();


        private List<object> _documentReaders = null;
        private IEnumerable<object> GetReaders()
        {
            return _documentReaders ?? (_documentReaders =
              LogicalTreeHelper.GetChildren(grid1).OfType<FrameworkElement>()
                .Where(el => el.GetType().GetProperties().Where(pi => pi.Name == "Document").Count() > 0)
                .Select(el => new
                {
                    Name = el.GetType().Name,
                    Instance = el
                }).Cast<object>().ToList());
        }
    }
}
