using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

namespace AdventOfCode.Solutions.Year2022
{

    class Day12 : ASolution
    {


        public Day12() : base(12, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            GridUtilities gridUtil = new GridUtilities();

            Point start = new Point();
            for (int y = 0; y < lines.Count(); y++)
            {
                for (int x = 0; x < lines[0].Count(); x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        gridUtil.SetValue(new Point(x, y), 'a' - 'a');
                        var cur = gridUtil.GetValue(new Point(x, y));
                        start = new Point(x, y);
                        cur.start = true;
                    }
                    else if (lines[y][x] == 'E')
                    {
                        gridUtil.SetValue(new Point(x, y), 'z' - 'a');
                        var cur = gridUtil.GetValue(new Point(x, y));
                        cur.end = true;
                    } else
                    {
                        gridUtil.SetValue(new Point(x, y), lines[y][x].ToString()[0] - 'a');
                    }
                    
                }
            }

            PriorityQueue<Point, int> queue = new PriorityQueue<Point, int>();

            queue.Enqueue(start, 0);
            List<Point> viewed = new List<Point>();
            int walk = int.MaxValue;
            while (true)
            {
                int priority = 0;
                Point current = new Point();
                queue.TryPeek(out current, out priority);
                current = queue.Dequeue();

                viewed.Add(current);

                GridItem curGridItem = gridUtil.GetValue(current);
                
                if (curGridItem.end)
                {
                    walk = priority;
                    break;
                }

                foreach (Point neighbor in gridUtil.GetNeighbours4Dir(current.X, current.Y, 1))
                {
                    GridItem neighbourGridItem = gridUtil.GetValue(neighbor);
                    if (!neighbourGridItem.visited)
                    {
                        if (Math.Abs(neighbourGridItem.number - curGridItem.number) <= 1 || neighbourGridItem.number < curGridItem.number)
                        {
                            int thisPriority = priority + 1;
                            if (neighbourGridItem.bestPriority <= thisPriority)
                            {
                                continue;
                            }
                            neighbourGridItem.bestPriority = thisPriority;
                            queue.Enqueue(neighbor, thisPriority);
                        }
                    }
                }
            }

            return walk.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            GridUtilities gridUtil = new GridUtilities();

            List<Point> starts = new List<Point>();
            for (int y = 0; y < lines.Count(); y++)
            {
                for (int x = 0; x < lines[0].Count(); x++)
                {
                    if (lines[y][x] == 'a')
                    {
                        gridUtil.SetValue(new Point(x, y), 'a' - 'a');
                        var cur = gridUtil.GetValue(new Point(x, y));
                        starts.Add(new Point(x, y));
                        cur.start = true;
                    }
                    else if (lines[y][x] == 'E')
                    {
                        gridUtil.SetValue(new Point(x, y), 'z' - 'a');
                        var cur = gridUtil.GetValue(new Point(x, y));
                        cur.end = true;
                    }
                    else
                    {
                        gridUtil.SetValue(new Point(x, y), lines[y][x].ToString()[0] - 'a');
                    }

                }
            }

            long best = int.MaxValue;
            foreach (var start in starts)
            {
                int walk = 0;
                PriorityQueue<Point, int> queue = new PriorityQueue<Point, int>();

                queue.Enqueue(start, 0);
                List<Point> viewed = new List<Point>();
                while (true)
                {

                    if (queue.Count == 0)
                    {
                        walk = int.MaxValue;
                        break;
                    }
                    int priority = 0;
                    Point current = new Point();
                    queue.TryPeek(out current, out priority);
                    current = queue.Dequeue();

                    viewed.Add(current);

                    GridItem curGridItem = gridUtil.GetValue(current);


                    if (curGridItem.end)
                    {
                        walk = priority;
                        break;
                    }

                    foreach (Point neighbor in gridUtil.GetNeighbours4Dir(current.X, current.Y, 1))
                    {
                        GridItem neighbourGridItem = gridUtil.GetValue(neighbor);
                        if (!neighbourGridItem.visited)
                        {
                            if (Math.Abs(neighbourGridItem.number - curGridItem.number) <= 1 || neighbourGridItem.number < curGridItem.number)
                            {
                                int thisPriority = priority + 1;
                                if (neighbourGridItem.bestPriority <= thisPriority)
                                {
                                    continue;
                                }
                                neighbourGridItem.bestPriority = thisPriority;
                                queue.Enqueue(neighbor, thisPriority);
                            }
                        }
                    }
                }

                if (walk < best)
                {
                    best = walk;
                    Console.WriteLine(best);
                }

            }
            return best.ToString(); ;
        }
    }
}