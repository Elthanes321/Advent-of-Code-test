using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Cathode
    {
        int register;
        int cycle;
        int stoppoint;
        int sum;
        int max;
        List<int[]> commandStack;
        
        public Cathode(int input)
        {
            stoppoint = 20;
            sum = 0;
            max = input;
            register = 1;
            cycle = 0;
            commandStack = new List<int[]>();
        }
    
        public void Command(string input,bool draw =false)
        {
            string[] cmd = input.Split(" ");
            cycle++;
            switch (cmd[0])
            {
                case "noop":
                    commandStack.Add(new int[] {1,0});
                    break;
                case "addx":
                    commandStack.Add(new int[] { 2, int.Parse(cmd[1]) });
                    break;
                default:
                    break;
            }
            if (cycle == stoppoint && cycle <=max)
            {
                sum += GetSignalStrength();
                stoppoint += 40;
            }
            if (draw)
            {
                Console.Write(Draw());
                if (cycle % 40 == 0)
                {
                    Console.Write(Environment.NewLine);
                }
            }
            commandStack[0][0]--;
            if (commandStack[0][0] == 0)
            {
                register += commandStack[0][1];
                commandStack.RemoveAt(0);
            }
        }

        private char Draw()
        {
            if (cycle % 40 <= register + 2 && cycle % 40 >= register)
                return '#';
            else
                return '.';
        }

        public void Finish()
        {
            while (commandStack.Count > 0)
            {
                Command("Finish", true); ;
            }

        }

        public int GetSum()
        {
            Finish();
            return sum;
        }
        private int GetSignalStrength()
        {
            return register*cycle;
        }
    
    }
}
