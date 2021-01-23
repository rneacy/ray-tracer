using System;

namespace ray_tracer.RayMath
{
    public class Tuple : IEquatable<Tuple>
    {
        public double x, y, z, w;

        public Tuple() : this(0, 0, 0, 0) { }
        public Tuple(double x, double y, double z) : this(x, y, z, 1) { }
        public Tuple(double x, double y, double z, double w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        

        // the fact this exists makes me want to die
        public double[] AsArray => new double[] { x, y, z, w };

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Tuple);
        }

        public bool Equals(Tuple other)
        {
            return
                Util.NearlyEqual(x, other.x) &&
                Util.NearlyEqual(y, other.y) &&
                Util.NearlyEqual(z, other.z);
                //Util.NearlyEqual(w, other.w);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public override string ToString()
        {
            return string.Format("[{0},{1},{2}]", x, y, z);
        }

        public static bool operator ==(Tuple a, Tuple b) => a.Equals(b);
        public static bool operator !=(Tuple a, Tuple b) => !(a == b);
    }

    public class Point : Tuple, ITransformable<Point>, ITranslatable<Point>
    {
        public Point() : base(0, 0, 0) { }

        public Point(double x, double y, double z) 
            : base(x, y, z, 1) { }

        public static Point operator +(Point a, Vector b) =>
            new Point(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Point operator -(Point a, Vector b) => // Sub vec from point
            new Point(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Vector operator -(Point a, Point b) => // Sub point from point
            new Vector(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Point operator -(Point a) => new Point(-a.x, -a.y, -a.z);

        public static Point operator *(Point a, double scalar)
            => new Point(a.x * scalar, a.y * scalar, a.z * scalar);

        public static Point operator /(Point a, double scalar)
            => new Point(a.x / scalar, a.y / scalar, a.z / scalar);

        // EVENTUALLY REMOVE THIS DISGUSTING DUPLICATION OMG
        public Point Translate(Vector v)
        {
            var tM = Matrix.TransformIdentity(v);
            return tM * this;
        }
        public Point Scale(Vector v)
        {
            var sM = Matrix.ScaleIdentity(v);
            return sM * this;
        }
        public Point Rotate(double rads, RotationAxes axis)
        {
            var rM = Matrix.RotateIdentity(rads, axis);
            return rM * this;
        }
        public Point Shear(double xByY, double xByZ, double yByX, double yByZ, double zByX, double zByY)
        {
            var rM = Matrix.ShearIdentity(xByY, xByZ, yByX, yByZ, zByX, zByY);
            return rM * this;
        }
    }

    public class Vector : Tuple, ITransformable<Vector>
    {
        public Vector(double x, double y, double z)
            : base(x, y, z, 0) { }

        public static Point operator +(Vector a, Point b) =>
            new Point(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector operator +(Vector a, Vector b) =>
            new Vector(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector operator -(Vector a, Vector b) =>
            new Vector(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Vector operator -(Vector a) => new Vector(-a.x, -a.y, -a.z);

        public static Vector operator *(Vector a, double scalar) 
            => new Vector(a.x * scalar, a.y * scalar, a.z * scalar);

        public static Vector operator /(Vector a, double scalar)
            => new Vector(a.x / scalar, a.y / scalar, a.z / scalar);

        // The magnitude of this vector based on Pythagoras' theorem.
        public double Magnitude => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

        // The normalised version of this vector (unit vector)
        public Vector Unit => new Vector(x / Magnitude, y / Magnitude, z / Magnitude);

        // Simple dot product of two vectors
        public static double Dot(Vector a, Vector b) 
            => a.x * b.x + a.y * b.y + a.z * b.z;

        // Simple cross product of two vectors
        public static Vector Cross(Vector a, Vector b)
            => new Vector(
                a.y * b.z - a.z * b.y, 
                a.z * b.x - a.x * b.z, 
                a.x * b.y - a.y * b.x
                );

        public Vector Scale(Vector v)
        {
            var sM = Matrix.ScaleIdentity(v);
            return sM * this;
        }
        public Vector Rotate(double rads, RotationAxes axis)
        {
            var rM = Matrix.RotateIdentity(rads, axis);
            return rM * this;
        }
        public Vector Shear(double xByY, double xByZ, double yByX, double yByZ, double zByX, double zByY)
        {
            var rM = Matrix.ShearIdentity(xByY, xByZ, yByX, yByZ, zByX, zByY);
            return rM * this;
        }
    }
}
