using System;
using System.Diagnostics;
using System.Linq;

namespace SweepLine
{
    [DebuggerDisplay("{Name}:[{Start}]-[{End}]")]
    public class Segment
    {
        public Segment(int id, Point a, Point b)
        {
            this.Id = id;
            this.Name = $"S{id}";

            var points = new[] { a, b }
                .OrderByDescending(p => p.Y)
                .ThenBy(p => p.X)
                .ToArray();

            this.Start = points[0];
            this.End = points[1];
        }

        public int Id { get; }
        public Point Start { get; }
        public Point End { get; }
        public string Name { get; }

        public double CalculateXAt(double ty)
        {
            var p = (ty - this.Start.Y) / (this.End.Y - this.Start.Y);
            var tx = this.Start.X + (this.End.X - this.Start.X) * p;
            return tx;
        }

        public Point Intersection(Segment other)
        {
            var area1 = Point.SignedArea(this.Start, this.End, other.Start);
            var area2 = Point.SignedArea(this.Start, this.End, other.End);

            if(Math.Sign(area1)* Math.Sign(area2) == 1)
            {
                return Point.Null;
            }

            var area3 = Point.SignedArea(other.Start, other.End, this.Start);
            var area4 = Point.SignedArea(other.Start, other.End, this.End);

            if (Math.Sign(area3) * Math.Sign(area4) == 1)
            {
                return Point.Null;
            }

            var ip = CalculateIntersectionParameter(this, other);
            var point = this.PointAtParameter(ip);
            return point;
        }

        private Point PointAtParameter(double t)
        {
            var x = Math.Round(t * (this.End.X - this.Start.X) + this.Start.X, 6);
            var y = Math.Round(t * (this.End.Y - this.Start.Y) + this.Start.Y, 6);

            return new Point(x, y);
        }

        private static double CalculateIntersectionParameter(Segment u, Segment v)
        {
            var p1 = u.Start;
            var p2 = u.End;
            var p3 = v.Start;
            var p4 = v.End;
            var t = 1d *
                ((p1.X - p3.X) * (p3.Y - p4.Y) - (p1.Y - p3.Y) * (p3.X - p4.X))
                /
                ((p1.X - p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X - p4.X));

            return t;
        }
    }
}
