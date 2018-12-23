using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;

namespace A11
{
    public class BinaryTreeTraversals : Processor
    {
        public BinaryTreeTraversals(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);

        List<Node> tree;

        public long[][] Solve(long[][] nodes)
        {
            tree = new List<Node>();
            for (int i = 0; i < nodes.Length; i++)
            {
                tree.Add(new Node(-1, -1, -1));
                tree[i].Parent = -1;
            }
            foreach (var n in nodes)
            {
                tree.Add(new Node(n[0], n[1], n[2]));
                tree[(int)n[1]].Parent = n[0];
                tree[(int)n[2]].Parent = n[0];
            }
            long[][] result = new long[3][];
            result[0] = InOrder();
            return result;
            
        }

        public long[] InOrder()
        {
            Stack<Node> inOrder = new Stack<Node>();
            List<long> result = new List<long>();
            inOrder.Push(tree[0]);
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
                    inOrder.Push(tree[(int)node.Right]);
                    inOrder.Push(node);
                    inOrder.Push(tree[(int)node.Left]);
                }
            }
            return result.ToArray();
        }

    }
}
