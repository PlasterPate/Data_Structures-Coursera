using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class PrimitiveCalculator: Processor
    {
        public PrimitiveCalculator(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)Solve);

        public long[] Solve(long n)
        {
            List<(long, long)> calculations = new List<(long, long)>() { (0, 0), (0, 0) };
            for (int i = 2; i <= n; i++)
            {
                calculations.Add((long.MaxValue, long.MaxValue));
                if ((i % 3) == 0)
                {
                    if (calculations[i / 3].Item1 + 1 < calculations[i].Item1)
                        calculations[i] = (calculations[i / 3].Item1 + 1, i / 3);
                }
                if ((i % 2) == 0)
                {
                    if (calculations[i / 2].Item1 + 1 < calculations[i].Item1)
                        calculations[i] = (calculations[i / 2].Item1 + 1, i / 2);
                }
                if (calculations[i - 1].Item1 + 1 < calculations[i].Item1)
                    calculations[i] = (calculations[i - 1].Item1 + 1, i - 1);
            }
            List<long> result = new List<long>() { n};
            for (long i = calculations.Count - 1; i > 1 ;)
            {
                i = calculations[(int)i].Item2;
                result.Add(i);
            }
            result.Reverse();
            return result.ToArray();
        }
    }
}
