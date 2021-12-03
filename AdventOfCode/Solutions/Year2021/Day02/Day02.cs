using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day02 : ASolution
    {
        // long[] Lines;
        string[] Lines;

        public Day02() : base(02, 2021, "")
        {
            //Debug = true;

            // Lines = Input.SplitByNewline().ToLongArray();
            Lines = Input.SplitByNewline();
            Console.WriteLine("Line Example: {0}", Lines[0]);

            //for (int i = 0; i < Lines.Length; i++)
            //{

            //}
        }

        protected override string SolvePartOne()
        {
            int x = 0;
            int z = 0;
            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i].Length == 0)
                {
                    continue;
                }
                var linesSplit = Lines[i].Split(" ");

                var dir = linesSplit[0];
                var num = int.Parse(linesSplit[1]);

                if (dir == "forward")
                {
                    x += num;
                }
                if (dir == "down")
                {
                    z += num;
                }
                if (dir == "up")
                {
                    z -= num;
                }
            }

            int ans = x * z;
            return ans.ToString();
        }

        protected override string SolvePartTwo()
        {
            /**
                down X increases your aim by X units.
                up X decreases your aim by X units.
                forward X does two things:
                    It increases your horizontal position by X units.
                    It increases your depth by your aim multiplied by X.
             */

            int x = 0;
            int z = 0;
            int aim = 0;
            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i].Length == 0)
                {
                    continue;
                }
                var linesSplit = Lines[i].Split(" ");

                var dir = linesSplit[0];
                var num = int.Parse(linesSplit[1]);

                if (dir == "forward")
                {
                    x += num;
                    z += num * aim;
                }
                if (dir == "down")
                {
                    aim += num;
                }
                if (dir == "up")
                {
                    aim -= num;
                }

                //Console.WriteLine("X : {0}, Z : {1}, aim: {2}", x, z, aim);
            }

            int ans = x * z;
            return ans.ToString();
        }
    }
}
