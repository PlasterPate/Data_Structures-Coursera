using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class HashingWithChain : Processor
    {
        public HashingWithChain(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, string[], string[]>)Solve);

        protected List<List<string>> PhoneBookHashTable;

        public string[] Solve(long bucketCount, string[] commands)
        {
            PhoneBookHashTable = new List<List<string>>((int)bucketCount);
            for (int i = 0; i < PhoneBookHashTable.Capacity; i++)
            {
                PhoneBookHashTable.Add(new List<string>());
            }
            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];
                
                switch (cmdType)
                {
                    case "add":
                        Add(arg);
                        break;
                    case "del":
                        Delete(arg);
                        break;
                    case "find":
                        result.Add(Find(arg));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str, int start , int count, long m = BigPrimeNumber, long p = BigPrimeNumber, long x = ChosenX)
        {
            long hash = 0;
            for (int i = start; i < start + count; i++)
            {
                hash += str[i] * Pow(x, (i - start), p) % p;
                hash = hash % p;
            }
            return hash % m;
        }

        public static long Pow(long x, long a, long p = BigPrimeNumber)
        {
            long power = 1;
            for (int i = 0; i < a; i++)
            {
                power *= x;
                power = power % p;
            }
            return power;
        }

        public void Add(string str)
        {
            int idx = (int)PolyHash(str, 0, str.Length, PhoneBookHashTable.Capacity);
            for (int i = 0; i < PhoneBookHashTable[idx].Count; i++)
            {
                if (PhoneBookHashTable[idx][i] == str)
                    return;
            }
            PhoneBookHashTable[idx].Add(str);
        }

        public string Find(string str)
        {
            int idx = (int)PolyHash(str, 0, str.Length, PhoneBookHashTable.Capacity);
            for (int i = 0; i < PhoneBookHashTable[idx].Count; i++)
            {
                if (PhoneBookHashTable[idx][i] == str)
                    return "yes";
            }
            return "no";
        }

        public void Delete(string str)
        {
            int idx = (int)PolyHash(str, 0, str.Length, PhoneBookHashTable.Capacity);
            for (int i = 0; i < PhoneBookHashTable[idx].Count; i++)
            {
                if (PhoneBookHashTable[idx][i] == str)
                    PhoneBookHashTable[idx].RemoveAt(i);
            }
        }

        public string Check(int i)
        {
            string result = "-";
            var a = PhoneBookHashTable[i];
            if(a.Count > 0)
            {
                a.Reverse();
                result = string.Join(" ", a);
            }
            return result;
        }
    }
}
