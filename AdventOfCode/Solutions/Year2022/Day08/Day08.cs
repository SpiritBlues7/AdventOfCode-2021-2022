using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day08 : ASolution
    {
        public Day08() : base(08, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");


            List<List<long>> components = Better.InputTo2dLong(input, new List<string> { "\n" }, new List<string> { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }, 1);
            long total = 0;
            for (int y = 0; y < components.Count(); y++)
            {
                for (int x = 0; x < components[0].Count(); x++)
                {
                    bool seeUp = true;
                    bool seeDown = true;
                    bool seeLeft = true;
                    bool seeRight = true;
                    for (int yu = y-1; yu >= 0; yu--)
                    {
                        if (components[yu][x] >= components[y][x])
                        {
                            seeUp = false;
                        }
                    }
                    for (int yd = y+1; yd < components.Count(); yd++)
                    {
                        if (components[yd][x] >= components[y][x])
                        {
                            seeDown = false;
                        }
                    }
                    for (int xl = x-1; xl >= 0; xl--)
                    {
                        if (components[y][xl] >= components[y][x])
                        {
                            seeLeft = false;
                        }
                    }
                    for (int xr = x+1; xr < components[0].Count(); xr++)
                    {
                        if (components[y][xr] >= components[y][x])
                        {
                            seeRight = false;
                        }
                    }

                    if (seeUp || seeDown || seeLeft || seeRight)
                    {
                        total++;
                    }
                }
            }

            return total.ToString();
        }

        protected override string SolvePartTwo(string input)
        {

            input = input.Replace("\r\n", "\n");


            List<List<long>> components = Better.InputTo2dLong(input, new List<string> { "\n" }, new List<string> { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }, 1);
            
            long best = 0;
            for (int y = 0; y < components.Count(); y++)
            {
                for (int x = 0; x < components[0].Count(); x++)
                {
                    int upCount = 0;
                    int downCount = 0;
                    int leftCount = 0;
                    int rightCount = 0;
                    for (int yu = y - 1; yu >= 0; yu--)
                    {
                        upCount += 1;
                        if (components[yu][x] >= components[y][x])
                        {
                            break;
                        }
                    }
                    for (int yd = y + 1; yd < components.Count(); yd++)
                    {
                        downCount += 1;
                        if (components[yd][x] >= components[y][x])
                        {
                            break;
                        }
                    }
                    for (int xl = x - 1; xl >= 0; xl--)
                    {
                        leftCount += 1;
                        if (components[y][xl] >= components[y][x])
                        {
                            break;
                        }
                    }
                    for (int xr = x + 1; xr < components[0].Count(); xr++)
                    {
                        rightCount += 1;
                        if (components[y][xr] >= components[y][x])
                        {
                            break;
                        }
                    }

                    long total = upCount * downCount * leftCount * rightCount;
                    if (total > best)
                    {
                        best = total;
                    }
                }
            }



            return best.ToString();
        }
    }
}