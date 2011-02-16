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

        public static bool operator <(stat s1, stat s2)
        {
            return s1.Amount < s2.Amount;
        }

        public static bool operator >(stat s1, stat s2)
        {
            return s1.Amount > s2.Amount;
        }

        public static bool operator <=(stat s1, stat s2)
        {
            return s1.Amount <= s2.Amount;
        }

        public static bool operator >=(stat s1, stat s2)
        {
            return s1.Amount >= s2.Amount;
        }

        public static bool operator ==(stat s1, stat s2)
        {
            return s1.Amount == s2.Amount;
        }

        public static bool operator !=(stat s1, stat s2)
        {
            return !(s1 == s2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region tests

        public override string ToString()
        {
            return this.Type.ToString() + ": " + this.Amount.ToString();
        }

        #endregion tests
    }
}
