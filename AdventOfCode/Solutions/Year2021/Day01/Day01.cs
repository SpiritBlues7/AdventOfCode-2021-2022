using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day01 : ASolution
    {
        long[] Lines;
        //string[] Lines;

        public Day01() : base(01, 2021, "")
        {
            //Debug = true;

            Lines = Input.SplitByNewline().ToLongArray();
            //Lines = Input.SplitByNewline();
            Console.WriteLine("Line Example: {0}", Lines[0]);

            //for (int i = 0; i < Lines.Length; i++)
            //{

            //}
        }

        protected override string SolvePartOne()
        {
            int t = 0;
            for (int i = 0; i < Lines.Length - 1; i++)
            {
                if (Lines[i] < Lines[i + 1])
                {
                    t++;
                }
            }

            return t.ToString();
        }

        protected override string SolvePartTwo()
        {
            int t = 0;
            for (int i = 0; i < Lines.Length - 3; i++)
            {
                if (Lines[i] + Lines[i + 1] + Lines[i + 2] < Lines[i + 1] + Lines[i + 2] + Lines[i + 3])
                {
                    t++;
                }
            }

            return t.ToString();
        }
    }
}
