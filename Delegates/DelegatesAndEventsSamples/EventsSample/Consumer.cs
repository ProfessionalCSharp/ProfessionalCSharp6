using static System.Console;

namespace Wrox.ProCSharp.Delegates
{
  public class Consumer
  {
    private string _name;

    public Consumer(string name)
    {
      _name = name;
    }

    public void NewCarIsHere(object sender, CarInfoEventArgs e)
    {
      WriteLine($"{_name}: car {e.Car} is new");
    }
  }
}
