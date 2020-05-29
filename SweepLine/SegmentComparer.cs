using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SweepLine
{
    public class SegmentComparer : IComparer<Segment>
    {
        private double _time = double.MinValue;

        public void SetTime(double t)
        {
            _time = t;
        }

        public int Compare([AllowNull] Segment x, [AllowNull] Segment y)
        {
            if(x == y)
            {
                return 0;
            }

            var xTx = x.CalculateXAt(_time);
            var yTx = y.CalculateXAt(_time);

            var diff = xTx - yTx;
            var sign = Math.Sign(diff);
            return sign;
        }
    }
}
