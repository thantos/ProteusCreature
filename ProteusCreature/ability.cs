using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    public class ability
    {
        public ability(string name, List<effect> effects, bool onSelf, bool passive, mastery req, Stats stats_req)
        {
            Name = name;
            Effects = effects;
            OnSelf = onSelf;
            Passive = passive;
            MasteryRequirments = req;
            StatsRequirments = stats_req;
        }

        /// <summary>
        /// Name of the ability to be displayed
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The effects that the ability puts on a creature it is used on
        /// </summary>
        public List<effect> Effects { get; private set; }
        public bool OnSelf { get; set; }
        public bool Passive { get; private set; }

        public mastery MasteryRequirments { get; set; }
        public Stats StatsRequirments { get; set; }

        #region tests

        public override string ToString()
        {
            StringBuilder ss = new StringBuilder();
            ss.AppendLine(this.Name + ": " + this.Passive + "-" + this.StatsRequirments.ToShortString() + "-" + this.MasteryRequirments.ToShortString());
            foreach (effect e in Effects)
                ss.AppendLine(e.ToString());
            return ss.ToString();
        }


        #endregion
    }
}
