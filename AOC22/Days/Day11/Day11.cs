using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC22
{
    static class Day11
    {
        internal static void MonkeyBusiness(string path, bool prvni)
        {
            List<Monkey> monkeys = new List<Monkey>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                List<string> monkeyConstruction = new List<string>();

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("Monkey"))
                    {
                        if (monkeyConstruction.Count > 3)
                            monkeys.Add(new Monkey(monkeyConstruction));
                        monkeyConstruction = new List<string>();
                    }
                    monkeyConstruction.Add(line);
                }
                monkeys.Add(new Monkey(monkeyConstruction));
            }

            for (int i = 1; i <= 20; i++)
            {
                //one round
                for (int j = 0; j < monkeys.Count; j++)
                {
                    List<Throw> throws = monkeys[j].DoOperations();

                    foreach (Throw oneThrow in throws)
                    {
                        monkeys[oneThrow.To].Items.Add(oneThrow.WorryLevel);
                    }
                }
            }
            monkeys = monkeys.OrderByDescending(x => x.InspectionsDone).ToList();
            Console.WriteLine("Monkey business: {0}", monkeys[0].InspectionsDone * monkeys[1].InspectionsDone);
        }

        private struct Throw
        {
            internal int WorryLevel;
            internal short To;
        }

        private class Monkey
        {
            internal List<int> Items { get; }
            internal int InspectionsDone { get; private set; }
            private short DivisibleBy { get; }
            //private short MonkeyNumber { get; }
            private short ThrowTrue { get; }
            private short ThrowFalse { get; }
            private string OperationInstructions { get; }

            public Monkey(List<string> monkeyConstructor)
            {
                Items = new List<int>();
                InspectionsDone = 0;

                //number
                //MonkeyNumber = short.Parse(monkeyConstructor[0].Replace("Monkey ", "").Trim(':'));
                //items
                Items.AddRange(Array.ConvertAll(monkeyConstructor[1].Replace("Starting items:", "").Replace(" ", "").Split(','), s => int.Parse(s)));
                //operation
                OperationInstructions = monkeyConstructor[2].Replace("Operation: new = ", "").Replace(" ", "");
                //divisible
                DivisibleBy = short.Parse(monkeyConstructor[3].Replace("Test: divisible by", "").Replace(" ", ""));
                //if true
                ThrowTrue = short.Parse(monkeyConstructor[4].Replace("If true: throw to monkey ", "").Replace(" ", ""));
                //if false
                ThrowFalse = short.Parse(monkeyConstructor[5].Replace("If false: throw to monkey ", "").Replace(" ", ""));
            }

            public List<Throw> DoOperations()
            {
                List<Throw> throws = new List<Throw>();
                while (this.Items.Count > 0)
                {
                    Items[0] = computeOperation();
                    throws.Add(new Throw { WorryLevel = Items[0], To = (Items[0] % DivisibleBy == 0 ? ThrowTrue : ThrowFalse) });
                    Items.RemoveAt(0);
                    InspectionsDone++;
                }

                return throws;

                int computeOperation()
                {
                    DataTable dt = new DataTable();
                    string tempInstructions = OperationInstructions.Replace("old", Convert.ToString(Items[0]));
                    return (int)Math.Floor(Convert.ToDouble(dt.Compute(tempInstructions, "")) / 3D);
                }
            }
        }
    }
}