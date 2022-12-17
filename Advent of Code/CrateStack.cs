using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class CrateStack
    {
        List<List<char>> stacks;

        public CrateStack(int width)
        {
            stacks = new();
            for (int i = 0; i < width; i++)
            {
                stacks.Add(new List<char>());
            }
        }

        public void FillStack(string input)
        {
            var fhelp = input.Replace("[","").Replace("]","").Split(" ");

            List<string> help = new List<string>();

            for (int i = 0; i < fhelp.Length; i++)
            {
                help.Add(fhelp[i]);
                if (string.IsNullOrEmpty(fhelp[i]))
                {
                    i += 3;
                }
            }
            for (int i = 0; i < help.Count(); i++)
            {
                if(string.IsNullOrEmpty(help[i]))
                    continue;
                stacks[i].Add(Char.Parse(help[i]));
            }
        }

        public string TopCrates()
        {
            string ret = "";

            foreach (var stack in stacks)
            {
                if(stack.Count() >0)
                    ret += stack.Last();
            }

            return ret;
        }

        public void Move(string input)
        {
            var commands = input.Split(" ");
            int amount = int.Parse(commands[1]);
            int from = int.Parse(commands[3])-1;
            int to = int.Parse(commands[5])-1;

            for (int i = 0; i < amount; i++)
            {
                var c  =stacks[from].Last();
                stacks[to].Add(c);
                stacks[from].RemoveAt(stacks[from].Count()-1);
            }

        }

        public void StackMove(string input)
        {
            var commands = input.Split(" ");
            int amount = int.Parse(commands[1]);
            int from = int.Parse(commands[3]) - 1;
            int to = int.Parse(commands[5]) - 1;

            List<char> tobemoved = new();
            for (int i = 1; i <= amount; i++)
            {
                tobemoved.Insert(0, stacks[from][stacks[from].Count() - i]);
            }
            stacks[to].AddRange(tobemoved);
            stacks[from].RemoveRange(stacks[from].Count-amount,amount);
        }

    }
}
