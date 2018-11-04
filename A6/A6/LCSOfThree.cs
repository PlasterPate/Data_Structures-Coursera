using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class LCSOfThree: Processor
    {
        public LCSOfThree(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2, long[] seq3)
        {
            int[,,] table = new int[seq1.Length + 1, seq2.Length + 1, seq3.Length + 1];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0, 0] = 0;
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                table[0, j, 0] = 0;
            }
            for (int k = 0; k < table.GetLength(2); k++)
            {
                table[0, 0, k] = 0;
            }
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    for (int k = 1; k < table.GetLength(2); k++)
                    {
                        if (seq2[j - 1] == seq1[i - 1] && seq3[k-1] == seq1[i-1])
                            table[i, j, k] = table[i - 1, j - 1, k-1] + 1;
                        else
                            table[i, j, k] = Max(table[i - 1, j - 1, k-1],
                                                 table[i - 1, j, k],
                                                 table[i, j - 1, k],
                                                 table[i, j, k-1]);
                        }
                }
            }
            return table[seq1.Length, seq2.Length, seq3.Length];
        }

        private static int Max(params int[] numbers)
        {
            return numbers.Max();
        }
    }
}
