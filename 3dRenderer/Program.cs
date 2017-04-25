using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Globalization;

namespace _3dRenderer
{
    //class Transformation
    //{
    //    private double[,] TransformationMatrix;
    //    private List<Point> polygon;
    //    // x y w, w = 1 (point)
    //    private double[] unicoord;
    //    private double Tx, Ty, Sx, Sy;
    //    private double phi;

    //    public Transformation(List<Point> polygon, double Tx = 2, double Ty = 2, double Sx = 3, double Sy = 3, double phi = 1.5)
    //    {
    //        this.polygon = polygon;
    //        this.Tx = Tx;
    //        this.Ty = Ty;
    //        this.Sx = Sx;
    //        this.Sy = Sy;
    //        this.phi = phi;

    //    }
    //    public double[] MultiplyMatrix(double[] A, double[,] B)
    //    {
    //        int c1 = A.GetLength(0);
    //        int r2 = B.GetLength(0); int c2 = B.GetLength(1);
    //        double tmp = 0;
    //        double[] kHasil = new double[c2];
    //        if (c1 != r2)
    //        {
    //            Console.WriteLine("Matrices can not be multiplied.");
    //            return new double[] { 1 };
    //        }
    //        else
    //        {
    //            for (int j = 0; j < c2; j++)
    //            {
    //                tmp = 0;
    //                for (int k = 0; k < c1; k++)
    //                {
    //                    tmp += A[k] * B[k, j];
    //                }
    //                kHasil[j] = tmp;
    //            }
    //            return kHasil;
    //        }
    //    }
    //}
/*
    class Edge
    {
        private double Y, Y2;
        public double y { get { return this.Y; } }
        public double y2 { get { return this.Y2; } }

        private double dX, X;
        public double x { get { return this.X; } set { this.X = value; } }
        public double dx { get { return this.dX; } }

        public Edge(int x1, int y1, int x2, int y2)
        {
            int x_1, y_1, x_2, y_2;

            // y2 must be >= than y1
            if (y1 < y2)
            {
                x_1 = x1; y_1 = y1; x_2 = x2; y_2 = y2;
            }
            else
            {
                x_1 = x2; y_1 = y2; x_2 = x1; y_2 = y1;
            }

            this.Y = y_1;
            this.Y2 = y_2;
            this.dX = (x_2 - x_1) / (double)(y_2 - y_1);
            this.X = x_1 + this.dX * (this.Y - y_1);
        }
    }
    */

    class Program
    {
        static Bitmap resultBmp;
        const int width = 2100;
        const int height = 2100;
        const int mult = 1000;
        static Random rnd;
        static Color[] randomColors = { Color.Red, Color.Blue, Color.Green, Color.Violet, Color.Yellow, Color.Brown, Color.Indigo,
                                        Color.Pink, Color.Purple, Color.DarkBlue, Color.ForestGreen };
        static int randomColorsCount = randomColors.Count();

        public static double[] MultiplyMatrix(double[] A, double[,] B)
        {
            int c1 = A.GetLength(0);
            int r2 = B.GetLength(0); int c2 = B.GetLength(1);
            double tmp = 0;
            double[] kHasil = new double[c2];
            if (c1 != r2)
            {
                Console.WriteLine("Matrices can not be multiplied.");
                return new double[] { 1 };
            }
            else
            {
                for (int j = 0; j < c2; j++)
                {
                    tmp = 0;
                    for (int k = 0; k < c1; k++)
                    {
                        tmp += A[k] * B[k, j];
                    }
                    kHasil[j] = tmp;
                }
                return kHasil;
            }
        }

        static void Main(string[] args)
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            rnd = new Random();

            resultBmp = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(resultBmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.Black, ImageSize);
            }
            var imageCoords = new List<Tuple<int, int>>();
            var triangleVertices = new List<Tuple<int, int, int>>();

