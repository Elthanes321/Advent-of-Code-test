using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Heightmap
    {
        List<Thread> threads = new();
        char[,] map;
        int[] end;
        int[] start;
        object o;

        private int possibleMoves;

        public Heightmap(string input)
        {
            o = "b";
            possibleMoves = 6000;
            var divide = input.Split(Environment.NewLine);
            map = new char[divide.Length, divide[0].Length];
            for(int i = 0; i < divide.Length; i++)
            {
                var help = divide[i].ToCharArray();
                for(int j =0; j < divide[0].Length; j++)
                {
                    map[i,j] = help[j];
                    if (help[j] == 'S')
                    {
                        start = new int[] { i, j };
                        map[i, j] = 'a';

                    }
                    if (help[j] == 'E')
                    {
                        end = new int[] { i, j };
                        map[i, j] = 'z';
                    }
                }
            }
        }
        // left , top, right , bottom
        private bool[] EvaluateSteps(int[]curr)
        {
            bool[] result = new bool[4] { true, true,true,true };
            if (curr[0]==0)
                result[0] &= false;
            if (curr[1] == 0)
                result[1] &= false;
            if (curr[0] == map.GetLength(0)-1)
                result[2] &= false;
            if (curr[1] == map.GetLength(1)-1)
                result[3] &= false;

            var currentHeight = map[curr[0],curr[1]];
            if(curr[0] > 0)
                if(Math.Abs(map[curr[0]-1, curr[1]]-currentHeight) >= 2)
                {
                    result[0] &= false;
                }
            if(curr[1] > 0)
                if (Math.Abs(map[curr[0], curr[1]-1] - currentHeight) >= 2)
                {
                    result[1] &= false;
                }

            if (curr[0] < map.GetLength(0) - 1)
                if (Math.Abs(map[curr[0] + 1, curr[1]] - currentHeight) >= 2)
                {
                    result[2] &= false;
                }

            if (curr[1] < map.GetLength(1) - 1)
                if (Math.Abs(map[curr[0], curr[1]+1] - currentHeight) >= 2)
                {
                    result[3] &= false;
                }

            return result;
        }

        public int GetMinimumWay()
        {
            var help = EvaluateSteps(start);
            List<string> ways = new();
            ways.Add(string.Join("," ,start));
            List<Thread> threads = new();
            if (help[0])
            {
                var t = new Thread(Move);
                t.Start(new Tuple<int, int[], bool, int, List<string>>(0, new int[] { start[0], start[1] }, true, -1, ways));
                threads.Add(t);
                //Move(0, new int[] { start[0], start[1] }, true, -1,ways);
            }
            if (help[1])
            {
                var t = new Thread(Move);
                t.Start(new Tuple<int, int[], bool, int, List<string>>(0, new int[] { start[0], start[1] }, false, -1, ways));
                threads.Add(t);
                //Move(0, new int[] { start[0], start[1] }, false, -1,ways);
            }
            if (help[2])
            {
                var t = new Thread(Move);
                t.Start(new Tuple<int, int[], bool, int, List<string>>(0, new int[] { start[0], start[1] }, true, 1, ways));
                threads.Add(t);
                //Move(0, new int[] { start[0], start[1] }, true, 1, ways);
            }
            if (help[3])
            {
                var t = new Thread(Move);
                t.Start(new Tuple<int, int[], bool, int, List<string>>(0, new int[] { start[0], start[1] }, false, 1, ways));
                threads.Add(t);
                //Move(0, new int[] { start[0], start[1] }, false, 1, ways);
            }
            foreach (var t in threads)
                t.Join();
            return possibleMoves;
        }

        private void Move(object o)
        {
            Tuple<int,int[],bool,int,List<string>>obj = o as Tuple<int, int[], bool, int, List<string>>;
            Move(obj.Item1, obj.Item2, obj.Item3, obj.Item4, obj.Item5);
        }

        private void Move(int steps, int[] cur, bool dim,int forward, List<string> visited)
        {
            if(dim)
                cur = new int[] { cur[0] + forward,cur[1]};
            else
                cur = new int[] { cur[0], cur[1]+forward };
            steps++;
            if (steps >= possibleMoves || visited.Contains(string.Join(",", cur)))
                return;
            visited.Add(string.Join(",", cur));
            if (cur[0] == end[0] && cur[1] == end[1])
            {
                lock (o)
                {
                    if(possibleMoves>steps)
                        possibleMoves = steps;
                }

                return;
            }
            var poss = EvaluateSteps(new int[] { cur[0], cur[1] });
            if (poss[0])
                Move(steps,new int[] { cur[0], cur[1] }, true, -1, new List<string>(visited));
            if (poss[1])
                Move(steps, new int[] { cur[0], cur[1] }, false, -1, new List<string>(visited));
            if (poss[2])
                Move(steps, new int[] { cur[0], cur[1] }, true, 1, new List<string>(visited));
            if (poss[3])
                Move(steps, new int[] { cur[0], cur[1] }, false, 1, new List<string>(visited));
            return;        
        }


    }
}
