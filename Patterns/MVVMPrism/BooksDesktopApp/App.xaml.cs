using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;

namespace BooksDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterDIContainer();

            MainWindow mainWindow = new BooksDesktopApp.MainWindow();
            mainWindow.Show();

        }

        private void RegisterDIContainer()
        {
            var services = new ServiceCollection();
            services.AddTransient<BooksViewModel>();
            services.AddTransient<BookViewModel>();
            services.AddSingleton<IBooksRepository, BooksRepository>();
            services.AddSingleton<IEventAggregator, EventAggregator>();

            Container = services.BuildServiceProvider();
        }
        public IServiceProvider Container { get; private set; }
    }
}
