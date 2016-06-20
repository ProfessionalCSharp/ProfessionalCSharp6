using static System.Console;

namespace Wrox.ProCSharp.Generics
{
  class Program
  {
    static void Main()
    {
      var dm = new DocumentManager<Document>();
      dm.AddDocument(new Document("Title A", "Sample A"));
      dm.AddDocument(new Document("Title B", "Sample B"));

      dm.DisplayAllDocuments();

      if (dm.IsDocumentAvailable)
      {
        Document d = dm.GetDocument();
        WriteLine(d.Content);
      }

      if (dm.IsDocumentAvailable)
      {
        Document d = dm.GetDocument();
        WriteLine(d.Content);
      }

      dm.AddDocument(new Document("Title C", "Sample C"));
	    if (dm.IsDocumentAvailable)
      {
        Document d = dm.GetDocument();
        WriteLine($"{d.Title} {d.Content}");
      }

    }
  }
}
