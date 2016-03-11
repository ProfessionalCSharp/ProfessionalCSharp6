using System.Collections.Generic;

namespace QueueSample
{
  public class DocumentManager
  {
    private object sync = new object();

    private readonly Queue<Document> _documentQueue = new Queue<Document>();

    public void AddDocument(Document doc)
    {
      lock (sync)
      {
        _documentQueue.Enqueue(doc);
      }
    }

    public Document GetDocument()
    {
      Document doc = null;
      lock (sync)
      {
        doc = _documentQueue.Dequeue();
      }
      return doc;
    }

    public bool IsDocumentAvailable => _documentQueue.Count > 0;

  }
}