using System;
using System.Threading.Tasks;
using static System.Console;

namespace QueueSample
{
  class Program
  {
    static void Main()
    {
      var dm = new DocumentManager();

      ProcessDocuments.StartAsync(dm);

      // Create documents and add them to the DocumentManager
      for (int i = 0; i < 1000; i++)
      {
        Document doc = new Document("Doc " + i.ToString(), "content");
        dm.AddDocument(doc);
        WriteLine("Added document {0}", doc.Title);
        Task.Delay(new Random().Next(20)).Wait();

      }

      ReadLine();

    }
  }
}
