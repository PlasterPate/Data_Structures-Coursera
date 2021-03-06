﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace E2
{

    public class Q1LinkedList
    {
        public class Node
        {
            public Node(int key) { this.Key = key;  }
            public int Key;
            public Node Next = null;
            public Node Prev = null;
            public override string ToString() => ToString(4);

            public string ToString(int maxDepth)
            {
                return maxDepth == 1 || Next == null ?
                    $"{Key.ToString()}" + (Next != null ? "..." : string.Empty) :
                    $"{Key.ToString()} {Next.ToString(maxDepth - 1)}";
            }
        }

        private Node Head = null;
        private Node Tail = null;

        public void Insert(int key)
        {
            if (Head == null)
            {
                Head = Tail = new Node(key);
            }
            else
            {
                var newNode = new Node(key);
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
        }

        public override string ToString() => Head.ToString();

        public void Reverse()
        {
            if(Head == Tail)
            {
                return;
            }
            else
            {
                var headTemp = Head;
                var next = Head.Next;
                    Head = next;
                    Head.Prev = null;
                Reverse();
                Insert(headTemp.Key);
            }
        }

        public void DeepReverse()
        {
            while (true)
            {
                var temp = Head.Key;
                Head.Key = Tail.Key;
                Tail.Key = temp;
                Head = Head.Next;
                Tail = Tail.Prev;
                if(Head.Key == Tail.Next.Key && Head.Prev.Key == Tail.Key && Head.Prev.Prev.Key == Tail.Prev.Key && Tail.Next.Next == Head.Next)
                {
                    break;
                }
                else if(Head.Prev.Key == Tail.Next.Key && Tail.Next.Next.Key == Head.Key && Head.Prev.Prev.Key == Tail.Key)
                {
                    break;
                }
            }
            while (Head.Prev != null)
                Head = Head.Prev;
            while (Tail.Next != null)
                Tail = Tail.Next;
        }

        public IEnumerable<int> GetForwardEnumerator()
        {
            var it = this.Head;
            while (it != null)
            {
                yield return it.Key;
                it = it.Next;
            }
        }

        public IEnumerable<int> GetReverseEnumerator()
        {
            var it = this.Tail;
            while (it != null)
            {
                yield return it.Key;
                it = it.Prev;
            }
        }
    }
}