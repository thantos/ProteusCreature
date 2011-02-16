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

        public string Name;

        /// <summary>
        /// Constructor
        /// </summary>
        public creature(bodypart h, bodypart b, bodypart l, bodypart a1, string name = " ") 
        {
            myStats = new Stats();
            Mastery = new mastery();
            effects = new List<effect>();
            Abilities = new List<ability>();
            AllAbilities = new Dictionary<ability, bool>();

            //TODO Update the generic body parts as it is changed!!!
            BodyParts = new Dictionary<slots, bodypart>();
            AddBodyPart(h,slots.Head);
            AddBodyPart(b,slots.Body);
            AddBodyPart(l,slots.Legs);
            AddBodyPart(a1, slots.Accessory1);
            Accessory2Locked = true;
            Name = name;
        }

        public creature(string name = " "):
            this(new bodypart(bodypart.ClassTypes.Invertebrate,bodypart.PartTypes.Head,"Starting head"),
            new bodypart(bodypart.ClassTypes.Invertebrate,bodypart.PartTypes.Body,"Starting head"),
            new bodypart(bodypart.ClassTypes.Invertebrate,bodypart.PartTypes.Legs,"Starting head"),
            new bodypart(bodypart.ClassTypes.Invertebrate,bodypart.PartTypes.Accessory,"Starting head"),name)
        {}

        public creature(bodypart h, bodypart b, bodypart l, bodypart a1, bodypart a2, string name= " ")
            : this(h, b, l, a1, name)
        {
            this.Accessory2Locked = false;
            AddBodyPart(a2, slots.Accessory2);
        }

        /**
         * TODO: Not sure....
         **/

        #region stats

        //the stats of one creature
        public Stats myStats { get; set; }

        /// <summary>
        /// Set all stats to 0
        /// </summary>
        public void resetStats()
        {
            myStats.resetStats();
        }

        /// <summary>
        /// Reapplies all Effects to Stats
        /// </summary>
        public void SetAllEffect()
        {
            resetStats();
            foreach (effect e in this.effects)
                myStats[e.EffectedType] += e.TotalMod;
        }

        public void SetHealthToFull()
        {
            myStats[Stats.statsType.CURRENT_HEALTH] = myStats[Stats.statsType.TOTAL_HEALTH];
        }

        private void CalculateTotalHealth()
        {
            myStats[Stats.statsType.TOTAL_HEALTH] = (myStats[Stats.statsType.STRENGTH] * .10 + myStats[Stats.statsType.ENDURANCE] * .75) * 100;
            SetHealthToFull();
        }

        #endregion

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
            removeEffectMod(e);
        }

        /// <summary>
        ///  Called by Effect's effectStep method. Adds the effect mod to the effect if the effect is Perturn.
        ///  The effects totalMod is increased by the total moded
        /// </summary>
        /// <param name="e">The effect to be added</param>
        public void addEffectMod(effect e)
        {
            double total = e.EffectMod * ((e.EffectPart != bodypart.ClassPartTypes.NULL && e.Timeout == -1) ? this.Mastery.MasterMod(this.Mastery.MasteryLevels[e.EffectPart]) : 1);
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
            e.TotalMod = 0;
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
        private Dictionary<ability, bool> AllAbilities { get; set; }

        private void UpdateAbilities()
        {
            AllAbilities.Clear();
            Abilities.Clear();
            foreach (effect e in this.effects)
                this.RemoveEffect(e);
            effects.Clear();
            foreach (bodypart b in BodyParts.Values)
                foreach (ability a in b.Abilities)
                    AllAbilities.Add(a,false);
            AllAbilities.OrderByDescending(x => x.Key.StatsRequirments.LowestBaseStat());
            while (!MarkAndAddAbilities())
                ;
            CalculateTotalHealth();
        }

        private bool MarkAndAddAbilities()
        {
            bool done = true;
            foreach(KeyValuePair<ability,bool> pair in AllAbilities)
            {
                if((!pair.Value) && ( this.myStats >= pair.Key.StatsRequirments) && (pair.Key.MasteryRequirments <= this.Mastery))
                {
                    done = false;
                    this.AddAbility(pair.Key);
                }
            }
            if(!done)
                foreach (ability a in Abilities)
                    AllAbilities[a] = true;
            return done;
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

        public enum slots
        {
            Head,Body,Legs,Accessory1,Accessory2
        }

        bool Accessory2Locked;

        Dictionary<slots, bodypart> BodyParts { get; set; }

        public String AddBodyPart(bodypart b, slots slot)
        {
            if (slot == slots.Accessory2 && Accessory2Locked)//Accessory slot 2 is intitially locked
                return "Accessory 2 slot is Locked.";

            this.RemoveBodyPart(slot); //Removes the body part and all residule effects
            this.BodyParts[slot] = b; //put the part in it's new place

            this.UpdateAbilities();

            return b.Name + "has been added.";
        }

        public void RemoveBodyPart(slots s)
        {


        }


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
         *          Need to check to see if the ability is to be used on self or not
         *              If it is to be used on self call use ability method on this
         *              Maybe... an ability on the screen would be denoted as self or other
         *                  So... using a ability on others would need to trugger a selection of an enemy to use it on
         *                      Combat will have a list of enemies.
         **/       

        #region Combat

        public void useAbilityOn(ability a)
        {
            foreach (effect e in a.Effects)
                this.AddEffect(e);
        }

        public void TakeDamage(double damage)
        {
            /**
             * TODO: Calculate Damage based on defence and defence type things
             **/
            double ActualDamage = 0;

            /** **/


            this.AddEffect(new effect(Stats.statsType.CURRENT_HEALTH, ActualDamage, 1, false, false));

            /**TODO check for death!**/
        }

        public void GiveDamage(creature Target, double Damage)
        {
            /** TODO: Calculate Actual damage based on damaged based by effect **/
            double ActualDamage = 0;


            Target.TakeDamage(ActualDamage);
        }

        #endregion

        #region tests

        public void PrintStats()
        {
            Console.WriteLine("Stats");
            Console.WriteLine(this.myStats.ToString());
        }

        public void PrintBodyParts()
        {
            Console.WriteLine("bodyParts");
            foreach (KeyValuePair<slots,bodypart> b in this.BodyParts)
                Console.WriteLine(b.Key.ToString()+": "+b.Value.ToString());
        }

        public void PrintEffects()
        {
            foreach (effect e in effects)
                Console.WriteLine(e.ToString());
        }

        public void PrintAbilities()
        {
            Console.WriteLine("Abilities");
            foreach (ability a in Abilities)
                Console.WriteLine(a.ToString());
        }

        #endregion
    }
}
