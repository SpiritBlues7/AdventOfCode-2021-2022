using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day13 : ASolution
    {


        public Day13() : base(13, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            var fixedInput = input.Replace("\r\n", "\n");
            string[] components = fixedInput.Split("\n\n");
            var lines = components[0].SplitByNewline();
            var folds = components[1].SplitByNewline();

            var G = new Dictionary<Point, bool>();

            foreach (var line in lines)
            {
                var pointString = line.Split(",");
                Point point = new Point(int.Parse(pointString[0]), int.Parse(pointString[1]));

                G[point] = true;
            }

            var total = G.Count();

            var foldString = folds[0].Replace("fold along ", "");
            var foldDir = foldString.Split("=")[0];
            var foldNum = int.Parse(foldString.Split("=")[1]);

            var maxX = G.Max(x => x.Key.X);
            var minX = G.Min(x => x.Key.X);
            var maxY = G.Max(y => y.Key.Y);
            var minY = G.Min(y => y.Key.Y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Point newPoint;
                    if (foldDir == "x" && x > foldNum && G.ContainsKey(new Point(x, y)))
                    {
                        G.Remove(new Point(x, y));
                        newPoint = new Point(foldNum - (x - foldNum), y);
                        G[newPoint] = true;
                    }
                    if (foldDir == "y" && y > foldNum && G.ContainsKey(new Point(x, y)))
                    {
                        G.Remove(new Point(x, y));
                        newPoint = new Point(x, foldNum - (y - foldNum));
                        G[newPoint] = true;
                    }
                }
            }

            return G.Count().ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            var fixedInput = input.Replace("\r\n", "\n");
            string[] components = fixedInput.Split("\n\n");
            var lines = components[0].SplitByNewline();
            var folds = components[1].SplitByNewline();

            var G = new Dictionary<Point, bool>();

            foreach (var line in lines)
            {
                var pointString = line.Split(",");
                Point point = new Point(int.Parse(pointString[0]), int.Parse(pointString[1]));

                G[point] = true;
            }



            var total = G.Count();


            int maxX;
            int minX;
            int maxY;
            int minY;

            foreach (var foldIn in folds)
            {
                var foldString = foldIn.Replace("fold along ", "");
                var foldDir = foldString.Split("=")[0];
                var foldNum = int.Parse(foldString.Split("=")[1]);


                maxX = G.Max(x => x.Key.X);
                minX = G.Min(x => x.Key.X);
                maxY = G.Max(y => y.Key.Y);
                minY = G.Min(y => y.Key.Y);

                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        Point newPoint;
                        if (foldDir == "x" && x > foldNum && G.ContainsKey(new Point(x, y)))
                        {
                            G.Remove(new Point(x, y));
                            newPoint = new Point(foldNum - (x - foldNum), y);
                            G[newPoint] = true;
                        }
                        if (foldDir == "y" && y > foldNum && G.ContainsKey(new Point(x, y)))
                        {
                            G.Remove(new Point(x, y));
                            newPoint = new Point(x, foldNum - (y - foldNum));
                            G[newPoint] = true;
                        }
                    }
                }
            }

            maxX = G.Max(x => x.Key.X);
            minX = G.Min(x => x.Key.X);
            maxY = G.Max(y => y.Key.Y);
            minY = G.Min(y => y.Key.Y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var currentPoint = new Point(x, y);
                    if (G.ContainsKey(currentPoint) && G[currentPoint])
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }

                }
                Console.WriteLine();
            }

            return "";
        }
    }
}