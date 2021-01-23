using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.RayMath
{
    public interface ITranslatable<T>
    {
        T Translate(Vector v);
    }
}
