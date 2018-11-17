using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximizingArithmeticExpression : Processor
    {
        public MaximizingArithmeticExpression(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string expression)
        {
            int[] digits = expression.ToCharArray()
                                     .Where((x, i) => i % 2 == 0)
                                     .Select(x => int.Parse(x.ToString()))
                                     .ToArray();
            char[] operations = expression.Where((x, i) => i % 2 == 1).ToArray();
            long[,] minTable = new long[digits.Length, digits.Length];
            long[,] maxTable = new long[digits.Length, digits.Length];
            for (int i = 0; i < digits.Length; i++)
            {
                minTable[i, i] = digits[i];
                maxTable[i, i] = digits[i];
            }
            for (int s = 1; s < digits.Length; s++)
            {
                for (int i = 0; i < digits.Length - s; i++)
                {
                    int j = i + s;
                    MinAndMax(minTable, maxTable, operations, i, j);
                }
            }
            return maxTable[0, digits.Length - 1];
        }

        private void MinAndMax(long[,] min, long[,] max, char[] op, int i, int j)
        {
            List<long> results = new List<long>();
            for (int k = i; k < j; k++)
            {
                results.AddRange(new long[]{Calculate(min[i, k], min[k + 1, j], op[k]),
                                            Calculate(min[i, k], max[k + 1, j], op[k]),
                                            Calculate(max[i, k], min[k + 1, j], op[k]),
                                            Calculate(max[i, k], max[k + 1, j], op[k])});
            }
            min[i, j] = results.Min();
            max[i, j] = results.Max();
        }

        private long Calculate(long n1, long n2, char op)
        {
            switch (op)
            {
                case '+': return n1 + n2;
                case '-': return n1 - n2;
                case '*': return n1 * n2;
                default: return 0;
            }
        }
    }
}
