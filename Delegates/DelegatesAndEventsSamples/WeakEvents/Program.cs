using System.Windows;

namespace Wrox.ProCSharp.Delegates
{
  class Program
  {
    static void Main()
    {
      var dealer = new CarDealer();

      var daniel = new Consumer("Daniel");
      WeakEventManager<CarDealer, CarInfoEventArgs>.AddHandler(dealer, "NewCarInfo", daniel.NewCarIsHere);

      dealer.NewCar("Mercedes");

      var sebastian = new Consumer("Sebastian");
      WeakEventManager<CarDealer, CarInfoEventArgs>.AddHandler(dealer, "NewCarInfo", sebastian.NewCarIsHere);

      dealer.NewCar("Ferrari");

      WeakEventManager<CarDealer, CarInfoEventArgs>.RemoveHandler(dealer, "NewCarInfo", daniel.NewCarIsHere);

      dealer.NewCar("Red Bull Racing");
    }
  }
}
