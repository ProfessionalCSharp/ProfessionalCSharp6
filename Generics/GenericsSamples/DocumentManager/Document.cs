
namespace Wrox.ProCSharp.Generics
{
  public interface IDocument
  {
    string Title { get; set; }
    string Content { get; set; }
  }

  public class Document : IDocument
  {
    public Document()
    {
    }

    public Document(string title, string content)
    {
      Title = title;
      Content = content;
    }

    public string Title { get; set; }
    public string Content { get; set; }
  }

}
