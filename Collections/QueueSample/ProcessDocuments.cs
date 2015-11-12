using System;
using System.Threading.Tasks;
using static System.Console;

namespace QueueSample
{
  public class ProcessDocuments
  {
    public static void StartAsync(DocumentManager dm)
    {
      Task.Run(new ProcessDocuments(dm).Run);
    }

    protected ProcessDocuments(DocumentManager dm)
    {
      if (dm == null)
        throw new ArgumentNullException("dm");
      _documentManager = dm;
    }

    private DocumentManager _documentManager;

    protected async Task Run()
    {
      while (true)
      {
        if (_documentManager.IsDocumentAvailable)
        {
          Document doc = _documentManager.GetDocument();
          WriteLine("Processing document {0}", doc.Title);
        }
        await Task.Delay(new Random().Next(20));
      }
    }
  }
}