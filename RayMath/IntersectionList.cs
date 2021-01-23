using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ray_tracer.RayMath
{
    public class IntersectionList : List<Ray.Intersection>
    {
        public Ray.Intersection Hit
        {
            get
            {
                if (Count == 0) return null;

                Ray.Intersection low = null;
                foreach(var i in this)
                {
                    if (i.Time < 0) continue;
                    if (low == null || i.Time < low.Time) low = i;
                }

                return low;
            }
        }
    }
}
