using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

namespace AdventOfCode.Solutions.Year2022
{

    class Day07 : ASolution
    {

        public Day07() : base(07, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            Node rootNode = new Node();
            Node curNode = rootNode;
            curNode.NodeDesc = "/";

            foreach (string line in lines)
            {
                if (line.Equals("$ cd /"))
                {
                    continue;
                }

                string[] comp = line.Split(" ");
                if (comp[0].Equals("dir"))
                {
                    continue;
                }
                if (comp[1].Equals("ls"))
                {
                    continue;
                }
                if (comp[1].Equals("cd"))
                {
                    if (comp[2].Equals(".."))
                    {
                        curNode = curNode.ParentNodes[0];
                    }
                    else
                    {
                        Node newNode = new Node();
                        newNode.NodeDesc = comp[2];
                        newNode.ParentNodes.Add(curNode);
                        bool alreadyFound = false;
                        foreach (Node childNode in curNode.ChildNodes)
                        {
                            if (childNode.NodeDesc.Equals(comp[2]))
                            {
                                newNode = childNode;
                                alreadyFound = true;
                                break;
                            }
                        }
                        if (!alreadyFound)
                        {
                            curNode.ChildNodes.Add(newNode);
                        }
                        curNode = newNode;
                    }
                }

                if (comp.Count() == 2)
                {
                    curNode.Value += int.Parse(comp[0]);
                }
            }


            CalculateDirectValues(rootNode);
            long ans = CalculateOver100000(rootNode);

            return ans.ToString();
        }


        public void CalculateDirectValues(Node nde)
        {
            long total = 0;
            foreach (Node chld in nde.ChildNodes)
            {
                CalculateDirectValues(chld);
                total += chld.Value;
            }
            nde.Value += total;
        }

        public long CalculateOver100000(Node nde)
        {
            long total = 0;
            foreach (Node chld in nde.ChildNodes)
            {
                total += CalculateOver100000(chld);
            }
            if (nde.Value <= 100000)
            {
                total += nde.Value;
            }
            return total;
        }

        public List<long> CalculateDeleteNodeValue(Node nde, long spaceNeeded)
        {
            List<long> deleteable = new List<long>();
            foreach (Node chld in nde.ChildNodes)
            {
                deleteable = deleteable.Concat(CalculateDeleteNodeValue(chld, spaceNeeded)).ToList();
            }
            if (nde.Value >= spaceNeeded)
            {
                deleteable.Add(nde.Value);
            }
            return new List<long>(deleteable);
        }


        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            Node rootNode = new Node();
            Node curNode = rootNode;
            curNode.NodeDesc = "/";

            foreach (string line in lines)
            {
                if (line.Equals("$ cd /"))
                {
                    continue;
                }

                string[] comp = line.Split(" ");
                if (comp[0].Equals("dir"))
                {
                    continue;
                }
                if (comp[1].Equals("ls"))
                {
                    continue;
                }
                if (comp[1].Equals("cd"))
                {
                    if (comp[2].Equals(".."))
                    {
                        curNode = curNode.ParentNodes[0];
                    }
                    else
                    {
                        Node newNode = new Node();
                        newNode.NodeDesc = comp[2];
                        newNode.ParentNodes.Add(curNode);
                        bool alreadyFound = false;
                        foreach (Node childNode in curNode.ChildNodes)
                        {
                            if (childNode.NodeDesc.Equals(comp[2]))
                            {
                                newNode = childNode;
                                alreadyFound = true;
                                break;
                            }
                        }
                        if (!alreadyFound)
                        {
                            curNode.ChildNodes.Add(newNode);
                        }
                        curNode = newNode;
                    }
                }

                if (comp.Count() == 2)
                {
                    curNode.Value += int.Parse(comp[0]);
                }
            }


            CalculateDirectValues(rootNode);

            long spaceFree = 70000000 - rootNode.Value;
            long spaceNeeded = 30000000 - spaceFree;
            List<long> viableOptions = CalculateDeleteNodeValue(rootNode, spaceNeeded);

            long ans = viableOptions.Min();
            return ans.ToString();
        }
    }


}