using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day06 : ASolution
    {

        public Day06() : base(06, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            int start = 0;

            for (int i = 0; i < input.Count() - 4; i++)
            {
                if (input.Substring(i, 4).Distinct().Count() == 4)
                {
                    start = i + 4;
                    break;
                }
            }
            return start.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");

            int start = 0;
            for (int i = 0; i < input.Count() - 14; i++)
            {
                if (input.Substring(i, 14).Distinct().Count() == 14)
                {
                    start = i + 14;
                    break;
                }
            }

            return start.ToString();
        }
    }
}