using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace AdventOfCode.Solutions.Year2022
{

    class Day23 : ASolution
    {

        

        public Day23() : base(23, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            int yMax = lines.Count() - 1;
            int xMax = lines[0].Count() - 1;
            int xMin = 0;
            int yMin = 0;
            long elvCount = 0;
            GridUtilities G = new GridUtilities();
            HashSet<Point> validPoints = new HashSet<Point>();
            HashSet<Point> validPointsOld = new HashSet<Point>();
            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    G.SetValue(x, y, lines[y][x]);
                    GridItem cur = G.GetValue(new Point(x, y));
                    
                    if (cur.character == '#')
                    {
                        validPoints.Add(new Point(x, y));
                        elvCount++;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                validPointsOld = new HashSet<Point>(validPoints);
                validPoints = new HashSet<Point>();
                Dictionary<Point, List<(Point, GridItem)>> taken = new Dictionary<Point, List<(Point, GridItem)>>();

                foreach (Point curr in validPointsOld)
                {
                    if (G.GetValue(curr).character == '#')
                    {
                        var moveToPoint = GetMovementPoint(G, curr, i);
                        if (!taken.ContainsKey(moveToPoint))
                        {
                            taken[moveToPoint] = new List<(Point, GridItem)>();
                        }
                        taken[moveToPoint].Add((curr, G.GetValue(curr)));
                    }
                }

                xMax = int.MinValue;
                yMax = int.MinValue;
                xMin = int.MaxValue;
                yMin = int.MaxValue;
                foreach (KeyValuePair<Point, List<(Point, GridItem)>> takenComp in taken)
                {
                    if (takenComp.Value.Count == 1)
                    {
                        G.SetValue(takenComp.Key, takenComp.Value[0].Item2);
                        if (takenComp.Key != takenComp.Value[0].Item1)
                        {
                            GridItem newItem = new GridItem();
                            newItem.character = '.';
                            G.SetValue(takenComp.Value[0].Item1, newItem);
                            
                            xMax = Math.Max(xMax, takenComp.Value[0].Item1.X);
                            xMin = Math.Min(xMin, takenComp.Value[0].Item1.X);
                            yMax = Math.Max(yMax, takenComp.Value[0].Item1.Y);
                            yMin = Math.Min(yMin, takenComp.Value[0].Item1.Y);
                        }
                        validPoints.Add(takenComp.Key);
                        xMax = Math.Max(xMax, takenComp.Key.X);
                        xMin = Math.Min(xMin, takenComp.Key.X);
                        yMax = Math.Max(yMax, takenComp.Key.Y);
                        yMin = Math.Min(yMin, takenComp.Key.Y);
                    } else
                    {
                        foreach ((Point, GridItem) ptComp in takenComp.Value)
                        {
                            validPoints.Add(ptComp.Item1);
                            xMax = Math.Max(xMax, ptComp.Item1.X);
                            xMin = Math.Min(xMin, ptComp.Item1.X);
                            yMax = Math.Max(yMax, ptComp.Item1.Y);
                            yMin = Math.Min(yMin, ptComp.Item1.Y);
                        }
                    }
                }
                //for (int y = yMin; y <= yMax; y++)
                //{
                //    for (int x = xMin; x <= xMax; x++)
                //    {
                //        Point curr = new Point(x, y);
                //        if (G.GetValue(curr).character == '#')
                //        {
                //            Console.Write('#');
                //        }
                //        else
                //        {
                //            Console.Write('.');
                //        }

                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine();
            }

            long total = Math.Abs(xMax - xMin + 1) * Math.Abs(yMax - yMin + 1) - elvCount;


            return total.ToString();
        }

        public Point GetMovementPoint(GridUtilities G, Point current, int round)
        {
            GridItem curr = G.GetValue(new Point(current.X, current.Y));
            var neigbours = G.GetNeighbours8Dir(current.X, current.Y, 1);
            bool neighbourFound = false;
            foreach (var neighbour in neigbours)
            {
                if (G.GetValue(new Point(neighbour.X, neighbour.Y)).character == '#')
                {
                    neighbourFound = true;
                    break;
                }
            }
            if (!neighbourFound)
            {
                return current;
            }

            int checks = 0;
            int checkNum = round%4;
            while (checks != 4)
            {
                
                if (checkNum == 0)
                {
                    bool topNeighbour = false;
                    foreach (var neighbour in neigbours.Where(x => x.Y == current.Y - 1))
                    {
                        if (G.GetValue(new Point(neighbour.X, neighbour.Y)).character == '#')
                        {
                            topNeighbour = true;
                            break;
                        }
                    }
                    if (!topNeighbour)
                    {
                        return new Point(current.X, current.Y - 1);
                    }
                }

                if (checkNum == 1)
                {
                    bool bottomNeighbour = false;
                    foreach (var neighbour in neigbours.Where(x => x.Y == current.Y + 1))
                    {
                        if (G.GetValue(new Point(neighbour.X, neighbour.Y)).character == '#')
                        {
                            bottomNeighbour = true;
                            break;
                        }
                    }
                    if (!bottomNeighbour)
                    {
                        return new Point(current.X, current.Y + 1);
                    }
                }

                if (checkNum == 2)
                {
                    bool westNeighbour = false;
                    foreach (var neighbour in neigbours.Where(x => x.X == current.X - 1))
                    {
                        if (G.GetValue(new Point(neighbour.X, neighbour.Y)).character == '#')
                        {
                            westNeighbour = true;
                            break;
                        }
                    }
                    if (!westNeighbour)
                    {
                        return new Point(current.X - 1, current.Y);
                    }
                }

                if (checkNum == 3)
                {
                    bool eastNeighbour = false;
                    foreach (var neighbour in neigbours.Where(x => x.X == current.X + 1))
                    {
                        if (G.GetValue(new Point(neighbour.X, neighbour.Y)).character == '#')
                        {
                            eastNeighbour = true;
                            break;
                        }
                    }
                    if (!eastNeighbour)
                    {
                        return new Point(current.X + 1, current.Y);
                    }
                }
                checks++;
                checkNum = (checkNum + 1)%4;
            }


            return current;
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            int yMax = lines.Count() - 1;
            int xMax = lines[0].Count() - 1;
            int xMin = 0;
            int yMin = 0;
            long elvCount = 0;
            GridUtilities G = new GridUtilities();
            HashSet<Point> validPoints = new HashSet<Point>();
            HashSet<Point> validPointsOld = new HashSet<Point>();
            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    G.SetValue(x, y, lines[y][x]);
                    GridItem cur = G.GetValue(new Point(x, y));

                    if (cur.character == '#')
                    {
                        validPoints.Add(new Point(x, y));
                        elvCount++;
                    }
                }
            }

            bool changed = true;
            int changes = 0;
            int count = 0;
            while (changed)
            {
                changed = false;
                changes = 0;
                Dictionary<Point, List<(Point, GridItem)>> taken = new Dictionary<Point, List<(Point, GridItem)>>();

                foreach (Point curr in validPoints)
                {
                    if (G.GetValue(curr).character == '#')
                    {
                        var moveToPoint = GetMovementPoint(G, curr, count);
                        if (!taken.ContainsKey(moveToPoint))
                        {
                            taken[moveToPoint] = new List<(Point, GridItem)>();
                        }
                        taken[moveToPoint].Add((curr, G.GetValue(curr)));
                    }
                }

                xMax = int.MinValue;
                yMax = int.MinValue;
                xMin = int.MaxValue;
                yMin = int.MaxValue;
                foreach (KeyValuePair<Point, List<(Point, GridItem)>> takenComp in taken)
                {
                    if (takenComp.Value.Count == 1)
                    {
                        G.SetValue(takenComp.Key, takenComp.Value[0].Item2);
                        if (takenComp.Key != takenComp.Value[0].Item1)
                        {
                            changed = true;
                            changes++;
                            GridItem newItem = new GridItem();
                            newItem.character = '.';
                            G.SetValue(takenComp.Value[0].Item1, newItem);
                            validPoints.Remove((takenComp.Value[0].Item1));

                            xMax = Math.Max(xMax, takenComp.Value[0].Item1.X);
                            xMin = Math.Min(xMin, takenComp.Value[0].Item1.X);
                            yMax = Math.Max(yMax, takenComp.Value[0].Item1.Y);
                            yMin = Math.Min(yMin, takenComp.Value[0].Item1.Y);
                        }
                        validPoints.Add(takenComp.Key);
                        xMax = Math.Max(xMax, takenComp.Key.X);
                        xMin = Math.Min(xMin, takenComp.Key.X);
                        yMax = Math.Max(yMax, takenComp.Key.Y);
                        yMin = Math.Min(yMin, takenComp.Key.Y);
                    }
                    else
                    {
                        foreach ((Point, GridItem) ptComp in takenComp.Value)
                        {
                            validPoints.Add(ptComp.Item1);
                            xMax = Math.Max(xMax, ptComp.Item1.X);
                            xMin = Math.Min(xMin, ptComp.Item1.X);
                            yMax = Math.Max(yMax, ptComp.Item1.Y);
                            yMin = Math.Min(yMin, ptComp.Item1.Y);
                        }
                    }
                }
                //for (int y = yMin; y <= yMax; y++)
                //{
                //    for (int x = xMin; x <= xMax; x++)
                //    {
                //        Point curr = new Point(x, y);
                //        if (G.GetValue(curr).character == '#')
                //        {
                //            Console.Write('#');
                //        }
                //        else
                //        {
                //            Console.Write('.');
                //        }

                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine();
                count ++;
                Console.WriteLine("{0}: {1}", count, changes);
            }


            return count.ToString();
        }
    }
}