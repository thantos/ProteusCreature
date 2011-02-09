using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class bodypart
    {
        public enum ClassTypes
        {
            InvertebrateHead, InvertebrateBody, InvertebrateLegs, InvertebrateAccessory,
            WarmBloodedHead, WarmBloodedBody, WarmBloodedLegs, WarmBloodedAccessory,
            ColdBloodedHead, ColdBloodedBody, ColdBloodedLegs, ColdBloodedAccessory, NULL
        }

        bodypart(ClassTypes ty, double str, double defense, double intel, double endur, double agil)
        {
            Class = ty;
            effect STR = new effect(new stat(0,Stats.statsType.STRENGTH), -1, false, true );
            effect DEFENSE=  new effect(new stat(0,Stats.statsType.DEFENCE) ,-1,false,true);

        }

        public ClassTypes Class { get; set; }

        public List<ability> Abilities { get; set; }

        public void AddAbility(ability ab)
        {
            Abilities.Add(ab);
        }
    }
}
