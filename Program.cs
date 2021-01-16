using System;
using System.Runtime.InteropServices;

using ray_tracer.Enviro;
using ray_tracer.Output;
using ray_tracer.RayMath;

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
            CuteRayShooter();
        }

        void TestPPM()
        {
            //meme
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
            PPM.RenderCanvas(c);
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
