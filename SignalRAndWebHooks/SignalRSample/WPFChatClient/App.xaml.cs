using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WPFChatClient.ViewModels;

namespace WPFChatClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<ChatViewModel>();
            services.AddTransient<GroupChatViewModel>();
            services.AddSingleton<IMessagingService, MessagingService>();

            Container = services.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }
    }
}
