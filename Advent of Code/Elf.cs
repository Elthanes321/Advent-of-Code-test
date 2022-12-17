using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public class Elf
    {
        private List<int> _carriedCalories;
        private int _totalCalories;

        public List<int> CarriedCalories
        {
            get
            {
                return _carriedCalories;
            }

            set
            {
                _carriedCalories = value;
                TotalCalories = CarriedCalories.Sum();
            }
        }

        public int TotalCalories
        {
            get
            {
                return _totalCalories;
            }

            set
            {
                _totalCalories = value;
            }
        }

        public Elf()
        {
            CarriedCalories = new List<int>();
            TotalCalories = 0;
        }
        public Elf(string input)
        {
            var car = input.Split(Environment.NewLine);
            CarriedCalories = new List<int>();
            foreach (var c in car)
            {
                CarriedCalories.Add(int.Parse(c));
            }
            TotalCalories = CarriedCalories.Sum();
        }

        public Elf(List<int> cCalories, int tCalories)
        {
            CarriedCalories = cCalories;
            TotalCalories = tCalories;
        }

        public Elf(Elf copy)
        {
            CarriedCalories = new List<int>(copy.CarriedCalories);
            TotalCalories = copy.TotalCalories;
        }
    }
}
