using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class PartitioningSouvenirs : Processor
    {
        public PartitioningSouvenirs(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long souvenirsCount, long[] souvenirs)
        {
            long souvenirsSum = souvenirs.Sum();
            if (souvenirsSum == 0 || (souvenirsSum % 3) != 0)
                return 0;
            long part1 = souvenirsSum / 3;
            long part2 = part1 * 2;
            bool[,] table = new bool[souvenirsCount + 1, part2 + 1];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0] = true;
            }
            for (int j = 1; j < table.GetLength(1); j++)
            {
                table[0, j] = false;
            }
            souvenirs = souvenirs.OrderByDescending(x => x).ToArray();
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = table[i - 1, j];
                    if (j >= souvenirs[i - 1])
                        table[i, j] = table[i - 1, j] || table[i - 1, j - souvenirs[i - 1]];
                }
            }
            if (table[souvenirsCount, part2] && table[souvenirsCount, part1])
                return 1;
            return 0;
        }
    }
}
