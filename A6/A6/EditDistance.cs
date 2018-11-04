using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class EditDistance: Processor
    {
        public EditDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long>)Solve);

        public long Solve(string str1, string str2)
        {
            int[,] table = new int[str1.Length + 1, str2.Length + 1];
            
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0] = i;
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                table[0, j] = j;
            }
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)    
                {
                    if (str2[j - 1] == str1[i - 1])
                        table[i, j] = table[i - 1, j - 1];
                    else
                        table[i, j] = Min(table[i - 1, j - 1],
                                          table[i - 1, j],
                                          table[i, j - 1] ) + 1;
                }
            }
            return table[str1.Length, str2.Length];

        }

        private static int Min(params int[] numbers)
        {
            return numbers.Min();
        }
    }
}
