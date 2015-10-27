using System;

namespace Wrox.ProCSharp.Generics
{
  public interface IAccount
  {
    decimal Balance { get; }
    string Name { get; }
  }


  public class Account : IAccount
  {
    public string Name { get; }
    public decimal Balance { get; private set; }

    public Account(string name, Decimal balance)
    {
      Name = name;
      Balance = balance;
    }
  }

}
