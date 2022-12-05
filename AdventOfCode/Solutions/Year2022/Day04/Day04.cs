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
                string[] components = line.Split(",");
                string first = components[0];
                string second = components[1];

                string[] firstComp = first.Split("-");
                string[] secondComp = second.Split("-");

                int first_first = int.Parse(firstComp[0]);
                int first_second = int.Parse(firstComp[1]);

                int second_first = int.Parse(secondComp[0]);
                int second_second = int.Parse(secondComp[1]);

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
                string[] components = line.Split(",");
                string first = components[0];
                string second = components[1];

                string[] firstComp = first.Split("-");
                string[] secondComp = second.Split("-");

                int first_first = int.Parse(firstComp[0]);
                int first_second = int.Parse(firstComp[1]);

                int second_first = int.Parse(secondComp[0]);
                int second_second = int.Parse(secondComp[1]);

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