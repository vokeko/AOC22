using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC22
{
    static class Day1
    {
        internal static void CalorieCount(string path, bool prvni)
        {
            List<int> calorieList = new List<int>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                int calories = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        calorieList.Add(calories);
                        calories = 0;
                    }
                    else
                        calories += int.Parse(line);
                }
                if (calories != 0) calorieList.Add(calories);
            }

            if (prvni && calorieList.Count > 0)
                Console.WriteLine(calorieList.Max());
            else if (!prvni && calorieList.Count > 2)
            {
                calorieList = calorieList.OrderByDescending(x => x).ToList();
                Console.WriteLine(calorieList[0] + calorieList[1] + calorieList[2]);
            }
            else
                Console.WriteLine("Kolekce je moc malá");
        }
    }
}
