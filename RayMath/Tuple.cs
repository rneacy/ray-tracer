using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ray_tracer
{
    public class Tuple : IEquatable<Tuple>
    {
        public float x, y, z, w;

        protected Tuple(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Tuple);
        }

        public bool Equals(Tuple other)
        {
            return
                Util.NearlyEqual(x, other.x) &&
                Util.NearlyEqual(y, other.y) &&
                Util.NearlyEqual(z, other.z) &&
                Util.NearlyEqual(w, other.w);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, w);
        }

        public static bool operator ==(Tuple a, Tuple b) => a.Equals(b);
        public static bool operator !=(Tuple a, Tuple b) => !(a == b);
    }

    public class Point : Tuple
    {
        public Point(float x, float y, float z) 
            : base(x, y, z, 1) { }

        public static Point operator +(Point a, Vector b) =>
            new Point(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Point operator -(Point a, Vector b) => // Sub vec from point
            new Point(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Vector operator -(Point a, Point b) => // Sub point from point
            new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public class Vector : Tuple
    {
        public Vector(float x, float y, float z)
            : base(x, y, z, 0) { }

        public static Point operator +(Vector a, Point b) =>
            new Point(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector operator +(Vector a, Vector b) =>
            new Vector(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector operator -(Vector a, Vector b) =>
            new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
    }
}
