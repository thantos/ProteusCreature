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
    
        public static bool operator >=(mastery m1, mastery m2)
        {
            foreach (bodypart.ClassTypes ty in Enum.GetValues(typeof(bodypart.ClassTypes)))
                if (m1.MasteryLevels[ty] < m2.MasteryLevels[ty])
                    return false;
            return true;
        }

        public static bool operator <(mastery m1, mastery m2)
        {
            return !(m1>=m2);
        }
    }
}
