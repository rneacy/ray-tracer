using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.Shapes
{
    public abstract class Shape
    {
        protected double ID => Util.NextID();

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
