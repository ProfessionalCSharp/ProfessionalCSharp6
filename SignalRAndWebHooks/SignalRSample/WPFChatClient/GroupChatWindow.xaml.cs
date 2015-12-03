using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using WPFChatClient.ViewModels;

namespace WPFChatClient
{
    /// <summary>
    /// Interaction logic for GroupChatWindow.xaml
    /// </summary>
    public partial class GroupChatWindow : Window
    {
        public GroupChatWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public GroupChatViewModel ViewModel { get; } =
         (App.Current as App).Container.GetService<GroupChatViewModel>();
    }
}
