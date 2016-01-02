using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BooksCacheProvider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            PackageFamilyName = Package.Current.Id.FamilyName;
        }

        public string PackageFamilyName
        {
            get { return (string)GetValue(PackageFamilyNameProperty); }
            set { SetValue(PackageFamilyNameProperty, value); }
        }

        public static readonly DependencyProperty PackageFamilyNameProperty =
            DependencyProperty.Register("PackageFamilyName", typeof(string), typeof(MainPage), new PropertyMetadata(string.Empty));


    }
}
