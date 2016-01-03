using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QuoteClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly QuoteInformation _quoteInfo = new QuoteInformation();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _quoteInfo;
        }

        private async void OnGetQuote(object sender, RoutedEventArgs e)
        {
            const int bufferSize = 1024;
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.Wait;
            _quoteInfo.EnableRequest = false;

            string serverName = Properties.Settings.Default.ServerName;
            int port = Properties.Settings.Default.PortNumber;

            var client = new TcpClient();

            NetworkStream stream = null;
            try
            {
                await client.ConnectAsync(serverName, port);
                stream = client.GetStream();
                byte[] buffer = new Byte[bufferSize];
                int received = await stream.ReadAsync(buffer, 0, bufferSize);
                if (received <= 0)
                {
                    return;
                }

                _quoteInfo.Quote = Encoding.Unicode.GetString(buffer).Trim('\0');
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "Error Quote of the day",
                      MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                stream?.Close();

                if (client.Connected)
                {
                    client.Close();
                }
            }
            this.Cursor = currentCursor;
            _quoteInfo.EnableRequest = true;
        }
    }
}
