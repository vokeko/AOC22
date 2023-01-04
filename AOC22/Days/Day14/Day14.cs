using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC22
{
    static class Day14
    {
        internal static void RegolithReservoir(string path, bool prvni)
        {
            List<List<Position>> rocks = new List<List<Position>>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    rocks.Add(line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Position() { X = short.Parse(x.Split(',')[0]), Y = short.Parse(x.Split(',')[1]) }).ToList());
                }

            }

            short floorY = Convert.ToInt16(rocks.SelectMany(o => o).Max(m => m.Y) + 2);
            if (!prvni) rocks.Add(new List<Position> { new Position { X = 0, Y = floorY }, new Position { X = 999, Y = floorY } });

            Obstacles[,] map = FillMap(rocks);

            VisualizeMap(map);

            Console.WriteLine("Usazeného písku: {0}", FillSand(map));

        }
        private static Obstacles[,] FillMap(List<List<Position>> rocks)
        {
            Obstacles[,] map = new Obstacles[1000, 200];

            //každá struktura
            for (int structure = 0; structure < rocks.Count; structure++)
            {
                // další příkaz
                do
                {
                    map[rocks[structure][0].X, rocks[structure][0].Y] = Obstacles.Rock;
                    Direction moveHere = GetDirection(rocks[structure][0], rocks[structure][1]);

                    // vykreslování
                    while (rocks[structure][0].X != rocks[structure][1].X || rocks[structure][0].Y != rocks[structure][1].Y)
                    {
                        switch (moveHere)
                        {
                            case Direction.Left:
                                rocks[structure][0].X = (short)(rocks[structure][0].X - 1);
                                break;
                            case Direction.Right:
                                rocks[structure][0].X = (short)(rocks[structure][0].X + 1);
                                break;
                            case Direction.Down:
                                rocks[structure][0].Y = (short)(rocks[structure][0].Y + 1);
                                break;
                            case Direction.Up:
                                rocks[structure][0].Y = (short)(rocks[structure][0].Y - 1);
                                break;
                            case Direction.None:
                                throw new Exception(string.Format("Nemožný výsledek {0} {1}", moveHere, structure));
                        }
                        map[rocks[structure][0].X, rocks[structure][0].Y] = Obstacles.Rock;
                    }
                    rocks[structure].RemoveAt(0);
                }
                while (rocks[structure].Count > 1);
            }
            map[500, 0] = Obstacles.PouringPoint;

            return map;
        }
        private static Direction GetDirection(Position pos1, Position pos2)
        {
            if (pos1.X > pos2.X)
                return Direction.Left;
            else if (pos1.X < pos2.X)
                return Direction.Right;
            else if (pos1.Y < pos2.Y)
                return Direction.Down;
            else if (pos1.Y > pos2.Y)
                return Direction.Up;
            else
                return Direction.None;
        }
        private static int FillSand(Obstacles[,] map)
        {
            int sands = 0;

            while (true)
            {
                Position sandPos = new Position { X = 500, Y = 0 };
                //next sand, if sand doesnt overflow

                //fall sand, as long as it can

                FallingSpace nextMove;

                do
                {
                    nextMove = CheckSurroundings(sandPos);
                    switch (nextMove)
                    {
                        case FallingSpace.Down:
                            sandPos.Y++;
                            break;
                        case FallingSpace.DownLeft:
                            sandPos.Y++;
                            sandPos.X--;
                            break;
                        case FallingSpace.DownRight:
                            sandPos.Y++;
                            sandPos.X++;
                            break;
                        case FallingSpace.None:
                            sands++;

                            if (map[sandPos.X, sandPos.Y] == Obstacles.PouringPoint)
                            {
                                map[sandPos.X, sandPos.Y] = Obstacles.Blocked;
                                VisualizeMap(map);
                                return sands;
                            }
                            else
                                map[sandPos.X, sandPos.Y] = Obstacles.Sand;

                            break;
                        case FallingSpace.Infinite:
                            map[sandPos.X, sandPos.Y + 1] = Obstacles.Fell;
                            VisualizeMap(map);
                            return sands;
                    }

                } while (nextMove != FallingSpace.None);
            }

            FallingSpace CheckSurroundings(Position sandPos)
            {
                if (map[sandPos.X, sandPos.Y + 1] == Obstacles.Air)
                {
                    if (sandPos.Y < 190)
                        return FallingSpace.Down;
                    else
                        return FallingSpace.Infinite;
                }
                else if (map[sandPos.X - 1, sandPos.Y + 1] == Obstacles.Air)
                    return FallingSpace.DownLeft;
                else if (map[sandPos.X + 1, sandPos.Y + 1] == Obstacles.Air)
                    return FallingSpace.DownRight;
                else
                    return FallingSpace.None;
            }
        }
        private static void VisualizeMap(Obstacles[,] map)
        {
            Console.WriteLine();
            for (int y = 0; y < 200; y++)
            {
                for (int x = 400; x < 600; x++)
                {
                    switch (map[x, y])
                    {
                        case Obstacles.Air:
                            Console.Write(".");
                            break;
                        case Obstacles.Rock:
                            Console.Write("#");
                            break;
                        case Obstacles.Sand:
                            Console.Write("O");
                            break;
                        case Obstacles.PouringPoint:
                            Console.Write("+");
                            break;
                        case Obstacles.Fell:
                            Console.Write("~");
                            break;
                        case Obstacles.Blocked:
                            Console.Write("X");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return;
        }

        private class Position
        {
            internal short X;
            internal short Y;
        }

        private enum Obstacles
        {
            Air,
            Sand,
            Rock,
            PouringPoint,
            Fell,
            Blocked
        }

        private enum Direction
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        private enum FallingSpace
        {
            Down,
            DownLeft,
            DownRight,
            None,
            Infinite,
        }
    }
}