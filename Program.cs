using System;
using System.Runtime.InteropServices;

using ray_tracer.Enviro;
using ray_tracer.Output;
using ray_tracer.RayMath;
using ray_tracer.Shapes;
using Tuple = ray_tracer.RayMath.Tuple;

namespace ray_tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            var r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            var s = new Sphere();
            var intersections = r.IntersectSphere(s);

            foreach (var i in intersections) Console.WriteLine(i.Time);
            Console.ReadLine();
        }

        void DrawAnalogDots()
        {
            Canvas c = new Canvas(300, 300);
            double clockWidth = c.Width / 3;

            Point[] points = new Point[12];
            points[0] = new Point(0, 1, 0);

            for (int i = 1; i < points.Length; i++)
            {
                points[i] = points[i - 1].Rotate((2 * Math.PI) / 12, RotationAxes.Z);
            }

            for (int i = 0; i < points.Length; i++)
            {
                points[i].x *= clockWidth;
                points[i].x += c.Width / 2;
                points[i].y *= clockWidth;
                points[i].y += c.Width / 2;
            }

            foreach (Point p in points)
            {
                //Console.WriteLine(string.Format("Writing pixel @{0}", p));
                c.WritePixel(((int)p.x), ((int)p.y), Colour.Bases.White);
            }
            PPM.RenderCanvas("clock", c);
        }

        void CuteRayShooter()
        {
            Canvas c = new Canvas(900, 550);

            // projectile that moves 1u per tick
            var p = new Projectile(new Point(0, 1, 0), new Vector(1, 1.8, 0).Unit * 11.25);

            // enviro with grav at -0.1u per tick and wind etc.
            var e = new Enviro.Environment(new Vector(0, -0.1, 0), new Vector(-0.01, 0, 0));

            // Simulate
            int t = 0;
            while (p.Position.y > 0)
            {
                p = Tick(e, p);
                t++;

                int canvX = (int)p.Position.x;
                int canvY = c.Height - (int)p.Position.y;
                c.WritePixel(canvX, canvY, Colour.Bases.Magenta);
            }

            // Render to file
            PPM.RenderCanvas("meme", c);
        }

        // Show how a projectile will move after an arbitrary tick time.
        Projectile Tick(Enviro.Environment env, Projectile proj)
        {
            Point newPos = proj.Position + proj.Velocity;
            Vector newVel = proj.Velocity + env.Gravity + env.Wind;

            return new Projectile(newPos, newVel);
        }
    }
}
