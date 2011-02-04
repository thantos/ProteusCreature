using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class stat
    {
        public stat(double amount, Stats.statsType type)
        {
            Amount = amount;
            Type = type;
        }
        public double Amount { get; set; }
        public Stats.statsType Type { get; set; }
    }
}
