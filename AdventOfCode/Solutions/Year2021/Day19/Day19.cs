using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Solutions.Year2021
{

    class Day19 : ASolution
    {

        const int BEACON_REQ = 12;

        public Day19() : base(19, 2021, "")
        {

        }



        private List<Scanner> inputProcessing(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] sLines = input.Split("\n\n");

            List<Scanner> scannerList = new List<Scanner>();

            foreach (string line in sLines)
            {
                List<Point3d> scannerPoints = new List<Point3d>();
                string[] scannerComponents = line.SplitByNewline();

                int scannerId = -1;
                for (int i = 0; i < scannerComponents.Length; i++)
                {
                    if (i == 0)
                    {
                        string replacedLine = scannerComponents[i].Replace("--- scanner ", "");
                        replacedLine = replacedLine.Replace(" ---", "");
                        scannerId = int.Parse(replacedLine);
                    }
                    else
                    {
                        string[] coords = scannerComponents[i].Split(",");
                        int xCoord = int.Parse(coords[0]);
                        int yCoord = int.Parse(coords[1]);
                        int zCoord = -1;
                        if (coords.Length > 2)
                        {
                            zCoord = int.Parse(coords[2]);
                        }
                        else
                        {
                            zCoord = 0;
                        }
                        scannerPoints.Add(new Point3d(xCoord, yCoord, zCoord));
                    }
                }

                scannerList.Add(new Scanner(scannerId, scannerPoints));
            }

            return scannerList;
        }

        public int FindOverlap(Scanner scanner1, Scanner scanner2)
        {
            HashSet<double> distances1 = scanner1.Distances;
            HashSet<double> distances2 = scanner2.Distances;

            int overlap = 0;
            foreach (double distance in distances1)
            {
                if (distances2.Contains(distance))
                {
                    overlap++;
                }
            }
            return overlap;
        }

        public Scanner FindAllOverlaps(List<Scanner> scanners)
        {

            foreach (Scanner scanner1 in scanners)
            {

                foreach (Scanner scanner2 in scanners)
                {
                    if (scanner1 == scanner2)
                    {
                        continue;
                    }
                    int overlap = FindOverlap(scanner1, scanner2);
                    scanner1.AddOverlapScanner(scanner2, overlap);
                }
            }

            return null;
        }


        public Point3d GetPointThatOverlaps(Scanner scanner, Scanner scannerOther)
        {
            int overlapBest = 0;
            Point3d bestPoint = null;
            List<Point3d> finalOverlaps = new List<Point3d>();
            foreach (KeyValuePair<Point3d, Dictionary<Point3d, double>> entry in scanner.DistancesByPoint)
            {
                int currentOverlap = 0;
                List<Point3d> overlaps = new List<Point3d>();
                foreach (KeyValuePair<Point3d, double> distancePair in entry.Value)
                {
                    if (scannerOther.Distances.Contains(distancePair.Value))
                    {
                        currentOverlap++;
                        overlaps.Add(distancePair.Key);
                    }
                }

                if (currentOverlap > overlapBest)
                {
                    overlapBest = currentOverlap;
                    bestPoint = entry.Key;
                    finalOverlaps = overlaps;
                }

            }

            Assert.IsTrue(bestPoint != null, "Best point null");
            //Assert.IsTrue(overlapBest >= BEACON_REQ - 1, "overlap wasn't high enough");
            if (overlapBest < BEACON_REQ - 1)
            {
                return null;
            }
            return bestPoint;
        }


        public Point3d GetAbsolutePoint(Scanner scanner, Point3d point)
        {
            int rotationX = scanner.rotX; 
            int rotationY = scanner.rotY;
            int rotationZ = scanner.rotZ;
            bool flippedX = scanner.flippedX;
            bool flippedY = scanner.flippedY;
            bool flippedZ = scanner.flippedZ;

            Point3d newPoint = new Point3d(point.X, point.Y, point.Z);
            Point3d newPointDupe = new Point3d(newPoint.X, newPoint.Y, newPoint.Z);

            newPoint.X = Convert.ToInt32(newPointDupe.X * Math.Cos((Math.PI / 180) * rotationZ) - newPointDupe.Y * Math.Sin((Math.PI / 180) * rotationZ));
            newPoint.Y = Convert.ToInt32(newPointDupe.X * Math.Sin((Math.PI / 180) * rotationZ) + newPointDupe.Y * Math.Cos((Math.PI / 180) * rotationZ));

            newPointDupe = new Point3d(newPoint.X, newPoint.Y, newPoint.Z);

            newPoint.Y = Convert.ToInt32(newPointDupe.Z * Math.Sin((Math.PI / 180) * rotationX) + newPointDupe.Y * Math.Cos((Math.PI / 180) * rotationX));
            newPoint.Z = Convert.ToInt32(newPointDupe.Z * Math.Cos((Math.PI / 180) * rotationX) - newPointDupe.Y * Math.Sin((Math.PI / 180) * rotationX));

            newPointDupe = new Point3d(newPoint.X, newPoint.Y, newPoint.Z);

            newPoint.X = Convert.ToInt32(newPointDupe.X * Math.Cos((Math.PI / 180) * rotationY) - newPointDupe.Z * Math.Sin((Math.PI / 180) * rotationY));
            newPoint.Z = Convert.ToInt32(newPointDupe.X * Math.Sin((Math.PI / 180) * rotationY) + newPointDupe.Z * Math.Cos((Math.PI / 180) * rotationY));

            if (flippedX) { newPoint.X = -newPoint.X; }
            if (flippedY) { newPoint.Y = -newPoint.Y; }
            if (flippedZ) { newPoint.Z = -newPoint.Z; }

            return newPoint;
        }

        public bool AttemptMatchTwoPoints(Scanner scanner, Scanner scannerOther,
            Point3d point, Point3d pointOther)
        {
            Point3d absPoint = GetAbsolutePoint(scanner, point);

            List<Point3d> beacons = scanner.Beacons;
            List<Point3d> beaconsAbs = new List<Point3d>();

            // Create list of points if given point is at the origin
            foreach (Point3d beacon in beacons)
            {
                Point3d tempAbsPoint = GetAbsolutePoint(scanner, beacon);
                tempAbsPoint.X += absPoint.X * -1;
                tempAbsPoint.Y += absPoint.Y * -1;
                tempAbsPoint.Z += absPoint.Z * -1;
                beaconsAbs.Add(tempAbsPoint);
            }

            List<Point3d> beaconsOther = scannerOther.Beacons;

            // All the rotations and flips
            for (int rx = 0; rx < 360; rx+=90)
            {
                for (int ry = 0; ry < 360; ry += 90)
                {
                    for (int rz = 0; rz < 360; rz += 90)
                    {
                        foreach (bool flipX in new List<bool>() { false, true })
                        {
                            foreach (bool flipY in new List<bool>() { false, true })
                            {
                                foreach (bool flipZ in new List<bool>() { false, true })
                                {

                                    scannerOther.flippedX = flipX;
                                    scannerOther.flippedY = flipY;
                                    scannerOther.flippedZ = flipZ;

                                    scannerOther.rotX = rx;
                                    scannerOther.rotY = ry;
                                    scannerOther.rotZ = rz;

                                    List<Point3d> beaconsAbsOther = new List<Point3d>();

                                    Point3d absPointOther = GetAbsolutePoint(scannerOther, pointOther);

                                    // Create list of points if given point is at the origin
                                    foreach (Point3d beaconOther in beaconsOther)
                                    {
                                        Point3d tempAbsPoint = GetAbsolutePoint(scannerOther, beaconOther);
                                        tempAbsPoint.X += absPointOther.X * -1;
                                        tempAbsPoint.Y += absPointOther.Y * -1;
                                        tempAbsPoint.Z += absPointOther.Z * -1;
                                        beaconsAbsOther.Add(tempAbsPoint);
                                    }

                                    int overlap = 0;
                                    foreach (Point3d beacon in beaconsAbs)
                                    {
                                        if (beaconsAbsOther.Any(b => beacon.X == b.X &&
                                            beacon.Y == b.Y && beacon.Z == b.Z))
                                        {
                                            overlap++;
                                        }
                                    }

                                    if (overlap >= BEACON_REQ)
                                    {
                                        scannerOther.offsetX = scanner.offsetX + (absPointOther.X - absPoint.X) * -1;
                                        scannerOther.offsetY = scanner.offsetY + (absPointOther.Y - absPoint.Y) * -1;
                                        scannerOther.offsetZ = scanner.offsetZ + (absPointOther.Z - absPoint.Z) * -1;
                                        scannerOther.Orientated = true;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void OrientateToMatch(Scanner scanner, Scanner scannerOther)
        {
            Point3d point = GetPointThatOverlaps(scanner, scannerOther);
            if (point == null)
            {
                return;
            }
            Dictionary<Point3d, double> DistancesFromPoint = scanner.DistancesByPoint[point];

            foreach (KeyValuePair<Point3d, Dictionary<Point3d, double>> entry in scannerOther.DistancesByPoint)
            {
                int currentOverlap = 0;
                foreach (KeyValuePair<Point3d, double> distancePair in entry.Value)
                {
                    foreach (KeyValuePair<Point3d, double> distanceOriginal in DistancesFromPoint)
                    {
                        if (distancePair.Value == distanceOriginal.Value)
                        {
                            currentOverlap++;
                        }
                    }
                }

                if (currentOverlap < BEACON_REQ - 1)
                {
                    continue;
                }

                Point3d otherPoint = entry.Key;

                bool match = AttemptMatchTwoPoints(scanner, scannerOther, point, otherPoint);

                if (match)
                {
                    break;
                }

            }
        }

        protected override string SolvePartOne(string input)
        {
            List<Scanner> scanners = inputProcessing(input);


            foreach (Scanner scanner in scanners)
            {
                scanner.CalculateDistances();
            }

            FindAllOverlaps(scanners);

            scanners[0].Orientated = true;
            bool allFound = false;
            while (!allFound)
            {
                foreach (Scanner scanner in scanners)
                {
                    if (!scanner.Orientated)
                    {
                        continue;
                    }

                    foreach (KeyValuePair<Scanner, int> entry in scanner.Overlaps)
                    {
                        if (entry.Key.Orientated)
                        {
                            continue;
                        }
                        if (entry.Value >= BEACON_REQ)
                        {
                            OrientateToMatch(scanner, entry.Key);
                        }
                    }
                }

                allFound = true;
                
                foreach (Scanner scanner in scanners)
                {
                    if (!scanner.Orientated)
                    {
                        allFound = false;
                        break;
                    }
                }
            }

            Dictionary<(int, int, int), bool> Grid = new Dictionary<(int, int, int), bool>();

            foreach (Scanner scanner in scanners)
            {
                if (!scanner.Orientated)
                {
                    continue;
                }
                foreach (Point3d beacon in scanner.Beacons)
                {
                    Point3d point = GetAbsolutePoint(scanner, beacon);
                    point.X = point.X + scanner.offsetX;
                    point.Y = point.Y + scanner.offsetY;
                    point.Z = point.Z + scanner.offsetZ;
                    Grid[(point.X, point.Y, point.Z)] = true;
                }
            }

            return Grid.Count().ToString();
        }


        protected override string SolvePartTwo(string input)
        {
            List<Scanner> scanners = inputProcessing(input);


            foreach (Scanner scanner in scanners)
            {
                scanner.CalculateDistances();
            }

            FindAllOverlaps(scanners);

            scanners[0].Orientated = true;
            bool allFound = false;
            while (!allFound)
            {
                foreach (Scanner scanner in scanners)
                {
                    if (!scanner.Orientated)
                    {
                        continue;
                    }

                    foreach (KeyValuePair<Scanner, int> entry in scanner.Overlaps)
                    {
                        if (entry.Key.Orientated)
                        {
                            continue;
                        }
                        if (entry.Value >= BEACON_REQ)
                        {
                            OrientateToMatch(scanner, entry.Key);
                        }
                    }
                }

                allFound = true;

                foreach (Scanner scanner in scanners)
                {
                    if (!scanner.Orientated)
                    {
                        allFound = false;
                        break;
                    }
                }

            }

            Dictionary<(int, int, int), bool> Grid = new Dictionary<(int, int, int), bool>();

            foreach (Scanner scanner in scanners)
            {
                if (!scanner.Orientated)
                {
                    continue;
                }
                foreach (Point3d beacon in scanner.Beacons)
                {
                    Point3d point = GetAbsolutePoint(scanner, beacon);
                    point.X = point.X + scanner.offsetX;
                    point.Y = point.Y + scanner.offsetY;
                    point.Z = point.Z + scanner.offsetZ;
                    Grid[(point.X, point.Y, point.Z)] = true;
                }
            }

            long bestDistance = 0;
            foreach (Scanner scanner1 in scanners)
            {
                foreach (Scanner scanner2 in scanners)
                {
                    long distX = Math.Abs(scanner1.offsetX - scanner2.offsetX);
                    long distY = Math.Abs(scanner1.offsetY - scanner2.offsetY);
                    long distZ = Math.Abs(scanner1.offsetZ - scanner2.offsetZ);

                    if (distX + distY + distZ > bestDistance)
                    {
                        bestDistance = distX + distY + distZ;
                    }
                }
            }

            return bestDistance.ToString();
        }
    }

    class Scanner
    {
        public int Id;
        public List<Point3d> Beacons;
        public Dictionary<Point3d, Dictionary<Point3d, double>> DistancesByPoint;
        public HashSet<double> Distances = new HashSet<double>();
        public Dictionary<Scanner, int> Overlaps = new Dictionary<Scanner, int>();
        public bool Orientated = false;

        public bool flippedX = false;
        public bool flippedY = false;
        public bool flippedZ = false;
        public int rotX = 0;
        public int rotY = 0;
        public int rotZ = 0;
        public int offsetX = 0;
        public int offsetY = 0;
        public int offsetZ = 0;



        public Scanner(int id, List<Point3d> beacons)
        {
            Id = id;
            Beacons = beacons;
            DistancesByPoint = new Dictionary<Point3d, Dictionary<Point3d, double>>();
        }

        public void CalculateDistances()
        {
            foreach (var beacon1 in Beacons)
            {
                DistancesByPoint[beacon1] = new Dictionary<Point3d, double>();
                foreach (var beacon2 in Beacons)
                {
                    if (beacon1 == beacon2)
                    {
                        continue;
                    }
                    double distance = beacon1.DistanceTo(beacon2);
                    DistancesByPoint[beacon1].Add(beacon2, distance);
                    Distances.Add(distance);
                }
            }
        }

        public void AddOverlapScanner(Scanner scanner, int overlap)
        {
            Overlaps.Add(scanner, overlap);
        }
    }

    class Point3d
    {
        public int X;
        public int Y;
        public int Z;

        public Point3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double DistanceTo(Point3d other)
        {
            double distance = Math.Sqrt(Math.Pow((other.X - X), 2) + Math.Pow((other.Y - Y), 2)
                + Math.Pow((other.Z - Z), 2));
            return distance;
        }

    }
}