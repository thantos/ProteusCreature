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
    }
}
