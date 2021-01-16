using System;
using System.Collections.Generic;
using System.Text;

using ray_tracer.RayMath;

namespace ray_tracer.Enviro
{
    public readonly struct Projectile
    {
        public Projectile(Point position, Vector velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public Point Position { get; }
        public Vector Velocity { get; }
    }

}
