using System;
using static System.Console;

namespace ArraySegmentSample
{
    class Program
    {
        static void Main()
        {
            int[] ar1 = { 1, 4, 5, 11, 13, 18 };
            int[] ar2 = { 3, 4, 5, 18, 21, 27, 33 };
            var segments = new ArraySegment<int>[2]
            {
                new ArraySegment<int>(ar1, 0, 3),
                new ArraySegment<int>(ar2, 3, 3)
            };


            var sum = SumOfSegments(segments);
            WriteLine($"sum of all segments: {sum}");

        }

        static int SumOfSegments(ArraySegment<int>[] segments)
        {
            int sum = 0;
            foreach (var segment in segments)
            {
                for (int i = segment.Offset; i < segment.Offset + segment.Count; i++)
                {
                    sum += segment.Array[i];
                }

            }
            return sum;
        }
    }
}
