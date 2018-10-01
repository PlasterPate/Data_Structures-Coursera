using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static int MaximumPairwiseProduct(int[] inputArray)
        {
            for(int i = 0; i < 2; i++)
            {
                for(int j = i + 1; j < inputArray.Length; j++)
                {
                    if (inputArray[i] < inputArray[j])
                        Swap(ref inputArray[i], ref inputArray[j]);
                }
            }
            return inputArray[0] * inputArray[1];
        }

        public static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        public static string Process(string input)
        {
            var data = input.Split(new char[] { '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToList();
            return FastMaxPairwiseProduct(data).ToString();
        }

        public static int NaiveMaxPairwiseProduct(List<int> numbers)
        {
            int product = 0;
            for(int i = 0; i < numbers.Count; i++)
            {
                for(int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] * numbers[j] > product)
                        product = numbers[i] * numbers[j];
                }
            }
            return product;
        }

        public static int FastMaxPairwiseProduct(List<int> numbers)
        {
            int x1 = 0;
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[x1])
                    x1 = i;
            }
            int x2 = 0;
            if (x1 == 0)
                x2 = 1;
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[x2] && i != x1)
                    x2 = i;
            }
            return numbers[x1] * numbers[x2];
        }

    }
}
