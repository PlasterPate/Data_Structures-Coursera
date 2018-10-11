using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        // Process
        public static string Process(string inStr, Func<long, long> longProcessor)
        {
            long n = long.Parse(inStr);
            return longProcessor(n).ToString();
        }

        public static string Process(string inStr, Func<long, long, long> longProcessor)
        {
            var toks = inStr.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            long a = long.Parse(toks[0]);
            long b = long.Parse(toks[1]);
            return longProcessor(a, b).ToString();
        }

        // 1
        public static long Fibonacci(long n)
        {
            List<long> Fib = new List<long>();
            Fib.Add(0);
            Fib.Add(1);
            for (int i = 2; i <= n; i++)
            {
                Fib.Add( Fib[i - 1] + Fib[i - 2]);
            }
            return Fib[(int)n];
        }

        public static string ProcessFibonacci(string inStr) => Process(inStr, Fibonacci);

        // 2
        public static long Fibonacci_LastDigit(long n)
        {
            List<long> Fib = new List<long>();
            Fib.Add(0);
            Fib.Add(1);
            for (int i = 2; i <= n; i++)
            {
                Fib.Add((Fib[i - 1] + Fib[i - 2]) % 10);
            }
            return Fib[(int)n];
        }

        public static string ProcessFibonacci_LastDigit(string inStr) => Process(inStr, Fibonacci_LastDigit);

        // 3
        public static long GCD(long a, long b)
        {
            long max = a;
            if (a < b)
                max = b;
            if (b == 0)
                return a;
            else
                return GCD(b, a % b);

        }

        public static string ProcessGCD(string inStr) => Process(inStr, GCD);

        // 4
        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;

        }

        public static string ProcessLCM(string inStr) => Process(inStr, LCM);

        // 5
        public static long Fibonacci_Mod(long n, long m)
        {
            List<long> Fib = new List<long>();
            Fib.Add(0);
            Fib.Add(1);
            for (int i = 2; i < int.MaxValue; i++)
            {
                Fib.Add((Fib[i - 1] + Fib[i - 2]) % m);
                if ((Fib[i] % m) == 1 && (Fib[i - 1] % m) == 0)
                    break;
            }

            return Fib[(int)(n % (Fib.Count - 2))];

        }

        public static string ProcessFibonacci_Mod(string inStr) => Process(inStr, Fibonacci_Mod);

        // 6
        public static long Fibonacci_Sum(long n)
        {
            return (Fibonacci_Mod(n + 2, 10) + 9) % 10;
        }

        public static string ProcessFibonacci_Sum(string inStr) => Process(inStr, Fibonacci_Sum);

        // 7
        public static long Fibonacci_Partial_Sum(long n, long m)
        {
            if(n > m)
                return (Fibonacci_Sum(n) + 10 - Fibonacci_Sum(m - 1)) % 10;
            return (Fibonacci_Sum(m) + 10 - Fibonacci_Sum(n - 1)) % 10;
        }

        public static string ProcessFibonacci_Partial_Sum(string inStr) => Process(inStr, Fibonacci_Partial_Sum);


        // 8
        public static long Fibonacci_Sum_Squares(long n)
        {
            return (Fibonacci_Mod(n, 10) * Fibonacci_Mod(n + 1, 10)) % 10;
        }

        public static string ProcessFibonacci_Sum_Squares(string inStr) => Process(inStr, Fibonacci_Sum_Squares);
    }
}
