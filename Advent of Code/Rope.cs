using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
public class Rope
    {
        List<int[]> rope;
        private List<int[]> visited;
        
        public Rope(int l)
        {
            rope = new List<int[]>();
            for (int i = 0; i < l; i++)
            {
                rope.Add(new int[] { 0,0 });
            }
            visited = new List<int[]>();
        }


        public int VisitedSpaces()
        {
            List<string> help = new List<string>();
            foreach (var e in visited)
            {
                help.Add(String.Join(",", e));
            }

            return help.Distinct().Count();
        }

        public void Move(string input)
        {
            var cmd = input.Split(" ");
            var b = visited.Count;
            switch (cmd[0])
            {
                case "U":
                    Up(int.Parse(cmd[1]));
                    break;
                case "D":
                    Down(int.Parse(cmd[1]));
                    break;
                case "L":
                    Left(int.Parse(cmd[1]));
                    break;
                case "R":
                    Right(int.Parse(cmd[1]));
                    break;
            }
            List<string> a = new List<string>();
            foreach (var e in visited)
            {
                a.Add(String.Join(",", e));
            }
            foreach (var e in a)
                Console.WriteLine(e);
        }

        private void Left(int input)
        {
            for (int i = 0; i < input; i++)
            {
                rope[0][0]--;
                for (int j = 1; j < rope.Count; j++)
                {

                    if (rope[j-1][0] == rope[j][0]-2)
                    {
                        if (rope[j - 1][1] == rope[j][1])
                        {
                            rope[j][0]--;
                        }
                        else if (rope[j - 1][1] > rope[j][1])
                        {
                            rope[j][1]++;
                            rope[j][0]--;
                        }
                        else
                        {
                            rope[j][1]--;
                            rope[j][0]--;
                        }
                    }
                }
                visited.Add(new int[] { rope.Last()[0], rope.Last()[1] });
            }

        }

        private void Right(int input)
        {
            for (int i = 0; i < input; i++)
            {
                rope[0][0]++;
                for (int j = 1; j < rope.Count; j++)
                {
                    if (rope[j-1][0] == rope[j][0] + 2)
                    {
                        if (rope[j-1][1] == rope[j][1])
                        {
                            rope[j][0]++;
                        }
                        else if (rope[j-1][1] > rope[j][1])
                        {
                            rope[j][1]++;
                            rope[j][0]++;
                        }
                        else
                        {
                            rope[j][1]--;
                            rope[j][0]++;
                        }
                    }
                }
                visited.Add(new int[] { rope.Last()[0], rope.Last()[1] });
            }
        }

        private void Up(int input)
        {
            for (int i = 0; i < input; i++)
            {
                rope[0][1]++;
                for (int j = 1; j < rope.Count; j++)
                {
                    if (rope[j - 1][1] == rope[j][1] + 2)
                    {
                        if (rope[j - 1][0] == rope[j][0])
                        {
                            rope[j][1]++;
                        }
                        else if (rope[j - 1][0] > rope[j][0])
                        {
                            rope[j][0]++;
                            rope[j][1]++;
                        }
                        else
                        {
                            rope[j][0]--;
                            rope[j][1]++;
                        }
                    }
                }
                visited.Add(new int[] { rope.Last()[0], rope.Last()[1] });

            }
        }

        private void Down(int input)
        {
                for (int i = 0; i < input; i++)
                {
                    rope[0][1]--;
                    for (int j = 1; j < rope.Count; j++)
                    {
                        if (rope[j - 1][1] == rope[j][1] - 2)
                        {
                            if (rope[j - 1][0] == rope[j][0])
                            {
                                rope[j][1]--;
                            }
                            else if (rope[j - 1][0] > rope[j][0])
                            {
                                rope[j][0]++;
                                rope[j][1]--;
                            }
                            else
                            {
                                rope[j][0]--;
                                rope[j][1]--;
                            }
                        }
                    }
                    visited.Add(new int[] { rope.Last()[0], rope.Last()[1] });
                }
            }
        }


}

