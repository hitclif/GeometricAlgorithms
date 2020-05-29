using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SweepLine
{
    [DebuggerDisplay("{ToString()}")]
    public class Status
    {
        private SegmentComparer _comparer = new SegmentComparer();
        private List<Segment> _segments = new List<Segment>();

        public int Size => _segments.Count;

        public Segment At(int index)
        {
            return _segments[index];
        }
        public Segment TryGetAt(int index) => index < 0 || index >= _segments.Count ? null : _segments[index];
        public Segment TryGetLeft(int fromIndex) => this.TryGetAt(fromIndex - 1);
        public Segment TryGetRight(int fromIndex) => this.TryGetAt(fromIndex + 1);

        public int Find(Segment segment, Point intersection)
        {
            _comparer.SetTime(intersection.Y + 0.00001);
            var index = _segments.BinarySearch(segment, _comparer);
            return index;
        }

        public int Add(Segment segment)
        {
            _comparer.SetTime(segment.Start.Y);
            var index = _segments.BinarySearch(segment, _comparer);
            if(index < 0)
            {
                index = ~index;
            }

            _segments.Insert(index, segment);

            return index;
        }

        public int Remove(Segment segment)
        {
            _comparer.SetTime(segment.End.Y);
            var index = _segments.BinarySearch(segment, _comparer);
            _segments.RemoveAt(index);
            return index;
        }

        public void Swap(int leftIndex, int rightIndex)
        {
            var temp = _segments[leftIndex];
            _segments[leftIndex] = _segments[rightIndex];
            _segments[rightIndex] = temp;
        }

        public override string ToString()
        {
            return string.Join(" ", _segments.Select(s => s.Name));
        }
    }
}
