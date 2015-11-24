using System.Collections.ObjectModel;

namespace Wrox.ProCSharp.Async
{
  public class SearchInfo : BindableBase
  {
    public SearchInfo()
    {
      _list = new ObservableCollection<SearchItemResult>();
      _list.CollectionChanged += delegate { OnPropertyChanged("List"); };
    }

    private string _searchTerm;

    public string SearchTerm
    {
      get { return _searchTerm; }
      set { SetProperty(ref _searchTerm, value); }
    }

    private ObservableCollection<SearchItemResult> _list;
    public ObservableCollection<SearchItemResult> List => _list;

  } 
}
