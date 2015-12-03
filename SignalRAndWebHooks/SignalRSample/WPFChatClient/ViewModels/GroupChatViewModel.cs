using Microsoft.AspNet.SignalR.Client;
using MVVMLib;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

namespace WPFChatClient.ViewModels
{
    public sealed class GroupChatViewModel : IDisposable, INotifyPropertyChanged
    {
        private readonly IMessagingService _messagingService;
        public GroupChatViewModel(IMessagingService messagingService)
        {
            _messagingService = messagingService;

            ConnectCommand = new DelegateCommand(OnConnect);
            SendCommand = new DelegateCommand(OnSendMessage);
            EnterGroupCommand = new DelegateCommand(OnEnterGroup);
            LeaveGroupCommand = new DelegateCommand(OnLeaveGroup);
        }


        private const string ServerURI = "http://localhost:45269/signalr";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public string Message { get; set; }

        public string NewGroup { get; set; }

        private string _selectedGroup;
        public string SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedGroup)));
            }
        }

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>();

        public DelegateCommand SendCommand { get; }

        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand EnterGroupCommand { get; }
        public DelegateCommand LeaveGroupCommand { get; }



        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        public void OnConnect()
        {
            CloseConnection();
            _hubConnection = new HubConnection(ServerURI);
            _hubConnection.Closed += HubConnectionClosed;
            _hubProxy = _hubConnection.CreateHubProxy("GroupChatHub");
            _hubProxy.On<string, string, string>("MessageToGroup", OnMessageReceived);

            try
            {
                _hubConnection.Start();
            }
            catch (HttpRequestException ex)
            {
                _messagingService.ShowMessage(ex.Message);
            }
            _messagingService.ShowMessage("client connected");
        }

        public async void OnSendMessage()
        {
            try
            {
                await _hubProxy.Invoke("Send", SelectedGroup, Name, Message);
            }
            catch (Exception ex)
            {
                _messagingService.ShowMessage(ex.Message);
            }
        }

        public void OnMessageReceived(string group, string name, string message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add($"{group}-{name}: {message}");
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

        public async void OnEnterGroup()
        {
            try
            {
                await _hubProxy.Invoke("AddGroup", NewGroup);
                Groups.Add(NewGroup);
                SelectedGroup = NewGroup;
            }
            catch (Exception ex)
            {
                _messagingService.ShowMessage(ex.Message);
            }
        }

        public async void OnLeaveGroup()
        {
            try
            {
                await _hubProxy.Invoke("RemoveGroup", SelectedGroup);
                Groups.Remove(SelectedGroup);
            }
            catch (Exception ex)
            {
                _messagingService.ShowMessage(ex.Message);
            }
        }
    }
}
