using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class RabinKarp : Processor
    {
        public RabinKarp(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long[]>)Solve);

        public long[] Solve(string pattern, string text)
        {
            List<long> occurrences = new List<long>();
            long hash = HashingWithChain.PolyHash(pattern, 0, pattern.Length);
            var textHashes = PreComputeHashes(text, pattern.Length);
            for (long i = 0; i < textHashes.Length; i++)
            {
                if (hash == textHashes[i] && pattern == text.Substring((int)i, pattern.Length))
                {
                    occurrences.Add(i);
                }
            }
            return occurrences.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long[] PreComputeHashes(
            string T, 
            int len, 
            long p = BigPrimeNumber, 
            long x = ChosenX)
        {
            long xp = HashingWithChain.Pow(x, len, p);
            long[] result = new long[T.Length - len + 1];
            result[T.Length - len] = HashingWithChain.PolyHash(T, T.Length - len , len, (int)p, p, x);
            for (int i = T.Length - len - 1; i >= 0; i--)
            {
                result[i] = ((x * result[i + 1] + T[i] - T[i + len] * xp % p) % p + p) % p ;
            }
            return result;
        }
    }
}
