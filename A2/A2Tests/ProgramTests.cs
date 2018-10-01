using Microsoft.VisualStudio.TestTools.UnitTesting;
using A2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace A2.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        
        [TestMethod()]
        [DeploymentItem("TestData", "TestData")]
        public void GradedTest_Correctness()
        {
            TestCommon.TestTools.RunLocalTest(Program.Process);
        }

        [TestMethod(),Timeout(500)]
        [DeploymentItem("TestData", "TestData")]
        public void GradedTest_Performance()
        {
            TestCommon.TestTools.RunLocalTest(Program.Process);
        }

        [TestMethod()]
        public void GradedTest_Stress()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 5000)
            {
                int listSize = new Random().Next(2, 50);
                List<int> numbers = new List<int>();
                for (int i = 0; i < listSize; i++)
                {
                    numbers.Add(new Random().Next(1, 1000));
                }
                Assert.AreEqual(Program.NaiveMaxPairwiseProduct(numbers), Program.FastMaxPairwiseProduct(numbers));
            }

        }
    }
}