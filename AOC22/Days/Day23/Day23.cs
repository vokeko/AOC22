using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC22
{
    static class Day23
    {
        internal static void UnstableDiffusion(string path, bool prvni)
        {
            List<Elf> elves = GetElves(path);
            short rounds = prvni ? (short)10 : short.MaxValue;
            bool moved = false;
            List<Direction> directions = new List<Direction> { Direction.North, Direction.South, Direction.West, Direction.East };

            for (int r = 0; r < rounds; r++)
            {
                moved = false;

                Visualize(elves, r, r % 100 == 0 || prvni);

                foreach (Elf elf in elves)
                {
                    elf.ProposeSpace(directions, elves);
                }

                foreach (Elf elf in elves)
                {
                    moved = elf.TryMoving(elves) || moved;
                }

                foreach (Elf elf in elves)
                {
                    elf.Move();
                }

                if (!moved && !prvni)
                {
                    rounds = (short)(r + 1);
                    break;
                }

                DirectionsShift(directions);
            }
            Visualize(elves, rounds, true);
        }
        private class Elf
        {
            private int ProposedSpaceX;
            private int ProposedSpaceY;
            private bool move = false;
            internal int X { get; private set; }
            internal int Y { get; private set; }

            internal void ProposeSpace(List<Direction> directions, List<Elf> elves)
            {
                Direction direction = Direction.None;
                if (elves.Any(o => o.X >= X - 1 && o.X <= X + 1 && o.Y >= Y - 1 && o.Y <= Y + 1 && !(o.X == this.X && o.Y == this.Y)))
                {
                    foreach (Direction directionNew in directions)
                    {
                        if (!IsOccupied(directionNew, elves))
                        {
                            direction = directionNew;
                            break;
                        }
                    }
                }

                switch (direction)
                {
                    case Direction.North:
                        ProposedSpaceX = X;
                        ProposedSpaceY = Y + 1;
                        break;
                    case Direction.South:
                        ProposedSpaceX = X;
                        ProposedSpaceY = Y - 1;
                        break;
                    case Direction.West:
                        ProposedSpaceX = X - 1;
                        ProposedSpaceY = Y;
                        break;
                    case Direction.East:
                        ProposedSpaceX = X + 1;
                        ProposedSpaceY = Y;
                        break;
                    case Direction.None:
                        ProposedSpaceX = X;
                        ProposedSpaceY = Y;
                        break;
                }

                return;
            }

            internal bool TryMoving(List<Elf> elves)
            {
                if (elves.Where(o => o.ProposedSpaceX == this.ProposedSpaceX && o.ProposedSpaceY == this.ProposedSpaceY).ToList().Count > 1 || (this.ProposedSpaceX == this.X && this.ProposedSpaceY == this.Y))
                    move = false;
                else
                    move = true;
                return move;
            }

            internal void Move()
            {
                if (this.move)
                {
                    this.X = ProposedSpaceX;
                    this.Y = ProposedSpaceY;
                    this.move = false;
                }
            }

            public Elf(int _x, int _y)
            {
                X = _x;
                Y = _y;
            }

            private bool IsOccupied(Direction direction, List<Elf> elves)
            {
                switch (direction)
                {
                    case Direction.North:
                        if (elves.Any(o => o.Y == Y + 1 && o.X >= X - 1 && o.X <= X + 1))
                            return true;
                        return false;
                    case Direction.South:
                        if (elves.Any(o => o.Y == Y - 1 && o.X >= X - 1 && o.X <= X + 1))
                            return true;
                        return false;
                    case Direction.West:
                        if (elves.Any(o => o.X == X - 1 && o.Y >= Y - 1 && o.Y <= Y + 1))
                            return true;
                        return false;
                    case Direction.East:
                        if (elves.Any(o => o.X == X + 1 && o.Y >= Y - 1 && o.Y <= Y + 1))
                            return true;
                        return false;
                }
                return false;
            }

        }

        private enum Direction
        {
            None,
            North,
            South,
            West,
            East,
        }
        private static List<Elf> GetElves(string path)
        {
            string[] lines;
            using (StreamReader sr = new StreamReader(path))
            {
                string data;
                data = sr.ReadToEnd();
                char[] delims = new[] { '\r', '\n' };
                lines = data.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            }

            List<Elf> elves = new List<Elf>();

            int oldY = 0;
            for (int y = lines.Length + 10; y > 10; y--)
            {
                int oldX = 0;
                for (int x = 10; x < 10 + lines[oldY].Length; x++)
                {
                    if (lines[oldY][oldX] != '.')
                        elves.Add(new Elf(x, y));
                    oldX++;
                }
                oldY++;
            }
            return elves;
        }
        private static void DirectionsShift(List<Direction> directions)
        {
            Direction direction = directions[0];
            directions.RemoveAt(0);
            directions.Add(direction);
        }

        private static void Visualize(List<Elf> elves, int cycle, bool visualize)
        {
            Console.WriteLine("Proběhlo kol: {0}", cycle);
            Console.WriteLine();

            int xMax = elves.Max(o => o.X);
            int yMax = elves.Max(o => o.Y);
            int xmin = elves.Min(o => o.X);
            int ymin = elves.Min(o => o.Y);
            int empty = 0;

            if (visualize)
            {
                for (int y = yMax; y >= ymin; y--)
                {
                    for (int x = xmin; x <= xMax; x++)
                    {
                        if (elves.Where(o => o.X == x && o.Y == y).ToList().Count > 0) Console.Write('#');
                        else
                        {
                            Console.Write('.');
                            empty++;
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("Prázdných polí: {0}", empty);
            }
            Console.WriteLine();
        }
    }
}