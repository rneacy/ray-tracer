using ray_tracer.Shapes;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ray_tracer.RayMath
{
    public class Ray
    {
        public Point origin;
        public Vector direction;

        public Ray (Point origin, Vector direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public Point PositionAt(double time) => origin + direction * time;

        public IntersectionList IntersectSphere(Sphere sphere)
        {
            Vector sphereToRay = origin - new Point();
            var a = Vector.Dot(direction, direction);
            var b = 2 * Vector.Dot(direction, sphereToRay);
            var c = Vector.Dot(sphereToRay, sphereToRay) - 1;

            var disc = Math.Pow(b, 2) - 4 * a * c;

            // Miss
            if (disc < 0) {
                return new IntersectionList();
            }

            var t1 = (-b - Math.Sqrt(disc)) / (2 * a);
            var t2 = (-b + Math.Sqrt(disc)) / (2 * a);

            // Return in increasing order
            return (t2 > t1) ?
                new IntersectionList { new Intersection(sphere, t1), new Intersection(sphere, t2) } :
                new IntersectionList { new Intersection(sphere, t2), new Intersection(sphere, t1) };
        }

        public class Intersection
        {
            public Shape Shape;
            public double Time;

            public Intersection(Shape shape, double time)
            {
                Shape = shape;
                Time = time;
            }

            public override string ToString()
            {
                return string.Format("I: shape {0} @ time {1}.", Shape, Time);
            }
        }
    }
}
