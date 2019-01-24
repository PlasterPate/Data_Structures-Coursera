using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class Tree
    {
        public List<Node> Nodes;

        public Tree(long[][] nodeList)
        {
            Nodes = new List<Node>();
            foreach (var n in nodeList)
            {
                Nodes.Add(new Node(n[0], n[1], n[2]));
            }
        }

        public void Reset()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].UnCheck();
            }
        }

        public long[] InOrder()
        {
            Stack<Node> inOrder = new Stack<Node>();
            List<long> result = new List<long>();
            inOrder.Push(Nodes[0]);
            while (inOrder.Any())
            {
                var node = inOrder.Pop();
                if (node.IsChecked)
                {
                    result.Add(node.Key);
                }
                else
                {
                    node.Check();
                    if (node.Right != -1)
                        inOrder.Push(Nodes[(int)node.Right]);
                    inOrder.Push(node);
                    if (node.Left != -1)
                        inOrder.Push(Nodes[(int)node.Left]);
                }
            }
            return result.ToArray();
        }

        public long[] PreOrder()
        {
            Stack<Node> preOrder = new Stack<Node>();
            List<long> result = new List<long>();
            preOrder.Push(Nodes[0]);
            while (preOrder.Any())
            {
                var node = preOrder.Pop();
                if (node.IsChecked)
                {
                    result.Add(node.Key);
                }
                else
                {
                    node.Check();
                    if (node.Right != -1)
                        preOrder.Push(Nodes[(int)node.Right]);
                    if (node.Left != -1)
                        preOrder.Push(Nodes[(int)node.Left]);
                    preOrder.Push(node);
                }
            }
            return result.ToArray();
        }

        public long[] PostOrder()
        {
            Stack<Node> postOrder = new Stack<Node>();
            List<long> result = new List<long>();
            postOrder.Push(Nodes[0]);
            while (postOrder.Any())
            {
                var node = postOrder.Pop();
                if (node.IsChecked)
                {
                    result.Add(node.Key);
                }
                else
                {
                    node.Check();
                    postOrder.Push(node);
                    if (node.Right != -1)
                        postOrder.Push(Nodes[(int)node.Right]);
                    if (node.Left != -1)
                        postOrder.Push(Nodes[(int)node.Left]);
                }
            }
            return result.ToArray();
        }

        public long Min(Node node)
        {
            while (node.Left != -1)
                node = Nodes[(int)node.Left];
            return node.Key;
        }

        public long Max(Node node)
        {
            while (node.Right != -1)
                node = Nodes[(int)node.Right];
            return node.Key;
        }

        public bool CheckLeftAndRight(Node root)
        {
            long leftMax = root.Left == -1 ? -1 : Max(Nodes[(int)root.Left]);
            long rightMin = root.Right == -1 ? root.Key : Min(Nodes[(int)root.Right]);
            if (leftMax < root.Key && rightMin >= root.Key)
                return true;
            return false;
        }

        public bool IsBst()
        {
            var inOrder = InOrder();
            for (int i = 1; i < inOrder.Length; i++)
            {
                if (inOrder[i - 1] >= inOrder[i])
                    return false;
            }
            return true;
        }

        public bool IsBstHard()
        {
            Stack<Node> inOrder = new Stack<Node>();
            List<long> result = new List<long>();
            inOrder.Push(Nodes[0]);
            while (inOrder.Any())
            {
                var node = inOrder.Pop();
                if (!node.IsChecked)
                {
                    if (!CheckLeftAndRight(node))
                        return false;
                    node.Check();
                    if (node.Right != -1)
                    {
                        inOrder.Push(Nodes[(int)node.Right]);
                        if (Nodes[(int)node.Right].Key < node.Key)
                            return false;
                    }
                    inOrder.Push(node);
                    if (node.Left != -1)
                    {
                        inOrder.Push(Nodes[(int)node.Left]);
                        if (Nodes[(int)node.Left].Key >= node.Key)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
