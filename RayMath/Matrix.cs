using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.RayMath
{
    public class Matrix
    {
        private double[,] matrix;

        public Matrix() : this(4, 4) { }
        public Matrix(int rows, int columns){
            matrix = new double[rows, columns];
        }
        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
        }
    }
}
