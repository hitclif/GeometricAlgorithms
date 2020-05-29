using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SweepLine
{
    [DebuggerDisplay("{DebugDisplay()}")]
    public class EventQueue
    {
        private EventComparer _comparer = new EventComparer();
        private List<Event> _events = new List<Event>();

        public bool HasEvents => _events.Count > 0;

        public void Queue(Event @event)
        {
            var index = _events.BinarySearch(@event, _comparer);
            if(index < 0)
            {
                index = ~index;
            }

            _events.Insert(index, @event);
        }

        public Event Pop()
        {
            var @event = _events[0];
            _events.RemoveAt(0);
            return @event;
        }

        public void Dismiss(Event @event)
        {
            var index = _events.BinarySearch(@event, _comparer);
            _events.RemoveAt(index);
        }

        private string DebugDisplay()
        {
            return string.Join(" | ", _events.Select(e => e.DebugDisplay()));
        }

        private class EventComparer : IComparer<Event>
        {
            public int Compare([AllowNull] Event x, [AllowNull] Event y)
            {
                var sign = -x.Point.Y.CompareTo(y.Point.Y);
                if (sign != 0)
                {
                    return sign;
                }

                sign = x.Point.X.CompareTo(y.Point.X);
                if (sign != 0)
                {
                    return sign;
                }

                sign = x.EventType - y.EventType;
                return sign;
            }
        }
    }
}
