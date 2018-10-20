using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MaximizeSalary6(3,new long[] {1,29,159 }));
        }

        // 1
        public static long ChangingMoney1(long money)
        {
            return money / 10 + (money % 10) / 5 + money % 5;
        }

        public static string ProcessChangingMoney1(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>)ChangingMoney1);

        // 2
        public static long MaximizingLoot2(long capacity, long[] weights, long[] values)
        {
            double result = 0;
            var valuePerWeight = weights.Zip(values, (first, second) =>
                new
                {
                    Weight = first,
                    Value = second,
                    VPW = (double)second / first
                }).OrderByDescending(x => x.VPW).ToList();
            for (int i = 0; i < valuePerWeight.Count; i++)
            {
                if (capacity > valuePerWeight[i].Weight)
                {
                    result += valuePerWeight[i].Value;
                    capacity -= valuePerWeight[i].Weight;
                }
                else
                {
                    result += (double)valuePerWeight[i].Value * capacity / valuePerWeight[i].Weight;
                    break;
                }
            }
            return (long)result;
        }

        public static string ProcessMaximizingLoot2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)MaximizingLoot2);

        // 3
        public static long MaximizingOnlineAdRevenue3
            (long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
            long result = 0;
            adRevenue = adRevenue.OrderByDescending(x => x).ToArray();
            averageDailyClick = averageDailyClick.OrderByDescending(x => x).ToArray();
            for (int i = 0; i < adRevenue.Length; i++)
            {
                result += adRevenue[i] * averageDailyClick[i];
            }
            return result;
        }

        public static string ProcessMaximizingOnlineAdRevenue3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)MaximizingOnlineAdRevenue3);

        // 4
        public static long CollectingSignatures4
            (long tenantCount, long[] startTimes, long[] endTimes)
        {
            var times = startTimes.Zip(endTimes, (s, f) => new
            {
                Start = s,
                End = f,
                Length = f - s,
                
            }).OrderBy(x => x.Length).ToList();
            for (int i = 0; i < times.Count ; i++ )
            {
                for (int j = i + 1; j < times.Count; j++)
                {
                    if (times[i].Start >= times[j].Start && times[i].End <= times[j].End)
                    {
                        times.RemoveAt(j);
                        j--;
                    }
                    
                }
            }
            times = times.OrderBy(x => x.Start).ToList();
            int result = 0;
            while (times.Count > 0)
            {
                for (int i = 1; i < times.Count;)
                {
                    if (times[i].Start <= times[0].End)
                        times.RemoveAt(i);
                    else
                        i++;
                }
                times.RemoveAt(0);
                result++;
            }
            return result;
        }

        public static string ProcessCollectingSignatures4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)CollectingSignatures4);

        // 5
        public static long[] MaxmimizeNumberOfPrizePlaces5(long n)
        {
            List<long> prizes = new List<long>();
            for (int i = 1; i <= n; n -= i, i++)
            {
                prizes.Add(i);
            }
            if(n != 0)
                prizes[prizes.Count - 1] += n;
            return prizes.ToArray();
        }

        public static string ProcessMaxmimizeNumberOfPrizePlaces5(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)MaxmimizeNumberOfPrizePlaces5);

        // 6
        public static string MaximizeSalary6(long n, long[] numbers)
        {
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1 ; j < n; j++)
                {
                    if (IsBigger(numbers[j], numbers[i]))
                    {
                        long temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
            string result = string.Empty;
            foreach (var num in numbers)
            {
                result += num.ToString();
            }
            return result;
        }

        private static bool IsBigger(long n1, long n2)
        {
            string n1n2 = n1.ToString() + n2.ToString();
            string n2n1 = n2.ToString() + n1.ToString();
            if (long.Parse(n1n2) > long.Parse(n2n1))
                return true;
            else
                return false;
        }

        public static string ProcessMaximizeSalary6(string inStr) =>
            TestTools.Process(inStr, MaximizeSalary6);
    }
}
