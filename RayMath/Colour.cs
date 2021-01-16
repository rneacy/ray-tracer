using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.RayMath
{
    public class Colour : Point
    {
        public Colour() : base(0, 0, 0) { }

        public Colour(double r, double g, double b)
            : base(r, g, b) 
        {
            if (r > 1) x = 1;
            if (r < 0) x = 0;
            if (g > 1) y = 1;
            if (g < 0) y = 0;
            if (b > 1) z = 1;
            if (b < 0) z = 0;
        }

        public double Red => x;
        public double Green => y;
        public double Blue => z;

        public static Colour operator *(Colour a, Colour b)
            => new Colour(a.Red * b.Red, a.Green * b.Green, a.Blue * b.Blue);

        public struct Bases
        {
            public static Colour Red = new Colour(1, 0, 0);
            public static Colour Green = new Colour(0, 1, 0);
            public static Colour Blue = new Colour(0, 0, 1);

            public static Colour Magenta = new Colour(0.8, 0.1, 0.65);

            public static Colour Black = new Colour(0, 0, 0);
            public static Colour White = new Colour(1, 1, 1);
        }
    }
}
