using System;
using System.Collections.Generic;
using System.Text;

using ray_tracer.RayMath;

namespace ray_tracer.Enviro
{
    public readonly struct Environment
    {
        public Environment(Vector gravity, Vector wind)
        {
            Gravity = gravity;
            Wind = wind;
        }

        public Vector Gravity { get; }
        public Vector Wind { get; }
    }

}
