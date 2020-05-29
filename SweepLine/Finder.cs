using System.Collections.Generic;

namespace SweepLine
{
    public class Finder
    {
        private Status _status = new Status();
        private EventQueue _events = new EventQueue();

        public IntersectionsResult Process(IEnumerable<Segment> segments)
        {
            this.InitializeEventQueue(segments);
            var result = new IntersectionsResult();

            while (_events.HasEvents)
            {
                var @event = _events.Pop();
                result.Handle(@event);

                switch (@event.EventType)
                {
                    case EventType.Begin:
                        this.AddSegment(@event.Segment1);
                        break;
                    case EventType.End:
                        this.RemoveSegment(@event.Segment1);
                        break;
                    case EventType.Intersection:
                        this.SwapSegments(@event.Segment1, @event.Segment2, @event.Point);
                        break;
                }
            }

            return result;
        }

        private void AddSegment(Segment newSegment)
        {
            var index = _status.Add(newSegment);

            var left = _status.TryGetLeft(index);
            var right = _status.TryGetRight(index);

            this.RemoveIntersectionEventIfAny(left, right, newSegment.Start.Y);

            this.AddIntersectionEventIfAny(left, newSegment, newSegment.Start.Y);
            this.AddIntersectionEventIfAny(newSegment, right, newSegment.Start.Y);
        }

        private void RemoveSegment(Segment segment)
        {
            var index = _status.Remove(segment);

            var left = _status.TryGetLeft(index);
            var right = _status.TryGetAt(index);

            this.AddIntersectionEventIfAny(left, right, segment.End.Y);
        }

        private void SwapSegments(Segment left, Segment right, Point intersectionPoint)
        {
            var leftIndex = _status.Find(left, intersectionPoint);
            var rightIndex = _status.Find(right, intersectionPoint);

            var leftNeighbour = _status.TryGetLeft(leftIndex);
            var rightNeighbour = _status.TryGetRight(rightIndex);

            this.RemoveIntersectionEventIfAny(leftNeighbour, left, intersectionPoint.Y);
            this.RemoveIntersectionEventIfAny(right, rightNeighbour, intersectionPoint.Y);
            this.AddIntersectionEventIfAny(leftNeighbour, right, intersectionPoint.Y);
            this.AddIntersectionEventIfAny(left, rightNeighbour, intersectionPoint.Y);

            _status.Swap(leftIndex, rightIndex);
        }

        private void RemoveIntersectionEventIfAny(Segment left, Segment right, double ytime)
        {
            if(left == null || right == null)
            {
                return;
            }

            var intersection = left.Intersection(right);
            if(intersection == Point.Null || intersection.Y > ytime)
            {
                return;
            }

            var @event = Event.Intersection(left, right, intersection);
            _events.Dismiss(@event);
        }

        private void AddIntersectionEventIfAny(Segment left, Segment right, double ytime)
        {
            if(left == null || right == null)
            {
                return;
            }

            var intersection = left.Intersection(right);
            if (intersection == Point.Null || intersection.Y > ytime)
            {
                return;
            }

            var @event = Event.Intersection(left, right, intersection);
            _events.Queue(@event);
        }

        private void InitializeEventQueue(IEnumerable<Segment> segments)
        {
            foreach(var segment in segments)
            {
                _events.Queue(Event.Begin(segment));
                _events.Queue(Event.End(segment));
            }
        }
    }
}
