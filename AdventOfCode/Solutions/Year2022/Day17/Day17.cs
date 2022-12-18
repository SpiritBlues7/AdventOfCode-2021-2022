using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day17 : ASolution
    {


        public Day17() : base(17, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");

            Dictionary<Point, char> grid = new Dictionary<Point, char>();
            int maxHeight = 0;
            Dictionary<int, List<Point>> rockTypes = new Dictionary<int, List<Point>>();
            rockTypes.Add(0, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) });    
            rockTypes.Add(1, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(1, 1), new Point(1, -1) });
            rockTypes.Add(2, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(2, 1), new Point(2, 2) });
            rockTypes.Add(3, new List<Point>() { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3)});
            rockTypes.Add(4, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1) });

            Dictionary<int, int> rockWidths = new Dictionary<int, int>()
            {
                { 0, 3 },
                { 1, 2 },
                { 2, 2 },
                { 3, 0 },
                { 4, 1 },
            };
            Dictionary<int, int> rockHeights = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
            };

            for (int y = 0; y < 500000; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    grid[new Point(x,y)] = '.';
                }
            }

            int rockNum = 0;
            int count = 0;
            int currentHeight = maxHeight + 3;
            int currentX = 2;

            while (true)
            {
                if (count == 2022)
                {
                    break;
                }
                for (int i = 0; i < input.Length; i++)
                {
                    if (count == 2022)
                    {
                        break;
                    }

                    bool collidedOne = false;

                    char dir = input[i];
                    if (dir == '<')
                    {

                        if (currentX > 0 )
                        {
                            foreach (Point part in rockTypes[rockNum])
                            {
                                if (grid[new Point(currentX + part.X - 1, currentHeight + part.Y)] == '#')
                                {
                                    collidedOne = true;
                                }
                            }
                            if (!collidedOne)
                            {
                                currentX--;
                            }
                            
                        }
                    }
                    if (dir == '>')
                    {

                        if (currentX + rockWidths[rockNum] < 6)
                        {
                            foreach (Point part in rockTypes[rockNum])
                            {
                                if (grid[new Point(currentX + part.X + 1, currentHeight + part.Y)] == '#')
                                {
                                    collidedOne = true;
                                }
                            }
                            if (!collidedOne)
                            {
                                currentX++;
                            }
                        }
                    }

                    bool collidedTwo = false;
                    foreach (Point part in rockTypes[rockNum])
                    {
                        if (currentHeight + part.Y - 1 == -1 || grid[new Point(currentX + part.X, currentHeight + part.Y - 1)] == '#')
                        {
                            collidedTwo = true;
                        }
                    }
                    if (collidedTwo)
                    {
                        foreach (Point part in rockTypes[rockNum])
                        {
                            grid[new Point(currentX + part.X, currentHeight + part.Y)] = '#';
                            if (currentHeight + part.Y + 1> maxHeight)
                            {
                                maxHeight = currentHeight + part.Y + 1;
                                Console.WriteLine(maxHeight);

                            }
                        }
                        //for (int y = 10; y >= 0; y--)
                        //{
                        //    for (int x = 0; x < 7; x++)
                        //    {
                        //        Console.Write(grid[new Point(x, y)]);
                        //    }
                        //    Console.WriteLine();
                        //}
                        //Console.WriteLine();
                        count++;
                        rockNum = (rockNum + 1) % 5;
                        currentHeight = maxHeight + 3 + rockHeights[rockNum];
                        currentX = 2;
                    }
                    else
                    {
                        currentHeight--;
                    }
                }
            }




            return maxHeight.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");

            Dictionary<Point, char> grid = new Dictionary<Point, char>();
            int maxHeight = 0;
            Dictionary<int, List<Point>> rockTypes = new Dictionary<int, List<Point>>();
            rockTypes.Add(0, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) });
            rockTypes.Add(1, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(1, 1), new Point(1, -1) });
            rockTypes.Add(2, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(2, 1), new Point(2, 2) });
            rockTypes.Add(3, new List<Point>() { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) });
            rockTypes.Add(4, new List<Point>() { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1) });

            Dictionary<int, int> rockWidths = new Dictionary<int, int>()
            {
                { 0, 3 },
                { 1, 2 },
                { 2, 2 },
                { 3, 0 },
                { 4, 1 },
            };
            Dictionary<int, int> rockHeights = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
            };

            for (int y = 0; y < 5000000; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    grid[new Point(x, y)] = '.';
                }
            }

            int rockNum = 0;
            int count = 0;
            int currentHeight = maxHeight + 3;
            int currentX = 2;
            Console.WriteLine((long)(24476 - 3064)*(long)70721357 + 3064 + 18204 - 3064);
            Console.WriteLine((long)(1110053 - 40665)*(long)1447513 + 184349);
            Console.WriteLine((long)(5402 - 2755)*(long)584795320 + 4353);
            //1491263477320
            //1491267631983
            string floorXState = ".......";
            string prevXState = ".......";
            string prevPrevXState = ".......";
            string prevPrevPrevXState = ".......";
            string prevPrevPrevPrevXState = ".......";
            List<int> vals = new List<int>();
            Dictionary<(string, string, string, string, string, int, int), int> floorHeightsStates = new Dictionary<(string, string, string, string, string, int, int), int>();

            while (true)
            {

                if (count == 1000000000)
                {
                    break;
                }
                for (int i = 0; i < input.Length; i++)
                {
                    //if (count % 1000 == 0)
                    //{
                    //    Console.WriteLine(count);
                    //}
                    if (count % 2800 == 0 && count != 0)
                    {
                        Console.WriteLine("{0}, {1}", count, currentHeight);
                    }


                    bool collidedOne = false;

                    char dir = input[i];
                    if (dir == '<')
                    {

                        if (currentX > 0)
                        {
                            foreach (Point part in rockTypes[rockNum])
                            {
                                if (grid[new Point(currentX + part.X - 1, currentHeight + part.Y)] == '#')
                                {
                                    collidedOne = true;
                                }
                            }
                            if (!collidedOne)
                            {
                                currentX--;
                            }

                        }
                    }
                    if (dir == '>')
                    {

                        if (currentX + rockWidths[rockNum] < 6)
                        {
                            foreach (Point part in rockTypes[rockNum])
                            {
                                if (grid[new Point(currentX + part.X + 1, currentHeight + part.Y)] == '#')
                                {
                                    collidedOne = true;
                                }
                            }
                            if (!collidedOne)
                            {
                                currentX++;
                            }
                        }
                    }

                    bool collidedTwo = false;
                    foreach (Point part in rockTypes[rockNum])
                    {
                        if (currentHeight + part.Y - 1 == -1 || grid[new Point(currentX + part.X, currentHeight + part.Y - 1)] == '#')
                        {
                            collidedTwo = true;
                        }
                    }
                    if (collidedTwo)
                    {
                        foreach (Point part in rockTypes[rockNum])
                        {
                            grid[new Point(currentX + part.X, currentHeight + part.Y)] = '#';
                            if (currentHeight + part.Y + 1 > maxHeight)
                            {
                                maxHeight = currentHeight + part.Y + 1;
                                //Console.WriteLine(maxHeight);

                            }
                        }
                        //for (int y = 10; y >= 0; y--)
                        //{
                        //    for (int x = 0; x < 7; x++)
                        //    {
                        //        Console.Write(grid[new Point(x, y)]);
                        //    }
                        //    Console.WriteLine();
                        //}
                        //Console.WriteLine();
                        count++;
                        rockNum = (rockNum + 1) % 5;
                        currentHeight = maxHeight + 3 + rockHeights[rockNum];
                        currentX = 2;
                        //Console.WriteLine("{0}, {1}, {2}", count, count / 2020, maxHeight);
                        //for (int y = currentHeight; y >= 0; y--)
                        //{
                        //    for (int x = 0; x < 7; x++)
                        //    {
                        //        Console.Write(grid[new Point(x, y)]);
                        //    }
                        //    Console.WriteLine();
                        //}
                        //Console.WriteLine();
                        if (currentHeight > 10)
                        {
                            floorXState = "";
                            prevXState = "";
                            prevPrevXState = "";
                            prevPrevPrevXState = "";
                            prevPrevPrevPrevXState = "";
                            for (int x = 0; x < 7; x++)
                            {
                                floorXState += grid[new Point(x, maxHeight - 1)];
                                prevXState += grid[new Point(x, maxHeight - 2)];
                                prevPrevXState += grid[new Point(x, maxHeight - 3)];
                                prevPrevPrevXState += grid[new Point(x, maxHeight - 4)];
                                prevPrevPrevPrevXState += grid[new Point(x, maxHeight - 5)];
                            }
                            if (floorHeightsStates.ContainsKey((floorXState, prevXState, prevPrevXState, prevPrevPrevXState, prevPrevPrevPrevXState, rockNum, i + 1)))
                            {
                                Console.WriteLine("{0}, {1}, {2}, {3}", count, maxHeight, rockNum, i + 1);
                                floorHeightsStates = new Dictionary<(string, string, string, string, string, int, int), int>();

                            }
                            floorHeightsStates[(floorXState, prevXState, prevPrevXState, prevPrevPrevXState, prevPrevPrevPrevXState, rockNum, i + 1)] = maxHeight;
                        }

                        if (count % 2020 == 0 && count != 0 && count >= 2020)
                        {
                            if (grid[new Point(3, maxHeight - 1)] == '#' &&
                                grid[new Point(3, maxHeight - 2)] == '#' &&
                                grid[new Point(3, maxHeight - 3)] == '#' &&
                                grid[new Point(3, maxHeight - 4)] == '#' &&
                                grid[new Point(2, maxHeight - 4)] == '#' &&
                                grid[new Point(1, maxHeight - 4)] == '#' &&
                                grid[new Point(4, maxHeight - 4)] == '#' &&
                                grid[new Point(2, maxHeight - 2)] == '#' &&
                                grid[new Point(4, maxHeight - 2)] == '#'
                                )
                            {

                            }
                            if (((count) / 2020 - 13) % 342 == 0)
                            {
                                if (vals.Contains(((count) / 2020 - 13) / 2))
                                {
                                    //Console.WriteLine("###################################");
                                }
                                vals.Add((count) / 2020 - 13);
                                //Console.WriteLine("{0}, {1}, {2}", count, (count) / 2020 - 13, maxHeight);
                            }



                            //for (int y = currentHeight; y >= currentHeight - 20; y--)
                            //{
                            //    for (int x = 0; x < 7; x++)
                            //    {
                            //        Console.Write(grid[new Point(x, y)]);
                            //    }
                            //    Console.WriteLine();
                            //}
                            //Console.WriteLine();
                        }

                    }
                    else
                    {
                        currentHeight--;
                    }
                }
            }
            return maxHeight.ToString();
        }
    }
}