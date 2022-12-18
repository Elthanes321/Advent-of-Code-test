using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Distress
    {

        int counter = 0;
        int sum = 0;

        List<int> indices = new();
        int depth = 0;
        public Distress() { }

        public Tuple<int,List<object>> ConstructList(string input)
        {
            List<object> list = new List<object>();

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                    list.Add((int)Char.GetNumericValue(input[i]));
                else if (input[i] == ',')
                    continue;
                else if (input[i] == '[')
                {
                    depth++;
                    var help = ConstructList(input.Substring(i + 1));
                    list.Add(new List<object>(help.Item2));
                    i = help.Item1+i+1;
          
                }
                else if (input[i] == ']')
                    return new Tuple<int, List<object>>(i, list);
            }
            return new Tuple<int,List<object>>(0,list);
        }
        public void Compare(string[] input)
        {
            counter++;
            List<object> argument1 = ConstructList(input[0].Substring(1, input[0].Length-2)).Item2;
            List<object> argument2 = ConstructList(input[1].Substring(1, input[1].Length - 2)).Item2;
            bool win;
            if(argument1.Count>= argument2.Count)
            {
                win = false;
                for(int i = 0; i < argument2.Count; i++)
                {
                    switch (Compare(argument1[i], argument2[i]))
                    {
                        case 1:
                            win = true;
                            i = argument2.Count;
                            break;
                        case -1:
                            win = false;
                            i = argument2.Count;
                            break;
                        case 0:
                            continue;
                            break;
                    }
                }
                if (win)
                    indices.Add(counter);
            }
            else
            {
                win = true;
                for (int i = 0; i < argument1.Count; i++)
                {
                    switch (Compare(argument1[i], argument2[i]))
                    {
                        case 1:
                            win = true;
                            i = argument1.Count;
                            break;
                        case -1:
                            win = false;
                            i = argument1.Count;
                            break;
                        case 0:
                            continue;
                            break;
                    }
                }
                if (win)
                    indices.Add(counter);
            }

        }

        public int Compare(object eins, object zwei)
        {
            if (eins is int && zwei is int)
                return Compare((int)eins, (int)zwei);
            else if (eins is int)
                switch (Compare((List<object>)zwei, (int)eins))
                {
                    case 0:
                        return 0;
                    case 1:
                        return -1;
                    case -1:
                        return 1;
                }
            else if (zwei is int)
                return Compare((List<object>)eins,(int)zwei);
            else
            {
                return Compare((List<object>)eins, (List<object>)zwei);
            }
            return -99;
        }


        private int Compare(List<object>i, int j)
        {
            List<object> help = new();
            help.Add(j);
            return Compare((List<object>)i, (List<object>)help);
        }

        private int Compare(List<object> i,List<object> j)
        {
            if (i.Count == 0 && j.Count == 0)
                return 0;
            else if (i.Count == 0)
                return 1;
            else if (j.Count == 0)
                return -1;

            if(i.Count >= j.Count)
            {
                for(int x = 0; x < j.Count; x++)
                {
                    switch (Compare(i[x], j[x]))
                    {
                        case 0:
                            continue;
                            break;
                        case 1:
                            return 1;
                        case -1:
                            return -1;
                    }
                }
                if (i.Count > j.Count)
                    return -1;
            }
            else
            {
                for (int x = 0; x < i.Count; x++)
                {
                    switch (Compare(i[x], j[x]))
                    {
                        case 0:
                            continue;
                            break;
                        case 1:
                            return 1;
                        case -1:
                            return -1;
                    }
                }
                return 1;
            }

            return 0;
        }

        private int Compare(int i,int j)
        {
           if(i == j)
                return 0;
            if(i<j)
                return 1;
            if (i > j)
                return -1;


            return -99;
        }

        public int GetResult()
        {
            return indices.Sum();
        }

    }
}
