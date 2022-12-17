using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class CommSystem
    {

        string received = "";

        public CommSystem(string input)
        {
            received = input.Replace(Environment.NewLine,"");
        }

        public int GetStartOfPacketMarker()
        {
            for (int i = 4; i < received.Length; i++)
            {
                string help = received.Substring(i - 4, 4);
                if (help.Distinct().Count() == 4)
                    return i;
            }
            return -1;
        }

        public int GetStartOfMessage()
        {

            for (int i = 14; i < received.Length; i++)
            {
                string help = received.Substring(i - 14, 14);
                if (help.Distinct().Count() == 14)
                    return i;
            }
            return -1;

        }

    }
}
