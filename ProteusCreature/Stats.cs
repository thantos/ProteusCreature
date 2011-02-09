using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class Stats
    {
        public enum statsType
        {
            STRENGTH, AGILITY, ENDURANCE, DEFENCE, INTELLIGENCE,
            CURRENT_HEALTH, TOTAL_HEALTH, DAMAGE, RESISTANCE,
            CLOCK, ATTACK_RANGE, MOVE_DISTANCE, DROP_CHANCE
        };

        public List<stat> stats { get; set; }

        public Stats()
        {
            resetStats();
        }

        public void resetStats()
        {
            foreach (statsType s in Enum.GetValues(typeof(statsType)))
                stats.Add(new stat(0, s));
        }

        //returns -1 if not set
        public double this[statsType index]
        {
            set 
            {
                foreach (stat s in stats)
                    if (s.Type == index)
                        s.Amount = value;
            }
            get
            {
                foreach (stat s in stats)
                    if (s.Type == index)
                        return s.Amount;
                return -1;
            }
        }

        #region operator overloads

        public static bool operator >=(Stats s1, Stats s2)
        {
            foreach (statsType ty in Enum.GetValues(typeof(statsType)))
                if (s1.stats.Find((x) => (x.Type == ty)) < s2.stats.Find((x) => (x.Type == ty)))
                    return false;
            return true;
        }

        public static bool operator <(Stats m1, Stats m2)
        {
            return !(m1 >= m2);
        }

        public static bool operator <=(Stats s1, Stats s2)
        {
            return !(s1 > s2);
        }

        public static bool operator >(Stats s1, Stats s2)
        {
            return (s1 >= s2) && !(s1 == s2);
        }

        public static bool operator ==(Stats s1, Stats s2)
        {
            foreach (statsType ty in Enum.GetValues(typeof(statsType)))
                if (s1.stats.Find((x) => (x.Type == ty)) != s2.stats.Find((x) => (x.Type == ty)))
                    return false;
            return true;
        }

        public static bool operator !=(Stats s1, Stats s2)
        {
            return !(s1 == s2);
        }

        #endregion
    }
}
