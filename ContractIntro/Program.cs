using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSearch();
            //TestInit();
            //TestReverse();
            //TestSwap();
            TestGcd();
        }

        static int Add(int x, int y)
        {
            Contract.Requires(x > 0);
            Contract.Ensures(Contract.Result<int>() == (x + y));
            return x + y;

        }
        private static void TestInit()
        {

            int[] aa = new int[30];
            //init(aa, 4);
            init(null, 4);


        }

        /**
         * Pre: a != null
         * Post: all values in a are equal to v
        **/
        public static void init(int[] a, int v)
        {
            Contract.Requires(a != null, "Pre condition not meet!");
            Contract.Ensures(Contract.ForAll(0, a.Length, index => a[index] == v));

            int i = 0;
            while (i != a.Length)
            {
                a[i] = v;
                ++i;
            }
        }

        static void TestReverse()
        {
            int[] a = { 1, 2, 3, 4, 5, 6, 7 };
            Reverse(a);
            foreach (int v in a)
                Console.Write(" " + v);
            Console.ReadLine();

        }

        [Pure]
        public static bool AreReverseEqual(int[] a, int[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[a.Length - 1 - i]) return false;
            return true;
        }

        /**
         * Pre: a != null && a.Length > 0
         * Post: a is the reverse of its orginal value
        **/
        public static void Reverse(int[] a)
        {
            Contract.Requires(a != null && a.Length > 0);
            Contract.Ensures(Contract.ForAll(0, a.Length / 2, index =>
                      Contract.OldValue(a[index]) == a[a.Length - 1 - index]));

            Contract.Ensures(AreReverseEqual(Contract.OldValue(a.ToArray()), a));
            int halfArray = a.Length / 2; // Floor
            int i = 0;

            while (i != halfArray)
            {
                int old = a[a.Length - 1 - i];
                a[a.Length - 1 - i] = a[i];

                a[i] = old;

                ++i;
            }
            a[0] = 34;


        }

        static void TestSearch()
        {
            int[] a = { 1, 2, 4, 7, 5, 3, 2 };
            Console.WriteLine(Search(a, 2));
            Console.ReadLine();
        }
        /**
         * Pre: b != null and b.Length > 0 and x is in b
         * Post: Return value is the smallest index of b where x is allocated
        **/
        public static int Search(int[] b, int x)
        {
            Contract.Requires(b != null && Contract.Exists(0, b.Length, j => x == b[j]));

            // x = b[the result of the function]
            Contract.Ensures(x == b[Contract.Result<int>()]);

            // and it is the first
            Contract.Ensures(Contract.ForAll(0, Contract.Result<int>(), j => x != b[j]));

            // search will not change the array
            Contract.Ensures(Contract.ForAll(0, b.Length,
                             idx => Contract.OldValue(b[idx]) == b[idx]));
            int i = 0;

            int res = 0;
            while (i < b.Length)
            {
                if (x == b[i])
                {
                    res = i;
                    break;
                }
                i = i + 1;

            }
            return res;
        }

        public static void Swap(ref int a, ref int b)
        {
            Contract.Ensures(a == Contract.OldValue(b) && b == Contract.OldValue(a));
            int tmp = a;
            a = b;
            b = tmp;
        }

        public static void TestSwap()
        {
            int x = 4, y = 2;
            Swap(ref x, ref y);
            Console.WriteLine(x + " " + y);
        }

        static void TestGcd()
        {
            for (int x = 1; x < 100; x++)
            {
                for (int y = x + 1; y < 100; y++)
                    Console.WriteLine("GCD " + x + ", " + y + " = " + Gcd(x, y));
            }
            Console.ReadKey();
        }

        static int Gcd(int x, int y)
        {
            Contract.Requires(x > 0 && y > 0);

            // the result is a divisor
            Contract.Ensures(x % Contract.Result<int>() == 0 &&
                             y % Contract.Result<int>() == 0);

            //and it is the largest one...
            Contract.Ensures(Contract.ForAll(Contract.Result<int>() + 1,
                                             Math.Min(x, y) + 1,
                                             d => x % d != 0 || y % d != 0));
            if (x == y) return x;
            if (x > y) return Gcd(x - y, y);
            return Gcd(y - x, x);
        }

        static bool IsPrime(int p)
        {
            Contract.Requires(p > 1);
            Contract.Ensures(Contract.Result<bool>() ==
                Contract.ForAll(2, p - 1, divisor => p % divisor != 0));
            
            for (int divisor = 2; divisor < p - 2; divisor++)
                if (p % divisor == 0)
                    return false;
            return true;
        }
    }

}
