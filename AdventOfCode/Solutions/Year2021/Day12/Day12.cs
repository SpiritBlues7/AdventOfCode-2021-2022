using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{

    class Day12 : ASolution
    {


        public Day12() : base(12, 2021, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            string[] lines = input.SplitByNewline();

            var connections = new Dictionary<string, HashSet<string>>();
            for (int i = 0; i < lines.Length; i++)
            {
                var components = lines[i].Split("-");
                if (!connections.ContainsKey(components[0]))
                {
                    connections[components[0]] = new HashSet<string>();
                }                
                if (!connections.ContainsKey(components[1]))
                {
                    connections[components[1]] = new HashSet<string>();
                }

                connections[components[0]].Add(components[1]);
                connections[components[1]].Add(components[0]);
            }

            var currentCave = "start";
            var paths = new List<List<string>>();
            paths.Add((new List<string>() { currentCave }));
            var queue = new List<string>();
            var finalPaths = new List<List<string>>();
 
            while (paths.Count > 0)
            {

                for (int i = 0; i < paths.Count; i++)
                {
                    foreach (var cave in paths[i])
                    {
                        //Console.Write(cave);
                    }
                    //Console.WriteLine();
                    var pathCopy = new List<string>(paths[i]);
                    paths.RemoveAt(i);
                    i--;

                    foreach (var cave in connections[pathCopy.Last()])
                    {
                        if (cave == "end")
                        {
                            finalPaths.Add(pathCopy);
                            continue;
                        }
                        if (cave != cave.ToUpper())
                        {
                            if (!pathCopy.Contains(cave))
                            {
                                var pathCopy2 = new List<string>(pathCopy);
                                pathCopy2.Add(cave);
                                paths.Add(pathCopy2);
                                i++;
                            }
                        } else
                        {
                            var pathCopy2 = new List<string>(pathCopy);
                            pathCopy2.Add(cave);
                            paths.Add(pathCopy2);
                            i++;
                        }
                    }
                }

            }
            foreach (var path in finalPaths)
            {
                foreach (var cave in path)
                {
                    //Console.Write(cave);
                }
                //Console.WriteLine();
            }


            return finalPaths.Count.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            string[] lines = input.SplitByNewline();

            var connections = new Dictionary<string, HashSet<string>>();
            for (int i = 0; i < lines.Length; i++)
            {
                var components = lines[i].Split("-");
                if (!connections.ContainsKey(components[0]))
                {
                    connections[components[0]] = new HashSet<string>();
                }
                if (!connections.ContainsKey(components[1]))
                {
                    connections[components[1]] = new HashSet<string>();
                }

                connections[components[0]].Add(components[1]);
                connections[components[1]].Add(components[0]);
            }

            var currentCave = "start";
            var paths = new List<(List<string>, bool)>();
            paths.Add((new List<string>() { currentCave }, false));
            var queue = new List<string>();
            var finalPaths = new List<List<string>>();

            while (paths.Count > 0)
            {

                for (int i = 0; i < paths.Count; i++)
                {
                    foreach (var cave in paths[i].Item1)
                    {
                        //Console.Write(cave);
                    }
                    //Console.WriteLine();
                    var pathCopy = (new List<string>(paths[i].Item1), paths[i].Item2);
                    paths.RemoveAt(i);
                    i--;

                    foreach (var cave in connections[pathCopy.Item1.Last()])
                    {   
                        if (cave == "start")
                        {
                            continue;
                        }
                        if (cave == "end")
                        {
                            finalPaths.Add(pathCopy.Item1);
                            continue;
                        }
                        if (cave != cave.ToUpper())
                        {
                            if (!pathCopy.Item1.Contains(cave))
                            {
                                var pathCopy2 = new List<string>(pathCopy.Item1);
                                pathCopy2.Add(cave);
                                paths.Add((pathCopy2, pathCopy.Item2));
                                i++;
                            } else
                            {
                                if (!pathCopy.Item2)
                                {
                                    var pathCopy2 = new List<string>(pathCopy.Item1);
                                    pathCopy2.Add(cave);
                                    paths.Add((pathCopy2, true));
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            var pathCopy2 = new List<string>(pathCopy.Item1);
                            pathCopy2.Add(cave);
                            paths.Add((pathCopy2, pathCopy.Item2));
                            i++;
                        }
                    }
                }

            }
            foreach (var path in finalPaths)
            {
                foreach (var cave in path)
                {
                    //Console.Write(cave);
                }
                //Console.WriteLine();
            }


            return finalPaths.Count.ToString();
        }
    }
}