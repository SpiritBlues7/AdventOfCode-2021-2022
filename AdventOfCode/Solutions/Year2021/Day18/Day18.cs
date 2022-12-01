using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Solutions.Year2021
{

    class Day18 : ASolution
    {


        public Day18() : base(18, 2021, "")
        {

        }


        private LinkedList2 InitialSetup(string line)
        {
            LinkedList2 nodes = new LinkedList2();


            int depth = 0;
            foreach (char c in line)
            {
                if (c == ',')
                {
                    continue;
                }
                if (c == '[')
                {
                    depth++;
                    continue;
                }

                if (c == ']')
                {
                    depth--;
                    continue;
                }
                int element = c - '0';


                Assert.IsTrue(element >= 0 && element <= 9, "element not in range: " + element);
                Assert.IsTrue(depth <= 5 && depth >= 0, "depth was too deep: " + depth);

                nodes.Add(element, depth);

            }

            System.Diagnostics.Debug.Assert(depth == 0, "depth not zero");

            return nodes;
        }


        private List<LinkedNode> GetSplitsAndExplodes(LinkedList2 nodes)
        {
            List<LinkedNode> explodesAndSplits = new List<LinkedNode>();
            nodes.ResetHead();
            LinkedNode node = nodes.GetHead();
            while (true)
            {
                Assert.IsTrue(node.Value >= 0, "element not in range: " + node.Value);
                Assert.IsTrue(node.Depth <= 5 && node.Depth >= 0, "depth was too deep: " + node.Depth);
                if (node.Depth == 5)
                {

                    explodesAndSplits.Add(node);
                }
                if (node.Value > 9)
                {
                    explodesAndSplits.Add(node);
                }

                if (node.Next == null)
                {
                    break;
                }
                node = node.Next;
            }

            return explodesAndSplits;
        }

        private void Explode(LinkedList2 nodes, LinkedNode nodeOne, LinkedNode nodeTwo, List<LinkedNode> explodesAndSplits, int currentIndex)
        {
            LinkedNode prevNode = nodeOne.Prev;
            LinkedNode nextNode = nodeTwo.Next;
            if (prevNode != null)
            {
                prevNode.Value = prevNode.Value + nodeOne.Value;
            } else
            {
                nodes.SetHead(nodeOne);
            }
            if (nextNode != null)
            {
                nextNode.Value = nextNode.Value + nodeTwo.Value;
                nextNode.Prev = nodeOne;
                
            } else
            {
                nodes.SetLast(nodeOne);
            }

            nodeOne.Value = 0;
            nodeOne.Depth = nodeOne.Depth - 1;
            nodeOne.Next = nextNode;

            nodeTwo.Value = -1;
            nodeTwo.Next = null;
            nodeTwo.Prev = null;


            if (prevNode != null && prevNode.Depth == 5)
            {
                //Console.WriteLine("EXPLODING: [" + prevNode.Value + ", " + nodeOne.Value + "]");
                Explode(nodes, prevNode, nodeOne, explodesAndSplits, currentIndex - 1);
            }
            if (nextNode != null && nextNode.Value > 9)
            {
                //Console.WriteLine("SPLITTING: " + nextNode.Value);
                explodesAndSplits.Insert(currentIndex, nextNode);
            }
            if (prevNode != null && prevNode.Value > 9)
            {
                //Console.WriteLine("SPLITTING: " + prevNode.Value);
                if (currentIndex - 1 >= 0)
                {
                    explodesAndSplits.Insert(currentIndex - 1, prevNode);
                } else
                {
                    explodesAndSplits.Insert(0, prevNode);
                }
                
            }

            if (nextNode != null && nextNode.Depth == 5 && nextNode != null)
            {
                if (nextNode.Next == null)
                {
                    //Console.WriteLine("ERROR");
                } else
                {
                    //Console.WriteLine("EXPLODING: [" + nextNode.Value + ", " + nextNode.Next.Value + "]");
                    Explode(nodes, nextNode, nextNode.Next, explodesAndSplits, currentIndex + 1);

                }
            }

        }

        private void SplitNodes(LinkedList2 nodes, LinkedNode node, List<LinkedNode> explodesAndSplits, int currentIndex)
        {
            LinkedNode nextNode = node.Next;

            int newNodeDepth = node.Depth + 1;
            int newNodeOneValue = node.Value / 2;
            int newNodeTwoValue = (int)Math.Ceiling((Decimal)node.Value / (Decimal)2);
            LinkedNode newNodeTwo = new LinkedNode(newNodeTwoValue, newNodeDepth);
            node.Value = newNodeOneValue;
            node.Depth = newNodeDepth;

            if (nextNode != null)
            {
                nextNode.Prev = newNodeTwo;
            } else
            {
                nodes.SetLast(newNodeTwo);
            }


            node.Next = newNodeTwo;
            newNodeTwo.Prev = node;
            newNodeTwo.Next = nextNode;
            

            if (node.Depth == 5)
            {
                //Console.WriteLine("EXPLODING: [" + node.Value + ", " + newNodeTwo.Value + "]");
                Explode(nodes, node, newNodeTwo, explodesAndSplits, currentIndex);
            }
            if (newNodeTwo.Value > 9)
            {
                explodesAndSplits.Insert(currentIndex, newNodeTwo);
            }
            if (node.Value > 9)
            {
                explodesAndSplits.Insert(currentIndex, node);
            }
        }

        private void SplitsAndExplodes(LinkedList2 nodes, List<LinkedNode> splitsAndExplodes)
        {

            LinkedNode nodeExplodeOne = null;
            for (int i = 0; i < splitsAndExplodes.Count; i++) 
            {
                LinkedNode node = splitsAndExplodes[i];
                if (node.Value == -1)
                {
                    continue;
                }
                Assert.IsTrue(node.Depth <= 5 && node.Depth >= 0, "depth was too deep" + node.Depth);

                // Explode
                if (node.Depth == 5)
                {
                    LinkedNode nodeExplodeTwo;
                    if (nodeExplodeOne == null)
                    {
                        nodeExplodeOne = node;
                        continue;
                    }
                    else
                    {
                        nodeExplodeTwo = node;
                    }
                    //Console.WriteLine("EXPLODING: [" + nodeExplodeOne.Value + ", " + nodeExplodeTwo.Value + "]");
                    Explode(nodes, nodeExplodeOne, nodeExplodeTwo, splitsAndExplodes, i);

                    nodeExplodeOne = null;

                    //PrintNodes(nodes);
                }
            }

            for (int i = 0; i < splitsAndExplodes.Count; i++)
            {
                LinkedNode node = splitsAndExplodes[i];
                if (node.Value == -1)
                {
                    continue;
                }
                Assert.IsTrue(node.Depth <= 5 && node.Depth >= 0, "depth was too deep" + node.Depth);

                if (node.Value > 9)
                {
                    if (nodeExplodeOne == null)
                    {
                        //Console.WriteLine("SPLITTING: " + node.Value);
                        SplitNodes(nodes, node, splitsAndExplodes, i);

                        //PrintNodes(nodes);
                    }
                    i = -1;
                }
                
            }
        }


        private void incrementDepths(LinkedList2 nodes)
        {
            nodes.ResetHead();
            LinkedNode node = nodes.GetHead();
            while (true)
            {
                node.Depth = node.Depth + 1;

                if (node.Next == null)
                {
                    break;
                }
                node = node.Next;
            }
        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();



            var initialNodes = InitialSetup(lines[0]);
            var initialSplitsAndExplodes = GetSplitsAndExplodes(initialNodes);
            SplitsAndExplodes(initialNodes, initialSplitsAndExplodes);



            LinkedList2 currentNodes = initialNodes;
            //PrintNodes(currentNodes);
            for (int i = 0; i < lines.Count(); i++)
            {
                if (i == 0)
                {
                    continue;
                }
                //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@");
                //Console.WriteLine("@@@@@@@@ NEW LINE @@@@@@@@");
                //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@");

                var nextNodes = InitialSetup(lines[i]);



                incrementDepths(currentNodes);
                incrementDepths(nextNodes);


                nextNodes.ResetHead();
                currentNodes.ResetHead();
                currentNodes.GetLast().Next = nextNodes.GetHead();
                nextNodes.GetHead().Prev = currentNodes.GetLast();
                currentNodes.SetLast(nextNodes.GetLast());

                //PrintNodes(currentNodes);
                var nextSplitsAndExplodes = GetSplitsAndExplodes(currentNodes);

                SplitsAndExplodes(currentNodes, nextSplitsAndExplodes);

                //PrintNodes(currentNodes);
            }


            
            long t = 0;

            collapse(currentNodes);


            return currentNodes.GetHead().Value.ToString();
        }

        private static void PrintNodes(LinkedList2 currentNodes)
        {
            Console.WriteLine("#################");

            currentNodes.ResetHead();
            LinkedNode node = currentNodes.GetHead();
            while (true)
            {
                
                for (int i = 0; i < node.Depth; i++)
                {
                    Console.Write(" ");
                }
                Console.Write("Value:" + node.Value + ", Node Depth:" + node.Depth);
                Console.WriteLine();

                if (node.Next == null)
                {
                    break;
                }
                node = node.Next;
            }
            Console.WriteLine("#################");
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();


            long best = 0;
            //PrintNodes(currentNodes);
            for (int i = 0; i < lines.Count(); i++)
            {


                for (int j = 0; j < lines.Count(); j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var initialNodes = InitialSetup(lines[i]);
                    var initialSplitsAndExplodes = GetSplitsAndExplodes(initialNodes);
                    SplitsAndExplodes(initialNodes, initialSplitsAndExplodes);

                    LinkedList2 currentNodes = initialNodes;




                    //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    //Console.WriteLine("@@@@@@@@ NEW LINE @@@@@@@@");
                    //Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@");

                    var nextNodes = InitialSetup(lines[j]);



                    incrementDepths(currentNodes);
                    incrementDepths(nextNodes);


                    nextNodes.ResetHead();
                    currentNodes.ResetHead();
                    currentNodes.GetLast().Next = nextNodes.GetHead();
                    nextNodes.GetHead().Prev = currentNodes.GetLast();
                    currentNodes.SetLast(nextNodes.GetLast());

                    //PrintNodes(currentNodes);
                    var nextSplitsAndExplodes = GetSplitsAndExplodes(currentNodes);

                    SplitsAndExplodes(currentNodes, nextSplitsAndExplodes);

                    //PrintNodes(currentNodes);

                    collapse(currentNodes);

                    if (currentNodes.GetHead().Value > best)
                    {
                        best = currentNodes.GetHead().Value;
                        //Console.WriteLine(best.ToString());
                        //Console.WriteLine(lines[i]);
                        //Console.WriteLine(lines[j]);
                    }
                }
            }


            

            return best.ToString();
        }

        private static void collapse(LinkedList2 currentNodes)
        {
            int depth = 5;
            for (int i = depth; i >= 0; i--)
            {
                LinkedNode firstItem = null;
                currentNodes.ResetHead();
                LinkedNode node = currentNodes.GetHead();
                while (true)
                {
                    if (node.Depth == i)
                    {
                        LinkedNode secondItem = null;
                        if (firstItem == null)
                        {
                            firstItem = node;
                        }
                        else
                        {
                            secondItem = node;

                            LinkedNode nextNode = secondItem.Next;

                            firstItem.Value = firstItem.Value * 3 + secondItem.Value * 2;
                            firstItem.Next = nextNode;
                            firstItem.Depth = firstItem.Depth - 1;

                            if (secondItem == currentNodes.GetLast())
                            {
                                currentNodes.SetLast(secondItem);
                            }
                            firstItem = null;
                        }

                    }


                    if (node.Next == null)
                    {
                        break;
                    }
                    node = node.Next;
                }
            }
        }
    }

}