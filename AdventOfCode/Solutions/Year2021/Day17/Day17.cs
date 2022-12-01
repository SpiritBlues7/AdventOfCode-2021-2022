using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day17 : ASolution
    {


        public Day17() : base(17, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("target area: x=", "");
            string[] linesComp = input.Split(", y=");
            int yMin = int.Parse(linesComp[1].Split("..")[0]);
            int yMax = int.Parse(linesComp[1].Split("..")[1]);
            int xMin = int.Parse(linesComp[0].Split("..")[0]);
            int xMax = int.Parse(linesComp[0].Split("..")[1]);


            var pointsFound = new List<(Point, long)>();
            for (int x = -500; x < 500; x++)
            {
                for (int y = -500; y < 500; y++)
                {
                    long step = 0;
                    long maxY = -5000;
                    long currentY = 0;
                    long currentX = 0;                    
                    long velocityY = y;
                    long velocityX = x;
                    while (true)
                    {

                        step++;
                        if (currentY < yMin)
                        {
                            break;
                        }
                        currentY += velocityY;
                        currentX += velocityX;
                        if (velocityX < 0)
                        {
                            velocityX++;
                        }
                        if (velocityX > 0)
                        {
                            velocityX--;
                        }

                        velocityY--;

                        if (currentY > maxY)
                        {
                            maxY = currentY;
                        }

                        if (currentX <= xMax && currentX >= xMin && currentY <= yMax && currentY >= yMin)
                        {
                            pointsFound.Add((new Point(x, y), maxY));
                            break;
                        }
                    }
                }
            }


            return pointsFound.Max(p => p.Item2).ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("target area: x=", "");
            string[] linesComp = input.Split(", y=");
            int yMin = int.Parse(linesComp[1].Split("..")[0]);
            int yMax = int.Parse(linesComp[1].Split("..")[1]);
            int xMin = int.Parse(linesComp[0].Split("..")[0]);
            int xMax = int.Parse(linesComp[0].Split("..")[1]);


            var pointsFound = new List<(Point, long)>();
            for (int x = -500; x < 500; x++)
            {
                for (int y = -500; y < 500; y++)
                {
                    long step = 0;
                    long maxY = -5000;
                    long currentY = 0;
                    long currentX = 0;
                    long velocityY = y;
                    long velocityX = x;
                    while (true)
                    {

                        step++;
                        if (currentY < yMin)
                        {
                            break;
                        }
                        currentY += velocityY;
                        currentX += velocityX;
                        if (velocityX < 0)
                        {
                            velocityX++;
                        }
                        if (velocityX > 0)
                        {
                            velocityX--;
                        }

                        velocityY--;

                        if (currentY > maxY)
                        {
                            maxY = currentY;
                        }

                        if (currentX <= xMax && currentX >= xMin && currentY <= yMax && currentY >= yMin)
                        {
                            pointsFound.Add((new Point(x, y), maxY));
                            break;
                        }
                    }
                }
            }


            return pointsFound.Count().ToString();
        }
    }
}