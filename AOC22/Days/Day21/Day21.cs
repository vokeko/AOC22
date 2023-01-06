using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;

namespace AOC22
{
    static class Day21
    {
        internal static void MonkeyMatch(string path, bool prvni)
        {
            List<Monkey> allMonkeys = new List<Monkey>();
            List<Monkey> unsolvedMonkeys = new List<Monkey>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    allMonkeys.Add(new Monkey(line.Replace(" ", "")));
                }
            }
            unsolvedMonkeys.AddRange(allMonkeys);

            while (unsolvedMonkeys.Any(o => o.Name == "root"))
            {
                for (int m = 0; m < unsolvedMonkeys.Count; m++)
                {
                    Monkey monkey = unsolvedMonkeys[m];
                    if (monkey.Number == null)
                    {
                        monkey.ComputeNumber();
                    }

                    if (monkey.Number != null)
                    {
                        unsolvedMonkeys.Where(o => o.Operation != null && o.Operation.Contains(monkey.Name))
                            .ToList()
                            .SetValue(c => c.Operation = c.Operation
                            .Replace(monkey.Name, Convert.ToString(monkey.Number)));
                        unsolvedMonkeys.Remove(monkey);
                        m--;
                    }
                }
            }

            Console.WriteLine("Číslo: {0}", allMonkeys.First(o => o.Name == "root").Number);
        }
        private class Monkey
        {
            internal string Name { get; }
            internal Nullable<long> Number { get; private set; }
            internal string Operation { get; set; }

            public Monkey(string line)
            {
                Name = line.Substring(0, 4);
                line = line.Substring(5, line.Length - 5);
                if (line.Intersect(new List<char> { '*', '-', '+', '/' }).Any())
                {
                    Operation = line;
                    Number = null;
                }
                else
                {
                    Number = int.Parse(line);
                    Operation = null;
                }

            }

            internal void ComputeNumber()
            {
                if (Operation.Contains('/'))
                {
                    string[] nums = Operation.Split('/');
                    bool succ = long.TryParse(nums[0], out long num1);
                    if (succ)
                    {
                        succ = long.TryParse(nums[1], out long num2);
                        if (!succ)
                        {
                            Number = null;
                            return;
                        }
                        Number = num1 / num2;
                    }
                    else Number = null;
                }
                else if (Operation.Contains('*'))
                {
                    string[] nums = Operation.Split('*');
                    bool succ = long.TryParse(nums[0], out long num1);
                    if (succ)
                    {
                        succ = long.TryParse(nums[1], out long num2);
                        if (!succ)
                        {
                            Number = null;
                            return;
                        }
                        Number = num1 * num2;
                    }
                    else Number = null;
                }
                else if (Operation.Contains('+'))
                {
                    string[] nums = Operation.Split('+');
                    bool succ = long.TryParse(nums[0], out long num1);
                    if (succ)
                    {
                        succ = long.TryParse(nums[1], out long num2);
                        if (!succ)
                        {
                            Number = null;
                            return;
                        }
                        Number = num1 + num2;
                    }
                    else Number = null;
                }
                else if (Operation.Contains('-'))
                {
                    string[] nums = Operation.Split('-');
                    bool succ = long.TryParse(nums[0], out long num1);
                    if (succ)
                    {
                        succ = long.TryParse(nums[1], out long num2);
                        if (!succ)
                        {
                            Number = null;
                            return;
                        }
                        Number = num1 - num2;
                    }
                    else Number = null;
                }
            }
        }
    }
}