using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace AdventOfCode.Solutions.Year2022
{

    class Day24 : ASolution
    {


        public Day24() : base(24, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            int yMax = lines.Length - 1;
            int xMax = lines[0].Length - 2 - 1;
            HashSet<(int x, int y)> blizzard = new HashSet<(int, int)>();
            HashSet<(int x, int y, char dir)> blizzardAnDir = new HashSet<(int, int, char)>();
            Dictionary<(int, int), char> origGrid = new Dictionary<(int, int), char>();
            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    origGrid[(x, y)] = lines[y][x + 1];
                    if (lines[y][x + 1] != '.' && lines[y][x + 1] != '#')
                    {
                        origGrid[(x, y)] = '.';
                        blizzard.Add((x, y));
                        blizzardAnDir.Add((x, y, lines[y][x + 1]));
                    }
                }
            }

            (int x, int y) start = (0, 0);
            (int x, int y) end = (0, 0);
            for (int x = 0; x <= xMax; x++)
            {
                if (lines[0][x + 1] != '#')
                {
                    start = (x, 0);
                }
                if (lines[yMax][x + 1] != '#')
                {
                    end = (x, yMax);
                }
            }

            List<(int, int)> directs = new List<(int, int)>() { (0,0), (0, -1), (1, 0), (0, 1), (-1, 0) };
            Dictionary<int, HashSet<(int, int)>> grids = new Dictionary<int, HashSet<(int, int)>>();
            grids[0] = blizzard;
            for (int i = 0; i < 10000; i++)
            {
                HashSet<(int, int)> gridBlizzards = new HashSet<(int, int)>();
                HashSet<(int x, int y, char dir)> newBlizzardAnDir = new HashSet<(int, int, char)>();
                foreach ((int x, int y, char dir) bliz in blizzardAnDir)
                {
                    
                    switch (bliz.dir)
                    {
                        case '^':
                            newBlizzardAnDir.Add((bliz.x, (bliz.y - 1 - 1 + (yMax - 1)) % (yMax - 1) + 1, bliz.dir));
                            gridBlizzards.Add((bliz.x, (bliz.y - 1 - 1 + (yMax - 1)) % (yMax - 1) + 1));
                            break;  
                        case '>':
                            newBlizzardAnDir.Add(((bliz.x + 1 + (xMax + 1)) % (xMax + 1), bliz.y, bliz.dir));
                            gridBlizzards.Add(((bliz.x + 1 + (xMax + 1)) % (xMax + 1), bliz.y));
                            break;  
                        case 'v':
                            newBlizzardAnDir.Add((bliz.x, (bliz.y + 1 - 1 + (yMax - 1)) % (yMax - 1) + 1, bliz.dir));
                            gridBlizzards.Add((bliz.x, (bliz.y + 1 - 1 + (yMax - 1)) % (yMax - 1) + 1));
                            break;  
                        case '<':
                            newBlizzardAnDir.Add(((bliz.x - 1 + (xMax + 1)) % (xMax + 1),  bliz.y, bliz.dir));
                            gridBlizzards.Add(((bliz.x - 1 + (xMax + 1)) % (xMax + 1),  bliz.y));
                            break;

                        default:
                            break;
                    }

                }
                grids[i + 1] = gridBlizzards;
                blizzardAnDir = newBlizzardAnDir;
            }



            HashSet<(int, int, int)> seen = new HashSet<(int, int, int)>();
            Dictionary<(int, int), int> best = new Dictionary<(int, int), int>();
            Queue<(int n, int x, int y,  string path)> Q = new Queue<(int, int, int, string)>();
            Q.Enqueue((0, start.x, start.y, ""));
            int bestVal = int.MaxValue;
            while (Q.Count > 0)
            {
                (int n, int x, int y, string path) current = Q.Dequeue();
                if (current.x == end.x && current.y == end.y)
                {
                    bestVal = Math.Min(bestVal, current.n);
                    Console.WriteLine("{0}: {1}", bestVal, current.path);
                    continue;
                }
                if (current.n >= bestVal)
                {

                    continue;
                }
                for (int d = 0; d < directs.Count; d++)
                {
                    (int x, int y) dir = directs[d];
                    (int x, int y) newPos = ((current.x + dir.x), current.y + dir.y);
                    if (!origGrid.ContainsKey(newPos))
                    {
                        continue;
                    }
                    if (origGrid[newPos] != '.')
                    {
                        continue;
                    }
                    if (grids[current.n + 1].Contains((newPos.x, newPos.y)))
                    {
                        continue;
                    }
                    if (seen.Contains((current.n + 1, newPos.x, newPos.y)))
                    {
                        continue;
                    }
                    seen.Add((current.n + 1, newPos.x, newPos.y));


                    Q.Enqueue((current.n + 1, newPos.x, newPos.y, current.path + d.ToString()));
                }
            }


            return bestVal.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            int yMax = lines.Length - 1;
            int xMax = lines[0].Length - 2 - 1;
            HashSet<(int x, int y)> blizzard = new HashSet<(int, int)>();
            HashSet<(int x, int y, char dir)> blizzardAnDir = new HashSet<(int, int, char)>();
            Dictionary<(int, int), char> origGrid = new Dictionary<(int, int), char>();
            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    origGrid[(x, y)] = lines[y][x + 1];
                    if (lines[y][x + 1] != '.' && lines[y][x + 1] != '#')
                    {
                        origGrid[(x, y)] = '.';
                        blizzard.Add((x, y));
                        blizzardAnDir.Add((x, y, lines[y][x + 1]));
                    }
                }
            }

            (int x, int y) start = (0, 0);
            (int x, int y) end = (0, 0);
            for (int x = 0; x <= xMax; x++)
            {
                if (lines[0][x + 1] != '#')
                {
                    start = (x, 0);
                }
                if (lines[yMax][x + 1] != '#')
                {
                    end = (x, yMax);
                }
            }

            List<(int, int)> directs = new List<(int, int)>() { (0, 0), (0, -1), (1, 0), (0, 1), (-1, 0) };
            Dictionary<int, HashSet<(int, int)>> grids = new Dictionary<int, HashSet<(int, int)>>();
            grids[0] = blizzard;
            for (int i = 0; i < 10000; i++)
            {
                HashSet<(int, int)> gridBlizzards = new HashSet<(int, int)>();
                HashSet<(int x, int y, char dir)> newBlizzardAnDir = new HashSet<(int, int, char)>();
                foreach ((int x, int y, char dir) bliz in blizzardAnDir)
                {

                    switch (bliz.dir)
                    {
                        case '^':
                            newBlizzardAnDir.Add((bliz.x, (bliz.y - 1 - 1 + (yMax - 1)) % (yMax - 1) + 1, bliz.dir));
                            gridBlizzards.Add((bliz.x, (bliz.y - 1 - 1 + (yMax - 1)) % (yMax - 1) + 1));
                            break;
                        case '>':
                            newBlizzardAnDir.Add(((bliz.x + 1 + (xMax + 1)) % (xMax + 1), bliz.y, bliz.dir));
                            gridBlizzards.Add(((bliz.x + 1 + (xMax + 1)) % (xMax + 1), bliz.y));
                            break;
                        case 'v':
                            newBlizzardAnDir.Add((bliz.x, (bliz.y + 1 - 1 + (yMax - 1)) % (yMax - 1) + 1, bliz.dir));
                            gridBlizzards.Add((bliz.x, (bliz.y + 1 - 1 + (yMax - 1)) % (yMax - 1) + 1));
                            break;
                        case '<':
                            newBlizzardAnDir.Add(((bliz.x - 1 + (xMax + 1)) % (xMax + 1), bliz.y, bliz.dir));
                            gridBlizzards.Add(((bliz.x - 1 + (xMax + 1)) % (xMax + 1), bliz.y));
                            break;

                        default:
                            break;
                    }

                }
                grids[i + 1] = gridBlizzards;
                blizzardAnDir = newBlizzardAnDir;
            }



            HashSet<(int, int, int, bool, bool)> seen = new HashSet<(int, int, int, bool, bool)>();
            Dictionary<(int, int), int> best = new Dictionary<(int, int), int>();
            Queue<(int n, int x, int y, bool endReached, bool startReached, string path)> Q = new Queue<(int, int, int, bool, bool, string)>();
            Q.Enqueue((0, start.x, start.y, false, false, ""));
            int bestVal = int.MaxValue;
            while (Q.Count > 0)
            {
                (int n, int x, int y, bool endReached, bool startReached, string path) current = Q.Dequeue();
                if (current.x == end.x && current.y == end.y && current.startReached)
                {
                    bestVal = Math.Min(bestVal, current.n);
                    Console.WriteLine("{0}: {1}", bestVal, current.path);
                    continue;
                }
                if (current.n >= bestVal)
                {
                    continue;
                }
                if (current.x == end.x && current.y == end.y && !current.startReached)
                {
                    current.endReached = true;
                }
                if (current.x == start.x && current.y == start.y && current.endReached)
                {
                    current.startReached = true;
                }
                for (int d = 0; d < directs.Count; d++)
                {
                    (int x, int y) dir = directs[d];
                    (int x, int y) newPos = ((current.x + dir.x), current.y + dir.y);
                    if (!origGrid.ContainsKey(newPos))
                    {
                        continue;
                    }
                    if (origGrid[newPos] != '.')
                    {
                        continue;
                    }
                    if (grids[current.n + 1].Contains((newPos.x, newPos.y)))
                    {
                        continue;
                    }
                    if (seen.Contains((current.n + 1, newPos.x, newPos.y, current.endReached, current.startReached)))
                    {
                        continue;
                    }
                    seen.Add((current.n + 1, newPos.x, newPos.y, current.endReached, current.startReached));


                    //Q.Enqueue((current.n + 1, newPos.x, newPos.y, current.endReached, current.startReached, current.path + d.ToString()));
                    Q.Enqueue((current.n + 1, newPos.x, newPos.y, current.endReached, current.startReached, ""));
                }
            }


            return bestVal.ToString();
        }
    }
}