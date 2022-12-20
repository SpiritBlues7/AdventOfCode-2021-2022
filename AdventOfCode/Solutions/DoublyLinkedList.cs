using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace AdventOfCode.Solutions
{
    public class LinkedNode
    {
        public long Value { get; set; }
        public LinkedNode Next { get; set; }
        public LinkedNode Previous { get; set; }
    }

    public class DoublyLinkedList
    {
        public LinkedNode Head { get; set; }
        public LinkedNode Tail { get; set; }
        public LinkedNode CurrentPosition { get; set; }
        public bool Circular { get; set; } = false;

        public int Count { get; set; }

        public void AddFirst(long value)
        {
            var newNode = new LinkedNode { Value = value };
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
                newNode.Next = newNode;
                newNode.Previous = newNode;
            }
            else
            {
                newNode.Next = Head;
                if (Circular)
                {
                    newNode.Previous = Tail;
                }
                Head.Previous = newNode;
                Tail.Next = newNode;
                Head = newNode;
            }
            Count++;
            CurrentPosition = Head;
        }

        public void AddLast(long value)
        {
            var newNode = new LinkedNode { Value = value };
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
                newNode.Next = newNode;
                newNode.Previous = newNode;
            }
            else
            {
                if (Circular)
                {
                    newNode.Next = Head;
                }
                newNode.Previous = Tail;
                Head.Previous = newNode;
                Tail.Next = newNode;
                Tail = newNode;
            }
            Count++;
            CurrentPosition = Tail;
        }

        public void AddAfterNode(LinkedNode newNode, LinkedNode node)
        {
            if (node != Tail || Circular)
            {
                LinkedNode nextNode = node.Next;
                nextNode.Previous = newNode;
                newNode.Next = nextNode; 
            }

            if (Circular && node == Tail)
            {
                Head = newNode;
            }

            node.Next = newNode;
            newNode.Previous = node;
            CurrentPosition = node;

            Count++;
        }

        public void AddBeforeNode(LinkedNode newNode, LinkedNode node)
        {
            if (node != Head || Circular)
            {
                LinkedNode prevNode = node.Previous;
                prevNode.Next = newNode;
                newNode.Previous = prevNode; 
            }

            if (Circular && node == Head)
            {
                Tail = newNode;
            }

            node.Previous = newNode;
            newNode.Next = node;
            CurrentPosition = node;

            Count++;
        }

        public void AddAtCurrent(LinkedNode newNode)
        {
            AddBeforeNode(newNode, CurrentPosition);
        }

        public LinkedNode RemoveFirst()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            LinkedNode removedNode = Head;
            if (Head == Tail)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Head = Head.Next;
                if (Circular)
                {
                    Tail.Next = Head;
                    Head.Previous = Tail;
                } else
                {
                    Head.Previous = null;
                }
            }
            Count--;
            CurrentPosition = Head;
            return removedNode;
        }

        public LinkedNode RemoveLast()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }
            LinkedNode removedNode = Tail;
            if (Head == Tail)
            {
                
                Head = null;
                Tail = null;
            }
            else
            {
                Tail = Tail.Previous;
                if (Circular)
                {
                    Tail.Next = Head;
                    Head.Previous = Tail;
                } else
                {
                    Tail.Next = null;
                }
            }
            Count--;
            CurrentPosition = Head;
            return removedNode;
        }

        public LinkedNode Remove(LinkedNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("The node to remove cannot be null.");
            }

            if (Head == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }

            if (node == Head)
            {
                return RemoveFirst();
            }
            else if (node == Tail)
            {
                return RemoveLast();
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                Count--;
                CurrentPosition = node.Next;
                return node;
            }
        }

        public LinkedNode RemoveAtCurrent()
        {
            return Remove(CurrentPosition);
        }

        public void MoveCurrentPos(long num)
        {
            num = num % Count;
            while (num > 0)
            {
                CurrentPosition = CurrentPosition.Next;
                num--;
            }
            while (num < 0)
            {
                CurrentPosition = CurrentPosition.Previous;
                num++;
            }
        }

        public void ResetCurrentPos()
        {
            CurrentPosition = Head;
        }

        public void PrintList()
        {
            LinkedNode current = Head;
            while (current != Tail)
            {
                Console.Write(current.Value);
                Console.Write(" ");
                current = current.Next;
            }
            Console.Write(current.Value);
            Console.WriteLine();
        }

        public LinkedNode FindValue(LinkedNode val)
        {
            LinkedNode current = Head;
            while (current != null)
            {
                if (current == val)
                {
                    return current;
                }
                if (current == Tail)
                {
                    break;
                }
                current = current.Next;
            }
            return null;
        }
    }


}
