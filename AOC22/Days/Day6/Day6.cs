using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC22
{
    static class Day6
    {
        internal static void TuningTrouble(string path, bool prvni)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                int markerPosition;

                if (prvni) markerPosition = 4;
                else markerPosition = 14;

                for (int i = 0; i < line.Length; i++)
                {
                    string sequence = line.Substring(i, markerPosition);
                    var distinct = sequence.Distinct().ToList();
                    if (distinct.Count == sequence.Length)
                    {
                        markerPosition += i;
                        break;
                    }
                }
                Console.WriteLine("Pozice: {0}", markerPosition);
            }
        }
    }
}
