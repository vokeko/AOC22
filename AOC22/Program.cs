using System;
using System.IO;

namespace AOC22
{
    class Program
    {
        static void Main()
        {
            ConsoleKey key = default;

            do
            {
                Console.WriteLine("Testování?");
                string tempTestLine = Console.ReadLine().ToLower();
                bool test = tempTestLine == "ano" || tempTestLine == "a" || tempTestLine == "y" || tempTestLine == "yes";

                Console.WriteLine("První úkol?");
                tempTestLine = Console.ReadLine().ToLower();
                bool prvni = tempTestLine == "ano" || tempTestLine == "a" || tempTestLine == "y" || tempTestLine == "yes";

                bool success;
                int day = 0;
                do
                {
                    Console.WriteLine("Zadejte den");

                    success = int.TryParse(Console.ReadLine(), out int tempDay);
                    if (!success) 
                        Console.WriteLine("String musí být číslo");
                    else 
                        day = tempDay;
                } while (!success);

                string path = GetPath(test, day);

                switch (day)
                {
                    case 1:
                        Day1.CalorieCount(path, prvni);
                        break;
                    case 2:
                        Day2.RPSScore(path, prvni);
                        break;
                    case 3:
                        Day3.RucksackOrganization(path, prvni);
                        break;
                    case 4:
                        Day4.CampCleanup(path, prvni);
                        break;
                    case 5:
                        Day5.SupplyStacks(path, prvni);
                        break;
                    case 6:
                        Day6.TuningTrouble(path, prvni);
                        break;
                    case 9:
                        Day9.RopeBridge(path, prvni);
                        break;
                    case 10:
                        Day10.CathodeRayTube(path, prvni);
                        break;
                    case 11:
                        Day11.MonkeyBusiness(path, prvni);
                        break;
                    case 14:
                        Day14.RegolithReservoir(path, prvni);
                        break;
                    case 18:
                        Day18.BoilingBoulders(path, prvni);
                        break;
                    case 20:
                        Day20.GrovePositioningSystem(path, prvni);
                        break;
                    default:
                        Console.WriteLine("Den mimo rozsah");
                        continue;
                }
                Console.WriteLine("Pokračovat?");
                key = Console.ReadKey().Key;
                Console.WriteLine("");
            } while (key == ConsoleKey.A || key == ConsoleKey.Y || key == ConsoleKey.Enter);
        }
        private static string GetPath(bool test, int day)
        {
            string file = test ? "test.txt" : "data.txt";
            return Path.Combine(string.Format(@"..\..\Days\Day{0}", day), file);
        }
    }
}
