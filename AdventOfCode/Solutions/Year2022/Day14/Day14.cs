using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day14 : ASolution
    {


        public Day14() : base(14, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            Dictionary<Point, char> grid = new Dictionary<Point, char>();  
            for (int y = 0; y < 1000; y++)
            {
                for (int x = 0; x < 1000; x++)
                {
                    grid[new Point(x,y)] = '.';
                }
            }

            foreach (string line in lines)
            {
                string[] comp = line.Split(" -> ");
                Point current = new Point();
                for (int i = 0; i < comp.Count(); i++)
                {
                    string[] pointComp = comp[i].Split(",");
                    Point newPoint = new Point(int.Parse(pointComp[0]), int.Parse(pointComp[1]));
                    if (i == 0)
                    {
                        current = newPoint;
                        grid[current] = '#';
                    }
                    else
                    {
                        while (Math.Abs(newPoint.X - current.X) != 0)
                        {
                            current.X += 1 * Math.Sign(newPoint.X - current.X);
                            grid[current] = '#';
                        }
                        while (Math.Abs(newPoint.Y - current.Y) != 0)
                        {
                            current.Y += 1 * Math.Sign(newPoint.Y - current.Y);
                            grid[current] = '#';
                        }
                        current = newPoint;
                    }
                }

            }

            bool offEdge = false;
            long count = 0;
            while (!offEdge)
            {
                count++;
                Point sandStart = new Point(500, 0);
                Point sandCurrent = sandStart;
                Point prevCurrent = sandStart;
                while (prevCurrent == sandStart || prevCurrent != sandCurrent)
                {
                    if (sandCurrent.Y >= 800)
                    {
                        offEdge = true;
                        break;
                    } 
                    prevCurrent = sandCurrent;
                    Point down = new Point(sandCurrent.X, sandCurrent.Y + 1);
                    Point diagLeft = new Point(sandCurrent.X - 1, sandCurrent.Y + 1);
                    Point diagRight = new Point(sandCurrent.X + 1, sandCurrent.Y + 1);
                    if (grid[down] == '#' || grid[down] == '+')
                    {
                        if (grid[diagLeft] == '#' || grid[diagLeft] == '+')
                        {
                            if (grid[diagRight] == '#' || grid[diagRight] == '+')
                            {
                                break;
                            }
                            else
                            {
                                sandCurrent = diagRight;
                            }
                        }
                        else
                        {
                            sandCurrent = diagLeft;
                        }
                    }
                    else
                    {
                        sandCurrent = down;
                    }
                    grid[sandCurrent] = '+';
                    grid[prevCurrent] = '.';
                }
            }

            return (count-1).ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            Dictionary<Point, char> grid = new Dictionary<Point, char>();
            for (int y = 0; y < 1000; y++)
            {
                for (int x = 0; x < 1000; x++)
                {
                    grid[new Point(x, y)] = '.';
                }
            }

            int maxY = 0;
            foreach (string line in lines)
            {
                string[] comp = line.Split(" -> ");
                Point current = new Point();
                for (int i = 0; i < comp.Count(); i++)
                {
                    string[] pointComp = comp[i].Split(",");
                    Point newPoint = new Point(int.Parse(pointComp[0]), int.Parse(pointComp[1]));
                    if (newPoint.Y > maxY)
                    {
                        maxY = newPoint.Y;
                    }
                    if (i == 0)
                    {
                        current = newPoint;
                        grid[current] = '#';
                    }
                    else
                    {
                        while (Math.Abs(newPoint.X - current.X) != 0)
                        {
                            current.X += 1 * Math.Sign(newPoint.X - current.X);
                            grid[current] = '#';
                        }
                        while (Math.Abs(newPoint.Y - current.Y) != 0)
                        {
                            current.Y += 1 * Math.Sign(newPoint.Y - current.Y);
                            grid[current] = '#';
                        }
                        current = newPoint;
                    }
                }

            }

            for (int x = 0; x < 1000; x++)
            {
                grid[new Point(x, maxY + 2)] = '#';
            }

            bool backAtStart = false;
            long count = 0;
            while (!backAtStart)
            {
                count++;
                Point sandStart = new Point(500, 0);
                Point sandCurrent = sandStart;
                Point prevCurrent = sandStart;
                
                while (prevCurrent == sandStart || prevCurrent != sandCurrent)
                {
                    prevCurrent = sandCurrent;
                    Point down = new Point(sandCurrent.X, sandCurrent.Y + 1);
                    Point diagLeft = new Point(sandCurrent.X - 1, sandCurrent.Y + 1);
                    Point diagRight = new Point(sandCurrent.X + 1, sandCurrent.Y + 1);
                    if (grid[down] == '#' || grid[down] == '+')
                    {
                        if (grid[diagLeft] == '#' || grid[diagLeft] == '+')
                        {
                            if (grid[diagRight] == '#' || grid[diagRight] == '+')
                            {
                                if (sandCurrent == sandStart)
                                {
                                    backAtStart = true;
                                }
                                break;
                            }
                            else
                            {
                                sandCurrent = diagRight;
                            }
                        }
                        else
                        {
                            sandCurrent = diagLeft;
                        }
                    }
                    else
                    {
                        sandCurrent = down;
                    }
                    grid[sandCurrent] = '+';
                    grid[prevCurrent] = '.';
                }
            }

            return count.ToString();
        }
    }
}