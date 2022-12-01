using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day01 : ASolution
    {

        public Day01() : base(01, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] groups = input.Split("\n\n");

            List<int> totals = new List<int>();

            foreach (string group in groups)
            {
                int[] values = Utilities.ToIntArray(group, "\n");
                totals.Add(values.Sum());
            }

            return totals.Max().ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] groups = input.Split("\n\n");

            List<int> totals = new List<int>();

            foreach (string group in groups)
            {
                int[] values = Utilities.ToIntArray(group, "\n");
                totals.Add(values.Sum());
            }

            totals.Sort();
            totals.Reverse();
            List<int> tops = totals.Take(3).ToList();
            return tops.Sum().ToString();
        }
    }
}