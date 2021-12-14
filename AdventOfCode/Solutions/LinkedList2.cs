using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class LinkedNode
    {
        public char Value;
        public LinkedNode Next;
        public LinkedNode Prev;


        public LinkedNode()
        {
            Value = '.';
            Next = null;
        }

        public LinkedNode(char value)
        {
            Value = value;
            Next = null;
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

        public void Add(char element)
        {
            LinkedNode node = new LinkedNode();
            node.Value = element;
            node.Next = null;
            if (last == null)
            {
                header = node;
                last = node;
                node.Prev = null;
            } else
            {
                last.Next = node;
                last = node;
                node.Prev = last;
            }

            currentPos = node;
        }


        public void AddafterNode(char newitem, LinkedNode node)
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
    }
}
