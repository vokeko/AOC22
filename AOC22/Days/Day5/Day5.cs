using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC22
{
    static class Day5
    {
        internal static void SupplyStacks(string path, bool prvni)
        {
            #region Inicializace
            List<string> unsortedBoxLines = new List<string>();
            List<Instruction> instructions = new List<Instruction>();
            int columnCount = 0;
            #endregion

            #region Čtení, plnění do kolekcí
            using (StreamReader sr = new StreamReader(path))
            {
                bool isInstructions = false;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (isInstructions)
                    {
                        instructions.Add(new Instruction(line));
                    }
                    else if (line.Contains("["))
                        unsortedBoxLines.Add(line);
                    else if (string.IsNullOrWhiteSpace(line))
                        isInstructions = true;
                    else
                    {
                        columnCount = line.Split(' ').Where(o => !string.IsNullOrWhiteSpace(o)).Select(int.Parse).ToArray().Max();
                    }
                }
            }
            #endregion

            #region Plnění kolekce boxů
            unsortedBoxLines.Reverse();

            List<List<char>> boxes = new List<List<char>>();
            for (int i = 0; i < columnCount; i++)
            {
                boxes.Add(new List<char>());
            }

            foreach (string unsortedLine in unsortedBoxLines)
            {
                List<string> chunks = SplitToChunks(unsortedLine).ToList();
                for (int j = 0; j < chunks.Count(); j++)
                {
                    if (chunks[j].Contains("["))
                    {
                        boxes[j].Add(chunks[j].Replace(" ", "").Replace("[", "").Replace("]", "").ToCharArray()[0]);
                    }
                }
            } 
            #endregion

            #region Přesun beden podle instrukcí
            if (prvni)
                foreach (Instruction instruction in instructions)
                {
                    instruction.MoveBoxPart1(ref boxes);
                }
            else
                foreach (Instruction instruction in instructions)
                {
                    instruction.MoveBoxPart2(ref boxes);
                }
            #endregion

            #region Zjišťování vrchních beden
            string topCrates = "";

            for (int k = 0; k < columnCount; k++)
            {
                topCrates += boxes[k][boxes[k].Count - 1];
            }

            Console.WriteLine("Vrchní bedny jsou: {0}", topCrates);
            #endregion
        }
        private class Instruction
        {
            private readonly short Amount;
            private readonly short From;
            private readonly short To;

            public Instruction(string line)
            {
                string[] splitLine = line.Split(' ');
                Amount = short.Parse(splitLine[1]);
                From = short.Parse(splitLine[3]);
                To = short.Parse(splitLine[5]);

            }
            internal void MoveBoxPart1(ref List<List<char>> boxes)
            {
                for (int i = 0; i < this.Amount; i++)
                {
                    char box = boxes[From - 1].Last();
                    boxes[From - 1].RemoveAt(boxes[From - 1].Count - 1);
                    boxes[To - 1].Add(box);
                }
            }
            internal void MoveBoxPart2(ref List<List<char>> boxes)
            {
                List<char> movedBoxes = boxes[From - 1].Skip(boxes[From - 1].Count - this.Amount).ToList();
                boxes[From - 1].RemoveRange(boxes[From - 1].Count - this.Amount, this.Amount);
                boxes[To - 1].AddRange(movedBoxes);
            }
        }
        private static IEnumerable<string> SplitToChunks(string str)
        {
            //return Enumerable.Range(0, str.Length / 4)
            //    .Select(i => str.Substring(i * 4, 4));
            for (int i = 0; i < str.Length; i += 4)
                yield return str.Substring(i, str.Length - i >= 4 ? 4 : str.Length - i);
        }
    }
}
