using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.RayMath
{
    public class NoIntersectException : Exception
    {
        public NoIntersectException()
            :base ("No intersection found.") { }
    }
}
