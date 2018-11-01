using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }

        public static long[] BinarySearch1(long[] a, long[] b)
        {
            List<long> result = new List<long>();
            foreach (long num in b)
            {
                long low = 0;
                long high = a.Length - 1;
                while (true)
                {
                    long mid = (high + low) / 2;
                    if (num == a[mid])
                    {
                        result.Add(mid);
                        break;
                    }
                    else if (num < a[mid])
                        high = mid - 1;
                    else
                        low = mid + 1;
                    if (low > high)
                    {
                        result.Add(-1);
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        public static string ProcessBinarySearch1(string inStr) =>
            TestTools.Process(inStr, BinarySearch1);

        public static long MajorityElement2(long n, long[] a)
        {
            var nums = a.ToList();
            nums.Sort();
            int mid = (int)(n - 1) / 2;
            int low1 = 0;
            int high1 = mid;
            int low2 = mid + 1;
            int high2 = (int)n - 1;
            while (true)
            {
                int q1 = (low1 + high1) / 2;
                int q2 = (low2 + high2) / 2;
                if ((nums[q1] != nums[mid] && nums[q2] != nums[mid]) || (low1 > high1 && low2 > high2))
                    return 0;
                if ((nums[q1] == nums[mid] && nums[q2] == nums[mid]))
                    return 1;
                else if (nums[q1] == nums[mid])
                {
                    high1 = q1 - 1;
                    high2 = q2 - 1;
                }
                else if (nums[q2] == nums[mid])
                {
                    low1 = q1 + 1;
                    low2 = q2 + 1;
                }
            }
        }

        public static string ProcessMajorityElement2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)MajorityElement2);

        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            return QuickSort(a.ToList()).ToArray();
        }

        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        public static List<long> QuickSort(List<long> list)
        {
            if (list.Count() < 2)
                return list;
            int pivotIdx = 0;
            List<long> pivotValues = new List<long>() { list[pivotIdx] };
            var lowers = new List<long>();
            var highers = new List<long>();
            for (int i = 0; i < list.Count(); i++)
            {
                if (i != pivotIdx)
                {
                    if (list[i] < pivotValues[0])
                    {
                        lowers.Add(list[i]);
                    }
                    else if(list[i] > pivotValues[0])
                    {
                        highers.Add(list[i]);
                    }
                    else
                    {
                        pivotValues.Add(list[i]);
                    }
                }
            }
            var mergedList = QuickSort(lowers);
            mergedList.AddRange(pivotValues);
            mergedList.AddRange(QuickSort(highers));
            return mergedList;
        }

        public static long NumberofInversions4(long n, long[] a)
        {
            long result = 0;
            MergeSort(a.ToList(), 0, (int)n - 1, ref result);
            return result;
        }

        private static void MergeSort(List<long> list, int l, int r, ref long inversionCounter)
        {
            if (r > l)
            {
                int m = (l + r) / 2;
                MergeSort(list, l, m, ref inversionCounter);
                MergeSort(list, m + 1, r, ref inversionCounter);
                Merge(list, l, m, r, ref inversionCounter);
            }
        }

        private static void Merge(List<long> list, int l, int m, int r, ref long inversionCounter)
        {
            List<long> firstHalf = list.GetRange(l, m - l + 1);
            List<long> secondHalf = list.GetRange(m + 1, r - m);
            int i = 0;
            int j = 0;
            int k = l;
            while (i < firstHalf.Count && j < secondHalf.Count)
            {
                if (secondHalf[j] < firstHalf[i])
                {
                    list[k] = secondHalf[j];
                    j++;
                    inversionCounter += firstHalf.Count - i;
                }
                else
                {
                    list[k] = firstHalf[i];
                    i++;
                }
                k++;
            }
            while (i < firstHalf.Count)
            {
                list[k] = firstHalf[i];
                i++;
                k++;
            }
            while (j < secondHalf.Count)
            {
                list[k] = secondHalf[j];
                j++;
                k++;
            }
        }

        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        public static long[] OrganizingLottery5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            long[] result = new long[points.Length];
            List<PointData> mergedList = new List<PointData>();
            foreach (var p in startSegments)
            {
                mergedList.Add(new PointData(p, 1));
            }
            for (int i = 0; i < points.Length; i++)
            {
                mergedList.Add(new PointData(points[i], 2, i));
            }
            foreach (var p in endSegment)
            {
                mergedList.Add(new PointData(p, 3));
            }
            mergedList.Sort();
            int left = 0, right = 0;
            for (int i = 0; i < mergedList.Count; i++)
            {
                if (mergedList[i].type == 1)
                    left++;
                else if (mergedList[i].type == 3)
                    right++;
                else if (mergedList[i].type == 2)
                    result[mergedList[i].index] = (left - right);
            }
            return result.ToArray();
        }

        public static string ProcessOrganizingLottery5(string inStr) =>
            TestTools.Process(inStr, OrganizingLottery5);

        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            var points = xPoints
                       .Zip(yPoints, (x, y) => (x, y))
                       .OrderBy(p => p.x).ToList();
            return MinimumDistance(points, 0, (int)n - 1);
        }

        private static double MinimumDistance(List<(long, long)> points, int left, int right)
        {
            if (left >= right)
                return double.MaxValue;
            if (right - left > 1)
            {
                int mid = (left + right) / 2;
                double d1 = MinimumDistance(points, left, mid);
                double d2 = MinimumDistance(points, mid + 1, right);
                double d = Math.Min(d1, d2);

                List<(long, long)> edgeList = new List<(long, long)>();

                for (int i = left; i <= right; i++)
                    if (Math.Abs(points[i].Item2 - points[mid].Item2) < d)
                        edgeList.Add(points[i]);

                double dPrime = d;
                for (int i = 0; i < edgeList.Count - 1; i++)
                    for (int j = i + 1; j < edgeList.Count; j++)
                    {
                        double dist = Distance(edgeList[i], edgeList[j]);
                        if (dist < dPrime)
                            dPrime = dist;
                    }

                return Math.Min(d, dPrime);
            }
            return Distance(points[left], points[right]);
        }

        private static double Distance((long, long) p1, (long, long) p2)
        {
            long x = p1.Item1 - p2.Item1;
            long y = p1.Item2 - p2.Item2;
            double value = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            return Math.Round(value, 4);
        }

        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

    }
}
