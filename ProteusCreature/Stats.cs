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
            CURRENT_HEALTH, TOTAL_HEALTH, ADD_TOTAL_HEALTH, DAMAGE, RESISTANCE,
            CLOCK, ATTACK_RANGE, MOVE_DISTANCE, DROP_CHANCE
        };

        public static bool IsBaseStateType(statsType s)
        {
            return  (int)s <5;
        }

        public List<stat> stats { get; set; }

        public Stats()
        {
            stats = new List<stat>();
            resetStats();
        }

        public Stats(double str, double agil, double end, double intel, double def):this()
        {
            SetBaseStats(str, agil, end, intel, def);
        }

        public void resetStats()
        {
            foreach (statsType s in Enum.GetValues(typeof(statsType)))
                stats.Add(new stat(0, s));
        }

        public void SetBaseStats(double str, double agil, double end, double intel, double def)
        {
            this[statsType.STRENGTH] = str;
            this[statsType.AGILITY] = agil;
            this[statsType.ENDURANCE] = end;
            this[statsType.INTELLIGENCE] = intel;
            this[statsType.DEFENCE] = def;
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

        public double LowestBaseStat()
        {
            double min = double.MaxValue;
            foreach (stat s in stats)
                if (IsBaseStateType(s.Type) && s.Amount < min)
                    min = s.Amount;
            return min;
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

        public override bool Equals(object obj)
        {
            return this == (Stats)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        #endregion

        #region tests

        public override string ToString()
        {
            StringBuilder ss = new StringBuilder();
            foreach (stat s in stats)
                ss.AppendLine(s.ToString());
            return ss.ToString();
        }

        public string ToShortString()
        {
            StringBuilder ss = new StringBuilder();
            foreach (stat s in stats)
                ss.Append(s.Amount.ToString()+",");
            return ss.ToString();
        }
        #endregion 
    }
}
