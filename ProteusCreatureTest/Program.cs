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
                Console.WriteLine("0: EXIT");
                
                switch ((int.Parse(Console.ReadLine())))
                {
                    case 1:
                        Test1();
                        break;
                    case 2:
                        AlgorithmTestMenu();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Not currently a viable option!");
                        break;
                }
            }

        }

        static void AlgorithmTestMenu()
        {
            Dictionary<int, AlgorithmTests> alg = new Dictionary<int, AlgorithmTests>()
            {
                {0,exit},
                {1,creature.CalculateTotalHealth}
                
            };

            Console.WriteLine("Welcome to the Algorithm tester, here we will test any algorithms that have been defined and added tot the test program");
            Console.WriteLine("Please choose a algorithm to test");
            Console.WriteLine("0: EXIT");
            Console.WriteLine("1: MaxHealth");


            int ch = int.Parse(Console.ReadLine());
            Console.WriteLine("Would you like specify the parameters 'y' for yes, 'n' for defaults (100,100,10,10,0)");
            int rangex = 100, rangey = 100, incx = 10, incy = 10;
            double add = 0;

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
    }
}
