using System;
using System.Collections.Generic;
using System.IO;

namespace AOC22
{
    static class Day9
    {
        internal static void RopeBridge(string path, bool prvni)
        {
            List<Instruction> Instructions = new List<Instruction>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                string[] info;
                while ((line = sr.ReadLine()) != null)
                {
                    info = line.Split(' ');

                    Instruction instruction = new Instruction {Steps = short.Parse(info[1]) };
                    instruction.SetDirection(info[0]);

                    Instructions.Add(instruction);
                }
            }

            bool[,] grid;
            if (path.Contains("test"))
            {
                grid = new bool[20, 20];
            }
            else
            {
                grid = new bool[1000, 1000];
            }

            Position headPosition = new Position();
            headPosition.X = grid.GetLength(1) / 2;
            headPosition.Y = grid.GetLength(0) / 2;

            if (prvni)
            {
                Position tailPosition = new Position();
                tailPosition.X = grid.GetLength(1) / 2;
                tailPosition.Y = grid.GetLength(0) / 2;

                foreach (Instruction instruction in Instructions)
                {
                    for (int i = 0; i < instruction.Steps; i++)
                    {
                        StepPart1(ref tailPosition, ref headPosition, instruction.Direction);
                        grid[tailPosition.X, tailPosition.Y] = true;
                    }
                }
            }
            else
            {
                List<Position> knots = new List<Position>();
                for (int k = 0; k < 9; k++)
                {
                    knots.Add(new Position { X = grid.GetLength(1) / 2, Y = grid.GetLength(0) / 2 });
                }

                foreach (Instruction instruction in Instructions)
                {
                    for (int i = 0; i < instruction.Steps; i++)
                    {
                        StepPart2(ref knots, ref headPosition, instruction.Direction);
                        grid[knots[8].X, knots[8].Y] = true;
                    }
                }
            }

            Console.WriteLine("Počet: {0}", SumArray(grid));
        }
        private enum Direction
        {
            None,
            North,
            South,
            West,
            East,
            NorthWest,
            NorthEast,
            SouthWest,
            SouthEast,
        }

        private class Instruction
        {
            internal Direction Direction { get; private set; }
            internal short Steps { get; set; }
            internal void SetDirection(string letter)
            {
                switch(letter)
                {
                    case "U":
                        this.Direction = Direction.North;
                        break;
                    case "D":
                        this.Direction = Direction.South;
                        break;
                    case "L":
                        this.Direction = Direction.West;
                        break;
                    case "R":
                        this.Direction = Direction.East;
                        break;
                }
            }
        }

        private struct Position
        {
            internal int X { get; set; }
            internal int Y { get; set; }
        }

        private static void StepPart1(ref Position tailPosition, ref Position headPosition, Direction headDirection)
        {
            Position tempTailPosition = tailPosition;
            Position tempHeadPosition = Move(headDirection, headPosition);

            Direction tailDirection = getTailDirection();

            tailPosition = Move(tailDirection, tailPosition);
            headPosition = tempHeadPosition;
            
            return;

            Direction getTailDirection()
            {
                Direction tempDirection = default;
                if (Math.Abs(tempTailPosition.X - tempHeadPosition.X) > 1 || Math.Abs(tempHeadPosition.X - tempTailPosition.X) > 1 || Math.Abs(tempTailPosition.Y - tempHeadPosition.Y) > 1 || Math.Abs(tempHeadPosition.Y - tempTailPosition.Y) > 1)
                {
                    if (tempTailPosition.X == tempHeadPosition.X)
                    {
                        if (tempTailPosition.Y > tempHeadPosition.Y)
                            tempDirection = Direction.South;
                        else
                            tempDirection = Direction.North;
                    }
                    else if (tempTailPosition.Y == tempHeadPosition.Y)
                    {
                        if (tempTailPosition.X > tempHeadPosition.X)
                            tempDirection = Direction.West;
                        else
                            tempDirection = Direction.East;
                    }
                    else
                    {
                        if (tempTailPosition.X > tempHeadPosition.X && tempTailPosition.Y < tempHeadPosition.Y)
                            tempDirection = Direction.NorthWest;
                        else if (tempTailPosition.Y < tempHeadPosition.Y && tempTailPosition.X < tempHeadPosition.X)
                            tempDirection = Direction.NorthEast;
                        else if (tempTailPosition.Y > tempHeadPosition.Y && tempTailPosition.X > tempHeadPosition.X)
                            tempDirection = Direction.SouthWest;
                        else if (tempTailPosition.Y > tempHeadPosition.Y && tempTailPosition.X < tempHeadPosition.X)
                            tempDirection = Direction.SouthEast;
                    }
                }

                return tempDirection;
            }
        }

        private static void StepPart2(ref List<Position> knots, ref Position headPosition, Direction headDirection)
        {
            Position tempHeadPosition = Move(headDirection, headPosition);
            headPosition = tempHeadPosition;

            for (int k = 0; k < knots.Count; k++)
            {
                Direction knotDirection = getKnotDirection(knots[k], k == 0 ? headPosition : knots[k - 1]);
                knots[k] = Move(knotDirection, knots[k]);
            }
            
            return;

            Direction getKnotDirection(Position thisKnot, Position previousKnot)
            {
                Direction tempDirection = default;
                if (Math.Abs(thisKnot.X - previousKnot.X) > 1 || Math.Abs(previousKnot.X - thisKnot.X) > 1 || Math.Abs(thisKnot.Y - previousKnot.Y) > 1 || Math.Abs(previousKnot.Y - thisKnot.Y) > 1)
                {
                    if (thisKnot.X == previousKnot.X)
                    {
                        if (thisKnot.Y > previousKnot.Y)
                            tempDirection = Direction.South;
                        else
                            tempDirection = Direction.North;
                    }
                    else if (thisKnot.Y == previousKnot.Y)
                    {
                        if (thisKnot.X > previousKnot.X)
                            tempDirection = Direction.West;
                        else
                            tempDirection = Direction.East;
                    }
                    else
                    {
                        if (thisKnot.X > previousKnot.X && thisKnot.Y < previousKnot.Y)
                            tempDirection = Direction.NorthWest;
                        else if (thisKnot.Y < previousKnot.Y && thisKnot.X < previousKnot.X)
                            tempDirection = Direction.NorthEast;
                        else if (thisKnot.Y > previousKnot.Y && thisKnot.X > previousKnot.X)
                            tempDirection = Direction.SouthWest;
                        else if (thisKnot.Y > previousKnot.Y && thisKnot.X < previousKnot.X)
                            tempDirection = Direction.SouthEast;
                    }
                }

                return tempDirection;
            }
        }

        private static int SumArray(bool[,] grid)
        {
            int count = 0;
            int width = grid.GetLength(1);
            int height = grid.GetLength(0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[x,y] == true)
                        count++;
                }
            }
            return count;
        }

        private static Position Move(Direction direction, Position position)
        {
            switch (direction)
            {
                case Direction.North:
                    position.Y++;
                    break;
                case Direction.South:
                    position.Y--;
                    break;
                case Direction.West:
                    position.X--;
                    break;
                case Direction.East:
                    position.X++;
                    break;
                case Direction.NorthWest:
                    position.Y++;
                    position.X--;
                    break;
                case Direction.NorthEast:
                    position.Y++;
                    position.X++;
                    break;
                case Direction.SouthWest:
                    position.Y--;
                    position.X--;
                    break;
                case Direction.SouthEast:
                    position.Y--;
                    position.X++;
                    break;
            }
            return position;
        }
    }
}
