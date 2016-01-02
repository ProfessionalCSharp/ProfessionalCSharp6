using ApplicationLifetimeSample.Utlitities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ApplicationLifetimeSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private BackButtonManager _backButtonManager;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _backButtonManager = new BackButtonManager(Frame);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            _backButtonManager.Dispose();
        }

        public void GotoPage1()
        {
            Frame.Navigate(typeof(Page1), Parameter1);

        }

        public string Parameter1 { get; set; }

        public void GotoPage2()
        {
            Frame.Navigate(typeof(Page2), Parameter2);
        }

        public string Parameter2 { get; set; }
    }
}
