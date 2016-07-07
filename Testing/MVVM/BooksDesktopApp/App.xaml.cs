using Contracts;
using Framework;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
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
        private IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<BooksViewModel>();
            serviceCollection.AddTransient<BookViewModel>();
            serviceCollection.AddSingleton<IBooksService, BooksService>();
       //     serviceCollection.AddSingleton<IBooksRepository, BooksSampleRepository>();
            return serviceCollection.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {         
            base.OnStartup(e);

            Container = RegisterServices();


            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private BooksService _booksService;
        public BooksService BooksService =>
            _booksService ?? (_booksService = new BooksService(new BooksSampleRepository())); 
        


    }
}
