using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer
{
    public class Util
    {
        public const float EPSILON = 0.00001f;
        public static bool NearlyEqual(double a, double b) => Math.Abs(a - b) < EPSILON;

        private Util() { }
    }
}
