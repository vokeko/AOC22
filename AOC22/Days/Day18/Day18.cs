using System;
using System.IO;

namespace AOC22
{
    static class Day18
    {
        internal static void BoilingBoulders(string path, bool prvni)
        {
            Area[,,] grid = new Area[22, 22, 22];
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                short[] cube;
                while ((line = sr.ReadLine()) != null)
                {
                    cube = Array.ConvertAll(line.Split(','), s => short.Parse(s));
                    grid[cube[0], cube[1], cube[2]] = Area.Cube;
                }
            }

            int sides = 0;

            if (prvni)
            {
                for (short z = 0; z <= 21; z++)
                {
                    for (short y = 0; y <= 21; y++)
                    {
                        for (short x = 0; x <= 21; x++)
                        {
                            if (grid[x, y, z] == Area.Air)
                                continue;
                            else
                            {
                                sides += CountSidesPart1(grid, x, y, z);
                            }
                        }
                    }
                }
            }
            else
            {
                SteamGrid(grid);

                for (short z = 0; z <= 21; z++)
                {
                    for (short y = 0; y <= 21; y++)
                    {
                        for (short x = 0; x <= 21; x++)
                        {
                            if (grid[x, y, z] != Area.Cube)
                                continue;
                            else
                            {
                                sides += CountSidesPart2(grid, x, y, z);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Stran: {0}", sides);
        }

        #region Part 1
        private static short CountSidesPart1(Area[,,] grid, short x, short y, short z)
        {
            short sides = 0;

            if (x == 0 || grid[x - 1, y, z] == Area.Air) sides++;
            if (x == 21 || grid[x + 1, y, z] == Area.Air) sides++;
            if (y == 0 || grid[x, y - 1, z] == Area.Air) sides++;
            if (y == 21 || grid[x, y + 1, z] == Area.Air) sides++;
            if (z == 0 || grid[x, y, z - 1] == Area.Air) sides++;
            if (z == 21 || grid[x, y, z + 1] == Area.Air) sides++;

            return sides;
        }
        #endregion

        #region Part 2
        private static short CountSidesPart2(Area[,,] grid, short x, short y, short z)
        {
            short sides = 0;

            if (x == 0 || grid[x - 1, y, z] == Area.Steam) sides++;
            if (x == 21 || grid[x + 1, y, z] == Area.Steam) sides++;
            if (y == 0 || grid[x, y - 1, z] == Area.Steam) sides++;
            if (y == 21 || grid[x, y + 1, z] == Area.Steam) sides++;
            if (z == 0 || grid[x, y, z - 1] == Area.Steam) sides++;
            if (z == 21 || grid[x, y, z + 1] == Area.Steam) sides++;

            return sides;
        }

        private static void SteamGrid(Area[,,] grid)
        {
            //It takes multiple iterations to steam all the viable spaces
            for (int l = 0; l < 4; l++)
            {
                for (short z = 0; z <= 21; z++)
                {
                    for (short y = 0; y <= 21; y++)
                    {
                        for (short x = 0; x <= 21; x++)
                        {
                            if (grid[x, y, z] != Area.Air)
                                continue;
                            else
                            {
                                if (x == 0 || x == 21 || y == 0 || y == 21 || z == 0 || z == 21)
                                    grid[x, y, z] = Area.Steam;
                                else if (grid[x - 1, y, z] == Area.Steam || grid[x + 1, y, z] == Area.Steam || grid[x, y - 1, z] == Area.Steam || grid[x, y + 1, z] == Area.Steam || grid[x, y, z - 1] == Area.Steam || grid[x, y, z + 1] == Area.Steam)
                                    grid[x, y, z] = Area.Steam;
                            }
                        }
                    }
                }


                for (short z = 21; z >= 0; z--)
                {
                    for (short y = 21; y >= 0; y--)
                    {
                        for (short x = 21; x >= 0; x--)
                        {
                            if (grid[x, y, z] != Area.Air)
                                continue;
                            else
                            {
                                if (x == 0 || x == 21 || y == 0 || y == 21 || z == 0 || z == 21)
                                    grid[x, y, z] = Area.Steam;
                                else if (grid[x - 1, y, z] == Area.Steam || grid[x + 1, y, z] == Area.Steam || grid[x, y - 1, z] == Area.Steam || grid[x, y + 1, z] == Area.Steam || grid[x, y, z - 1] == Area.Steam || grid[x, y, z + 1] == Area.Steam)
                                    grid[x, y, z] = Area.Steam;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private enum Area
        {
            Air,
            Cube,
            Steam
        }
    }
}