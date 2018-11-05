using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class FastLM
    {
        public readonly WordCount[] WordCounts;


        public FastLM(WordCount[] wordCounts)
        {
            this.WordCounts = wordCounts.OrderBy(wc => wc.Word).ToArray();
        }

        public bool GetCount(string word, out ulong count)
        {
            count = Find(word, 0, WordCounts.Length - 1);
            if (count == 0)
                return false;
            return true;
        }

        private ulong Find(string word, int left, int right)
        {
            if (right > left)
            {
                int mid = (left + right) / 2;
                if (string.Compare(word, WordCounts[mid].Word) < 0)
                {
                    return Find(word, left, mid);
                }
                else if (string.Compare(word, WordCounts[mid].Word) > 0)
                {
                    return Find(word, mid + 1, right);
                }
                else
                {
                    return WordCounts[mid].Count;
                }
            }else
                return 0;
        }
    }
}
