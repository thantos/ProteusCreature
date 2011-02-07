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

        public string Name { get; set; }
        public List<effect> Effects { get; private set; }
        public bool OnSelf { get; set; }
        public bool Passive { get; private set; }

        public mastery MasteryRequirments { get; set; }
        public Stats StatsRequirments { get; set; }

        public void AddMainPassiveAbility(stat str, stat defe, stat agil, stat end, stat intel)
        {


        }
    }
}
