using System;
using System.Windows;
using RoomReservationClient.RoomReservationService;

namespace Wrox.ProCSharp.WCF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RoomReservation _reservation;
        public MainWindow()
        {
            InitializeComponent();
            _reservation = new RoomReservation { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            this.DataContext = _reservation;
        }

        private async void OnReserveRoom(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new RoomServiceClient();
                bool reserved = await client.ReserveRoomAsync(_reservation);
                client.Close();

                if (reserved)
                {
                    MessageBox.Show("reservation ok");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
