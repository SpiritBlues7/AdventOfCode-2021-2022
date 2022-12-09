using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Channels;

namespace AdventOfCode.Solutions.Year2022
{

    class Day09 : ASolution
    {

        public Day09() : base(09, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            Dictionary<char, (int, int)> DIR = new Dictionary<char, (int, int)>() {
                {'U', ( 0,  1)},
                {'D', ( 0, -1)},
                {'L', (-1,  0)},
                {'R', ( 1,  0)},
            };

            int headX = 0;
            int headY = 0;
            int tailX = 0;
            int tailY = 0;

            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            visited.Add((tailX, tailY));
            foreach (string line in lines)
            {
                string[] comp = line.Split(" ");
                char dir = comp[0][0];
                long amount = comp[1].ToLong();

                for (int i = 0; i < amount; i++)
                {
                    headX += DIR[dir].Item1;
                    headY += DIR[dir].Item2;

                    int adjX = 0;
                    int adjY = 0;

                    adjY = 1 * Math.Sign(headY - tailY);
                    adjX = 1 * Math.Sign(headX - tailX);

                    if (tailX + adjX == headX && tailY + adjY == headY)
                    {
                        continue;
                    }

                    tailX += adjX;
                    tailY += adjY;
                    visited.Add((tailX, tailY));
                }
            }

            return visited.Count().ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            Dictionary<char, (int, int)> DIR = new Dictionary<char, (int, int)>() {
                {'U', ( 0,  1)},
                {'D', ( 0, -1)},
                {'L', (-1,  0)},
                {'R', ( 1,  0)},
            };

            int headX = 0;
            int headY = 0;
            List<int> tailsX = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> tailsY = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            HashSet<(int, int)> beenTo = new HashSet<(int, int)>();
            beenTo.Add((0, 0));
            foreach (string line in lines)
            {
                string[] comp = line.Split(" ");
                char dir = comp[0][0];
                long amount = comp[1].ToLong();

                for (int i = 0; i < amount; i++)
                {
                    headX += DIR[dir].Item1;
                    headY += DIR[dir].Item2;

                    for (int j = 0; j < tailsX.Count(); j++)
                    {
                        int tailX = tailsX[j];
                        int tailY = tailsY[j];
                        int prevTailX = headX;
                        int prevTailY = headY;
                        if (j != 0)
                        {
                            prevTailX = tailsX[j-1];
                            prevTailY = tailsY[j-1];
                        }

                        int adjX = 0;
                        int adjY = 0;
                        adjY = 1 * Math.Sign(prevTailY - tailY);
                        adjX = 1 * Math.Sign(prevTailX - tailX);


                        if (tailX + adjX == prevTailX && tailY + adjY == prevTailY)
                        {
                            continue;
                        }

                        tailX += adjX;
                        tailY += adjY; 
                        tailsX[j] = tailX;
                        tailsY[j] = tailY;
                    }

                    beenTo.Add((tailsX[8], tailsY[8]));
                }

            }

            return beenTo.Count().ToString();
        }
    }
}