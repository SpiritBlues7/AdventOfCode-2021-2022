using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day07 : ASolution
    {


        public Day07() : base(07, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            int[] lines = input.ToIntArray(",");

            long bestNum = long.MaxValue;
            long bestTotal = long.MaxValue;
            for (int i = 0; i < lines.Max(); i++)
            {
                long currentTotal = 0;

                for (int j = 0; j < lines.Length; j++)
                {
                    currentTotal += Math.Abs(lines[j] - i);  
                }

                if (currentTotal < bestTotal)
                {
                    bestTotal = currentTotal;
                    bestNum = i;
                }
                
            }

            return bestTotal.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            int[] lines = input.ToIntArray(",");

            long bestNum = long.MaxValue;
            long bestTotal = long.MaxValue;
            for (int i = 0; i < lines.Max(); i++)
            {
                long currentTotal = 0;

                for (int j = 0; j < lines.Length; j++)
                {
                    int count = 1;
                    for (int k = 0; k < Math.Abs(lines[j] - i); k++)
                    {
                        currentTotal += count;
                        count++;
                    }

                }

                if (currentTotal < bestTotal)
                {
                    bestTotal = currentTotal;
                    bestNum = i;
                }

            }

            return bestTotal.ToString();
        }
    }
}