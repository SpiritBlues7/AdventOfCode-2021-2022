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

            return null;
        }

        protected override string SolvePartTwo(string input)
        {
            return null;
        }
    }
}