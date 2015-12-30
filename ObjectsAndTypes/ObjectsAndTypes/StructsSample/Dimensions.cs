using System;

namespace StructsSample
{
    public struct Dimensions
    {
        public double Length { get; set;  }
        public double Width { get; set;  }

        public Dimensions(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public double Diagonal => Math.Sqrt(Length * Length + Width * Width);

    }
}