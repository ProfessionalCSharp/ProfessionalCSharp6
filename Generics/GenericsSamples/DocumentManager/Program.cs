using static System.Console;

namespace Wrox.ProCSharp.Generics
{
    {


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
