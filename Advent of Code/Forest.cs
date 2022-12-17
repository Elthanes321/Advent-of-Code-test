using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Forest
    {
        private List<List<int>> field = new List<List<int>>();
        private List<List<bool>> used = new List<List<bool>>();
        private int border;

        public Forest(string[] input)
        {
            field = new List<List<int>>();
            used = new List<List<bool>>();
            for (int i = 0; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                    continue;
                field.Add(new List<int>());
                used.Add(new List<bool>());

                foreach (var c in input[i])
                {
                    field[i].Add((int)Char.GetNumericValue(c));
                    used[i].Add(false);
                }
            }

            for (int i = 0; i < field.Count; i++)
            {
                for (int j = 0; j < field[i].Count; j++)
                {
                    if (i == 0 || i == field.Count - 1 || j == 0 || j == field[0].Count - 1)
                        used[i][j] = true;
                }
            }
            border = field.Count()*2;
            border += (field[0].Count() - 2) * 2;
        }

        public int CalcVisibile()
        {
            int ret = 0;
            for (int i = 1; i < field.Count - 1; i++)
            {
                for(int j=1; j < field[0].Count-1;j++)
                {
                    if (field[i].GetRange(0, j).Max() < field[i][j] || field[i].GetRange(j + 1, field[i].Count - j - 1).Max() < field[i][j])
                    {
                        if (used[i][j])
                            continue;
                        else
                        {
                            ret++;
                            used[i][j] = true;
                        }
                    }
                }
            }
            for (int i = 1; i < field[0].Count - 1; i++)
            {
                for (int j = 1; j < field.Count-1; j++)
                {
                    if (field[j].ElementAt(i) > field.GetRange(0, j).Max(x => x.ElementAt(i)) || field[j].ElementAt(i) > field.GetRange(j+1, field.Count - j-1).Max(x => x.ElementAt(i)))
                    {
                        if (used[j][i])
                            continue;
                        else
                        {
                            ret++;
                            used[j][i] = true;
                        }
                    }
                }
            }
            return ret+border;
        }

        public int CalcScenicScore()
        {
            int ret = 0;

            for (int i = 1; i < field.Count-1; i++)
            {
                for (int j = 1; j < field[0].Count-1; j++)
                {
                    if (!used[i][j])
                        continue;

                    int l = 0;
                    int r = 0;
                    int t = 0;
                    int b = 0;
                    for (int x = j + 1; x < field[0].Count; x++)
                    {
                        r++;
                        if (field[i][x] >= field[i][j])
                            break;
                    }
                    for (int x = j -1; x >=0; x--)
                    {
                        l++;
                        if (field[i][x] >= field[i][j])
                            break;
                    }
                    for (int x = i + 1; x < field.Count; x++)
                    {
                        b++;
                        if (field[x][j] >= field[i][j])
                            break;
                    }
                    for (int x = i - 1; x >= 0; x--)
                    {
                        t++;
                        if (field[x][j] >= field[i][j])
                            break;
                    }
                    int help = l * r * t * b;
                    if (help > ret)
                        ret = help;
                }            
            }


            return ret;
        }

    }
}
