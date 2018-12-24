using System;
using System.Collections;
using System.Linq;

namespace E2
{
    public class Q3BloomFilter
    {
        BitArray Filter;
        Func<string, long>[] HashFunctions;

        public Q3BloomFilter(int filterSize, int hashFnCount)
        {
            Random rnd = new Random();
            int[] randoms = new int[hashFnCount];
            Filter = new BitArray(filterSize);
            HashFunctions = new Func<string, long>[hashFnCount];
        }

        public int MyHashFunction(string str, int num)
        {
            int hash = str.GetHashCode();
            for (int i = 1; i < num; i++)
            {
                hash += hash.ToString().GetHashCode();
            }
            return Math.Abs(hash) % Filter.Length;
        }

        public void Add(string str)
        {
            long hash = 0;
            for (int i = 1; i <= HashFunctions.Length; i++)
            {
                hash = MyHashFunction(str, i);
                Filter[(int)hash] = true;
            }
        }

        public bool Test(string str)
        {
            long hash = 0;
            for (int i = 1; i <= HashFunctions.Length; i++)
            {
                hash = MyHashFunction(str, i);
                if (Filter[(int)hash] == false)
                    return false;
            }
            return true;
        }
    }
}