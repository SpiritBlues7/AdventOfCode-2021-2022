using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day25 : ASolution
    {


        public Day25() : base(25, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            var grid = new Dictionary<Point, char>();
            int height = lines.Length;
            int width = lines[0].Length;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (lines[y][x] == '.')
                    {
                        continue;
                    }
                    grid[new Point(x, y)] = lines[y][x];
                }
            }

            bool change = true;
            long count = 0;
            while (change)
            {
                count++;
                change = false;
                var checkGrid = new Dictionary<Point, char>();
                var newGrid = new Dictionary<Point, char>();

                foreach (KeyValuePair<Point, char> entry in grid)
                {
                    var curPoint = entry.Key;
                    var curChar = entry.Value;
                    if (curChar == '>')
                    {
                        if (curPoint.X + 1 >= width)
                        {
                            if (!grid.ContainsKey(new Point(0, curPoint.Y)))
                            {
                                checkGrid[curPoint] = curChar;
                            }
                        } else
                        {
                            if (!grid.ContainsKey(new Point(curPoint.X + 1, curPoint.Y)))
                            {
                                checkGrid[curPoint] = curChar;
                            }
                        }

                    }
                   
                }

                foreach (KeyValuePair<Point, char> entry in grid)
                {
                    var curPoint = entry.Key;
                    var curChar = entry.Value;
                    if (curChar == '>')
                    {
                        if (!checkGrid.ContainsKey(curPoint))
                        {
                            newGrid[curPoint] = curChar;
                        } else
                        {
                            if (curPoint.X + 1 >= width)
                            {
                                if (newGrid.ContainsKey(new Point(0, curPoint.Y)))
                                {
                                    newGrid[curPoint] = curChar;
                                }
                                else
                                {
                                    newGrid[new Point(0, curPoint.Y)] = curChar;
                                    change = true;
                                }
                            } else
                            {
                                if (newGrid.ContainsKey(new Point(curPoint.X + 1, curPoint.Y)))
                                {
                                    newGrid[curPoint] = curChar;
                                }
                                else
                                {
                                    newGrid[new Point(curPoint.X + 1, curPoint.Y)] = curChar;
                                    change = true;
                                }
                            }

                        }
                    } else
                    {
                        newGrid[curPoint] = curChar;
                    }
                }

                grid = newGrid;
                checkGrid = new Dictionary<Point, char>();
                newGrid = new Dictionary<Point, char>();

                foreach (KeyValuePair<Point, char> entry in grid)
                {
                    var curPoint = entry.Key;
                    var curChar = entry.Value;
                    if (curChar == 'v')
                    {
                        if (curPoint.Y + 1 >= height)
                        {
                            if (!grid.ContainsKey(new Point(curPoint.X, 0)))
                            {
                                checkGrid[curPoint] = curChar;
                            }
                        }
                        else
                        {
                            if (!grid.ContainsKey(new Point(curPoint.X, curPoint.Y + 1)))
                            {
                                checkGrid[curPoint] = curChar;
                            }
                        }
                    }
                }

                foreach (KeyValuePair<Point, char> entry in grid)
                {
                    var curPoint = entry.Key;
                    var curChar = entry.Value;
                    if (curChar == 'v')
                    {
                        if (!checkGrid.ContainsKey(curPoint))
                        {
                            newGrid[curPoint] = curChar;
                        }
                        else
                        {
                            if (curPoint.Y + 1 >= height)
                            {
                                if (newGrid.ContainsKey(new Point(curPoint.X, 0)))
                                {
                                    newGrid[curPoint] = curChar;
                                }
                                else
                                {
                                    newGrid[new Point(curPoint.X, 0)] = curChar;
                                    change = true;
                                }
                            }
                            else
                            {
                                if (newGrid.ContainsKey(new Point(curPoint.X, curPoint.Y + 1)))
                                {
                                    newGrid[curPoint] = curChar;
                                }
                                else
                                {
                                    newGrid[new Point(curPoint.X, curPoint.Y + 1)] = curChar;
                                    change = true;
                                }
                            }

                        }
                    }
                    else
                    {
                        newGrid[curPoint] = curChar;
                    }
                }

                //for (int y = 0; y < height; y++)
                //{
                //    for (int x = 0; x < width; x++)
                //    {
                //        if (newGrid.ContainsKey(new Point(x,y))) {
                //            Console.Write(newGrid[new Point(x, y)]);
                //        } else
                //        {
                //            Console.Write(".");
                //        }
                       
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine();

                grid = newGrid;

            }

            





            return count.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            return null;
        }
    }
}