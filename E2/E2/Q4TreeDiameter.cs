using System;
using System.Collections.Generic;
using System.Linq;

namespace E2
{
    public class Q4TreeDiameter
    {
        /// <summary>
        /// ریشه همیشه نود صفر است.
        ///توی این آرایه در مکان صفر لیستی از بچه های ریشه موجودند.
        ///و در مکانه آی از این آرایه لیست بچه های نود آیم هستند
        ///اگر لیست خالی بود، بچه ندارد
        /// </summary>
        public List<int>[] Nodes;

        public List<Node> Tree;

        public Q4TreeDiameter(int nodeCount, int seed = 0)
        {
            Nodes = GenerateRandomTree(size: nodeCount, seed: seed);
            BuildTree(Nodes, nodeCount);
        }

        public void BuildTree(List<int>[] nodes, int nodeCount)
        {
            Tree = new List<Node>();
            for (int i = 0; i < nodeCount; i++)
            {
                Tree.Add(new Node(i));
                Tree[i].connecteds.AddRange(Nodes[i]);
            }
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < Nodes[i].Count; j++)
                {
                    Tree[Nodes[i][j]].connecteds.Add(i);
                }
            }
        }

        public int TreeHeight()
        {
            return TreeHeightFromNode(0).Item1;
        }

        public (int, int) TreeHeightFromNode(int node)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Tree[node]);
            Tree[node].isChecked = true;
            Tree[node].height = 0;
            int maxHeight = 0;
            Node temp = new Node(-1);
            while (queue.Any())
            {
                temp = queue.Dequeue();
                foreach(var c in temp.connecteds)
                {
                    if (!Tree[c].isChecked)
                    {
                        Tree[c].isChecked = true;
                        Tree[c].height = temp.height + 1;
                        maxHeight = Math.Max(maxHeight, Tree[c].height);
                        queue.Enqueue(Tree[c]);
                    }
                }
            }
            return (maxHeight, temp.key);
        }

        public int TreeDiameterN2()
        {
            return TreeDiameterN();
        }

        public int TreeDiameterN()
        {
            int nextRoot = TreeHeightFromNode(0).Item2;
            for (int i = 0; i < Tree.Count; i++)
            {
                Tree[i].isChecked = false;
            }
            return TreeHeightFromNode(nextRoot).Item1;
        }

        private static List<int>[] GenerateRandomTree(int size, int seed)
        {
            Random rnd = new Random(seed);
            List<int>[] nodes = Enumerable.Range(0, size)
                .Select(n => new List<int>())
                .ToArray();
            
            List<int> orphans = 
                new List<int>(Enumerable.Range(1, size-1)); // 0 is root it will remain orphan
            Queue<int> parentsQ = new Queue<int>();
            parentsQ.Enqueue(0);
            while (orphans.Count > 0)
            {
                int parent = parentsQ.Dequeue();
                int childCount = rnd.Next(1, 4);
                for (int i=0; i< Math.Min(childCount, orphans.Count); i++)
                {
                    int orphanIdx = rnd.Next(0, orphans.Count-1);
                    int orphan = orphans[orphanIdx];
                    orphans.RemoveAt(orphanIdx);
                    nodes[parent].Add(orphan);
                    parentsQ.Enqueue(orphan);
                }
            }
            return nodes;
        }
    }

    public class Node
    {
        public int key;
        public int height;
        public List<int> connecteds;
        public bool isChecked;

        public Node(int key)
        {
            this.key = key;
            connecteds = new List<int>();
            isChecked = false;
            height = -1;
        }
    }
}