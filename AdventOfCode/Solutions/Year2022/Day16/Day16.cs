using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Xml.Linq;

namespace AdventOfCode.Solutions.Year2022
{

    class Day16 : ASolution
    {

        public Day16() : base(16, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            Dictionary<string, int> flows = new Dictionary<string, int>();
            Dictionary<string, (int, int)> bestValues = new Dictionary<string, (int, int)>();

            foreach (string line in lines)
            {
                string[] largeComp = line.Replace("valves", "valve").Split(" valve ");
                string[] smallComp = largeComp[0].Split();
                string valve = smallComp[1];
                int flow = int.Parse(smallComp[4].Replace(";", "").Split("=")[1]);
                string[] connectToString = largeComp[1].Replace(",", "").Split();
                flows[valve] = flow;
                connections[valve] = new List<string>();
                foreach (string connectValueString in connectToString)
                {
                    connections[valve].Add(connectValueString);
                    bestValues[connectValueString] = (0, -1);
                }
                bestValues[valve] = (0, -1);
            }

            PriorityQueue<NodeTest, int> queue = new PriorityQueue<NodeTest, int>();
            NodeTest start = new NodeTest();
            start.Minutes = 1;
            start.Name = "AA";
            queue.Enqueue(start, 0);


            HashSet<NodeTest> viewed = new HashSet<NodeTest>();
            int maxVal = 0;
            while (queue.Count > 0)
            {
                NodeTest current = queue.Dequeue();
                string currentValve = current.Name;
                if (bestValues[currentValve].Item1 <= current.Minutes &&
                    bestValues[currentValve].Item2 >= current.Value)
                {
                    continue;
                }

                if (current.Minutes >= 30)
                {
                    if (current.Value > maxVal)
                    {
                        maxVal = current.Value;
                    }
                }

                viewed.Add(current);
                bestValues[currentValve] = (current.Minutes, current.Value);

                if (!current.Opens.ContainsKey(currentValve) || !current.Opens[currentValve])
                {
                    NodeTest newNode = new NodeTest();
                    newNode.Name = currentValve;
                    newNode.Opens = new Dictionary<string, bool>(current.Opens);
                    newNode.Opens[currentValve] = true;
                    newNode.Minutes = current.Minutes + 1;
                    newNode.Value = current.Value + (flows[currentValve] * (30 - current.Minutes));

                    queue.Enqueue(newNode, newNode.Value);
                }

                foreach (string conn in connections[currentValve])
                {
                    NodeTest newNode = new NodeTest();
                    newNode.Name = conn;
                    newNode.Opens = new Dictionary<string, bool>(current.Opens);
                    newNode.Minutes = current.Minutes + 1;
                    newNode.Value = current.Value;

                    queue.Enqueue(newNode, newNode.Value);
                }
            }


            return maxVal.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            long startTime = DateTime.Now.Ticks;
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            Dictionary<string, int> flows = new Dictionary<string, int>();
            Dictionary<(string,string), (int, int)> bestValues = new Dictionary<(string,string), (int, int)>();
            List<string> connects = new List<string>();
            foreach (string line in lines)
            {
                string[] largeComp = line.Replace("valves", "valve").Split(" valve ");
                string[] smallComp = largeComp[0].Split();
                string valve = smallComp[1];
                int flow = int.Parse(smallComp[4].Replace(";", "").Split("=")[1]);
                string[] connectToString = largeComp[1].Replace(",", "").Split();
                flows[valve] = flow;
                connections[valve] = new List<string>();
                foreach (string connectValueString in connectToString)
                {
                    connections[valve].Add(connectValueString);
                    connects.Add(connectValueString);
                }
                connects.Add(valve);
            }

            foreach (string con1 in connects)
            {
                foreach (string con2 in connects)
                {
                    bestValues[(con1, con2)] = (0, -1);
                }
            }

            PriorityQueue<NodeTest, int> queue = new PriorityQueue<NodeTest, int>();
            NodeTest start = new NodeTest();
            start.Minutes = 1;
            start.Name = "AA";
            start.EleName = "AA";

            queue.Enqueue(start, 0);


            HashSet<NodeTest> viewed = new HashSet<NodeTest>();
            int maxVal = 0;
            int maxMin = 0;
            while (queue.Count > 0)
            {
                NodeTest current = queue.Dequeue();

                string currentValve = current.Name;
                string eleValve = current.EleName;

                if (current.Minutes > maxMin)
                {
                    maxMin = current.Minutes;
                    Console.WriteLine(current.Minutes);
                }
               
                if (bestValues[(currentValve, eleValve)].Item1 <= current.Minutes &&
                    bestValues[(currentValve, eleValve)].Item2 >= current.Value + current.EleValue)
                {
                    continue;
                }

                // 216 is the maximum flow per minute
                if (maxVal > (current.Value + current.EleValue) + 216 * (26 - current.Minutes))
                {
                    continue;
                }

                if (current.Minutes >= 26)
                {
                    
                    if (current.Value > maxVal)
                    {
                        maxVal = current.Value;
                        Console.WriteLine(current.Value);
                    }
                    continue;
                }

                
                bestValues[(currentValve, eleValve)] = (current.Minutes, current.Value + current.EleValue);

                List<NodeTest> currentNodes = new List<NodeTest>();
                if ((!current.Opens.ContainsKey(currentValve) || !current.Opens[currentValve]) && flows[currentValve] != 0)
                {
                    NodeTest newNode = new NodeTest();
                    newNode.Name = currentValve;                    
                    newNode.EleName = eleValve;
                    newNode.Opens = new Dictionary<string, bool>(current.Opens);
                    newNode.Opens[currentValve] = true;
                    newNode.Minutes = current.Minutes + 1;
                    newNode.Value = current.Value + (flows[currentValve] * (26 - current.Minutes));

                    if (bestValues[(currentValve, eleValve)].Item1 > newNode.Minutes - 1 ||
                        bestValues[(currentValve, eleValve)].Item2 < newNode.Value + newNode.EleValue)
                    {
                        currentNodes.Add(newNode);
                    }
                    
                }

                foreach (string conn in connections[currentValve])
                {
                    NodeTest newNode = new NodeTest();
                    newNode.Name = conn;
                    newNode.EleName = eleValve;
                    newNode.Opens = new Dictionary<string, bool>(current.Opens);
                    newNode.Minutes = current.Minutes + 1;
                    newNode.Value = current.Value;

                    if (bestValues[(conn, eleValve)].Item1 <= newNode.Minutes - 1 &&
                        bestValues[(conn, eleValve)].Item2 >= newNode.Value + newNode.EleValue)
                    {
                        continue;
                    }
                    currentNodes.Add(newNode);
                }

                foreach (NodeTest n in currentNodes)
                {
                    string currentTempValve = n.Name;

                    if ((!n.Opens.ContainsKey(eleValve) || !n.Opens[eleValve]) && flows[eleValve] != 0)
                    {
                        NodeTest newNode = new NodeTest();
                        newNode.Name = currentTempValve;
                        newNode.EleName = eleValve;
                        newNode.Opens = new Dictionary<string, bool>(n.Opens);
                        newNode.Opens[eleValve] = true;
                        newNode.Minutes = n.Minutes;
                        newNode.Value = n.Value + (flows[eleValve] * (26 - n.Minutes + 1));

                        if (bestValues[(currentTempValve, eleValve)].Item1 > newNode.Minutes - 1 ||
                            bestValues[(currentTempValve, eleValve)].Item2 < newNode.Value + newNode.EleValue)
                        {
                            queue.Enqueue(newNode, newNode.Value);
                        }
                        
                    }

                    foreach (string conn in connections[eleValve])
                    {
                        NodeTest newNode = new NodeTest();
                        newNode.Name = currentTempValve;
                        newNode.EleName = conn;
                        newNode.Opens = new Dictionary<string, bool>(n.Opens);
                        newNode.Minutes = n.Minutes;
                        newNode.Value = n.Value;

                        if (bestValues[(currentTempValve, conn)].Item1 <= newNode.Minutes - 1 &&
                            bestValues[(currentTempValve, conn)].Item2 >= newNode.Value + newNode.EleValue)
                        {
                            continue;
                        }
                        queue.Enqueue(newNode, newNode.Value);
                    }
                }
            }

            double secondsElapsed = new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds;
            Console.WriteLine("SECOND ELAPSED:");
            Console.WriteLine(secondsElapsed);
            return maxVal.ToString();
        }
    }

    public class NodeTest
    {
        public string EleName { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int Value { get; set; } = 0;
        public int EleValue { get; set; } = 0;

        public Dictionary<string, bool> Opens { get; set; } = new Dictionary<string, bool>();


    }
}