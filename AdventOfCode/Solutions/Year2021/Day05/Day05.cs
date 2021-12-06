using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day05 : ASolution
    {
        // long[] Lines;
        string[] Lines;

        public Day05() : base(05, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            Lines = input.SplitByNewline();
            GridUtilities gridUtil = new GridUtilities();
            
            for (int i = 0; i < Lines.Length; i++)
            {
                var coords = Lines[i].Split(" -> ");
                int coords1x = Convert.ToInt32(coords[0].Split(",")[0]);
                int coords1y = Convert.ToInt32(coords[0].Split(",")[1]);
                int coords2x = Convert.ToInt32(coords[1].Split(",")[0]);
                int coords2y = Convert.ToInt32(coords[1].Split(",")[1]);

                int coordX;
                int coordY;

                if (coords2x == coords1x)
                {
                    coordX = coords2x;
                    for (int k = 0; k <= Math.Abs(coords2y - coords1y); k++)
                    {

                        if (coords1y > coords2y)
                        {
                            coordY = coords2y + k;
                        }
                        else
                        {
                            coordY = coords1y + k;
                        }

                        Point finalPoint = new Point(coordX, coordY);
                        if (gridUtil.grid.ContainsKey(finalPoint))
                        {
                            gridUtil.SetValue(finalPoint, gridUtil.grid[finalPoint].number + 1);
                        }
                        else
                        {
                            gridUtil.SetValue(finalPoint, 1);
                        }
                    }

                }

                if (coords2y == coords1y)
                {
                    coordY = coords2y;
                    for (int j = 0; j <= Math.Abs(coords2x - coords1x); j++)
                    {
                        if (coords1x > coords2x)
                        {
                            coordX = coords2x + j;
                        }
                        else
                        {
                            coordX = coords1x + j;
                        }


                        Point finalPoint = new Point(coordX, coordY);
                        if (gridUtil.grid.ContainsKey(finalPoint))
                        {
                            gridUtil.SetValue(finalPoint, gridUtil.grid[finalPoint].number + 1);
                        }
                        else
                        {
                            gridUtil.SetValue(finalPoint, 1);
                        }


                    }
                }

                
            }


            int t = 0;
            foreach (KeyValuePair<Point, GridItem> keyValue in gridUtil.grid)
            {
                if (keyValue.Value.number >= 2)
                {
                    t++;
                }
            }

            return t.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            Lines = input.SplitByNewline();
            GridUtilities gridUtil = new GridUtilities();
            for (int i = 0; i < Lines.Length; i++)
            {
                var coords = Lines[i].Split(" -> ");
                int coords1x = Convert.ToInt32(coords[0].Split(",")[0]);
                int coords1y = Convert.ToInt32(coords[0].Split(",")[1]);
                int coords2x = Convert.ToInt32(coords[1].Split(",")[0]);
                int coords2y = Convert.ToInt32(coords[1].Split(",")[1]);

                int coordX;
                int coordY;

                if (coords2x == coords1x)
                {
                    coordX = coords2x;
                    for (int k = 0; k <= Math.Abs(coords2y - coords1y); k++)
                    {

                        if (coords1y > coords2y)
                        {
                            coordY = coords2y + k;
                        }
                        else
                        {
                            coordY = coords1y + k;
                        }

                        Point finalPoint = new Point(coordX, coordY);
                        if (gridUtil.grid.ContainsKey(finalPoint))
                        {
                            gridUtil.SetValue(finalPoint, gridUtil.grid[finalPoint].number + 1);
                        }
                        else
                        {
                            gridUtil.SetValue(finalPoint, 1);
                        }
                    }

                }
                else if (coords2y == coords1y)
                {
                    coordY = coords2y;
                    for (int j = 0; j <= Math.Abs(coords2x - coords1x); j++)
                    {
                        if (coords1x > coords2x)
                        {
                            coordX = coords2x + j;
                        }
                        else
                        {
                            coordX = coords1x + j;
                        }


                        Point finalPoint = new Point(coordX, coordY);
                        if (gridUtil.grid.ContainsKey(finalPoint))
                        {
                            gridUtil.SetValue(finalPoint, gridUtil.grid[finalPoint].number + 1);
                        }
                        else
                        {
                            gridUtil.SetValue(finalPoint, 1);
                        }


                    }
                }
                else
                {

                    for (int k = 0; k <= Math.Abs(coords2y - coords1y); k++)
                    {
                        if (coords1x > coords2x)
                        {
                            coordX = coords2x + k;
                            if (coords1y > coords2y)
                            {
                                coordY = coords2y + k;
                            }
                            else
                            {
                                coordY = coords2y - k;
                            }
                                
                        }
                        else
                        {
                            coordX = coords2x - k;
                            if (coords1y > coords2y)
                            {
                                coordY = coords2y + k;
                            }
                            else
                            {
                                coordY = coords2y - k;
                            }
                        }

                           
                        Point finalPoint = new Point(coordX, coordY);
                        if (gridUtil.grid.ContainsKey(finalPoint))
                        {
                            gridUtil.SetValue(finalPoint, gridUtil.grid[finalPoint].number + 1);
                        }
                        else
                        {
                            gridUtil.SetValue(finalPoint, 1);
                        }
                    }


                }
            }

            int t = 0;
            foreach (KeyValuePair<Point, GridItem> keyValue in gridUtil.grid)
            {
                if (keyValue.Value.number >= 2)
                {
                    //Console.WriteLine(keyValue.Key);
                    t++;
                }
            }

            return t.ToString();
        }
    }
}