using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Wrox.ProCSharp.WinServices
{
    public class QuoteServer
    {
        private TcpListener _listener;
        private int _port;
        private string _filename;
        private List<string> _quotes;
        private Random _random;
        private Task _listenerTask;

        public QuoteServer()
          : this("quotes.txt")
        {
        }
        public QuoteServer(string filename)
          : this(filename, 7890)
        {
        }
        public QuoteServer(string filename, int port)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort) throw new ArgumentException("port value not valid", nameof(port));

            _filename = filename;
            _port = port;
        }

        protected void ReadQuotes()
        {
            try
            {
                _quotes = File.ReadAllLines(_filename).ToList();
                if (_quotes.Count == 0)
                    throw new QuoteException("quotes file is empty");

                _random = new Random();
            }
            catch (IOException ex)
            {
                throw new QuoteException("I/O error", ex);
            }
        }

        protected string GetRandomQuoteOfTheDay()
        {
            int index = _random.Next(0, _quotes.Count);
            return _quotes[index];
        }

        public void Start()
        {
            ReadQuotes();

            _listenerTask = ListenerAsync(); // Task.Factory.StartNew(Listener, TaskCreationOptions.LongRunning);
        }

        protected async Task ListenerAsync()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Any;
                _listener = new TcpListener(ipAddress, _port);
                _listener.Start();
                while (true)
                {
                    using (Socket clientSocket = await _listener.AcceptSocketAsync())
                    {
                        string message = GetRandomQuoteOfTheDay();
                        var encoder = new UnicodeEncoding();
                        byte[] buffer = encoder.GetBytes(message);
                        clientSocket.Send(buffer, buffer.Length, 0);
                    }
                }
            }
            catch (SocketException ex)
            {
                Trace.TraceError($"QuoteServer {ex.Message}");
                throw new QuoteException("socket error", ex);
            }
        }

        public void Stop() => _listener.Stop();

        public void Suspend() => _listener.Stop();


        public void Resume() => Start();

        public void RefreshQuotes() => ReadQuotes();

    }
}
