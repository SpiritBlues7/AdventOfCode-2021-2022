using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AdventOfCode.Solutions.Year2022
{

    class Day19 : ASolution
    {

        public Day19() : base(19, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            var ints = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                ints.Add(i);
            }
            ConcurrentBag<int> resultCollection = new ConcurrentBag<int>();
            Parallel.ForEach(ints, (i) =>
            {

                string[] comp = lines[i].Split(' ');
                int blueprint = int.Parse(comp[1].Replace(":", ""));
                int oreRobotCost = int.Parse(comp[6]);
                int clayRobotCost = int.Parse(comp[12]);
                int obsRobotCostOre = int.Parse(comp[18]);
                int obsRobotCostClay = int.Parse(comp[21]);
                int geodeRobotCostOre = int.Parse(comp[27]);
                int geodeRobotCostObs = int.Parse(comp[30]);

                int maxOreAmountNeeded = new List<int>() { oreRobotCost, clayRobotCost, obsRobotCostOre, geodeRobotCostOre }.Max();
                int maxClayAmountNeeded = obsRobotCostClay;
                int maxObsAmountNeeded = geodeRobotCostObs;
                Dictionary<(int, int, int, int), int> bestGeodes = new Dictionary<(int, int, int, int), int>();
                Dictionary<int, int> bestGeodeAtTime = new Dictionary<int, int>();
                NodeTest2 start = new NodeTest2();

                for (int temp = 0; temp <= 32; temp++)
                {
                    bestGeodeAtTime[temp] = 0;
                }

                Queue<NodeTest2> queue = new Queue<NodeTest2>();
                queue.Enqueue(start);
                HashSet<(int, int, int, int, int, int, int, int, int)> seen = new HashSet<(int, int, int, int, int, int, int, int, int)>();
                int bestGeode = 0;
                int maxTime = 0;
                long startTime = DateTime.Now.Ticks;
                while (queue.Count > 0)
                {

                    NodeTest2 current = queue.Dequeue();

                    if (current.time >= 24)
                    {
                        if (bestGeode < current.geode)
                        {
                            bestGeode = current.geode;
                            if (bestGeode > 5)
                            {
                                //Console.WriteLine("{0} best is currently {1}", i, bestGeode);
                            }

                        }
                        continue;
                    }


                    if (!isBetterOrSame(seen, bestGeodes, current, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                    {
                        continue;
                    }
                    else
                    {
                        seen.Add((current.ore, current.clay, current.obs, current.geode, current.oreRate, current.clayRate, current.obsRate, current.geodeRate, current.time));
                        setBetter(bestGeodes, current);
                    }

                    if (current.ore >= oreRobotCost)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= oreRobotCost;
                        increaseAll(newNode);
                        newNode.oreRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("ore", newNode.time));

                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    if (current.ore >= clayRobotCost)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= clayRobotCost;
                        increaseAll(newNode);
                        newNode.clayRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("clay", newNode.time));
                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    if (current.ore >= obsRobotCostOre && current.clay >= obsRobotCostClay)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= obsRobotCostOre;
                        newNode.clay -= obsRobotCostClay;
                        increaseAll(newNode);
                        newNode.obsRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("obs", newNode.time));
                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    if (current.ore >= geodeRobotCostOre && current.obs >= geodeRobotCostObs)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= geodeRobotCostOre;
                        newNode.obs -= geodeRobotCostObs;
                        increaseAll(newNode);
                        newNode.geodeRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("geode", newNode.time));
                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    increaseAll(current);
                    current.time += 1;
                    //current.steps.Add(("", current.time));
                    if (isBetterOrSame(seen, bestGeodes, current, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                    {
                        queue.Enqueue(current);
                        setBetter(bestGeodes, current);
                    }

                }

                double secondsElapsed = new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds;
                Console.WriteLine("{0} is {1}, time elapsed: {2}", blueprint, bestGeode, secondsElapsed);
                resultCollection.Add(bestGeode * blueprint);
            });


            int total = 0;
            foreach (var result in resultCollection)
            {
                total += result;
            }
            return total.ToString();
        }

        private static void increaseAll(NodeTest2 current)
        {
            current.ore += current.oreRate;
            current.clay += current.clayRate;
            current.obs += current.obsRate;
            current.geode += current.geodeRate;
            current.totalOre += current.oreRate;
            current.totalClay += current.clayRate;
            current.totalObs += current.obsRate;
            current.totalGeode += current.geodeRate;

        }

        private static bool isBetterOrSame(HashSet<(int,int,int,int,int,int,int,int,int)> seen, Dictionary<(int, int, int, int), int> bestGeodes,
                    NodeTest2 newNode,
                    Dictionary<int, int> bestGeoAtTime, 
                    int maxOreAmountNeeded, int maxClayAmountNeeded, int maxObsAmountNeeded)
        {
            if (seen.Contains((newNode.ore, newNode.clay, newNode.obs, newNode.geode, newNode.oreRate, newNode.clayRate, newNode.obsRate, newNode.geodeRate, newNode.time)))
            {
                return false;
            }
            if (newNode.oreRate > maxOreAmountNeeded)
            {
                return false;
            }            
            if (newNode.clayRate > maxClayAmountNeeded)
            {
                return false;
            }            
            if (newNode.obsRate > maxObsAmountNeeded)
            {
                return false;
            }
            if (bestGeoAtTime[newNode.time] - 3 > newNode.totalGeode)
            {
                return false;
            }
            if (bestGeoAtTime[newNode.time] < newNode.totalGeode)
            {
                bestGeoAtTime[newNode.time] = newNode.totalGeode;
            }

            for (int i = newNode.totalGeode + 2; i >= newNode.totalGeode; i--)
            {
                for (int j = newNode.totalObs + 2; j >= newNode.totalObs; j--)
                {
                    for (int k = newNode.totalClay + 2; k >= newNode.totalClay; k--)
                    {
                        for (int l = newNode.totalOre + 2; l >= newNode.totalOre; l--)
                        {
                            if (bestGeodes.ContainsKey((l, k, j, i)))
                            {
                                if (bestGeodes[(l, k, j, i)] < newNode.time)
                                {
                                    return false;
                                }
                            }

                        }
                    }
                }
            }


            return true;
        }

        private static void setBetter(Dictionary<(int, int, int, int), int> bestGeodes, NodeTest2 newNode)
        {
            bestGeodes[(newNode.totalOre, newNode.totalClay, newNode.totalObs, newNode.totalGeode)] = newNode.time;
        }


        private static NodeTest2 copyNode(NodeTest2 current)
        {
            NodeTest2 newNode = new NodeTest2();
            newNode.ore = current.ore;
            newNode.clay = current.clay;
            newNode.obs = current.obs;
            newNode.geode = current.geode;
            newNode.totalOre = current.totalOre;
            newNode.totalClay = current.totalClay;
            newNode.totalObs = current.totalObs;
            newNode.totalGeode = current.totalGeode;
            newNode.time = current.time;
            newNode.oreRate = current.oreRate;
            newNode.clayRate = current.clayRate;
            newNode.obsRate = current.obsRate;
            newNode.geodeRate = current.geodeRate;
            //newNode.steps = new List<(string, int)>(current.steps);
            return newNode;
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            var ints = new List<int>();
            for (int i = 0; i < Math.Min(3, lines.Length); i++)
            {
                ints.Add(i);
            }
            ConcurrentBag<int> resultCollection = new ConcurrentBag<int>();
            Parallel.ForEach(ints, (i) =>
            {

                string[] comp = lines[i].Split(' ');
                int blueprint = int.Parse(comp[1].Replace(":", ""));
                int oreRobotCost = int.Parse(comp[6]);
                int clayRobotCost = int.Parse(comp[12]);
                int obsRobotCostOre = int.Parse(comp[18]);
                int obsRobotCostClay = int.Parse(comp[21]);
                int geodeRobotCostOre = int.Parse(comp[27]);
                int geodeRobotCostObs = int.Parse(comp[30]);

                int maxOreAmountNeeded = new List<int>() { oreRobotCost, clayRobotCost, obsRobotCostOre, geodeRobotCostOre }.Max();
                int maxClayAmountNeeded = obsRobotCostClay;
                int maxObsAmountNeeded = geodeRobotCostObs;
                Dictionary<(int, int, int, int), int> bestGeodes = new Dictionary<(int, int, int, int), int>();
                Dictionary<int, int> bestGeodeAtTime = new Dictionary<int, int>();
                NodeTest2 start = new NodeTest2();

                for (int temp = 0; temp <= 32; temp++)
                {
                    bestGeodeAtTime[temp] = 0;
                }

                Queue<NodeTest2> queue = new Queue<NodeTest2>();
                queue.Enqueue(start);
                HashSet<(int, int, int, int, int, int, int, int, int)> seen = new HashSet<(int, int, int, int, int, int, int, int, int)>();
                int bestGeode = 1;
                int maxTime = 0;
                long startTime = DateTime.Now.Ticks;
                while (queue.Count > 0)
                {

                    NodeTest2 current = queue.Dequeue();

                    if (current.time >= 32)
                    {
                        if (bestGeode < current.geode)
                        {
                            bestGeode = current.geode;
                            if (bestGeode > 5)
                            {
                                //Console.WriteLine("{0} best is currently {1}", i, bestGeode);
                            }
                            
                        }
                        continue;
                    }


                    if (!isBetterOrSame(seen, bestGeodes, current, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                    {
                        continue;
                    }
                    else
                    {
                        seen.Add((current.ore, current.clay, current.obs, current.geode, current.oreRate, current.clayRate, current.obsRate, current.geodeRate, current.time));
                        setBetter(bestGeodes, current);
                    }

                    if (current.ore >= oreRobotCost)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= oreRobotCost;
                        increaseAll(newNode);
                        newNode.oreRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("ore", newNode.time));

                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    if (current.ore >= clayRobotCost)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= clayRobotCost;
                        increaseAll(newNode);
                        newNode.clayRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("clay", newNode.time));
                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    if (current.ore >= obsRobotCostOre && current.clay >= obsRobotCostClay)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= obsRobotCostOre;
                        newNode.clay -= obsRobotCostClay;
                        increaseAll(newNode);
                        newNode.obsRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("obs", newNode.time));
                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    if (current.ore >= geodeRobotCostOre && current.obs >= geodeRobotCostObs)
                    {
                        NodeTest2 newNode = copyNode(current);
                        newNode.ore -= geodeRobotCostOre;
                        newNode.obs -= geodeRobotCostObs;
                        increaseAll(newNode);
                        newNode.geodeRate++;
                        newNode.time += 1;
                        //newNode.steps.Add(("geode", newNode.time));
                        if (isBetterOrSame(seen, bestGeodes, newNode, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                        {
                            queue.Enqueue(newNode);
                            setBetter(bestGeodes, newNode);
                        }
                    }
                    increaseAll(current);
                    current.time += 1;
                    //current.steps.Add(("", current.time));
                    if (isBetterOrSame(seen, bestGeodes, current, bestGeodeAtTime, maxOreAmountNeeded, maxClayAmountNeeded, maxObsAmountNeeded))
                    {
                        queue.Enqueue(current);
                        setBetter(bestGeodes, current);
                    }

                }
                double secondsElapsed = new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds;
                Console.WriteLine("{0} is {1}, time elapsed: {2}", blueprint, bestGeode, secondsElapsed);
                resultCollection.Add(bestGeode);
            });

            int total = 1;
            foreach (var result in resultCollection)
            {
                total *= result;
            }
            

            return total.ToString();
        }

        public class NodeTest2
        {

            public int oreRate {get; set; } = 1;
            public int clayRate {get; set; } = 0;
            public int obsRate {get; set; } = 0;
            public int geodeRate {get; set; } = 0;
            public int totalOre { get; set; } = 0;
            public int totalClay { get; set; } = 0;
            public int totalObs { get; set; } = 0;
            public int totalGeode { get; set; } = 0;
            public int ore {get; set; } = 0;
            public int clay {get; set; } = 0;
            public int obs {get; set; } = 0;
            public int geode { get; set; } = 0;
            public int time { get; set; } = 0;





        }
    }
}