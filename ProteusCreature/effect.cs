using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class effect
    {
        /// <summary>
        /// This constructor takes a statsType and mod(double) and makes it into a stat
        /// </summary>
        /// <param name="type">Type for the stat to be modded</param>
        /// <param name="mod">The mod for a stat</param>
        /// <param name="timeout">How many rounds before the effect is removed -1 is perminent, 1 is one round and is 0 done</param>
        /// <param name="perturn">Flag if the mod is added per turn or once</param>
        /// <param name="reset">Does the stat reset after the effect is over</param>
        /// <param name="name">Optional name of the parent</param>
        public effect(Stats.statsType type, double mod, int timeout, bool perturn, bool reset, String name = " "):
            this( new stat(mod, type), timeout, perturn, reset, name){}

        /// <summary>
        /// This constructor takes a statsType and mod(double) and makes it into a stat
        /// </summary>
        /// <param name="st">Stat to be modded</param>
        /// <param name="timeout">How many rounds before the effect is removed -1 is perminent, 1 is one round and is 0 done</param>
        /// <param name="perturn">Flag if the mod is added per turn or once</param>
        /// <param name="reset">Does the stat reset after the effect is over</param>
        /// <param name="name">Optional name of the parent</param>
        public effect(stat st, int timeout, bool perturn, bool reset, String name = " ")
        {
            Timeout = timeout;
            effectedStat = st;
            Perturn = perturn;
            ParentName = name;
            ResetStat = reset;
            TotalMod = effectedStat.Amount;
        }


        private stat effectedStat{get;set;}
        
        /// <summary>
        /// The type of the stat
        /// </summary>
        public Stats.statsType EffectedType 
        { 
            get
            {
                return effectedStat.Type;
            } 
        }

        /// <summary>
        /// the amount to change the effected stat
        /// </summary>
        public double EffectMod
        {
            get
            {
                return effectedStat.Amount;
            }
        }

        //An enum for all the possible editable stats.
        /// <summary>
        /// -1 == permintent, 1 == once, 0 == done
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// true == +/- each clock cycle
        /// </summary>
        public bool Perturn { get; set; }

        /// <summary>
        /// Optional name of the parent ability
        /// </summary>
        public String ParentName { get; set; }

        /// <summary>
        /// for Perturn effects, keeps a total of the mod done
        /// </summary>
        public double TotalMod { get; set; }

        /// <summary>
        ///stat will go back down when the effect wears off
        /// </summary>
        public bool ResetStat { get; set; }
        
        /// <summary>
        /// Step the timeout, add per turn
        /// </summary>
        /// <param name="c">Creature to effect</param>
        /// <returns>False means timeout, time to remove</returns>
        public bool Step(creature c)
        {
            if (Timeout > 0)
                Timeout--;
            if (Timeout == 0)
                return false;
            if (Perturn)
            {
                c.addEffectMod(this);
            }
            return true;
        }
    }
}
