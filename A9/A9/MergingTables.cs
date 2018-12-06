using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class MergingTables : Processor
    {
        public MergingTables(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);

        public long[] Solve(long[] tableSizes, long[] sourceTables, long[] targetTables)
        {
            List<Table> tables = new List<Table>();
            tables.Add(new Table(0, 0));
            for (int i = 0; i < tableSizes.Length; i++)
            {
                tables.Add(new Table(i + 1, tableSizes[i]));
            }
            long maxSize = tableSizes.OrderByDescending(x => x).First();
            List<long> result = new List<long>();
            for (int i = 0; i < sourceTables.Length; i++)
            {
                int source = (int)sourceTables[i];
                int target = (int)targetTables[i];
                while (tables[source].Link != source)
                    source = tables[source].Link;
                while (tables[target].Link != target)
                    target = tables[target].Link;
                if (source != target)
                {
                    tables[target].Size += tables[source].Size;
                    tables[source].Link = target;
                    tables[source].Size = 0;
                }
                maxSize = Math.Max(maxSize, tables[target].Size);
                result.Add(maxSize);
            }
            return result.ToArray();
        }
    }
}