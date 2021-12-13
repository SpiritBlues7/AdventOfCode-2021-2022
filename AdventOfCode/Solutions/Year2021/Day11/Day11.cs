using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day11 : ASolution
    {


        public Day11() : base(11, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            string[] lines = input.SplitByNewline();
            GridUtilities gridUtil = new GridUtilities();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    gridUtil.SetValue(new Point(x,y), int.Parse(lines[y][x].ToString()));
                }
            }

            
            long count = 0;
            for (int i = 0;i < 100; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        //Console.Write(gridUtil.GetValue(new Point(x, y)).number);
                    }
                    //Console.WriteLine();
                }
                //Console.WriteLine();
                //Console.WriteLine();

                var flashed = new Dictionary<Point, bool>();
                var firstpass = true;
                while (true)
                {
                    var changed = false;
                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            var currentPoint = new Point(x, y);
                            if (flashed.ContainsKey(currentPoint))
                            {
                                continue;
                            }
                            if (gridUtil.GetValue(currentPoint).number >= 10)
                            {

                                count++;
                                changed = true;
                                flashed[currentPoint] = true;
                                List<Point> neighs = gridUtil.GetNeighbours8Dir(x, y, 1);
                                foreach (var neighbour in neighs)
                                {
                                    gridUtil.SetValue(currentPoint, 0);
                                    var currentNvalue = gridUtil.GetValue(neighbour).number;
                                    gridUtil.SetValue(neighbour, currentNvalue + 1);
                                    
                                }
                            } else
                            {
                                if (firstpass)
                                {
                                    changed = true;
                                    var currentValue = gridUtil.GetValue(currentPoint).number;
                                    gridUtil.SetValue(currentPoint, currentValue + 1);
                                }
                            }
                        }
                    }
                    if (!changed)
                    {
                        break;
                    }


                    firstpass = false;
                }

                foreach (KeyValuePair<Point, bool> entry in flashed)
                {
                    gridUtil.SetValue(entry.Key, 0);

                }
            }


            return count.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            string[] lines = input.SplitByNewline();
            GridUtilities gridUtil = new GridUtilities();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    gridUtil.SetValue(new Point(x, y), int.Parse(lines[y][x].ToString()));
                }
            }

            long answer = 0;
            long count = 0;
            for (int i = 0; i < 5000; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        //Console.Write(gridUtil.GetValue(new Point(x, y)).number);
                    }
                    //Console.WriteLine();
                }
                //Console.WriteLine();
                //Console.WriteLine();

                var flashed = new Dictionary<Point, bool>();
                var firstpass = true;
                while (true)
                {
                    var changed = false;
                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            var currentPoint = new Point(x, y);
                            if (flashed.ContainsKey(currentPoint))
                            {
                                continue;
                            }
                            if (gridUtil.GetValue(currentPoint).number >= 10)
                            {

                                count++;
                                changed = true;
                                flashed[currentPoint] = true;
                                List<Point> neighs = gridUtil.GetNeighbours8Dir(x, y, 1);
                                foreach (var neighbour in neighs)
                                {
                                    gridUtil.SetValue(currentPoint, 0);
                                    var currentNvalue = gridUtil.GetValue(neighbour).number;
                                    gridUtil.SetValue(neighbour, currentNvalue + 1);

                                }
                            }
                            else
                            {
                                if (firstpass)
                                {
                                    changed = true;
                                    var currentValue = gridUtil.GetValue(currentPoint).number;
                                    gridUtil.SetValue(currentPoint, currentValue + 1);
                                }
                            }
                        }
                    }
                    if (!changed)
                    {
                        break;
                    }


                    firstpass = false;
                }

                long fcount = 0;
                foreach (KeyValuePair<Point, bool> entry in flashed)
                {
                    gridUtil.SetValue(entry.Key, 0);
                    fcount++;

                }
                //Console.WriteLine(fcount);
                if (fcount == 100)
                {
                    //Console.WriteLine(i);
                    answer = i + 1;
                    break;
                }


            }



            return answer.ToString();
        }
    }
}