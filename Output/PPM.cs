using ray_tracer.Enviro;
using ray_tracer.RayMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ray_tracer.Output
{
    public class PPM
    {
        public const string MAGIC = "P3";
        public const int COLOURMAX = 255;

        // Converts canvas data into a viewable .ppm image.
        public static void RenderCanvas(Canvas c)
        {
            string header =
                string.Format("{0}\n{1} {2}\n{3}\n", MAGIC, c.Width, c.Height, COLOURMAX);

            System.IO.Directory.CreateDirectory(@"Output");
            System.IO.File.WriteAllText(@"Output\meme.ppm", header);

            int charsWritten = 0;

            using StreamWriter sw = File.AppendText(@"Output\meme.ppm");
            for (int i = 0; i < c.Height; i++)
            {
                string pixData = "";

                for (int j = 0; j < c.Width; j++)
                {
                    Point conv = c.GetPixel(j, i) * 255;

                    string thisPixel =
                        string.Format(
                            "{0} {1} {2} ",
                            Math.Round(conv.x),
                            Math.Round(conv.y),
                            Math.Round(conv.z)
                            );
                    pixData += thisPixel;

                    charsWritten += thisPixel.Length;
                    if (charsWritten >= 70)
                    {
                        pixData += "\n";
                        charsWritten = 0;
                    }
                }

                sw.Write(pixData);
            }

        }
        private PPM() { }
    }
}
