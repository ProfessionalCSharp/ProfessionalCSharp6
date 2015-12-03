using Microsoft.AspNet.SignalR.Client;
using MVVMLib;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace WPFChatClient.ViewModels
{
    public sealed class ChatViewModel : IDisposable
    {
        private const string ServerURI = "http://localhost:45269/signalr";
        private readonly IMessagingService _messagingService;
        public ChatViewModel(IMessagingService messagingService)
        {
            _messagingService = messagingService;

            ConnectCommand = new DelegateCommand(OnConnect);
            SendCommand = new DelegateCommand(OnSendMessage);
        }

        public string Name { get; set; }
        public string Message { get; set; }

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        public DelegateCommand SendCommand { get; }

        public DelegateCommand ConnectCommand { get; }

        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        public async void OnConnect()
        {
            CloseConnection();
            _hubConnection = new HubConnection(ServerURI);
            _hubConnection.Closed += HubConnectionClosed;
            _hubProxy = _hubConnection.CreateHubProxy("ChatHub");
            _hubProxy.On<string, string>("BroadcastMessage", OnMessageReceived);

            try
            {
                await _hubConnection.Start();
            }
            catch (HttpRequestException ex)
            {
                _messagingService.ShowMessage(ex.Message);
            }
            _messagingService.ShowMessage("client connected");
        }

        public void OnSendMessage()
        {
            _hubProxy.Invoke("Send", Name, Message);
        }

        public void OnMessageReceived(string name, string message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add($"{name}: {message}");
            });

        }

        private void HubConnectionClosed()
        {
            _messagingService.ShowMessage("Hub connection closed");
        }

        private void CloseConnection()
        {
            _hubConnection?.Stop();
            _hubConnection?.Dispose();
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
