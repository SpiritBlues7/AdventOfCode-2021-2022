using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace AdventOfCode.Solutions
{
    public class LinkedNodeOld
    {
        public long Value;
        public int Depth;
        public LinkedNodeOld Next;
        public LinkedNodeOld Prev;


        public LinkedNodeOld()
        {
            Value = -1;
            Next = null;
        }

        public LinkedNodeOld(long value)
        {
            Value = value;
            Next = null;
        }

        public LinkedNodeOld(long value, int depth) : this(value)
        {
            Depth = depth;
        }
    }

    class LinkedListOld
    {
        protected LinkedNodeOld header;
        protected LinkedNodeOld last;
        protected LinkedNodeOld first;
        public LinkedNodeOld currentPos { get; set; }


        public void Add(long element, int depth = -1)
        {
            LinkedNodeOld node = new LinkedNodeOld(element, depth);

            node.Next = null;
            if (last == null)
            {
                header = node;
                node.Prev = null;
                last = node;
                first = node;
            }
            else
            {
                last.Next = node;
                node.Prev = last;
                last = node;
            }

            currentPos = node;
        }

        public void AddAtHead(LinkedNodeOld node)
        {
            node.Next = null;
            if (last == null)
            {
                header = node;
                node.Prev = null;
                last = node;
            }
            else
            {
                if (currentPos == first)
                {
                    first = node;
                }
                else
                {
                    currentPos.Prev.Next = node;
                    node.Prev = currentPos.Prev;
                }
                currentPos.Prev = node;
                node.Next = currentPos;
                currentPos = node;
            }

            currentPos = node;
        }

        public void AddafterNode(int newitem, LinkedNodeOld node)
        {
            LinkedNodeOld newNode = new LinkedNodeOld(newitem);
            LinkedNodeOld nextNode = node.Next ?? GetFirst();

            node.Next = newNode;
            nextNode.Prev = newNode;
            newNode.Next = nextNode;
            newNode.Prev = node;

            currentPos = nextNode;
        }

        public LinkedNodeOld RemoveNode()
        {
            LinkedNodeOld thisNode = currentPos;
            LinkedNodeOld nextNode = null;
            LinkedNodeOld prevNode = null;
            if (thisNode == first)
            {
                first = thisNode.Next;
                first.Prev = null;
            }
            else
            {
                prevNode = thisNode.Prev ?? GetLast();
            }
            if (thisNode == last)
            {
                last = thisNode.Prev;
                last.Next = null;
            }
            else
            {
                nextNode = thisNode.Next ?? GetFirst();
            }


            if (nextNode != null && prevNode != null)
            {
                prevNode.Next = nextNode;
                nextNode.Prev = prevNode;
            }

            thisNode.Next = null;
            thisNode.Prev = null;
            currentPos = nextNode ?? first;

            return thisNode;
        }

        public void MovePos(long num, int listLength)
        {
            num = num % (listLength - 1);
            while (num > 0)
            {
                currentPos = currentPos.Next ?? GetFirst();
                num--;
            }
            while (num < 0)
            {
                currentPos = currentPos.Prev ?? GetLast();
                num++;
            }
        }

        public void ResetHead()
        {
            currentPos = header;
        }

        public LinkedNodeOld GetHead()
        {
            return currentPos;
        }

        public LinkedNodeOld GetLast()
        {
            return last;
        }
        public LinkedNodeOld GetFirst()
        {
            return first;
        }
        public void SetLast(LinkedNodeOld node)
        {
            last = node;
        }
        public void SetFirst(LinkedNodeOld node)
        {
            first = node;
        }
        public void SetHead(LinkedNodeOld node)
        {
            header = node;
        }
        public void SetPos(LinkedNodeOld node)
        {
            currentPos = node;
        }
        public void PrintList()
        {
            LinkedNodeOld current = GetFirst();
            while (current != null)
            {
                Console.Write(current.Value);
                Console.Write(" ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public LinkedNodeOld FindValue(LinkedNodeOld val)
        {
            LinkedNodeOld current = GetFirst();
            while (current != null)
            {
                if (current == val)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }
    }

}