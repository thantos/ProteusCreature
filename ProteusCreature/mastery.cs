using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class mastery
    {
        public mastery()
        {
            foreach (bodypart.ClassTypes s in Enum.GetValues(typeof(bodypart.ClassTypes)))
                MasteryLevels[s] = 0;
        }

        public Dictionary<bodypart.ClassTypes, double> MasteryLevels { get; set; }

        public void addExperience(bodypart.ClassTypes type, double Amount)
        {
            MasteryLevels[type] += Amount;
        }
    
    }
}
