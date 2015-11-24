using DataLib;
using System;
using System.Collections.Generic;

namespace EnumerableSample
{
  public static class ChampionshipExtension
  {
    public static IEnumerable<RacerInfo> GetRacerInfo(this IEnumerable<Championship> source)
    {
      Func<string, string[]> split = s =>
      {
        string[] result = new string[2];
        int ix = s.LastIndexOf(' ');
        result[0] = s.Substring(0, ix);
        result[1] = s.Substring(ix + 1);
        return result;
      };

      Func<int, int, string[], RacerInfo> getRacerInfo = (year, place, names) => new RacerInfo { Year = year, Position = place, FirstName = names[0], LastName = names[1] };

      foreach (var item in source)
      {
        yield return getRacerInfo(item.Year, 1, split(item.First));
        yield return getRacerInfo(item.Year, 2, split(item.Second));
        yield return getRacerInfo(item.Year, 3, split(item.Third));
      }
    }
  }
}