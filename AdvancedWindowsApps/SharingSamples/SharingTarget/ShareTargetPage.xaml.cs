using SharingTarget.ViewModels;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SharingTarget
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShareTargetPage : Page
    {
        public ShareTargetPage()
        {
            this.InitializeComponent();
        }

        public ShareTargetPageViewModel ViewModel { get; } = new ShareTargetPageViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Activate(e.Parameter as ShareOperation);

            base.OnNavigatedTo(e);
        }

    }
}
