using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiledBindingSample.Models
{
    public class SomeData
    {
        public int Number { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
    }

    public class SomeDataFactory
    {
        public static IEnumerable<SomeData> GetSampleData(int count)
        {
            Random r = new Random();
            return Enumerable.Range(0, count).Select(x => new SomeData
            {
                Number = r.Next(1000),
                Text1 = $"first data {x}",
                Text2 = $"second data {x}",
                Text3 = $"third data {x}"
            });
        }
    }
}
