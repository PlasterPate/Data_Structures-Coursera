using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class MoneyChange: Processor
    {
        private static readonly int[] COINS = new int[] {1, 3, 4};

        public MoneyChange(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>) Solve);

        public long Solve(long n)
        {
            long[,] table = new long[COINS.Length, n + 1];
            for (int i = 0; i < table.GetLength(1); i++)
            {
                table[0, i] = i;
            }

            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    int coin = COINS[i];
                    if (coin > j)
                        table[i, j] = table[i - 1, j];
                    else
                        table[i, j] = Math.Min(table[i - 1, j], table[i, j-coin] + 1);
                }
            }

            return table[table.GetLength(0) - 1, table.GetLength(1) - 1];
        }
    }
}
