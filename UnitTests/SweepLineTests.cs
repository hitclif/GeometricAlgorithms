using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SweepLine;

namespace UnitTests
{
    [TestClass]
    public class SweepLineTests
    {
        private Finder _testee = new Finder();


        [TestMethod]
        public void Sample1()
        {
            var coordinates = new[]
            {
                "-7 -3 2 6",
                "-7 2 9 -2",
                "3 -3 -3 6"
            };

            var result = this.Execute(coordinates);

            Assert.AreEqual(3, result.Intersections);
        }

        [TestMethod]
        public void Case1()
        {
            var data = new[]
            {
                "91 179 760 353",
                "874 890 648 114",
                "687 715 939 747",
                "703 692 2 675",
                "87 616 149 23",
                "463 450 878 233",
                "255 695 51 823",
                "580 716 271 427",
                "35 318 383 639",
                "439 750 850 558",
                "314 491 247 283",
                "701 107 364 127",
                "850 538 672 225",
                "897 914 172 214",
                "131 747 481 151",
                "634 896 233 68",
                "128 290 294 668",
                "215 444 206 148",
                "722 649 638 243",
                "832 799 409 469",
                "73 914 626 779",
                "308 601 828 75",
                "654 626 810 543",
                "780 139 932 188",
                "463 58 786 433"
            };

            var result = this.Execute(data);
            Console.WriteLine($"Intersections : {result.Intersections}");
            Console.WriteLine($"Events : {result.TotalEvents}");
            Console.WriteLine($"3rd : {result.EventType3}");
            Console.WriteLine($"17th : {result.EventType17}");
            Console.WriteLine($"99th : {result.EventType99}");
            
        }

        [TestMethod]
        public void Case2()
        {
            var data = new[]
            {
                "819 157 547 598",
                "305 393 433 214",
                "492 662 926 222",
                "963 583 487 32",
                "469 6 511 971",
                "815 474 980 881",
                "342 36 355 422",
                "642 15 383 721",
                "867 204 253 484",
                "16 87 188 873",
                "94 512 974 668",
                "912 328 793 509",
                "363 723 394 546",
                "376 404 389 978",
                "197 841 400 864",
                "581 82 185 537",
                "893 901 151 139",
                "956 487 166 958",
                "994 386 6 651",
                "983 416 782 443",
                "999 596 738 690",
                "19 836 987 851",
                "12 925 251 975",
                "972 926 529 562",
                "740 546 512 432"
            };

            var result = this.Execute(data);
            Console.WriteLine($"Intersections : {result.Intersections}");
            Console.WriteLine($"Events : {result.TotalEvents}");
            Console.WriteLine($"3rd : {result.EventType3}");
            Console.WriteLine($"17th : {result.EventType17}");
            Console.WriteLine($"99th : {result.EventType99}");

        }

        private IntersectionsResult Execute(IEnumerable<string> coordinates)
        {
            var segments = this.FromStrings(coordinates).ToArray();

            var svg = segments.ToSvg();

            var result = _testee.Process(segments);
            return result;
        }

        private IEnumerable<Segment> FromStrings(IEnumerable<string> coordinates)
        {
            var segments = coordinates
                .Select((s, i) =>
                {
                    var p = s.Trim()
                        .Split(" ")
                        .Select(v => Convert.ToInt32(v))
                        .ToArray();

                    return new Segment(i + 1, new Point(p[0], p[1]), new Point(p[2], p[3]));
                });
            return segments;
        }
    }
}
