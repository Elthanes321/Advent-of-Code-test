using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Assignment
    {
        List<int> r1;
        List<int> r2;

        public Assignment(string input)
        {
            var elves = input.Split(",");
            var points = elves[0].Split("-");
            r1 = Enumerable.Range(int.Parse(points[0]),1+ int.Parse(points[1]) - int.Parse(points[0])).ToList();
            points = elves[1].Split("-");
            r2 = Enumerable.Range(int.Parse(points[0]),1+ int.Parse(points[1]) - int.Parse(points[0])).ToList();
        }

        public bool IsFullyContained()
        {
            bool ret = r1.All(x => r2.Contains(x)) || r2.All(x => r1.Contains(x));
            return ret;
        }

        public bool AnyOverlap()
        {
            bool ret = r1.Any(x => r2.Contains(x)) || r2.Any(x => r1.Contains(x));
            return ret;
        }

    }
}
