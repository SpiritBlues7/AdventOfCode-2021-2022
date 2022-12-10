using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day10 : ASolution
    {


        public Day10() : base(10, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            long x = 1;
            long ticks = 0;
            List<long> tickX = new List<long>();
            foreach (string line in lines)
            {
                string[] comp = line.Split(" ");
                ticks += 1;
                if (ticks % 20 == 0 && ticks % 40 != 0)
                {
                    tickX.Add(x * ticks);
                }

                
                if (comp[0].Equals("noop")) {

                    continue;
                }

                ticks += 1;
                if ((ticks) % 20 == 0 && (ticks) % 40 != 0)
                {
                    tickX.Add(x * (ticks));
                }
                x += comp[1].ToLong();
            }

            return tickX.Sum().ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            long x = 1;
            long screenInd = 0;
            List<char> screenRow = new List<char>();
            List<List<char>> screen = new List<List<char>>();

            foreach (string line in lines)
            {
                string[] comp = line.Split(" ");

                if (Math.Abs(x - screenInd) <= 1)
                {
                    screenRow.Add('#');
                }
                else
                {
                    screenRow.Add('.');
                }
                screenInd += 1;
                if (screenInd == 40)
                {
                    screenInd = 0;
                    screen.Add(screenRow);
                    screenRow = new List<char>();
                }

                if (comp[0].Equals("noop"))
                {
                    continue;
                }

                if (Math.Abs(x - screenInd) <= 1)
                {
                    screenRow.Add('#');
                }
                else
                {
                    screenRow.Add('.');
                }
                screenInd += 1;
                if (screenInd == 40)
                {
                    screenInd = 0;
                    screen.Add(screenRow);
                    screenRow = new List<char>();
                }

                x += comp[1].ToLong();
            }

            foreach (var screenR in screen)
            {
                Console.WriteLine(string.Join("", screenR));
            }


            return null;
        }
    }
}