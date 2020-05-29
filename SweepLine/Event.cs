using System;
using System.Diagnostics;

namespace SweepLine
{
    public enum EventType
    {
        Begin = 0,
        Intersection = 1,
        End = 2
    }

    [DebuggerDisplay("{DebugDisplay()}")]
    public class Event : IComparable<Event>
    {
        private Event(Point point, EventType eventType, Segment segment1, Segment segment2)
        {
            this.Point = point;
            this.EventType = eventType;
            this.Segment1 = segment1;
            this.Segment2 = segment2;
        }

        public Point Point { get; }
        public EventType EventType { get; }
        public Segment Segment1 { get; }
        public Segment Segment2 { get; }

        public static Event Begin(Segment segment)
        {
            return new Event(segment.Start, EventType.Begin, segment, null);
        }

        public static Event End(Segment segment)
        {
            return new Event(segment.End, EventType.End, segment, null);
        }

        public static Event Intersection(Segment left, Segment right, Point intersectionPoint)
        {
            return new Event(intersectionPoint, EventType.Intersection, left, right);
        }

        public int CompareTo(Event other)
        {
            var sign = -this.Point.Y.CompareTo(other.Point.Y);
            if(sign != 0)
            {
                return sign;
            }

            sign = this.Point.X.CompareTo(other.Point.X);
            if(sign != 0)
            {
                return sign;
            }

            sign = this.EventType - other.EventType;
            return sign;
        }

        public string DebugDisplay()
        {
            switch (this.EventType)
            {
                case EventType.Begin:
                    return $"B:{this.Segment1.Name}";
                case EventType.Intersection:
                    return $"I:{this.Segment1.Name}x{this.Segment2.Name}";
                case EventType.End:
                    return $"E:{this.Segment1.Name}";
                default:
                    throw new Exception();
            }
        }
    }
}
