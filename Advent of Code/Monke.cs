using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Monke
    {
        private List<ulong> items;
        public int testclause;
        private uint TMonke;
        private uint FMonke;
        private char operation;
        private string opNumber;
        public int smodulo;
        private bool worry;
        private ulong inspections;

        public ulong Inspections { get => inspections; }

        public Monke(string[] desc, bool wor = true) 
        {
            worry = wor;
            inspections = 0;
            foreach(var line in desc)
            {
                if (line.Contains("Starting"))
                {
                    items = new List<ulong>();
                    string[] split = line.Split(':')[1].Split(',');
                    foreach(var s in split)
                    {
                        items.Add(uint.Parse(s.Trim()));
                    }
                }
                else if (line.Contains("Operation"))
                {
                    string[] split = line.Split(' ');
                    opNumber = split.Last();
                    operation = Char.Parse(split[split.Length - 2].Trim());
                }
                else if (line.Contains("Test"))
                {
                    testclause = int.Parse(line.Split(' ').Last().Trim());
                }
                else if (line.Contains("true"))
                {
                    TMonke = uint.Parse(line.Split(' ').Last().Trim());
                }
                else if (line.Contains("false"))
                {
                    FMonke = uint.Parse(line.Split(' ').Last().Trim());
                }
            }
        }

        private ulong[] CheckAndThrow()
        {
            inspections++;
            ulong[] ret = new ulong[2];
            ulong curItem = items.First();
            switch (operation)
            {
                case '*':
                    if (opNumber == "old")
                        curItem *= curItem;
                    else
                        curItem = curItem*uint.Parse(opNumber);
                    break;
                case '+':
                    if (opNumber == "old")
                        curItem += curItem;
                    else
                        curItem = curItem + uint.Parse(opNumber);
                    break;
                case '-':
                    if (opNumber == "old")
                        curItem -= curItem;
                    else
                        curItem = curItem - uint.Parse(opNumber);
                    break;
                case '/':
                    if (opNumber == "old")
                        curItem /= curItem;
                    else
                        curItem = curItem / uint.Parse(opNumber);
                    break;
            }
            if (worry)
                curItem /= 3;
            else
                curItem %= (uint)smodulo;
            if (curItem % (uint)testclause == 0)
            {
                ret[0] = TMonke;
            }
            else
            {
                ret[0] = FMonke;
            }
            ret[1] = curItem;
            return ret;
        }

        public List<ulong[]> TakeTurn()
        {
            List<ulong[]> ret = new();
            while(items.Count > 0)
            {
                ret.Add(CheckAndThrow());
                items.RemoveAt(0);
            }
            return ret;
        }


        public void AddItem(ulong thrown)
        {
            items.Add(thrown);

        }
    }
}
