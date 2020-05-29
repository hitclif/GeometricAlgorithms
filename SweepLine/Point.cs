using System.Diagnostics;

namespace SweepLine
{
    [DebuggerDisplay("{X},{Y}")]
    public class Point
    {
        public static readonly Point Null = new Point(double.MaxValue, double.MaxValue);

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public static double SignedArea(Point a, Point b, Point c)
        {
            var area = a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y);
            return area / 2;
        }
    }
}
