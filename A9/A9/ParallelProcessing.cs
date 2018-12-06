using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class ParallelProcessing : Processor
    {
        public ParallelProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
            List<Tuple<long, long>> result = new List<Tuple<long, long>>();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < threadCount; i++)
            {
                threads.Add(new Thread(i, 0));
            }
            for (int i = 0; i < jobDuration.Length; i++)
            {
                Thread t = threads[0];
                result.Add(new Tuple<long, long>(t.Idx, t.Time));
                t.Time += jobDuration[i];
                int nextIdx = 1;
                for (; nextIdx < threads.Count; nextIdx++)
                {
                    if((threads[nextIdx].Time == t.Time && threads[nextIdx].Idx > t.Idx) || threads[nextIdx].Time > t.Time)
                    {
                        break;
                    }
                }
                threads.Insert(nextIdx, t);
                threads.RemoveAt(0);
            }
            return result.ToArray();
        }
    }
}
