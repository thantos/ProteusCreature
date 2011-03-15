using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProteusCreature;
namespace ProteusCreatureTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello and Welcome to the test program for proteus creature classes. We are going to try and make this neat and titdy");
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1: Test Case 1 - currently testing the creation of a creature and adding a few body parts");
                Console.WriteLine("2: AlgorithmTester");
                Console.WriteLine("3: Sam's Random sandbox");
                Console.WriteLine("4: Begining Combat Sim");
                Console.WriteLine("0: EXIT");
                
                switch ((int.Parse(Console.ReadLine())))
                {
                    case 1:
                        Test1();
                        break;
                    case 2:
                        AlgorithmTestMenu();
                        break;
                    case 3:
                        RandomSandbox();
                        break;
                    case 4:
                        BeginingCombatSim();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Not currently a viable option!");
                        break;
                }
            }

        }


        /// <summary>
        /// A menu detailing the different possible algorithms to test
        /// To add a new algorithm to test dot he following:
        /// 
        /// 
        /// Add the method to the dictionary
        /// Add a console.writeline with the key and a description
        /// add a enter in the custom defaults if the defaults do not match the default in the switch statement
        /// 
        /// </summary>
        static void AlgorithmTestMenu()
        {
            Dictionary<int, AlgorithmTests> alg = new Dictionary<int, AlgorithmTests>()
            {
                {0,exit},
                {1,creature.CalculateTotalHealth},
                {2, mastery.MasterMod},
                {3, creature.CalculateDamage},
                {4, creature.CalculateDamage2},
                {5, creature.CalculateDamageResistance}
            };

            Console.WriteLine("Welcome to the Algorithm tester, here we will test any algorithms that have been defined and added to the test program");
            Console.WriteLine("Please choose a algorithm to test");
            Console.WriteLine("0: EXIT");
            Console.WriteLine("1: MaxHealth");
            Console.WriteLine("2: MasteryMods");
            Console.WriteLine("3: DamageCalcuator Option 1");
            Console.WriteLine("4: DamageCalcuator Option 2");
            Console.WriteLine("5: ResistanceCalculator - Same as DamageCalculator2 for now");

            int rangex, rangey, incx, incy;
            double add;


            int ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 2:
                    rangex = 100;
                    rangey = 0;
                    incx = 1;
                    incy = 1;
                    add = 0;
                    break;
                case 3:
                    rangex = 400;
                    rangey = 0;
                    incx = 40;
                    incy = 1;
                    add = 100;
                    break;
                case 4:
                    rangex = rangey = 400;
                    incx = incy = 40;
                    add = 100;
                    break;
                case 5:
                    rangex = rangey = 400;
                    incx = incy = 40;
                    add = 100;
                    break;
                default: //The default default!
                    rangex = rangey = 100; 
                    incx = incy = 10;
                    add = 0;
                    break;
            }

            Console.WriteLine("Would you like specify the parameters 'y' for yes, 'n' for defaults (100,100,10,10,0)");

            if (Console.ReadLine() == "y")
            {
                Console.Write("Range of x: ");
                rangex = int.Parse(Console.ReadLine());
                Console.Write("Range of y: ");
                rangey = int.Parse(Console.ReadLine());
                Console.Write("increment of x: ");
                incx = int.Parse(Console.ReadLine());
                Console.Write("increment of y: ");
                incy = int.Parse(Console.ReadLine());
                Console.Write("Number to add: ");
                add = double.Parse(Console.ReadLine());
            }
            AlgorithmTester(rangex, rangey, incx,incy, alg[ch], add);
        }

        static double exit(double x, double y, double add)
        {
            return 0;
        }

        static void Test1()
        {
            creature testCreature1 = new creature("Proteus");
            testCreature1.PrintStats();
            mastery tempM = new mastery();
            tempM.SetClassMastery(bodypart.ClassTypes.ColdBlooded, 10, 10, 10, 10);
            tempM.SetClassMastery(bodypart.ClassTypes.Invertebrate, 10, 10, 10, 10);
            tempM.SetClassMastery(bodypart.ClassTypes.WarmBlooded, 10, 10, 10, 10);

            testCreature1.Mastery = tempM;

            bodypart head1 = new bodypart(bodypart.ClassTypes.Invertebrate, bodypart.PartTypes.Head, "test head1", 5, 3, 1, 2, 6);
            head1.AddAbility(new ability("bite!", new List<effect>() { new effect(Stats.statsType.CURRENT_HEALTH, -10, 1, true, false) }, false, false, new mastery(), new Stats(10, 10, 10, 10, 10)));
            bodypart Body1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Body, "Body 1", 2, 4, 5, 7, 8);
            bodypart Legs1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Legs, "Legs 1", 1, 3, 5, 3, 9);
            bodypart Accessory1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Accessory, "Accessory 1", 4, 1, 2, 1, 7);



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

        delegate double AlgorithmTests(double x, double y, double add);

        static void AlgorithmTester(int rangex, int rangey, int incx, int incy, AlgorithmTests method, double additives)
        {
            //StreamWriter sw = new StreamWriter("totalHealth.txt");
            for (int i = -(incx*2); i < rangex+1; i+=incx)
            {
                Console.Write("{0,-5}|",((i<=-(incx))? " " : i.ToString()));
                for (int j = 0; j < rangey+1; j += incy)
                {
                    if (i == -(incx*2))
                        Console.Write("{0,-6}", j);
                    else if (i == -incx)
                        Console.Write("{0,-6}", "---");
                    else
                        Console.Write("{0,-5} ", (method(i, j, additives)));
                }
                Console.Write('\n');
            }
        }

        static void RandomSandbox()
        {
            Console.WriteLine( creature.CalculateDamage(10,100,0));
            Console.WriteLine(creature.CalculateDamage(10, 100, 0));
            Console.WriteLine(creature.CalculateDamage(10, 100, 0));
            Console.WriteLine(creature.CalculateDamage(10, 100, 0));
            Console.WriteLine(creature.CalculateDamage(10, 100, 0));
        }

        static void BeginingCombatSim()
        {
            bodypart head1 = new bodypart(bodypart.ClassTypes.Invertebrate, bodypart.PartTypes.Head, "test head1", 400, 3, 100, 2, 6);
            head1.AddAbility(new ability("bite!", new List<effect>() { new effect(Stats.statsType.DAMAGE, 10, 2, true, false) }, false, false, new mastery(), new Stats(10, 10, 10, 10, 10)));
            bodypart Body1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Body, "Body 1", 2, 4, 5, 7, 8);
            bodypart Legs1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Legs, "Legs 1", 1, 3, 5, 3, 9);
            bodypart Accessory1 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Accessory, "Accessory 1", 4, 1, 2, 1, 7);

            bodypart head2 = new bodypart(bodypart.ClassTypes.Invertebrate, bodypart.PartTypes.Head, "test head1", 5, 3, 1, 2, 6);
            head2.AddAbility(new ability("bite!", new List<effect>() { new effect(Stats.statsType.DAMAGE, 10, 3, true, false) }, false, false, new mastery(), new Stats(10, 10, 10, 10, 10)));
            bodypart Body2 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Body, "Body 1", 2, 4, 5, 7, 8);
            bodypart Legs2 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Legs, "Legs 1", 1, 3, 5, 3, 9);
            bodypart Accessory2 = new bodypart(bodypart.ClassTypes.ColdBlooded, bodypart.PartTypes.Accessory, "Accessory 1", 4, 1, 2, 1, 7);

            creature Proteus = new creature(head1, Body1, Legs1, Accessory1, "Proteus");
            creature Enemy1 = new creature(head2, Body2, Legs2, Accessory2, "Enemy");

            Proteus.combatPrint();
            Enemy1.combatPrint();


            for (int i = 0; i < 10; i++)
            {
                Proteus.effectStep();
                Enemy1.effectStep();
                Proteus.useAbilityOn(Enemy1, "bite!");
                Enemy1.combatPrint();
            }
        }
    }
}
