using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class SpellChecker
    {
        public readonly FastLM LanguageModel;

        public SpellChecker(FastLM lm)
        {
            this.LanguageModel = lm;
        }

        public string[] Check(string misspelling)
        {
            List<WordCount> candidates = 
                new List<WordCount>();

            var arr = CandidateGenerator.GetCandidates(misspelling);
            foreach (var item in arr)
            {
                ulong count = 0;
                if (LanguageModel.GetCount(item, out count) == true)
                    candidates.Add(new WordCount(item, count));
            }
            

            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public string[] SlowCheck(string misspelling)
        {
            List<WordCount> candidates =
                new List<WordCount>();
            candidates.AddRange(LanguageModel.WordCounts.Where(x => EditDistance(misspelling, x.Word) <= 1).ToList());
            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public int EditDistance(string str1, string str2)
        {
            int n = str1.Length;
            int m = str2.Length;

            int[,] Distance = new int[n + 1, m + 1];
            for (int i = 0; i < n + 1; i++)
            {
                Distance[i, 0] = i;
            }

            for (int j = 0; j < m + 1; j++)
            {
                Distance[0, j] = j;
            }

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    if (str2[j - 1] == str1[i - 1])
                        Distance[i, j] = Distance[i - 1, j - 1];
                    else
                        Distance[i, j] = Min(Distance[i - 1, j - 1],
                                          Distance[i - 1, j],
                                          Distance[i, j - 1]) + 1;
                }
            }
            return Distance[n, m];
        }

        private static int Min(params int[] numbers)
        {
            return numbers.Min();
        }
    }
}
