namespace Wrox.ProCSharp.Async
{
  public class SearchItemResult : BindableBase
  {
    private string _title;

    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }

    private string _url;
    public string Url
    {
      get { return _url; }
      set { SetProperty(ref _url, value); }
    }

    private string _thumbnailUrl;
    public string ThumbnailUrl
    {
      get { return _thumbnailUrl; }
      set { SetProperty(ref _thumbnailUrl, value); }
    }

    private string _source;
    public string Source
    {
      get { return _source; }
      set { SetProperty(ref _source, value); }
    }

  }
}
