using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Rucksack
    {
        private char[] compartment1;
        private char[] compartment2;
        private char batch = '!';

        private Dictionary<char, int> prio =new();

        public Rucksack(string input)
        {
            prio.Add('!', 0);
            for (int i = 1; i < 27; i++)
            {
                prio.Add((char)('a' - 1 + i), i);
            }
            for (int i = 1; i < 27; i++)
            {
                prio.Add((char)('A' - 1+ i), i+26);
            }
            compartment1 = input.Substring(0, input.Length / 2).ToCharArray();
            compartment2 = input.Substring(input.Length / 2, input.Length / 2).ToCharArray();
        }

        public int GetMismatch()
        {
            var help = compartment1.Intersect(compartment2).ToArray();
            
            return help.Sum(x=> prio[x]);
        }

        public void setBatch(Rucksack[] rs)
        {
            string one = String.Join("", rs[0].compartment1) + String.Join("", rs[0].compartment2);
            string two = String.Join("", rs[1].compartment1) + String.Join("", rs[1].compartment2);
            string three = String.Join("", compartment1) + String.Join("", compartment2);

            batch = one.Intersect(two).Intersect(three).ToArray()[0];

        }

        public int GetBatch()
        {
            return prio[batch];
        }

    }
}
