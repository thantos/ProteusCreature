using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class effect
    {
        public effect(Stats.statsType type, double mod, int timeout, bool perturn, bool reset, String name):
            this( new stat(mod, type), timeout, perturn, reset, name){}
        public effect(stat type, int timeout, bool perturn, bool reset, String name)
        {
            Timeout = timeout;
            effectedStat = type;
            Perturn = perturn;
            ParentName = name;
            ResetStat = reset;
            TotalMod = effectedStat.Amount;
        }
        public effect(Stats.statsType type, double mods, int timeout, bool perturn, bool reset)
            : this(type, mods, timeout, perturn, reset, " "){}
        public effect(stat type, int timeout, bool perturn, bool reset)
            : this(type, timeout, perturn, reset, " ") { }


        public stat effectedStat{get;set;}
        //An enum for all the possible editable stats.

        public int Timeout { get; set; } //-1 == permintent // 1 == once // 0 == done
        public bool Perturn { get; set; } //true == +/- each clock cycle
        public String ParentName { get; set; }
        public double TotalMod { get; set; } //for Perturn effects, keeps a total of the mod done
        public bool ResetStat { get; set; } //stat will go back down when the effect wears off
        //Step
        //Steps timeout
        public bool Step()
        {
            if (Timeout != -1)
                Timeout--;
            if (Timeout == 0)
                return false;
            if (Perturn)
                TotalMod += effectedStat.Amount; //for perturn effects, increase the effect each time.
            return true;
        }
    }
}
