using System.Collections.Generic;
using static System.Console;

namespace ListSamples
{
    public class Program
  {
    public void Main()
    {
      var graham = new Racer(7, "Graham", "Hill", "UK", 14);
      var emerson = new Racer(13, "Emerson", "Fittipaldi", "Brazil", 14);
      var mario = new Racer(16, "Mario", "Andretti", "USA", 12);

      var racers = new List<Racer>(20) { graham, emerson, mario };

      racers.Add(new Racer(24, "Michael", "Schumacher", "Germany", 91));
      racers.Add(new Racer(27, "Mika", "Hakkinen", "Finland", 20));

      racers.AddRange(new Racer[] {
               new Racer(14, "Niki", "Lauda", "Austria", 25),
               new Racer(21, "Alain", "Prost", "France", 51)});

      // insert elements

      racers.Insert(3, new Racer(6, "Phil", "Hill", "USA", 3));

      // accessing elements

      for (int i = 0; i < racers.Count; i++)
      {
        WriteLine(racers[i]);
      }

      foreach (var r in racers)
      {
        WriteLine(r);
      }

      // remove elements

      if (!racers.Remove(graham))
      {
        WriteLine("object not found in collection");
      }



      var racers2 = new List<Racer>(new Racer[] {
               new Racer(12, "Jochen", "Rindt", "Austria", 6),
               new Racer(22, "Ayrton", "Senna", "Brazil", 41) });
    }
  }
}
