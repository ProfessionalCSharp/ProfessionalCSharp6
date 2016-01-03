using System;
using System.IO;
using System.ServiceProcess;

namespace Wrox.ProCSharp.WinServices
{
  public partial class QuoteService : ServiceBase
  {
    public const string QUOTESFILE = "quotes.txt";
    private QuoteServer quoteServer;

    public QuoteService()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      quoteServer = new QuoteServer(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, QUOTESFILE), 5678);
      quoteServer.Start();

    }

    protected override void OnStop() => quoteServer.Stop();


    protected override void OnContinue() => base.OnContinue();


    protected override void OnPause() => base.OnPause();


    public const int CommandRefresh = 128;
    protected override void OnCustomCommand(int command)
    {
      switch (command)
      {
        case CommandRefresh:
          quoteServer.RefreshQuotes();
          break;

        default:
          break;
      }

    }
  }
}
