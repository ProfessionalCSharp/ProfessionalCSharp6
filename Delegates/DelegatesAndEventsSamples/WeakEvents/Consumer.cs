using System;
using System.Windows;
using static System.Console;

namespace Wrox.ProCSharp.Delegates
{
  public class Consumer : IWeakEventListener
  {
    private string _name;

    public Consumer(string name)
    {
      this._name = name;
    }

    public void NewCarIsHere(object sender, CarInfoEventArgs e)
    {
      WriteLine($"{_name}: car {e.Car} is new");
    }

    bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
    {
      NewCarIsHere(sender, e as CarInfoEventArgs);
      return true;
    }


  }
}
