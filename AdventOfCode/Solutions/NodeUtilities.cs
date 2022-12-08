using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Node
    {
        public string NodeDesc { get; set; }
        public List<Node> ParentNodes { get; set; } = new List<Node>();
        public List<Node> AllParentNodes { get; set; } = new List<Node>();
        public List<Node> ChildNodes { get; set; } = new List<Node>();
        public List<Node> AllChildNodes { get; set; } = new List<Node>();
        public int ParentDepth { get; set; }
        public int ChildDepth { get; set; }
        public long Value { get; set; } = 0;


    }
    public static class NodeUtilities
    {
        public static void PopulateChildDepth(List<Node> nodeList)
        {
            //populate child depth
            var nextList = new List<Node>();
            var currentList = nodeList.Where(node => node.ParentNodes.Count == 0);
            int depth = 1;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                foreach (var item in currentList)
                {
                    if (item.ChildDepth == 0)
                    {
                        item.ChildDepth = depth;
                    }
                    nextList.AddRange(item.ChildNodes);

                }
                depth++;
                if (nextList.Count > 0)
                {
                    currentList = nextList;
                    nextList = new List<Node>();
                }
                else
                {
                    shouldContinue = false;
                }
            }
        }
        public static void PopulateParentDepth(List<Node> nodeList)
        {
            //populate child depth
            var nextList = new List<Node>();
            var currentList = nodeList.Where(node => node.ChildNodes.Count == 0);
            int depth = 1;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                foreach (var item in currentList)
                {
                    if (item.ParentDepth == 0)
                    {
                        item.ParentDepth = depth;
                    }
                    nextList.AddRange(item.ParentNodes);

                }
                depth++;
                if (nextList.Count > 0)
                {
                    currentList = nextList;
                    nextList = new List<Node>();
                }
                else
                {
                    shouldContinue = false;
                }
            }
        }
        public static List<Node> PopulateNodes(List<(string parent, string child)> nodeList)
        {
            var retval = new List<Node>();
            for (int i = 0; i < nodeList.Count; i++)
            {
                var node1 = nodeList[i].parent;
                var node2 = nodeList[i].child;
                var matchingNode1 = retval.FirstOrDefault(n => n.NodeDesc == node1);
                var matchingNode2 = retval.FirstOrDefault(n => n.NodeDesc == node2);
                if (matchingNode1 == null)
                {
                    matchingNode1 = new Node() { NodeDesc = node1 };
                    retval.Add(matchingNode1);
                }
                if (matchingNode2 == null)
                {
                    matchingNode2 = new Node() { NodeDesc = node2 };
                    retval.Add(matchingNode2);
                }
                
                matchingNode1.ChildNodes.Add(matchingNode2);
                matchingNode1.AllChildNodes.Add(matchingNode2);
                matchingNode1.AllChildNodes.AddRange(matchingNode2.AllChildNodes);
                matchingNode2.ParentNodes.Add(matchingNode1);
                matchingNode2.AllParentNodes.Add(matchingNode1);
                matchingNode2.AllParentNodes.AddRange(matchingNode1.AllParentNodes);
                for (int j = 0; j < matchingNode1.AllParentNodes.Count; j++)
                {
                    matchingNode1.AllParentNodes[j].AllChildNodes.Add(matchingNode2);
                    matchingNode1.AllParentNodes[j].AllChildNodes.AddRange(matchingNode2.AllChildNodes);
                    //     matchingNode2.AllParentNodes.Add(matchingNode1.AllParentNodes[j]);
                }
                for (int j = 0; j < matchingNode2.AllChildNodes.Count; j++)
                {
                    matchingNode2.AllChildNodes[j].AllParentNodes.Add(matchingNode1);
                    matchingNode2.AllChildNodes[j].AllParentNodes.AddRange(matchingNode1.AllParentNodes);
                    // matchingNode1.AllChildNodes.Add(matchingNode2.AllChildNodes[j]);
                }
            }
            PopulateChildDepth(retval);
            PopulateParentDepth(retval);
            return retval;
        }

        public static List<Node> PopulateNodes(List<(string parent, string current, string child)> nodeList)
        {
            var collapsedNodeList = new List<(string parent, string child)>();
            collapsedNodeList.AddRange(nodeList.Where(n => !string.IsNullOrWhiteSpace(n.parent)).Select(n => (n.parent, n.current)));
            collapsedNodeList.AddRange(nodeList.Where(n => !string.IsNullOrWhiteSpace(n.child)).Select(n => (n.current, n.child)));

            collapsedNodeList = collapsedNodeList.Distinct().ToList();

            return PopulateNodes(collapsedNodeList);
        }

        public static int CalculateTotalLinks(List<Node> nodeList)
        {
            int sum = 0;
            foreach (var node in nodeList)
            {
                sum += node.ChildNodes.Count;
            }
            return sum;
        }

        public static int CalculateAllChildLinks(List<Node> nodeList)
        {
            int sum = 0;
            foreach (var node in nodeList)
            {
                sum += node.AllChildNodes.Count;
            }
            return sum;
        }
        public static int CalculateAllParentLinks(List<Node> nodeList)
        {
            int sum = 0;
            foreach (var node in nodeList)
            {
                sum += node.AllParentNodes.Count;
            }
            return sum;
        }


        public static List<(int, string)> CalculateDistanceBetweenNodes_Parent(List<Node> nodeList, string node1Desc, string node2Desc)
        {
            var distances = new List<(int, string)>();
            foreach (var node in nodeList)
            {
                if ((node.NodeDesc == node1Desc || node.AllChildNodes.Any(c => c.NodeDesc == node1Desc))
                && (node.NodeDesc == node2Desc || node.AllChildNodes.Any(c => c.NodeDesc == node2Desc)))
                {
                    //parent to both
                    var distancesTo1 = new List<int>();
                    var distancesTo2 = new List<int>();
                    if (node.NodeDesc == node1Desc)
                    {
                        distancesTo1.Add(0);
                    }
                    if (node.NodeDesc == node2Desc)
                    {
                        distancesTo2.Add(0);
                    }
                    var nextList = new List<Node>();
                    var currentList = node.ChildNodes;
                    int depth = 1;
                    bool shouldContinue = true;
                    while (shouldContinue)
                    {
                        foreach (var item in currentList)
                        {
                            if (item.NodeDesc == node1Desc)
                            {
                                distancesTo1.Add(depth);
                            }
                            if (item.NodeDesc == node2Desc)
                            {
                                distancesTo2.Add(depth);
                            }
                            nextList.AddRange(item.ChildNodes);
                        }
                        depth++;
                        if (nextList.Count > 0)
                        {
                            currentList = nextList;
                            nextList = new List<Node>();
                        }
                        else
                        {
                            shouldContinue = false;
                        }
                    }
                    foreach (var distance1 in distancesTo1)
                    {
                        foreach (var distance2 in distancesTo2)
                        {
                            distances.Add((distance1 + distance2, node.NodeDesc));
                        }
                    }
                }
            }
            return distances.OrderBy(k => k.Item1).ToList();
        }

    }
}
