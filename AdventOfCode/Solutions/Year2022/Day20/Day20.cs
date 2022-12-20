using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2022
{

    class Day20 : ASolution
    {


        public Day20() : base(20, 2022, "")
        {

        }

        protected override string SolvePartOne(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            var llist = new DoublyLinkedList();
            llist.Circular = true;
            var olist = new List<int>();
            var olistNodes = new List<LinkedNode>();
            var zeroNode = new LinkedNode();
            for (int i = 0; i < lines.Length; i++)
            {
                int val = int.Parse(lines[i]);
                llist.AddLast(val);
                olist.Add(val);
                olistNodes.Add(llist.Tail);
                if (val == 0)
                {
                    zeroNode = llist.Tail;
                }
            }
            int count = 0;
            llist.ResetCurrentPos();
            long total = 0;

            for (int i = 0; i < olist.Count(); i++)
            {
                LinkedNode current = llist.RemoveAtCurrent();
                llist.MoveCurrentPos(olist[i]);
                llist.AddAtCurrent(current);
                llist.CurrentPosition = olistNodes[(i + 1) % (olistNodes.Count())];
                //llist.PrintList();
                count++;
            }

            LinkedNode zeroVal = llist.FindValue(zeroNode);
            LinkedNode curr = zeroVal;
            for (int j = 1; j <= 3000; j++)
            {
                curr = curr.Next ?? llist.Head;
                if (j % 1000 == 0)
                {
                    total += curr.Value;
                    Console.WriteLine(curr.Value);
                }

            }

            return total.ToString();
        }

        protected override string SolvePartTwo(string input)
        {
            input = input.Replace("\r\n", "\n");
            string[] lines = input.SplitByNewline();

            var llist = new DoublyLinkedList();
            llist.Circular = true;
            var olist = new List<long>();
            var olistNodes = new List<LinkedNode>();
            var zeroNode = new LinkedNode();
            long multiplier = 811589153;
            for (int i = 0; i < lines.Length; i++)
            {
                long val = long.Parse(lines[i]);
                llist.AddLast(val * multiplier);
                olist.Add(val * multiplier);
                olistNodes.Add(llist.Tail);
                if (val == 0)
                {
                    zeroNode = llist.Tail;
                }
            }
            int count = 0;
            llist.ResetCurrentPos();
            long total = 0;

            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < olist.Count(); i++)
                {
                    LinkedNode current = llist.RemoveAtCurrent();
                    llist.MoveCurrentPos(olist[i]);
                    llist.AddAtCurrent(current);
                    llist.CurrentPosition = olistNodes[(i + 1) % (olistNodes.Count())];
                    //llist.PrintList();
                    count++;
                }
            }

            LinkedNode zeroVal = llist.FindValue(zeroNode);
            LinkedNode curr = zeroVal;
            for (int j = 1; j <= 3000; j++)
            {
                curr = curr.Next ?? llist.Head;
                if (j % 1000 == 0)
                {
                    total += curr.Value;
                    Console.WriteLine(curr.Value);
                }

            }

            return total.ToString();
        }
    }


}