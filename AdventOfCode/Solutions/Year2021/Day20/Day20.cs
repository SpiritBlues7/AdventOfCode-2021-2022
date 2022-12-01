using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day20 : ASolution
    {


        public Day20() : base(20, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] components = input.Split("\n\n");
            string[] gridLines = components[1].SplitByNewline();

            string algorthm = components[0];
            Dictionary<Point, bool> grid = new Dictionary<Point, bool>();

            for (int y = 0; y < gridLines.Length; y++)
            {
                for (int x = 0; x < gridLines[0].Length; x++)
                {
                    if (gridLines[y][x] == '#')
                    {
                        grid[new Point(x, y)] = true;
                    } else
                    {
                        grid[new Point(x, y)] = false;
                    }
                }
            }

            int variant = 3;
            long t = 0;
            int minGrid = 0;
            int maxGrid = 0;

            for (int count = 0; count < 2; count++)
            {
                minGrid = Math.Min(grid.Min(p => p.Key.Y), grid.Min(p => p.Key.X)) - variant;
                maxGrid = Math.Max(grid.Max(p => p.Key.Y), grid.Max(p => p.Key.X)) + variant;

                Dictionary<Point, bool> newGrid = new Dictionary<Point, bool>();
                for (int y = minGrid; y < maxGrid; y++)
                {
                    for (int x = minGrid; x < maxGrid; x++)
                    {
                        var binaryString = "";

                        foreach (int y1 in new List<int>() { -1, 0, 1 })
                        {
                            foreach (int x1 in new List<int>() { -1, 0, 1 })
                            {
                                Point p = new Point(x + x1, y + y1);
                                if (grid.ContainsKey(p) && grid[p] == true)
                                {
                                    binaryString += "1";
                                }
                                else if (grid.ContainsKey(p)) 
                                {
                                    binaryString += "0";
                                } else
                                {
                                    string replacement = "0";
                                    string alt = "0";
                                    if (algorthm[0] == '#')
                                    {
                                        replacement = "0";
                                        alt = "1";
                                    }
                                    if (count % 2 == 0)
                                    {
                                        
                                        binaryString += replacement;
                                    } else
                                    {
                                        binaryString += alt;
                                    }
                                }
                            }
                        }
                        int binaryNum = Convert.ToInt32(binaryString, 2);

                        if (algorthm[binaryNum] == '#')
                        {
                            newGrid[new Point(x, y)] = true;
                        } else
                        {
                            newGrid[new Point(x, y)] = false;
                        }

                    }
                }

                grid = newGrid;
            }

            minGrid = Math.Min(grid.Min(p => p.Key.Y), grid.Min(p => p.Key.X)) - variant;
            maxGrid = Math.Max(grid.Max(p => p.Key.Y), grid.Max(p => p.Key.X)) + variant;

            for (int y = minGrid; y <= maxGrid; y++)
            {
                for (int x = minGrid; x <= maxGrid; x++)
                {
                    Point p = new Point(x, y);
                    if (grid.ContainsKey(p) && grid[p] == true)
                    {

                        t++;
                        Console.Write("#");




                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

            return t.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] components = input.Split("\n\n");
            string[] gridLines = components[1].SplitByNewline();

            string algorthm = components[0];
            Dictionary<Point, bool> grid = new Dictionary<Point, bool>();

            for (int y = 0; y < gridLines.Length; y++)
            {
                for (int x = 0; x < gridLines[0].Length; x++)
                {
                    if (gridLines[y][x] == '#')
                    {
                        grid[new Point(x, y)] = true;
                    }
                    else
                    {
                        grid[new Point(x, y)] = false;
                    }
                }
            }

            int variant = 3;
            long t = 0;
            int minGrid = 0;
            int maxGrid = 0;

            for (int count = 0; count < 50; count++)
            {
                minGrid = Math.Min(grid.Min(p => p.Key.Y), grid.Min(p => p.Key.X)) - variant;
                maxGrid = Math.Max(grid.Max(p => p.Key.Y), grid.Max(p => p.Key.X)) + variant;

                Dictionary<Point, bool> newGrid = new Dictionary<Point, bool>();
                for (int y = minGrid; y < maxGrid; y++)
                {
                    for (int x = minGrid; x < maxGrid; x++)
                    {
                        var binaryString = "";

                        foreach (int y1 in new List<int>() { -1, 0, 1 })
                        {
                            foreach (int x1 in new List<int>() { -1, 0, 1 })
                            {
                                Point p = new Point(x + x1, y + y1);
                                if (grid.ContainsKey(p) && grid[p] == true)
                                {
                                    binaryString += "1";
                                }
                                else if (grid.ContainsKey(p))
                                {
                                    binaryString += "0";
                                }
                                else
                                {
                                    string replacement = "0";
                                    string alt = "0";
                                    if (algorthm[0] == '#')
                                    {
                                        replacement = "0";
                                        alt = "1";
                                    }
                                    if (count % 2 == 0)
                                    {

                                        binaryString += replacement;
                                    }
                                    else
                                    {
                                        binaryString += alt;
                                    }
                                }
                            }
                        }
                        int binaryNum = Convert.ToInt32(binaryString, 2);

                        if (algorthm[binaryNum] == '#')
                        {
                            newGrid[new Point(x, y)] = true;
                        }
                        else
                        {
                            newGrid[new Point(x, y)] = false;
                        }

                    }
                }

                grid = newGrid;
            }

            minGrid = Math.Min(grid.Min(p => p.Key.Y), grid.Min(p => p.Key.X)) - variant;
            maxGrid = Math.Max(grid.Max(p => p.Key.Y), grid.Max(p => p.Key.X)) + variant;

            for (int y = minGrid; y <= maxGrid; y++)
            {
                for (int x = minGrid; x <= maxGrid; x++)
                {
                    Point p = new Point(x, y);
                    if (grid.ContainsKey(p) && grid[p] == true)
                    {

                        t++;
                        //Console.Write("#");




                    }
                    else
                    {
                        //Console.Write(".");
                    }
                }
                //Console.WriteLine();
            }

            return t.ToString();
        }
    }
}