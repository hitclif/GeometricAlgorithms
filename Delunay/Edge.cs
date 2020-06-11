using System.Diagnostics;

namespace Delunay
{
    [DebuggerDisplay("{A}-{B}")]
    public class Edge
    {
        public Edge(Point a, Point b)
        {
            this.A = a;
            this.B = b;
        }

        public Point A { get; }
        public Point B { get; }
    }
}
