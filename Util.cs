using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ray_tracer
{
    public class Util
    {
        public const float EPSILON = 0.00001f;
        public static bool NearlyEqual(double a, double b) => Math.Abs(a - b) < EPSILON;

        private static double _lastID = 0;

        public static IEnumerable<T> Flatten<T>(T[,] items)
        {
            for (int i = 0; i < items.GetLength(0); i++)
            {
                for (int j = 0; j < items.GetLength(1); j++)
                {
                    yield return items[i, j];
                }
            }
        }

        public static double DegToRad(double degrees)
            => (degrees / 360) * Math.PI;

        public static double NextID()
        {
            _lastID++;
            return _lastID - 1;
        }

        private Util() { }
    }
}
