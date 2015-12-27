using System.Threading;
using System.Windows;

namespace SingletonWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            bool mutexCreated;
            var mutex = new Mutex(false, "SingletonWinAppMutex", out mutexCreated);
            if (!mutexCreated)
            {
                MessageBox.Show("You can only start one instance of the application");
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
        }
    }
}
