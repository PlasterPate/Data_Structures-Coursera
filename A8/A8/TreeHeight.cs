using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class TreeHeight : Processor
    {
        public TreeHeight(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long nodeCount, long[] tree)
        {
            Dictionary<long, Node> nodes = new Dictionary<long, Node>();
            Queue<long> queue = new Queue<long>();
            for (int i = 0; i < nodeCount; i++)
            {
                nodes[i] = new Node(i);
                if (tree[i] == -1)
                {
                    nodes[i].Height = 1;
                    queue.Enqueue(i);
                }
            }
            for (int i = 0; i < nodeCount; i++)
            {
                if (tree[i] == -1)
                    continue;
                nodes[tree[i]].AddChild(i);
            }
            long maxHeight = 1;
            while(queue.Any())
            {
                long parent = queue.Dequeue();
                foreach (var node in nodes[parent].Childs)
                {
                    queue.Enqueue(node);
                    nodes[node].Height = nodes[parent].Height + 1;
                    maxHeight = Math.Max(maxHeight, nodes[node].Height);
                }
            }
            return maxHeight;
        }
    }
}
