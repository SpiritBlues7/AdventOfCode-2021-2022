using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day04 : ASolution
    {
        List<(List<(int, bool)>, bool)> boards = new List<(List<(int, bool)>, bool)>();

        public Day04() : base(04, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            long total = 0;
            foreach (string line in lines)
            {
                List<List<long>> components = Better.InputTo2dLong(line, new List<string>{","}, new List<string> { "-" }, 1);

                long first_first = components[0][0];
                long first_second = components[0][1];
                long second_first = components[1][0];
                long second_second = components[1][1];

                if (first_first <= second_first && first_second >= second_second)
                {
                    total++;
                    continue;
                }

                if (first_first >= second_first && first_second <= second_second)
                {
                    total++;
                    continue;
                }
            }
            
            return total.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            long total = 0;
            foreach (string line in lines)
            {
                List<List<long>> components = Better.InputTo2dLong(line, new List<string> { "," }, new List<string> { "-" }, 1);

                long first_first = components[0][0];
                long first_second = components[0][1];
                long second_first = components[1][0];
                long second_second = components[1][1];


                if (first_first <= second_first && first_second >= second_first)
                {
                    total++;
                    continue;
                }

                if (second_first <= first_first && second_second >= first_first)
                {
                    total++;
                    continue;
                }
            }

            return total.ToString();
        }
    }
}