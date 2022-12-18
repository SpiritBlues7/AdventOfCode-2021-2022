using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Solutions.Year2022
{

    class Day18 : ASolution
    {

        public Day18() : base(18, 2022, "")
        {

        }


        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            long total = lines.Count() * 6;
            List<(int, int, int)> cubes = new List<(int, int, int)>();
            foreach (string line in lines)
            {
                string[] comp = line.Split(",");
                int x = int.Parse(comp[0]);
                int y = int.Parse(comp[1]);
                int z = int.Parse(comp[2]);
                cubes.Add((x, y, z));
            }

            foreach ((int, int, int) cube1 in cubes)
            {
                foreach ((int, int, int) cube2 in cubes)
                {
                    if (cube1.Item1 == cube2.Item1 && cube1.Item2 == cube2.Item2 && cube1.Item3 != cube2.Item3 && Math.Abs(cube1.Item3 - cube2.Item3) <= 1)
                    {
                        total--;
                    }
                    if (cube1.Item2 == cube2.Item2 && cube1.Item3 == cube2.Item3 && cube1.Item1 != cube2.Item1 && Math.Abs(cube1.Item1 - cube2.Item1) <= 1)
                    {
                        total--;
                    }
                    if (cube1.Item1 == cube2.Item1 && cube1.Item3 == cube2.Item3 && cube1.Item2 != cube2.Item2 && Math.Abs(cube1.Item2 - cube2.Item2) <= 1)
                    {
                        total--;
                    }
                }
            }

            return total.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            long total = lines.Count() * 6;
            HashSet<(int, int, int)> cubes = new HashSet<(int, int, int)>();
            foreach (string line in lines)
            {
                string[] comp = line.Split(",");
                int x = int.Parse(comp[0]);
                int y = int.Parse(comp[1]);
                int z = int.Parse(comp[2]);
                cubes.Add((x, y, z));
            }

            foreach ((int, int, int) cube1 in cubes)
            {
                foreach ((int, int, int) cube2 in cubes)
                {
                    if (cube1.Item1 == cube2.Item1 && cube1.Item2 == cube2.Item2 && cube1.Item3 != cube2.Item3 && Math.Abs(cube1.Item3 - cube2.Item3) <= 1)
                    {
                        total--;
                    }
                    if (cube1.Item2 == cube2.Item2 && cube1.Item3 == cube2.Item3 && cube1.Item1 != cube2.Item1 && Math.Abs(cube1.Item1 - cube2.Item1) <= 1)
                    {
                        total--;
                    }
                    if (cube1.Item1 == cube2.Item1 && cube1.Item3 == cube2.Item3 && cube1.Item2 != cube2.Item2 && Math.Abs(cube1.Item2 - cube2.Item2) <= 1)
                    {
                        total--;
                    }
                }
            }

            HashSet<(int, int, int)> airPockets = new HashSet<(int, int, int)>();
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    for (int z = 0; z < 20; z++)
                    {
                        if (x == 15 && y == 15 && z == 15)
                        {
                            Console.WriteLine();
                        }
                        int edgesTouching = 0;
                        bool isCube = false;
                        foreach ((int, int, int) cube in cubes)
                        {
                            if (cube.Item1 == x && cube.Item2 == y && cube.Item3 == z)
                            {
                                isCube = true;
                                break;
                            }
                        }

                        if (isCube)
                        {
                            continue;
                        }
                        (bool, int) resultXp = checkDirectionForCube(cubes, x, y, z, 1, 0, 0);
                        (bool, int) resultXm = checkDirectionForCube(cubes, x, y, z, -1, 0, 0);
                        (bool, int) resultYp = checkDirectionForCube(cubes, x, y, z, 0, 1, 0);
                        (bool, int) resultYm = checkDirectionForCube(cubes, x, y, z, 0, -1, 0);
                        (bool, int) resultZp = checkDirectionForCube(cubes, x, y, z, 0, 0, 1);
                        (bool, int) resultZm = checkDirectionForCube(cubes, x, y, z, 0, 0, -1);
                        if (resultXp.Item1) { edgesTouching++; }
                        if (resultXm.Item1) { edgesTouching++; }
                        if (resultYp.Item1) { edgesTouching++; }
                        if (resultYm.Item1) { edgesTouching++; }
                        if (resultZp.Item1) { edgesTouching++; }
                        if (resultZm.Item1) { edgesTouching++; }


                        if (edgesTouching == 6)
                        {
                            airPockets.Add((x, y, z));
                            Console.WriteLine("{0} {1} {2}", x, y, z);
                            total -= 6;
                            //if (resultXp.Item2 == 0) { total--; }
                            //if (resultXm.Item2 == 0) { total--; }
                            //if (resultYp.Item2 == 0) { total--; }
                            //if (resultYm.Item2 == 0) { total--; }
                            //if (resultZp.Item2 == 0) { total--; }
                            //if (resultZm.Item2 == 0) { total--; }
                        }
                    }
                }
            }

            foreach ((int, int, int) cube1 in airPockets)
            {
                foreach ((int, int, int) cube2 in airPockets)
                {
                    if (cube1.Item1 == cube2.Item1 && cube1.Item2 == cube2.Item2 && cube1.Item3 != cube2.Item3 && Math.Abs(cube1.Item3 - cube2.Item3) <= 1)
                    {
                        total++;
                    }
                    if (cube1.Item2 == cube2.Item2 && cube1.Item3 == cube2.Item3 && cube1.Item1 != cube2.Item1 && Math.Abs(cube1.Item1 - cube2.Item1) <= 1)
                    {
                        total++;
                    }
                    if (cube1.Item1 == cube2.Item1 && cube1.Item3 == cube2.Item3 && cube1.Item2 != cube2.Item2 && Math.Abs(cube1.Item2 - cube2.Item2) <= 1)
                    {
                        total++;
                    }
                }
            }

            return total.ToString();
        }

        private static (bool, int) checkDirectionForCube(HashSet<(int, int, int)> cubes, int x, int y, int z, int Xmodifier, int Ymodifier, int Zmodifier)
        {
            int airCount = 0;
            while (x >= 0 && y >= 0 && z >= 0 && x <= 20 && y <= 20 && z <= 20)
            {
                bool foundCube = false;
                foreach ((int, int, int) cube in cubes)
                {
                    airCount = 0;
                    if (cube.Item1 == x && cube.Item2 == y && cube.Item3 == z)
                    {
                        foundCube = true;
                        break;
                    } else
                    {
                        airCount++;
                    }
                }
                if (foundCube)
                {
                    return (true, airCount);
                }
                x += Xmodifier;
                y += Ymodifier;
                z += Zmodifier;
            }
            return (false, 0);
        }
    }
}