using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.RayMath
{
    public interface ITransformable<T>
    {
        T Scale(Vector v);
        T Rotate(double rads, RotationAxes axis);
        T Shear(double xByY, double xByZ, double yByX, double yByZ, double zByX, double zByY);
    }
}
