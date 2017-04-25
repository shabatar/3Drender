using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dRenderer
{
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

}
