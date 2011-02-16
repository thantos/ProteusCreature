using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class bodypart
    {
        public enum ClassPartTypes
        {
            InvertebrateHead, InvertebrateBody, InvertebrateLegs, InvertebrateAccessory,
            WarmBloodedHead, WarmBloodedBody, WarmBloodedLegs, WarmBloodedAccessory,
            ColdBloodedHead, ColdBloodedBody, ColdBloodedLegs, ColdBloodedAccessory, NULL
        }

        public enum PartTypes
        {
            Head,Body,Legs,Accessory
        }

        public enum ClassTypes
        {
            Invertebrate,WarmBlooded,ColdBlooded
        }

        public bodypart(ClassTypes ty, PartTypes pt, string name = " ",double str = 0, double def = 0, double intel = 0, double agil = 0, double end = 0)
        {
            Name = name;
            Abilities = new List<ability>();
            
            Class = ty;
            Part = pt;
            ClassPart = (ClassPartTypes)Enum.Parse(typeof(ClassPartTypes),Class.ToString() + Part.ToString());
            if (str + agil + def + intel + end != 0)
            {
                ability tempAbility = new ability("Base", new List<effect>()
                    {
                    new effect(Stats.statsType.STRENGTH, str, -1, false, true, " ", this.ClassPart),
                    new effect(Stats.statsType.AGILITY, agil, -1, false, true, " ", this.ClassPart),
                    new effect(Stats.statsType.INTELLIGENCE, intel, -1, false, true, " ", this.ClassPart),
                    new effect(Stats.statsType.ENDURANCE, end, -1, false, true, " ", this.ClassPart),
                    new effect(Stats.statsType.DEFENCE, def, -1, false, true, " ",this.ClassPart)
                    }, true, true, new mastery(), new Stats());
                this.AddAbility(tempAbility);
            }
        }

        public ClassPartTypes ClassPart { get; private set; }

        public ClassTypes Class { get; set; }

        public PartTypes Part { get; set; }

        public List<ability> Abilities { get; set; }

        public string Name { get; private set; }

        public void AddAbility(ability ab)
        {
            Abilities.Add(ab);
        }

        public override string ToString()
        {
            StringBuilder ss = new StringBuilder();
            ss.AppendLine(this.Name + ": " + this.ClassPart.ToString());
            foreach (ability a in this.Abilities)
                ss.AppendLine(a.ToString());
            return ss.ToString();
        }

    }
}
