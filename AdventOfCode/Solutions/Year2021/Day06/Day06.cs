using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day06 : ASolution
    {

        public Day06() : base(06, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            List<int> Lines = input.ToIntArray(",").ToList<int>();

            int count = 0;
            while (true)
            {
                if (count == 80)
                {
                    break;
                }
                count++;

                int elements = Lines.Count;
                for (int i = 0; i < elements; i++)
                {
                    if (Lines[i] == 0)
                    {
                        Lines[i] = 6;
                        Lines.Add(8);
                        
                    }
                    else
                    {
                        Lines[i] -= 1;
                    } 
                }
            }

            
            return Lines.Count.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            int[] Lines = input.ToIntArray(",");
            var counts = new Dictionary<int, long>();

            for (int i = 0; i <= 9; i++)
            {
                counts[i] = Lines.Count(x => x == i);
            }

            for (int i = 0; i < 256; i++)
            {
                counts[7] += counts[0];
                counts[9] = counts[0];

                for (int j = 0; j < 9; j++)
                {
                    counts[j] = counts[j + 1];
                }
            }

            counts[9] = 0;

            long t = counts.Sum(x => x.Value);

            return t.ToString();
        }
    }
}
