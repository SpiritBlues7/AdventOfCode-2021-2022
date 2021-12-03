using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day03 : ASolution
    {
        List<string> Lines;

        public Day03() : base(03, 2021, "")
        {
            //Debug = true;

            Lines = Input.SplitByNewline().ToList();

        }

        protected override string SolvePartOne()
        {
            string x = "";
            string y = "";
            for (int i = 0; i < Lines[0].Length; i++)
            {
                int totalOnes = 0;
                for (int j = 0; j < Lines.Count; j++)
                {
                    if (Lines[j][i] == '1')
                    {
                        totalOnes++;
                    }
                }

                if (totalOnes >= Lines.Count / 2)
                {
                    x += '1';
                    y += '0';
                }
                else
                {
                    x += '0';
                    y += '1';
                }
            }

            return (Convert.ToInt32(x, 2) * Convert.ToInt32(y, 2)).ToString();
        }

        protected override string SolvePartTwo()
        {
            List<string> LinesClone = new List<string>(Lines);

            // Loop to determine oxygen generator rating
            for (int i = 0; i < Lines[0].Length; i++)
            {
                int totalOnes = 0;
                char commonNumber;
                for (int j = 0; j < Lines.Count; j++)
                {
                    if (Lines[j][i] == '1')
                    {
                        totalOnes++;
                    }
                }

                if (totalOnes >= Lines.Count / (double) 2)
                {
                    commonNumber = '1';
                }
                else
                {
                    commonNumber = '0';
                }

                for (int j = 0; j < Lines.Count; j++)
                {
                    if (Lines[j][i] == commonNumber)
                    {
                        Lines.RemoveAt(j);
                        j--;
                    }
                }

                if (Lines.Count <= 1)
                {
                    break;
                }
            }

            // Loop to determine CO2 scrubber rating
            for (int i = 0; i < LinesClone[0].Length; i++)
            {
                int totalOnes = 0;
                char commonNumber;
                for (int j = 0; j < LinesClone.Count; j++)
                {
                    if (LinesClone[j][i] == '1')
                    {
                        totalOnes++;
                    }
                }

                if (totalOnes >= LinesClone.Count / (double)2)
                {
                    commonNumber = '1';
                }
                else
                {
                    commonNumber = '0';
                }

                for (int j = 0; j < LinesClone.Count; j++)
                {
                    if (LinesClone[j][i] != commonNumber)
                    {
                        LinesClone.RemoveAt(j);
                        j--;
                    }
                }

                if (LinesClone.Count <= 1)
                {
                    break;
                }
            }

            return (Convert.ToInt32(Lines[0], 2) * Convert.ToInt32(LinesClone[0], 2)).ToString();
        }
    }
}
