using System.Collections.Generic;

namespace Wrox.ProCSharp.Generics
{
  class Program
  {
    static void Main()
    {
      var accounts = new List<Account>()
      {
        new Account("Christian", 1500),
        new Account("Stephanie", 2200),
        new Account("Angela", 1800),
        new Account("Matthias", 2400)
      };

      decimal amount = Algorithms.AccumulateSimple(accounts);

      amount = Algorithms.Accumulate(accounts);

      amount = Algorithms.Accumulate<Account, decimal>(accounts, (item, sum) => sum += item.Balance);
    }
  }
}
