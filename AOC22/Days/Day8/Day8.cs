using System;
using System.IO;

namespace AOC22
{
    static class Day8
    {
        internal static void TreetopTreeHouse(string path, bool prvni)
        {
            Tree[,] trees = GetTrees(path);

            if (prvni)
            {
                CheckVisibilityPart1(trees);
                int visible = CountVisibility(trees);
                Console.WriteLine("Viditelných: {0}", visible);
            }
            else
            {
                CheckVisibilityPart2(trees);
                int quality = CountQuality(trees);
                Console.WriteLine("Nejlepší kvalita: {0}", quality);
            }
        }
        private static Tree[,] GetTrees(string path)
        {
            string[] lines;
            using (StreamReader sr = new StreamReader(path))
            {
                string data;
                data = sr.ReadToEnd();
                char[] delims = new[] { '\r', '\n' };
                lines = data.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            }

            Tree[,] trees = new Tree[lines[0].Length, lines.Length];

            for (int y = lines.Length - 1; y >= 0; y--)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    trees[x, y] = new Tree(Convert.ToInt16(lines[y].Substring(x, 1)));
                }
            }

            return trees;
        }

        #region Part1
        private static void CheckVisibilityPart1(Tree[,] trees)
        {
            int lengthX = trees.GetLength(0);
            int lengthY = trees.GetLength(1);

            //>
            for (int y = 0; y < lengthY; y++)
            {
                short min = -1;
                for (int x = 0; x < lengthX; x++)
                {
                    if (trees[x, y].Number > min)
                    {
                        trees[x, y].VisibleFromWest = true;
                        min = trees[x, y].Number;
                    }
                    else
                        trees[x, y].VisibleFromWest = false;
                }
            }
            //<
            for (int y = 0; y < lengthY; y++)
            {
                short min = -1;
                for (int x = lengthX - 1; x >= 0; x--)
                {
                    if (trees[x, y].Number > min)
                    {
                        trees[x, y].VisibleFromEast = true;
                        min = trees[x, y].Number;
                    }
                    else
                        trees[x, y].VisibleFromEast = false;
                }
            }
            //^
            for (int x = 0; x < lengthX; x++)
            {
                short min = -1;
                for (int y = 0; y < lengthY; y++)
                {
                    if (trees[x, y].Number > min)
                    {
                        trees[x, y].VisibleFromNorth = true;
                        min = trees[x, y].Number;
                    }
                    else
                        trees[x, y].VisibleFromNorth = false;
                }
            }
            //v
            for (int x = 0; x < lengthX; x++)
            {
                short min = -1;
                for (int y = lengthY - 1; y >= 0; y--)
                {
                    if (trees[x, y].Number > min)
                    {
                        trees[x, y].VisibleFromSouth = true;
                        min = trees[x, y].Number;
                    }
                    else
                        trees[x, y].VisibleFromSouth = false;
                }
            }
        }
        private static int CountVisibility(Tree[,] trees)
        {
            int lengthX = trees.GetLength(0);
            int lengthY = trees.GetLength(1);
            int visible = 0;

            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (trees[x, y].Visible) visible++;
                }
            }

            return visible;
        }
        #endregion
        #region Part2
        private static void CheckVisibilityPart2(Tree[,] trees)
        {
            int lengthX = trees.GetLength(0);
            int lengthY = trees.GetLength(1);

            //check all trees then loop all trees
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    trees[x, y].SetQuality(trees, x, y);
                }
            }
        }
        private static int CountQuality(Tree[,] trees)
        {
            int lengthX = trees.GetLength(0);
            int lengthY = trees.GetLength(1);
            int quality = 0;

            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (trees[x, y].Quality > quality) quality = trees[x, y].Quality;
                }
            }

            return quality;
        }
        #endregion

        internal class Tree
        {
            internal short Number { get; }

            #region Part1
            internal bool Visible
            {
                get { return VisibleFromNorth || VisibleFromSouth || VisibleFromWest || VisibleFromEast; }
            }

            internal bool VisibleFromNorth;
            internal bool VisibleFromSouth;
            internal bool VisibleFromWest;
            internal bool VisibleFromEast;
            #endregion
            #region Part2
            internal int Quality { get; private set; }
            internal void SetQuality(Tree[,] trees, int _x, int _y)
            {


                int lengthX = trees.GetLength(0);
                int lengthY = trees.GetLength(1);

                short eastTrees = 0;

                //>
                for (int x = _x + 1; x < lengthX; x++)
                {
                    eastTrees++;
                    if (this.Number <= trees[x, _y].Number) break;
                }

                short westTrees = 0;

                //<
                for (int x = _x - 1; x >= 0; x--)
                {

                    westTrees++;
                    if (this.Number <= trees[x, _y].Number) break;
                }
                short northTrees = 0;

                //^
                for (int y = _y + 1; y < lengthY; y++)
                {
                    northTrees++;
                    if (this.Number <= trees[_x, y].Number) break;
                }

                short southTrees = 0;
                //v
                for (int y = _y - 1; y >= 0; y--)
                {
                    southTrees++;
                    if (this.Number <= trees[_x, y].Number) break;
                }

                this.Quality = westTrees * eastTrees * northTrees * southTrees;
            }
            #endregion

            internal Tree(short _number)
            {
                Number = _number;
            }
        }
    }
}