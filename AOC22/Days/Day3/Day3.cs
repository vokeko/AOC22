using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC22
{
    static class Day3
    {
        private static List<char> charList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();

        internal static void RucksackOrganization(bool test, bool prvni)
        {
            string path = "data.txt";
            if (test)
                path = "test.txt";

            path = Path.Combine(@"..\..\Days\Day3", path);

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                int score = 0;
                if (prvni)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        int length = line.Length / 2;
                        char[] compartment1 = line.Substring(0, length).ToCharArray();
                        char[] compartment2 = line.Substring(length, length).ToCharArray();
                        char duplicate = compartment1.Where(o => compartment2.Any(p => p == o)).First();
                        score += charList.IndexOf(duplicate) + 1;
                    }
                }
                else
                {
                    List<string> groups = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        groups.Add(line);
                        if (groups.Count >= 3)
                        {
                            char duplicate = groups[0].Where(o => groups[1].Any(p => p == o) && groups[2].Any(q => q == o)).First();
                            score += charList.IndexOf(duplicate) + 1;
                            groups.Clear();
                        }
                    }
                }
                Console.WriteLine("Součet skóre: {0}", score);
            }
        }
    }
}