            StreamReader inputFile = new StreamReader(@"african_head.obj");
            string line;
            string[] splitted;
            while ((line = inputFile.ReadLine()) != null)
            {
                splitted = line.Split();
                switch (splitted[0])
                {
                    case "v":
                        // "0.491 0.002" --> "491 2"
                        //int x = Convert.ToInt32((double.Parse(splitted[1]) * mult)) + mult;
                        //int y = Convert.ToInt32((double.Parse(splitted[2]) * mult)) + mult;
                        //int z = Convert.ToInt32((double.Parse(splitted[3]) * mult)) + mult;

                        double x = double.Parse(splitted[1]);
                        double y = double.Parse(splitted[2]);
                        double z = double.Parse(splitted[3]);

                        int c = 5;
                        //var rx = new double[,] { { 1, 0, 0; };
                        //var unicoord = new double[] { x, y, z, 1 };
                        //var TransformationMatrix = new double[,] { { 1, 0, 0, 0}, { 0, 1, 0, -1 / c }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
                        //double [] newcoord = MultiplyMatrix(unicoord, TransformationMatrix);
                        //double w = newcoord[3];
                        //double[] newnewcoord = new double[] { newcoord[0] / w, newcoord[1] / w };

                        //var rx = new double[,] { { Math.Cos(Math.PI / 4), -Math.Sin(Math.PI / 4), 0, 0 },
                        //                         { Math.Sin(Math.PI / 4), Math.Cos(Math.PI / 4), 0, 0 },
                        //                           { 0, 0, 1, 0 }, { 0, 0, 0, 1 }};
                        //var ry = new double[,] { { Math.Cos(Math.PI / 3), -Math.Sin(Math.PI / 3), 0, 0 },
                        //                         { Math.Sin(Math.PI / 3), Math.Cos(Math.PI / 3), 0, 0 },
                        //                           { 0, 0, 1, 0 }, { 0, 0, 0, 1 }};
                        double phi = Math.PI / 6;
                        double thetta = Math.PI / 4;
                        var T = new double[,] { { Math.Cos(phi), Math.Sin(phi) * Math.Sin(thetta), 0, 0 },
                                                { 0, Math.Sin(thetta), 0, 0 },
                                                { Math.Sin(phi), -Math.Cos(phi) * Math.Sin(thetta), 0, 0 },
                                                { 0, 0, 0, 1 } };
                        x = x + 0.1;
                        y = y + 0.1;
                        z = z + 0.1;
                        var unicoord = new double[] { x, y, z, 1 };
                        double[] newcoord = MultiplyMatrix(unicoord, T);

                        x = newcoord[0];
                        y = newcoord[1];
                        z = newcoord[2];

                        //PERSPECTIVE
                        double[] newnewcoord = new double[] { x / (1 - z / c), y / (1 - z / c), z / (1 - z / c) };
                        //double[] newnewcoord = new double[] { x / (c * z + 1), y / (c * z + 1), z / (c * z + 1) };




                        int x1 = Convert.ToInt32(newnewcoord[0] * mult) + mult;
                        int y1 = Convert.ToInt32(newnewcoord[1] * mult) + mult;

                        imageCoords.Add(new Tuple<int, int>(x1, y1));
                        break;
                    case "f":
                        // "23/45/183" --> "23"
                        string[] numbers1 = splitted[1].Split('/');
                        string[] numbers2 = splitted[2].Split('/');
                        string[] numbers3 = splitted[3].Split('/');
                        int v1 = Convert.ToInt32(numbers1[0]) - 1; // numbering from 0
                        int v2 = Convert.ToInt32(numbers2[0]) - 1;
                        int v3 = Convert.ToInt32(numbers3[0]) - 1;
                        triangleVertices.Add(new Tuple<int, int, int>(v1, v2, v3));
                        break;
                    default:
                        break;
                }
                //break;
            }

            for (int i = 0; i < triangleVertices.Count; i++)
            {
                var triangle = triangleVertices[i];
                bool isArea = false;

                // ----Comment if you want ActiveEdge!------
                double area = (imageCoords[triangle.Item1].Item1 * imageCoords[triangle.Item2].Item2 + imageCoords[triangle.Item2].Item1 * imageCoords[triangle.Item3].Item2 +
                    imageCoords[triangle.Item3].Item1 * imageCoords[triangle.Item1].Item2 - imageCoords[triangle.Item2].Item1 * imageCoords[triangle.Item1].Item2 -
                    imageCoords[triangle.Item3].Item1 * imageCoords[triangle.Item2].Item2 - imageCoords[triangle.Item1].Item1 * imageCoords[triangle.Item3].Item2);
                if (area > 100)
                {
                    isArea = true;
                }
                // ----------------------------------------

                DrawTriangle(imageCoords[triangle.Item1], imageCoords[triangle.Item2],
                             imageCoords[triangle.Item3], isArea);
            }


