using System;

namespace Wrox.ProCSharp.WinServices
{
  [Serializable]
  public class QuoteException : Exception
  {
    public QuoteException() { }
    public QuoteException(string message) : base(message) { }
    public QuoteException(string message, Exception inner) : base(message, inner) { }
    protected QuoteException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context)
      : base(info, context)
    { }
  }
}
