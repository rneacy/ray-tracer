using ray_tracer.RayMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace ray_tracer.Enviro
{
    public class Canvas
    {
        public int Width { get; }
        public int Height { get; }

        private Colour[,] _pixels;
        public Colour GetPixel(int x, int y) => _pixels[x, y];

        public Canvas(int width, int height)
        { 
            Width = width;
            Height = height;

            _pixels = new Colour[width, height];
            Fill(Colour.Bases.Black);
        }

        // Writes a single pixel at x and y to a colour c.
        public void WritePixel(int x, int y, Colour c)
        {
            // Only render inbounds.
            if(x < Width && x >= 0 && y < Height && y >= 0)
            {
                _pixels[x, y] = c;
            }
        }

        // Sets every pixel to the same colour.
        public void Fill(Colour c)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _pixels[i, j] = c;
                }
            }
        }
    }
}
