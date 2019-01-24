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

        Tree tree;

        public long[][] Solve(long[][] nodes)
        {
            tree = new Tree(nodes);
            long[][] result = new long[3][];
            result[0] = tree.InOrder();
            tree.Reset();
            result[1] = tree.PreOrder();
            tree.Reset();
            result[2] = tree.PostOrder();
            return result;
        }
    }
}
