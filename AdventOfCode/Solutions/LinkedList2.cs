using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class LinkedNode
    {
        public int Value;
        public int Depth;
        public LinkedNode Next;
        public LinkedNode Prev;


        public LinkedNode()
        {
            Value = -1;
            Next = null;
        }

        public LinkedNode(int value)
        {
            Value = value;
            Next = null;
        }

        public LinkedNode(int value, int depth) : this(value)
        {
            Depth = depth;
        }
    }

    class LinkedList2
    {
        protected LinkedNode header;
        protected LinkedNode last;
        protected LinkedNode currentPos;

        public LinkedList2()
        {

        }

        public void Add(int element, int depth = -1)
        {
            LinkedNode node = new LinkedNode(element, depth);

            node.Next = null;
            if (last == null)
            {
                header = node;
                node.Prev = null;
                last = node;
            } else
            {
                last.Next = node;
                node.Prev = last;
                last = node;
            }

            currentPos = node;
        }


        public void AddafterNode(int newitem, LinkedNode node)
        {
            LinkedNode newNode = new LinkedNode(newitem);
            LinkedNode nextNode = node.Next;

            node.Next = newNode;
            nextNode.Prev = newNode;
            newNode.Next = nextNode;
            newNode.Prev = node;

            currentPos = nextNode;
        }

        public void ResetHead()
        {
            currentPos = header;
        }

        public LinkedNode GetHead()
        {
            return currentPos;
        }

        public LinkedNode GetLast()
        {
            return last;
        }        
        public void SetLast(LinkedNode node)
        {
            last = node;
        }    
        public void SetHead(LinkedNode node)
        {
            header = node;
        }
    }
}
