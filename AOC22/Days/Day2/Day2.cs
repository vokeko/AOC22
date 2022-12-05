using System;
using System.IO;

namespace AOC22
{
    static class Day2
    {
        internal static void RPSScore(string path, bool prvni)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                char[] chars;
                string line;
                int score = 0;

                if (prvni)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Match match = new Match();
                        chars = line.ToLower().ToCharArray();

                        foreach (char c in chars)
                        {
                            match.SetSymbol_Part1(c);
                        }
                        score += (int)match.PlayerSymbol;
                        score += (int)match.CalculateWin();
                    }
                    Console.WriteLine("Skóre: {0}", score);
                }
                else
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Match match = new Match();
                        chars = line.ToLower().ToCharArray();

                        foreach (char c in chars)
                        {
                            match.SetSymbol_Part2(c);
                        }
                        match.CalculateSymbol();
                        score += (int)match.RequiredOutcome;
                        score += (int)match.PlayerSymbol;
                    }
                    Console.WriteLine("Skóre: {0}", score);
                }
            }
        }
        private class Match
        {
            public ChosenSymbol PlayerSymbol { get; private set; }
            public ChosenSymbol EnemySymbol { get; private set; }
            public Outcome RequiredOutcome { get; private set; }
            public void SetSymbol_Part1(char c)
            {
                switch (c)
                {
                    case 'a':
                        this.EnemySymbol = ChosenSymbol.Rock;
                        break;
                    case 'b':
                        this.EnemySymbol = ChosenSymbol.Paper;
                        break;
                    case 'c':
                        this.EnemySymbol = ChosenSymbol.Scissors;
                        break;
                    case 'x':
                        this.PlayerSymbol = ChosenSymbol.Rock;
                        break;
                    case 'y':
                        this.PlayerSymbol = ChosenSymbol.Paper;
                        break;
                    case 'z':
                        this.PlayerSymbol = ChosenSymbol.Scissors;
                        break;
                }
            }
            public void SetSymbol_Part2(char c)
            {
                switch (c)
                {
                    case 'a':
                        this.EnemySymbol = ChosenSymbol.Rock;
                        break;
                    case 'b':
                        this.EnemySymbol = ChosenSymbol.Paper;
                        break;
                    case 'c':
                        this.EnemySymbol = ChosenSymbol.Scissors;
                        break;
                    case 'x':
                        this.RequiredOutcome = Outcome.Lose;
                        break;
                    case 'y':
                        this.RequiredOutcome = Outcome.Draw;
                        break;
                    case 'z':
                        this.RequiredOutcome = Outcome.Win;
                        break;
                }
            }
            public Outcome CalculateWin()
            {
                if (this.PlayerSymbol == this.EnemySymbol) //draw
                    return Outcome.Draw;
                else if (this.PlayerSymbol == this.EnemySymbol + 1 || this.PlayerSymbol == this.EnemySymbol - 2) //win
                    return Outcome.Win;
                else //lose
                    return Outcome.Lose;
            }
            public void CalculateSymbol()
            {
                switch (RequiredOutcome)
                {
                    case Outcome.Draw:
                        this.PlayerSymbol = this.EnemySymbol;
                        break;
                    case Outcome.Win:
                        if (this.EnemySymbol == ChosenSymbol.Scissors)
                            this.PlayerSymbol = ChosenSymbol.Rock;
                        else
                            this.PlayerSymbol = this.EnemySymbol + 1;
                        break;
                    case Outcome.Lose:
                        if (this.EnemySymbol == ChosenSymbol.Rock)
                            this.PlayerSymbol = ChosenSymbol.Scissors;
                        else
                            this.PlayerSymbol = this.EnemySymbol - 1;
                        break;
                }
            }
        }
        private enum ChosenSymbol
        {
            Undefined = -1,
            Rock = 1,
            Paper = 2,
            Scissors = 3,
        }
        private enum Outcome
        {
            Undefined = -1,
            Lose = 0,
            Draw = 3,
            Win = 6,
        }
    }
}