            //resultBmp.SetPixel(1047, 104, Color.Red);
            //resultBmp.SetPixel(1138, 806, Color.Red);
            //resultBmp.SetPixel(1080, 756, Color.Red);
            //resultBmp.SetPixel(1040, 329, Color.Red);
            resultBmp.Save("afro.png", ImageFormat.Png);
        }

        public static void Swap<T>(ref T x, ref T y)
        {
            T tmp = x;
            x = y;
            y = tmp;
        }

        public static void Bresenham(int x0, int y0, int x1, int y1, Color c)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap<int>(ref x0, ref y0);
                Swap<int>(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap<int>(ref x0, ref x1);
                Swap<int>(ref y0, ref y1);
            }
            int dX = x1 - x0;
            int dY = Math.Abs(y1 - y0);
            int d = dX / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;

            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                    resultBmp.SetPixel(Math.Abs(y) % width, Math.Abs(x) % height, c);
                else
                    resultBmp.SetPixel(Math.Abs(x) % width, Math.Abs(y) % height, c);
                d = d - dY;
                if (d < 0)
                {
                    y += ystep;
                    d += dX;
                }
            }
        }

        static void DrawTriangle(Tuple<int, int> p1, Tuple<int, int> p2, Tuple<int, int> p3, bool isArea)
        {
            Color color = randomColors[rnd.Next() % randomColorsCount];
            //Color color = Color.Yellow;
            Bresenham(p1.Item1, height - p1.Item2 - 1, p2.Item1, height - p2.Item2 - 1, Color.Yellow);
            Bresenham(p1.Item1, height - p1.Item2 - 1, p3.Item1, height - p3.Item2 - 1, Color.Yellow);
            Bresenham(p2.Item1, height - p2.Item2 - 1, p3.Item1, height - p3.Item2 - 1, Color.Yellow);

            // ----Comment if you want ActiveEdge!------
            //int centerX = (p1.Item1 + p2.Item1 + p3.Item1) / 3;
            //int centerY = (height - p1.Item2 + height - p2.Item2 + height - p3.Item2) / 3;
            //Color currColor = resultBmp.GetPixel(centerX, centerY);
            //if (currColor == Color.FromArgb(255, 0, 0, 0) && isArea)
            //{
            //    RecursiveFill(new Point(centerX, centerY), Color.FromArgb(255, 0, 0, 0), color);
            //}
            //--------------------------------------------

            // --- Uncomment if you want ActiveEdge! ------
            //List<Edge> edges = new List<Edge>();
            //edges.Add(new Edge(p1.Item1, height - p1.Item2 - 1, p2.Item1, height - p2.Item2 - 1));
            //edges.Add(new Edge(p1.Item1, height - p1.Item2 - 1, p3.Item1, height - p3.Item2 - 1));
            //edges.Add(new Edge(p2.Item1, height - p2.Item2 - 1, p3.Item1, height - p3.Item2 - 1));
            //ActiveEdgeFill(edges, color);
            // --------------------------------------------
        }

        static void RecursiveFill(Point pt, Color targetColor, Color replacementColor)
        {
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();

                if (a.X < resultBmp.Width && a.X > 0 && a.Y < resultBmp.Height && a.Y > 0)
                {
                    if (resultBmp.GetPixel(a.X, a.Y) == targetColor)
                    {
                        resultBmp.SetPixel(a.X, a.Y, replacementColor);
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            return;
        }

        static void ActiveEdgeFill(List<Edge> edges, Color c)
        {
            edges = edges.OrderBy(i => i.y).ToList();
            List<Edge> CAP = new List<Edge>();
            double y = edges[0].y;

            do
            {
                foreach (var e in edges)
                {
                    if (e.y == y) CAP.Add(e);
                }

                CAP = CAP.OrderBy(i => i.x).ToList();
                edges.RemoveAll(e => e.y == y);

                for (int i = 0; i < CAP.Count - 1; i++)
                {
                    double x = CAP[i].x;
                    while (x <= CAP[i + 1].x)
                        resultBmp.SetPixel((int)x++, (int)y, c);
                }

                y++;

                CAP.RemoveAll(e => e.y2 < y);
                foreach (var e in CAP)
                {
                    e.x += e.dx;
                    CAP = CAP.OrderBy(j => j.x).ToList();
                }
            } while (CAP.Any());
        }
    }
}