using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuoteClientWPF
{
  public class QuoteInformation : INotifyPropertyChanged
  {
    public QuoteInformation()
    {
      EnableRequest = true;
    }
    private string _quote;
    public string Quote
    {
      get
      {
        return _quote;
      }
      internal set
      {
        SetProperty(ref _quote, value);
      }
    }

    private bool _enableRequest;
    public bool EnableRequest
    {
      get
      {
        return _enableRequest;
      }
      internal set
      {
        SetProperty(ref _enableRequest, value);
      }
    }

    private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
      if (!EqualityComparer<T>.Default.Equals(field, value))
      {
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
