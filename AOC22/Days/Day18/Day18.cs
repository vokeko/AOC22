using System;
using System.IO;

namespace AOC22
{
    static class Day18
    {
        internal static void BoilingBoulders(string path, bool prvni)
        {
            bool[,,] grid = new bool[22, 22, 22];
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                short[] cube;
                while ((line = sr.ReadLine()) != null)
                {
                    cube = Array.ConvertAll(line.Split(','), s => short.Parse(s));
                    grid[cube[0], cube[1], cube[2]] = true;
                }
            }

            int sides = 0;

            for (short z = 0; z <= 21; z++)
            {
                for (short y = 0; y <= 21; y++)
                {
                    for (short x = 0; x <= 21; x++)
                    {
                        if (grid[x, y, z] == false)
                            continue;
                        else
                        {
                            sides += CountSides(grid, x, y, z);
                        }
                    }
                }
            }

            Console.WriteLine("Stran: {0}", sides);
        }
        private static short CountSides(bool[,,] grid, short x, short y, short z)
        {
            short sides = 0;

            if (x == 0 || grid[x - 1, y, z] == false) sides++;
            if (x == 21 || grid[x + 1, y, z] == false) sides++;
            if (y == 0 || grid[x, y - 1, z] == false) sides++;
            if (y == 21 || grid[x, y + 1, z] == false) sides++;
            if (z == 0 || grid[x, y, z - 1] == false) sides++;
            if (z == 21 || grid[x, y, z + 1] == false) sides++;

            return sides;
        }
    }
}