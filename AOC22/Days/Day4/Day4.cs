using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    static class Day4
    {
        internal static void CampCleanup(string path, bool prvni)
        {
            int amount = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] pairs = line.Split(',');
                    int[] cleanPath1 = pairs[0].Split('-').Select(int.Parse).ToArray();
                    int[] cleanPath2 = pairs[1].Split('-').Select(int.Parse).ToArray();
                    if (prvni)
                        amount = Part1Contains(cleanPath1[0], cleanPath1[1], cleanPath2[0], cleanPath2[1]) ? amount + 1 : amount;
                    else
                        amount = Part2Overlaps(cleanPath1[0], cleanPath1[1], cleanPath2[0], cleanPath2[1]) ? amount + 1 : amount;
                }
            }
            Console.WriteLine("Počet: {0}", amount);
        }
        private static bool Part1Contains(int path1Min, int path1Max, int path2Min, int path2Max)
        {
            if (path1Min > path2Min)
            {
                if (path1Max <= path2Max)
                    return true;
            }
            else if (path1Min < path2Min)
            {
                if (path1Max >= path2Max)
                    return true;
            }
            else
                return true;

            return false;
        }
        private static bool Part2Overlaps(int path1Min, int path1Max, int path2Min, int path2Max)
        {
            if (path1Min > path2Max || path2Min > path1Max)
                return false;
            else
                return true;
        }
    }
}
