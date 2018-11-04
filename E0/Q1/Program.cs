using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {
        private static Dictionary<int, char[]> D =
            new Dictionary<int, char[]>
            {
                [0] = new char[] { '+' },
                [1] = new char[] { '_', ',', '@' },
                [2] = new char[] { 'A', 'B', 'C' },
                [3] = new char[] { 'D', 'E', 'F' },
                [4] = new char[] { 'G', 'H', 'I' },
                [5] = new char[] { 'J', 'K', 'L' },
                [6] = new char[] { 'M', 'N', 'O' },
                [7] = new char[] { 'P', 'Q', 'R', 'S' },
                [8] = new char[] { 'T', 'U', 'V' },
                [9] = new char[] { 'W', 'X', 'Y', 'Z' },
            };


        public static string[] GetNames(int[] phone)
        {
            return GetNamesOf(phone, 0, phone.Length - 1);
        }

        private static string[] GetNamesOf(int[] phone, int left, int right)
        {
            if(right > left)
            {
                int mid = (left + right) / 2;
                string[] First = GetNamesOf(phone, left, mid);
                string[] second = GetNamesOf(phone, mid + 1, right);
                return MergeStrings(First, second);
            }
            var result = D[phone[left]].Select(x => x.ToString()).ToArray();
            return result;

        }

        private static string[] MergeStrings(string[] first, string[] second)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < first.Length; i++)
            {
                for (int j = 0; j < second.Length; j++)
                {
                    result.Add(first[i] + second[j]);
                }
            }
            return result.ToArray();
        }

        static void Main(string[] args)
        {
            int[] phoneNumber = new int[] {0, 9, 1, 2, 2, 2, 4, 2, 5, 2, 5 };

            // چاپ یک رشته حرفی برای شماره تلفن
            for (int i=0; i< phoneNumber.Length; i++)
                Console.Write(D[phoneNumber[i]][0]);
            Console.WriteLine();
        }


    }
}
