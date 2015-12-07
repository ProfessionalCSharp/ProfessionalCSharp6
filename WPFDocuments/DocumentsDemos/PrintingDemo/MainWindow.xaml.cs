using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps;

namespace PrintingDemo
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

        private void OnPrint(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                dlg.PrintVisual(canvas1, "Print Demo");

            }


            // PrintServer printServer = new PrintServer(@"\\treslunas\laserjet");
            var printServer = new LocalPrintServer();

            PrintQueue queue = printServer.DefaultPrintQueue;


            PrintTicket ticket = queue.DefaultPrintTicket;
            PrintCapabilities capabilities = queue.GetPrintCapabilities(ticket);
            if (capabilities.DuplexingCapability.Contains(Duplexing.TwoSidedLongEdge))
                ticket.Duplexing = Duplexing.TwoSidedLongEdge;
            if (capabilities.InputBinCapability.Contains(InputBin.AutoSelect))
                ticket.InputBin = InputBin.AutoSelect;
            if (capabilities.MaxCopyCount > 3)
                ticket.CopyCount = 3;
            if (capabilities.PageOrientationCapability.Contains(PageOrientation.Landscape))
                ticket.PageOrientation = PageOrientation.Landscape;
            if (capabilities.PagesPerSheetCapability.Contains(2))
                ticket.PagesPerSheet = 2;
            if (capabilities.StaplingCapability.Contains(Stapling.StapleBottomLeft))
                ticket.Stapling = Stapling.StapleBottomLeft;

            XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(queue);
            writer.Write(canvas1, ticket);


        }
    }
}

