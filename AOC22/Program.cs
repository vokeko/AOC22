using System;

namespace AOC22
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Testování?");
            string tempTestLine = Console.ReadLine().ToLower();
            bool test = tempTestLine == "ano" || tempTestLine == "a" || tempTestLine == "y" || tempTestLine == "yes";

            Console.WriteLine("První úkol?");
            tempTestLine = Console.ReadLine().ToLower();
            bool prvni = tempTestLine == "ano" || tempTestLine == "a" || tempTestLine == "y" || tempTestLine == "yes";

            Console.WriteLine("Zadejte den");
            switch (Console.ReadLine())
            {
                case "1":
                    Day1.CalorieCount(test, prvni);
                    break;
                case "3":
                    Day3.RucksackOrganization(test, prvni);
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}
