using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFChatClient
{
    public interface IMessagingService
    {
        void ShowMessage(string message);
    }

    public class MessagingService : IMessagingService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
