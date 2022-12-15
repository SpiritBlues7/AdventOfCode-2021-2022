using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day15 : ASolution
    {

        public Day15() : base(15, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            List<(Point, int)> ranges = new List<(Point, int)>();
            long maxX = long.MinValue;
            long maxY = long.MinValue;
            long minX = long.MaxValue;
            long minY = long.MaxValue;
            long maxDist = 0;
            List<Point> beacons = new List<Point>();
            foreach (string line in lines)
            {
                string[] comp = line.Split("=");
                long myX = comp[1].Split(",")[0].ToLong();

                long myY = comp[2].Split(":")[0].ToLong();
                long closestX = comp[3].Split(",")[0].ToLong();
                long closestY = comp[4].ToLong();
                int distance = Utilities.ManhattanDistance(((int) myX, (int)myY), ((int)closestX, (int)closestY));
                ranges.Add((new Point((int)myX, (int)myY), distance));

                beacons.Add(new Point((int) closestX, (int) closestY));
                if (myX < minX)
                {
                    minX = myX;
                }
                if (myX > minX)
                {
                    maxX = myX;
                }
                if (myY < minY)
                {
                    minY = myY;
                }
                if (myY > minY)
                {
                    maxY = myY;
                }
                if (maxDist < distance)
                {
                    maxDist = distance;
                }
            }

            maxX += maxDist + 5;
            maxY += maxDist + 5;
            minX -= maxDist + 5;
            minY -= maxDist + 5;
            int currentY = 2000000;
            long count = 0;
            for (int x = (int) minX; x < maxX; x++)
            {
                bool beaconThere = false;
                foreach (Point beacon in beacons)
                {
                    if (beacon == new Point(x,currentY))
                    {
                        beaconThere = true;
                        break;
                    }
                }
                if (beaconThere)
                {
                    continue;
                }

                bool beaconNotThere = false;
                foreach ((Point, int) range in ranges)
                {
                    Point s = range.Item1;
                    int distance = range.Item2;
                    if (Math.Abs(x - s.X) + Math.Abs((int)currentY - s.Y) <= distance)
                    {
                        beaconNotThere = true;
                    }
                }

                if (beaconNotThere)
                {
                    count++;
                }
            }
            return count.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            List<(Point, int)> ranges = new List<(Point, int)>();
            long totalMaxX = 4000000;
            long totalMinX = 0;
            long totalMaxY = 4000000;
            long totalMinY = 0;

            List<Point> beacons = new List<Point>();
            foreach (string line in lines)
            {
                string[] comp = line.Split("=");
                long myX = comp[1].Split(",")[0].ToLong();

                long myY = comp[2].Split(":")[0].ToLong();
                long closestX = comp[3].Split(",")[0].ToLong();
                long closestY = comp[4].ToLong();
                int distance = Utilities.ManhattanDistance(((int)myX, (int)myY), ((int)closestX, (int)closestY));
                ranges.Add((new Point((int)myX, (int)myY), distance));

                beacons.Add(new Point((int)closestX, (int)closestY));
            }

            List<Point> possiblePoints = new List<Point>();
            foreach ((Point, int) range in ranges)
            {
                int sensX = range.Item1.X;
                int sensY = range.Item1.Y;
                int dist = range.Item2;
                for (int x = sensX - dist, y = 0; y <= dist; x++, y++)
                {

                    possiblePoints.Add(new Point(x - 1, sensY + y));
                    possiblePoints.Add(new Point(x - 1, sensY - y));
                    
                }
                for (int x = sensX + dist, y = 0; y <= dist; x--, y++)
                {
                    possiblePoints.Add(new Point(x + 1, sensY + y));
                    possiblePoints.Add(new Point(x + 1, sensY - y));
                }
                possiblePoints.Add(new Point(0, sensY + dist + 1));
                possiblePoints.Add(new Point(0, sensY - dist - 1));
            }

            long ans = 0;
            foreach (Point possiblePoint in possiblePoints)
            {
                bool beaconNotThere = false;
                foreach ((Point, int) range in ranges)
                {
                    Point s = range.Item1;
                    int distance = range.Item2;
                    int currentDistance = Math.Abs(possiblePoint.X - s.X) + Math.Abs(possiblePoint.Y - s.Y);
                    if (currentDistance <= distance)
                    {
                        beaconNotThere = true;
                        break;
                    }
                }

                if (!beaconNotThere && 
                    possiblePoint.Y <= totalMaxY && possiblePoint.X <= totalMaxX &&
                    possiblePoint.Y >= totalMinY && possiblePoint.X >= totalMinX)
                {

                    ans = (long)((long)possiblePoint.X * 4000000) + (long)possiblePoint.Y;
                    break;
                }
            }
           
            return ans.ToString();
        }
    }
}
