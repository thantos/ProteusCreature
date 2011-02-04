using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class creature
    {

        //An enum for all the possible editable stats.

        public Stats myStats { get; set; }

        
        public creature()
        {
            myStats = new Stats();


        }

        /**
         * all stats to 0
         **/
        public void resetStats()
        {
            myStats.resetStats();
        }

        /**Effects
         * 
         * List of effects.
         * Step will decrease the time of each effect, if it is perminent it does not get reduces
         * When an effect hits 0, it is removed.
         * TODO: add grab effects function - update the  function
         *       add calcuate stats -- loop through each stat and add it to the statList
         **/


        List<effect> effects;

        private void addEffectMod(Stats.statsType stat, double mods)
        {
            myStats[stat] += mods;
        }

        private void removeEffectMod(effect e)
        {
            myStats[e.effectedStat.Type] -= e.TotalMod;
        }

        public void effectStep()
        {
            foreach (effect e in effects)
            {
                if (!e.Step())
                {
                    if(e.ResetStat) // set stat back to where it was before effect (don't for things like healing)
                        removeEffectMod(e);
                    effects.Remove(e);
                }
            }
        }
        

        /**
         * End effect code
         **/

        /**
         * Abilities
         **/
        public List<ability> BattleAbilities { get; set; }
        public List<ability> PassiveAbilities { get; set; }

        private void UpdatePassiveAbilities()
        {



        }

        /**
         * End Abilities
         **/

        /**
         * Body Parts
         **/

        List<bodypart> BodyParts { get; set; }

    }
}
