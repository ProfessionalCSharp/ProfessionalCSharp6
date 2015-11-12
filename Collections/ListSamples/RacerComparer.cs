using System;
using System.Collections.Generic;

namespace ListSamples
{
  public enum CompareType
  {
    FirstName,
    LastName,
    Country,
    Wins
  }

  public class RacerComparer : IComparer<Racer>
  {
    private CompareType _compareType;
    public RacerComparer(CompareType compareType)
    {
      _compareType = compareType;
    }

    public int Compare(Racer x, Racer y)
    {
      if (x == null && y == null) return 0;
      if (x == null) return -1;
      if (y == null) return 1;

      int result;
      switch (_compareType)
      {
        case CompareType.FirstName:
          return string.Compare(x.FirstName, y.FirstName);
        case CompareType.LastName:
          return string.Compare(x.LastName, y.LastName);
        case CompareType.Country:
          result = string.Compare(x.Country, y.Country);
          if (result == 0)
          {
            return string.Compare(x.LastName, y.LastName);
          }
          else
          {
            return result;
          }
        case CompareType.Wins:
          return x.Wins.CompareTo(y.Wins);
        default:
          throw new ArgumentException("Invalid Compare Type");
      }
    }
  }
}