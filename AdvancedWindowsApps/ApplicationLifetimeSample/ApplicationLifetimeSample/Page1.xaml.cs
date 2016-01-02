using ApplicationLifetimeSample.Services;
using ApplicationLifetimeSample.Utlitities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationLifetimeSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();
        }

        private BackButtonManager _backButtonManager;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _backButtonManager = new BackButtonManager(Frame);


            ReceivedContent = e.Parameter?.ToString() ?? string.Empty;
            Bindings.Update();
        }

        public string ReceivedContent { get; private set; }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            _backButtonManager.Dispose();
        }




        public void GotoPage2()
        {
            Frame.Navigate(typeof(Page2), Parameter1);
        }

        public string Parameter1 { get; set; }

        public DataManager Data { get; } = DataManager.Instance;
    }
}
