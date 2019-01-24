using Microsoft.VisualStudio.TestTools.UnitTesting;
using A11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(10000)]
        [DeploymentItem("TestData", "A11_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new BinaryTreeTraversals("TD1"),
                new IsItBST("TD2"),
                new IsItBSTHard("TD3"),
                //new SetWithRageSums("TD4"), TD4_NOT_SOLVED
                //new Rope("TD5") TD5_NOT_SOLVED
            };

            foreach (var p in problems)
            {
                TestTools.RunLocalTest("A11", p.Process, p.TestDataName);
            }
        }
    }
}