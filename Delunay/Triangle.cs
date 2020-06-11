using System.Diagnostics;

namespace Delunay
{
    [DebuggerDisplay("{A}|{B}|{C}")]
    public class Triangle
    {
        public Triangle(Edge a, Edge b, Edge c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Edge A { get; }
        public Edge B { get; }
        public Edge C { get; }
    }
}
