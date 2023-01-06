using System;
using System.Collections.Generic;
using System.IO;

namespace AOC22
{
    static class Day20
    {
        internal static void GrovePositioningSystem(string path, bool prvni)
        {
            List<Number> EncryptedFile = new List<Number>();
            short pos = 0;
            int key = prvni ? 1 : 811589153;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    EncryptedFile.Add(new Number { Value = long.Parse(line) * key, InitialPosition = pos });
                    pos++;
                }
            }

            short mixes = prvni ? (short)1 : (short)10;

            for (short m = 0; m < mixes; m++)
            {
                for (int i = 0; i < EncryptedFile.Count; i++)
                {
                    int index = EncryptedFile.FindIndex(o => o.InitialPosition == i);
                    Number number = EncryptedFile[index];
                    MixNumber(index, number, EncryptedFile);
                }
            }

            long val1 = FindValue(EncryptedFile, 1000);
            long val2 = FindValue(EncryptedFile, 2000);
            long val3 = FindValue(EncryptedFile, 3000);

            Console.WriteLine("Suma: {0}", val1 + val2 + val3);
            Console.WriteLine("1000: {0} 2000: {1} 3000: {2}", val1, val2, val3);
        }
        private struct Number
        {
            internal short InitialPosition;
            internal long Value;

            public override string ToString() => $"Value: {Value}; InitialPosition: {InitialPosition}";
        }
        private static void MixNumber(int index, Number number, List<Number> EncryptedFile)
        {
            int newIndex = (int)((index + number.Value) % (EncryptedFile.Count - 1));
            if (newIndex <= 0 && index + number.Value != 0)
            {
                newIndex = EncryptedFile.Count - 1 + newIndex;
            }

            EncryptedFile.RemoveAt(index);
            EncryptedFile.Insert(newIndex, number);
        }

        private static long FindValue(List<Number> EncryptedFile, short position)
        {
            position = (short)(position + EncryptedFile.FindIndex(o => o.Value == 0));
            position = (short)(position % EncryptedFile.Count);
            return EncryptedFile[position].Value;
        }
    }
}