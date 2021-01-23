using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ray_tracer.RayMath
{
    public class Matrix : IEquatable<Matrix>
    {
        private double[,] _matrix;

        public Matrix() : this(4, 4) { }
        public Matrix(int size) : this(size, size) { }
        public Matrix(int rows, int columns) {
            _matrix = new double[rows, columns];
        }
        public Matrix(double[,] matrix)
        {
            this._matrix = matrix;
        }

        public int NRows => _matrix.GetLength(0);
        public int NColumns => _matrix.GetLength(1);

        public override bool Equals(object obj) => this.Equals(obj as Matrix);
        public bool Equals(Matrix m)
            => Enumerable.SequenceEqual(Util.Flatten(_matrix), Util.Flatten(m._matrix));

        public static bool operator ==(Matrix a, Matrix b) => a.Equals(b);
        public static bool operator !=(Matrix a, Matrix b) => !(a == b);

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a._matrix.Length != a._matrix.Length)
            {
                throw new Exception("Matrices must be of equal size.");
            }

            double[,] res = new double[a._matrix.GetLength(0), a._matrix.GetLength(1)];

            // each row of a
            for (int i = 0; i < a._matrix.GetLength(0); i++)
            {
                // each col of b
                for (int j = 0; j < b._matrix.GetLength(1); j++)
                {
                    res[i, j] = 0;
                    for (int k = 0; k < a._matrix.GetLength(0); k++)
                    {
                        res[i, j] += a._matrix[i, k] * b._matrix[k, j];
                    }
                }
            }

            return new Matrix(res);
        }

        // fuck it this only works with 4x4 matrices for now bc yeah
        public static Point operator *(Matrix a, Point b)
        {
            double[] res = { 0, 0, 0, 0 };
            double[] tAsArr = b.AsArray;

            // each row of a
            for (int i = 0; i < 4; i++)
            {
                // each val in tuple
                for (int j = 0; j < 4; j++)
                {
                    res[i] += a._matrix[i, j] * tAsArr[j];
                }
            }

            return new Point(res[0], res[1], res[2]);
        }

        // cos i can't figure out how to properly type these yet
        public static Vector operator *(Matrix a, Vector b)
        {
            double[] res = { 0, 0, 0, 0 };
            double[] tAsArr = b.AsArray;

            // each row of a
            for (int i = 0; i < 4; i++)
            {
                // each val in tuple
                for (int j = 0; j < 4; j++)
                {
                    res[i] += a._matrix[i, j] * tAsArr[j];
                }
            }

            return new Vector(res[0], res[1], res[2]);
        }

        public override string ToString()
        {
            string s = "[";
            for(int i = 0; i < NRows; i++)
            {
                for(int j = 0; j < NColumns; j++)
                {
                    s += string.Format("\t{0}", _matrix[i, j]);
                }
                s += (i == NRows - 1) ? "\t]" : "\n";
            }

            return s;
        }

        public static Matrix Identity(int size)
        {
            double[,] inner = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    inner[i, j] = (i == j) ? 1 : 0;
                }
            }

            return new Matrix(inner);
        }

        public static Matrix TransformIdentity(Vector v)
            => TransformIdentity(v.x, v.y, v.z);

        public static Matrix TransformIdentity(double x, double y, double z)
        {
            Matrix id = Identity(4);
            id._matrix[0, 3] = x;
            id._matrix[1, 3] = y;
            id._matrix[2, 3] = z;
            return id;
        }

        public static Matrix ScaleIdentity(Vector v)
            => ScaleIdentity(v.x, v.y, v.z);

        public static Matrix ScaleIdentity(double x, double y, double z)
        {
            Matrix id = Identity(4);
            id._matrix[0, 0] = x;
            id._matrix[1, 1] = y;
            id._matrix[2, 2] = z;
            return id;
        }

        public static Matrix RotateIdentity(double rads, RotationAxes axis)
        {
            Matrix id = Identity(4);

            switch (axis)
            {
                case RotationAxes.X:
                    id._matrix[1, 1] = Math.Cos(rads);
                    id._matrix[1, 2] = -Math.Sin(rads);
                    id._matrix[2, 1] = Math.Sin(rads);
                    id._matrix[2, 2] = Math.Cos(rads);
                    break;
                case RotationAxes.Y:
                    id._matrix[0, 0] = Math.Cos(rads);
                    id._matrix[2, 0] = -Math.Sin(rads);
                    id._matrix[0, 2] = Math.Sin(rads);
                    id._matrix[2, 2] = Math.Cos(rads);
                    break;
                case RotationAxes.Z:
                    id._matrix[0, 0] = Math.Cos(rads);
                    id._matrix[0, 1] = -Math.Sin(rads);
                    id._matrix[1, 0] = Math.Sin(rads);
                    id._matrix[1, 1] = Math.Cos(rads);
                    break;
            }
            return id;
        }

        public static Matrix ShearIdentity(double xByY, double xByZ, double yByX, double yByZ, double zByX, double zByY)
        {
            Matrix id = Identity(4);
            id._matrix[0, 1] = xByY;
            id._matrix[0, 2] = xByZ;
            id._matrix[1, 0] = yByX;
            id._matrix[1, 2] = yByZ;
            id._matrix[2, 0] = zByX;
            id._matrix[2, 1] = zByY;
            return id;
        }

        public Matrix Transpose()
        {
            var a = _matrix.GetLength(0);
            var b = _matrix.GetLength(1);
            double[,] inner = new double[b, a];

            // for every row of mat
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    inner[j, i] = _matrix[i, j];
                }
            }

            return new Matrix(inner);
        }

        public Matrix Submatrix(int row, int col)
        {
            var a = _matrix.GetLength(0);
            var b = _matrix.GetLength(1);
            double[,] inner = new double[a - 1, b - 1];

            //r
            int innerI = 0;
            int innerJ = 0;

            for (int i = 0; i < a; i++)
            {
                if (i == row) continue;
                //c
                for (int j = 0; j < b; j++)
                {
                    if (j == col) continue;

                    inner[innerI, innerJ] = _matrix[i, j];
                    innerJ++;
                }

                innerI++;
                innerJ = 0;
            }

            return new Matrix(inner);
        }

        // only works for square matrices rn because i can't do maths
        public double Determinant
        {
            get
            {
                double det = 0;
                if(_matrix.GetLength(0) == 2)
                {
                    det = (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);
                }
                else
                {
                    for (int i = 0; i < _matrix.GetLength(0); i++)
                    {
                        det = det + _matrix[0, i] * Cofactor(0, i);
                    }
                }
                return det;
            }
        }

        public double Minor(int row, int col) => Submatrix(row, col).Determinant;

        public double Cofactor(int row, int col)
        {
            var m = Minor(row, col);
            return ((row + col) % 2 == 0) ? m : -m;
        }

        public bool IsInvertible => Determinant != 0;
        public Matrix Inverse
        {
            get
            {
                if (!IsInvertible) throw new Exception("Matrix does not have an inverse.");
                Matrix inverse = new Matrix(NRows, NColumns);

                for(int i = 0; i < NRows; i++)
                {
                    for(int j = 0; j < NColumns; j++)
                    {
                        var c = Cofactor(i, j);
                        inverse._matrix[j, i] = c / Determinant; // includes transposition
                    }
                }

                return inverse;
            }
        }
    }
}
