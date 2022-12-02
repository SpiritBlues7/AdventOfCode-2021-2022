using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day02 : ASolution
    {


        public Day02() : base(02, 2022, "")
        {
        }

        protected override string SolvePartOne(string input)
        {

            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            //long[] values = input.ToLongArray(",");
            int points = 0;
            foreach (string line in lines)
            {
                
                string[] vals = line.Split(' ');

                if (vals[1].Equals("X"))
                {
                    points += 1;
                    if (vals[0].Equals("A"))
                    {
                        points += 3;
                    }
                    if (vals[0].Equals("C"))
                    {
                        points += 6;
                    }
                }

                if (vals[1].Equals("Y"))
                {
                    points += 2;
                    if (vals[0].Equals("A"))
                    {
                        points += 6;
                    }
                    if (vals[0].Equals("B"))
                    {
                        points += 3;
                    }

                }

                if (vals[1].Equals("Z"))
                {
                    points += 3;
                    if (vals[0].Equals("B"))
                    {
                        points += 6;
                    }
                    if (vals[0].Equals("C"))
                    {
                        points += 3;
                    }
                }
            }

            return points.ToString();
        }

        protected override string SolvePartTwo(string input)
        {

            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            //long[] values = input.ToLongArray(",");
            int points = 0;
            foreach (string line in lines)
            {

                string[] vals = line.Split(' ');

                if (vals[0].Equals("A"))
                {
                    
                    if (vals[1].Equals("X"))
                    {
                        points += 3;
                        points += 0;
                    }
                    if (vals[1].Equals("Y"))
                    {
                        points += 1;
                        points += 3;
                    }
                    if (vals[1].Equals("Z"))
                    {
                        points += 2;
                        points += 6;
                    }
                }

                if (vals[0].Equals("B"))
                {
                    if (vals[1].Equals("X"))
                    {
                        points += 1;
                        points += 0;
                    }
                    if (vals[1].Equals("Y"))
                    {
                        points += 2;
                        points += 3;
                    }
                    if (vals[1].Equals("Z"))
                    {
                        points += 3;
                        points += 6;
                    }

                }

                if (vals[0].Equals("C"))
                {
                    if (vals[1].Equals("X"))
                    {
                        points += 2;
                        points += 0;
                    }
                    if (vals[1].Equals("Y"))
                    {
                        points += 3;
                        points += 3;
                    }
                    if (vals[1].Equals("Z"))
                    {
                        points += 1;
                        points += 6;
                    }
                }
            }

            return points.ToString();
        }
    }
}