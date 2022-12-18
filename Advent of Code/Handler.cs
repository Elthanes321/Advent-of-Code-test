using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Handler
    {
        List<Monke> monkeys;
        int round;

        public Handler(List<Monke> monkeys)
        {
            this.monkeys = monkeys;
            this.round = 0;
            int modulo=1;
            foreach(Monke monke in monkeys)
            {
                modulo *= monke.testclause;
            }
            for(int i = 0; i < monkeys.Count; i++)
            {
                monkeys[i].smodulo = modulo;
            }
        }

        public ulong GetMonkeyBusiness()
        {
            return monkeys.OrderBy(x => x.Inspections).Last().Inspections * monkeys.OrderBy(x => x.Inspections).ElementAt(monkeys.Count-2).Inspections;
        }

        public void PlayRounds(int rounds)
        {
            for(int i = 0; i < rounds; i++)
            {
                for(int x=0; x < monkeys.Count; x++)
                {
                    var ret = monkeys[x].TakeTurn();
                    foreach(var item in ret)
                    {
                        monkeys[(int)item[0]].AddItem(item[1]);
                    }
                }
                round++;
            }
        }
    }
}
