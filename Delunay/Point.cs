using System.Diagnostics;

namespace Delunay
{
    [DebuggerDisplay("{X},{Y}")]
    public class Point
    {
        public Point(int id, int x, int y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }

        public int Id { get; }
        public int X { get; }
        public int Y { get; }

        public static int SignedArea(Point a, Point b, Point c)
        {
            var area = a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y);
            return area;
        }
    }
}
