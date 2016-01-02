using ApplicationLifetimeSample.Utlitities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ApplicationLifetimeSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
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

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            _backButtonManager.Dispose();
        }

        public string ReceivedContent { get; private set; }
    }
}
