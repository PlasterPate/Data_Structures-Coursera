using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;

namespace A11
{
    public class IsItBST : Processor
    {
        public IsItBST(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);

        Tree tree;

        public bool Solve(long[][] nodes)
        {
            tree = new Tree(nodes);
            return tree.IsBst();
        }
    }    
}
