using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class creature
    {
        ///note: Call achievements overachievments
        //An enum for all the possible editable stats.

        //the stats of one creature
        public Stats myStats { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public creature()
        {
            myStats = new Stats();
            Mastery = new mastery();
            effects = new List<effect>();
            Abilities = new List<ability>();

        }

        /// <summary>
        /// Set all stats to 0
        /// </summary>
        public void resetStats()
        {
            myStats.resetStats();
        }

        /**Effects
         * 
         * List of effects.
         * Step will decrease the time of each effect, if it is perminent it does not get reduced
         * When an effect hits 0, it is removed.
         * TODO: 
         **/

        #region effects

        List<effect> effects;
        
        /// <summary>
        /// An effect is added and the modification is done once.
        /// </summary>
        /// <param name="e">Effect to be added to the effects on the creature</param>
        private void AddEffect(effect e)
        {
            this.effects.Add(e);
            addEffectMod(e);
        }

        /// <summary>
        /// Makes an effect's timeout 0
        /// </summary>
        /// <param name="e"></param>
        private void RemoveEffect(effect e)
        {
            e.Timeout = 0;
        }

        /// <summary>
        ///  Called by Effect's effectStep method. Adds the effect mod to the effect if the effect is Perturn.
        ///  The effects totalMod is increased by the total moded
        /// </summary>
        /// <param name="e">The effect to be added</param>
        public void addEffectMod(effect e)
        {
            myStats[e.EffectedType] += e.EffectMod;
            e.TotalMod += e.EffectMod; //for perturn effects, increase the effect each time.
        }

        /// <summary>
        /// Decrease the stat effected by the total amount effected.
        /// </summary>
        /// <param name="e">The effect to be reduce</param>
        private void removeEffectMod(effect e)
        {
            myStats[e.EffectedType] -= e.TotalMod;
        }

        /// <summary>
        /// Step all effects
        /// AddEffectMod is called if the effect is per turn.
        /// When the effect has timed out, the effect is removed and the stat is reset if the effect is set to.
        /// </summary>
        public void effectStep()
        {
            foreach (effect e in effects)
            {
                if (!e.Step(this))
                {
                    if(e.ResetStat) // set stat back to where it was before effect (don't for things like healing)
                        removeEffectMod(e);
                    effects.Remove(e);
                }
            }
        }

        #endregion

        /**
         * TODO: Make sure they all work when we do tests I guess
         */

        #region Abilities

        public List<ability> Abilities { get; set; }

        private void updateAbilities()
        {
            List<ability> TempList = new List<ability>();

            foreach(bodypart b in BodyParts) //create a list of all availiable abilities
                foreach (ability a in b.Abilities)
                    if (this.myStats >= a.StatsRequirments && this.Mastery >= a.MasteryRequirments) 
                        TempList.Add(a); // add if the creaure's stats and mastery allow it

            foreach ( ability a in this.Abilities) //if TempList doesn't have a stat that is active on the creature
                if(!TempList.Contains(a))
                    this.RemoveAbility(a); // remove it

            foreach (ability a in TempList) // if templist has an ability that the creature doesn't have
                if (!this.Abilities.Contains(a))
                    this.AddAbility(a); //add it
        }

        private void RemoveAbility(ability a)
        {
            if (a.Passive)
                foreach (effect e in a.Effects)
                    e.Timeout = 0;
            this.Abilities.Remove(a);
        }

        private void AddAbility(ability a)
        {
            if (a.Passive)
                foreach (effect e in a.Effects)
                    this.AddEffect(e);
            this.Abilities.Add(a);
        }

        #endregion

        /**
         * TODO: Need a add body part method
         *          Will add a body part and remove the old part
         *          Then it will call the update abilities method
         *          This method will most likely do more when we have UI support for the bosy parts
         *          It will need to check for modfiers too
         *       Need a remove body part method
         *          Update abilities
         *          Possibly set body part to the generic, maybe
         *          
         **/

        #region bodyParts

        List<bodypart> BodyParts { get; set; }

        #endregion

        /**
         * TODO: Add fuctions to deal with mastery
         *          Add experiece
         *          Calculate level
         *          Reset Mastery
         *      Decide on system for experience
         *          Maybe a simple algorithm     
         **/ 

        #region mastery

        public mastery Mastery { get; set; }

        #endregion

        /**
         * TODO: Add the methods that deal damage
         *          Or is damage just a effect....
         *          We could have it be added as an effect by the damage method (healing is just negative damage)
         *          (Damage is just an effect that effects health)
         *       We need a use ability on method
         *       We need a ability used on method
         **/       

        #region Combat

        public void useAbilityOn(ability a)
        {
            foreach (effect e in a.Effects)
                this.AddEffect(e);
        }

        #endregion
    }
}
