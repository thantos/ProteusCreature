using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProteusCreature;
namespace ProteusCreatureTest
{
    class Program
    {
        static void Main(string[] args)
        {
            creature testCreature1 = new creature("Proteus");
            testCreature1.PrintStats();

            bodypart head1 = new bodypart(bodypart.ClassTypes.Invertebrate, bodypart.PartTypes.Head, "test head1",5,3,1,2,6);
            head1.AddAbility(new ability("bite!",new List<effect>(){new effect(Stats.statsType.CURRENT_HEALTH, -10, 1,true,false)},false,false,new mastery(),new Stats(10,10,10,10,10)));
            bodypart Body1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Body, "Body 1", 2, 4, 5, 7, 8);
            bodypart Legs1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Legs, "Body 1", 1, 3, 5, 3, 9);
            bodypart Accessory1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Accessory, "Body 1", 4, 1, 2, 1, 7);



            testCreature1.AddBodyPart(head1, creature.slots.Head);
            testCreature1.AddBodyPart(Body1, creature.slots.Body);
            testCreature1.AddBodyPart(Legs1, creature.slots.Legs);
            testCreature1.AddBodyPart(Accessory1, creature.slots.Accessory1);

            testCreature1.PrintBodyParts();
            testCreature1.PrintStats();
            testCreature1.PrintEffects();

            bodypart Accessory2 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Accessory, "Body 1", 4, 1, 2, 1, 0);

            testCreature1.AddBodyPart(Accessory2, creature.slots.Accessory1);
            testCreature1.PrintStats();
            testCreature1.PrintAbilities();
        }
    }
}
