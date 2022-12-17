using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class RPS
    {
        private Dictionary<char, int> RPSpoints = new();

        private Dictionary<string, int> Result = new();
        public RPS()
        {
            RPSpoints.Add('X', 1);
            RPSpoints.Add('Y', 2);
            RPSpoints.Add('Z', 3);
            RPSpoints.Add('A', 1);
            RPSpoints.Add('B', 2);
            RPSpoints.Add('C', 3);
            Result.Add("X", 0);
            Result.Add("Y", 3);
            Result.Add("Z", 6);
        }


        public int Contest(string input)
        {
            int ret= 0;
            string[] actions = input.Split(" ");
            ret += RPSpoints[Char.Parse(actions[1])];
            ret += Direct(Char.Parse(actions[0]), Char.Parse(actions[1]));
            return ret;
        }

        public int FixedContest(string input)
        {
            int ret = 0;

            string[] actions = input.Split(" ");
            ret += Result[actions[1]];
            ret += PointOfChosen(actions[0], Result[actions[1]]);

            return ret;
        }


        private int PointOfChosen(string enemy, int result)
        {
            int ret = 0;
            switch (result)
            {
                case 0:
                    if (enemy == "A")
                        ret += RPSpoints['Z'];
                    else if (enemy == "B")
                        ret += RPSpoints['X'];
                    else if (enemy == "C")
                        ret += RPSpoints['Y'];
                    break;
                case 3:
                    ret+= RPSpoints[Char.Parse(enemy)];
                    break;
                case 6:
                    if(enemy == "A")
                        ret+= RPSpoints['Y'];
                    else if (enemy == "B")
                        ret += RPSpoints['Z'];
                    else if (enemy == "C")
                        ret += RPSpoints['X'];
                    break;
            }

            return ret;
        }

        private int Direct(char enemy, char self)
        {
            switch (self)
            {
                case 'X':
                    if(enemy == 'A')
                        return 3;
                    if (enemy == 'B')
                        return 0;
                    if (enemy == 'C')
                        return 6;
                    break;
                case 'Y':
                    if (enemy == 'A')
                        return 6;
                    if (enemy == 'B')
                        return 3;
                    if (enemy == 'C')
                        return 0;
                    break;
                case 'Z':
                    if (enemy == 'A')
                        return 0;
                    if (enemy == 'B')
                        return 6;
                    if (enemy == 'C')
                        return 3;
                    break;              
            }
            return 0;
        }


    }
}
