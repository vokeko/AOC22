using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    static class Day10
    {
        internal static void CathodeRayTube(string path, bool prvni)
        {

            List<string> commands = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    commands.Add(line);
                }
            }

            int instruction = 0, signalStrength = 0, instructionId = 0, cycleNumber = 0;
            int X = 1;
            const int maxCycle = 220;
            bool wait = false;

            do
            {
                //start
                cycleNumber++;

                signalStrength += CheckForSignalCount(cycleNumber, X);

                if (wait == true)
                {
                    wait = false;
                    X += instruction;
                    continue;
                }


                if (commands[instructionId] != "noop")
                {
                    instruction = int.Parse(commands[instructionId].Remove(0, 5));
                    wait = true;
                }
                instructionId++;

            } while (cycleNumber < maxCycle);

            Console.WriteLine("Síla signálu: {0}",signalStrength);
        }
        private static int CheckForSignalCount(int cycleNumber, int X)
        {
            if (cycleNumber % 40 == 20)
            {
                return X * cycleNumber;
            }
            else return 0;
        }
    }
}
