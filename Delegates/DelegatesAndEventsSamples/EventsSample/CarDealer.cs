using System;
using static System.Console;

namespace Wrox.ProCSharp.Delegates
{
  public class CarInfoEventArgs : EventArgs
  {
    public CarInfoEventArgs(string car)
    {
      Car = car;
    }

    public string Car { get; }
  }

  public class CarDealer
  {
    public event EventHandler<CarInfoEventArgs> NewCarInfo;

    public void NewCar(string car)
    {
      WriteLine($"CarDealer, new car {car}");

      NewCarInfo?.Invoke(this, new CarInfoEventArgs(car));
    }

  }
}
