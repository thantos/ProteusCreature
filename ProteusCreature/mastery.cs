using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class mastery
    {
        public static double MasteryPerLevelFactor = .01; 

        /** 
         * TODO: Add method to return the modifier for the current mastery level
         *          We need to figure out how this will work!!!!!!!!!
         **/
        
        /**
         * MaxLevel = 100
         **/ 

        public mastery()
        {
            MasteryLevels = new Dictionary<bodypart.ClassPartTypes, double>();
            foreach (bodypart.ClassPartTypes s in Enum.GetValues(typeof(bodypart.ClassPartTypes)))
                MasteryLevels[s] = 0;
        }

        /// <summary>
        /// Creates a mastery with all but the specified class's mastery set to 0 and the Class set as specified
        /// </summary>
        /// <param name="Class">Class to have the mastery set for</param>
        /// <param name="head"></param>
        /// <param name="body"></param>
        /// <param name="legs"></param>
        /// <param name="accessory"></param>
        public mastery(bodypart.ClassTypes Class, double head, double body, double legs, double accessory)
            : this()
        {
            SetClassMastery(Class, head, body, legs, accessory);
        }

        public Dictionary<bodypart.ClassPartTypes, double> MasteryLevels { get; set; }

        public void addExperience(bodypart.ClassPartTypes type, double Amount)
        {
            MasteryLevels[type] += Amount;
        }

        public void SetMastery(bodypart.PartTypes part,bodypart.ClassTypes Class, double set)
        {
            MasteryLevels[(bodypart.ClassPartTypes)Enum.Parse(typeof(bodypart.ClassPartTypes),Class.ToString()+part.ToString())] = set;
        }

        public void SetClassMastery(bodypart.ClassTypes Class, double head, double body, double legs, double accessory)
        {
            SetMastery(bodypart.PartTypes.Head, Class, head);
            SetMastery(bodypart.PartTypes.Body, Class, body);
            SetMastery(bodypart.PartTypes.Legs, Class, legs);
            SetMastery(bodypart.PartTypes.Accessory, Class, accessory);
        }

        public static double MasterMod(double ma)
        {
            return (ma*MasteryPerLevelFactor) + 1;
        }


        #region Operators

        public static bool operator >=(mastery m1, mastery m2)
        {
            foreach (bodypart.ClassPartTypes ty in Enum.GetValues(typeof(bodypart.ClassPartTypes)))
                if (m1.MasteryLevels[ty] < m2.MasteryLevels[ty])
                    return false;
            return true;
        }

        public static bool operator <(mastery m1, mastery m2)
        {
            return !(m1>=m2);
        }

        public static bool operator >(mastery m1, mastery m2)
        {
            return m1 >= m2 && m1 != m2;
        }

        public static bool operator <=(mastery m1, mastery m2)
        {
            return m1 < m2 || m2 == m1;

        }

        public static bool operator ==(mastery m1, mastery m2)
        {
            foreach (bodypart.ClassPartTypes s in Enum.GetValues(typeof(bodypart.ClassPartTypes)))
                if (m1.MasteryLevels[s] != m2.MasteryLevels[s])
                    return false;
            return true;
        }

        public static bool operator != (mastery m1, mastery m2)
        {
            return !(m1 == m2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this == (mastery)obj;
        }

        #endregion

        #region tests

        public override string ToString()
        {
            StringBuilder ss = new StringBuilder();
            foreach (KeyValuePair<bodypart.ClassPartTypes, Double> s in MasteryLevels)
                ss.AppendLine(s.Key.ToString() + ": " + s.Value.ToString());
            return ss.ToString();
        }

        public string ToShortString()
        {
            StringBuilder ss = new StringBuilder();
            foreach (KeyValuePair<bodypart.ClassPartTypes, Double> s in MasteryLevels)
                ss.Append(s.Value.ToString()+",");
            return ss.ToString();
        }

        #endregion

    }
}
