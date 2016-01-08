using System.Xml.Linq;

namespace UnitTestingSamplesCore
{
    public class ChampionsLoader : IChampionsLoader
    {
        public XElement LoadChampions() => XElement.Load(F1Addresses.RacersUrl);
    }
}
