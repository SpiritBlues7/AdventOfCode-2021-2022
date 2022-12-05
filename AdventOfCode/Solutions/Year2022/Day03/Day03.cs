using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day03 : ASolution
    {

        public Day03() : base(03, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            int total = 0;
            foreach (string line in lines)
            {
                int count = line.Count();
                string first = line.Substring(0, count/2);
                string second = line.Substring(count/2);

                foreach (char x in first)
                {
                    if (second.Contains(x))
                    {
                        int temp = x - 'a' + 1;
                        if (x - 'a' < 0)
                        {
                            temp = x - 'A' + 1;
                            temp += 26;
                        }

                        total += temp;
                        break;
                    }
                }

            }

            return total.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            int total = 0;

            for (int i = 0; i < lines.Length; i+=3)
            {
                string first = lines[i];
                string second = lines[i + 1];
                string third = lines[i + 2];

                foreach (char x in first)
                {
                    if (second.Contains(x))
                    {
                        if (third.Contains(x))
                        {
                            int temp = x - 'a' + 1;
                            if (x - 'a' < 0)
                            {
                                temp = x - 'A' + 1;
                                temp += 26;
                            }
                            total += temp;
                            break;
                        }
                    }
                }
            }

            return total.ToString();
        }
    }
}