using TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class ConvertIntoHeap : Processor
    {
        public ConvertIntoHeap(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(
            long[] array)
        {
            int lastParent = (array.Length - 2) / 2;
            List<Tuple<long, long>> results = new List<Tuple<long, long>>();
            for (int i = lastParent; i >= 0; i--)
            {
                int checkingParent = i;
                int minChild = 0;
                while(checkingParent <= lastParent)
                {
                    minChild = CheckChilds(array, checkingParent);
                    if (minChild == checkingParent)
                        break;
                    Swap(ref array[checkingParent], ref array[minChild]);
                    results.Add(new Tuple<long, long>(checkingParent, minChild));
                    checkingParent = minChild;
                }
            }
            return results.ToArray();
        }

        private int CheckChilds(long[] array, int parent)
        {
            int firstChild = (2 * parent) + 1;
            int secondChild = ((2 * parent) + 2) < array.Length ? (2 * parent) + 2 : firstChild;
            int minChild = array[firstChild] < array[secondChild] ? firstChild : secondChild;
            return array[minChild] < array[parent] ? minChild : parent;
            
        }

        private void Swap(ref long a, ref long b)
        {
            long c = a;
            a = b;
            b = c;
        }
    }

}