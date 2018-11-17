using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximumGold : Processor
    {
        public MaximumGold(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long W, long[] goldBars)
        {
            long[,] table = new long[goldBars.Length + 1, W + 1];
            for (int i = 0; i < table.GetLength(1); i++)
            {
                table[0, i] = 0;
            }
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if(j < goldBars[i - 1])
                    {
                        table[i, j] = table[i - 1, j];
                    }
                    else
                    {
                        long gold = goldBars[i - 1];
                        table[i, j] = Math.Max(table[i - 1, j], table[i - 1, j - gold] + gold);
                    }
                }
            }
            return table[goldBars.Length, W];
        }
    }
}
