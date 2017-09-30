using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractIntro
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunMenu();
        }

        public static void RunMenu()
        {
            var running = true;
            do
            {
                DisplayMenu();
                var input = Convert.ToInt32(Console.ReadLine());
                switch (input.ToString())
                {
                    case "0":
                        running = false;
                        Environment.Exit(0);
                        break;
                    case "1":
                        new Comp1().RunMenu();
                        break;
                }
            } while (running);

        }

        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("| Compulsory Assignment 1");
            Console.WriteLine("| [1] Compulsory Assignment 1");
            Console.WriteLine("|");
            Console.WriteLine("| [0] Exit");
            Console.WriteLine();
        }





















        /*   Contract Intro   */

        static void TestSum()
        {
            Console.WriteLine(" ~~ Test Sum ~~ ");
            int[] a = { 1, 2, 3, 4, 5 };
            Console.Write("Initial Array: ");
            foreach (var item in a)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("");
            Console.Write("Array Sum: ");
            Console.Write(Sum(a));
            Console.WriteLine("");
            Console.WriteLine("Press any key to restart.");
            Console.ReadKey();
        }
        static int Sum(int[] a)
        {
            Contract.Requires(a != null);

            // the result is the sum of the numbers in a

            // a is not changed
            Contract.Ensures(Contract.ForAll(0, a.Length,
                             idx => a[idx] == Contract.OldValue(a[idx])));
            int res = 0;
            for (int i = 0; i < a.Length; i++) res += a[i];
            a[0]++;
            return res;
        }

        static int Add(int x, int y)
        {
            Contract.Requires(x > 0);
            Contract.Ensures(Contract.Result<int>() == (x + y));
            return x + y;

        }
        private static void TestInit()
        {

            int[] aa = new int[10];
            foreach (int v in CloneAndInit(aa, 10))
                Console.WriteLine(v);
            Console.ReadKey();

        }

        /**
         * Pre: a != null
         * Post: all values in a are equal to v
        **/
        public static void init(int[] a, int v)
        {
            Contract.Requires(a != null, "Pre condition not meet!");
            Contract.Ensures(Contract.ForAll(0, a.Length, index => a[index] == v));
            Contract.Ensures(Contract.ForAll(a, va => va == v));

            int i = 0;
            while (i != a.Length)
            {
                a[i] = v;
                ++i;
            }
        }

        /**
        * Pre: a != null
        * Post: return a clone of a with v in all cells.
       **/
        public static int[] CloneAndInit(int[] a, int v)
        {
            Contract.Requires(a != null, "Pre condition not meet!");
            Contract.Ensures(Contract.Result<int[]>().Length == a.Length);
            Contract.Ensures(Contract.ForAll(Contract.Result<int[]>(), value => value == v));

            int[] res = new int[a.Length];

            for (int i = 0; i != a.Length; i++)
                res[i] = v;

            return res;

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
            Console.WriteLine(Search(a, 45));
            Console.ReadLine();
        }
        /**
         * Pre: b != null and b.Length > 0 and x is in b
         * Post: Return value is the smallest index of b where x is allocated
        **/
        public static int Search(int[] b, int x)
        {
            Contract.Requires(b != null);
            Contract.Requires(Contract.Exists(0, b.Length, j => x == b[j]));

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
                //b[i] = 0; // the new internship disaster developer....
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
            Console.WriteLine(" ~~ Test Swap ~~ ");
            Console.Write("Input value x:");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input value y:");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("x: " + x + " y:" + y);
            Console.WriteLine("Swapping...");
            Swap(ref x, ref y);
            Console.WriteLine("x: " + x + " y:" + y);
            Console.WriteLine("Press any key to restart.");
            Console.ReadKey();

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

        /* Compolsory Assignment 1*/

        //Exercise 1
        static int[] ReturnNonTrivialDivisors()
        {
            Console.WriteLine("~~ Return Non Trivial Divisors ~~ ");
            Console.Write("Input integer: ");
            int a = Convert.ToInt32(Console.ReadLine());
            List<int> divisors = GetDivisors(a).ToList();
            Console.Write("The non-trivial divisors of {0} is: ", a);
            foreach (var item in divisors)
            {
                Console.Write(item + " ");
            }
            Console.ReadLine();
            return divisors.ToArray();
        }

        //Helper Method for exercise 1
        static IEnumerable<int> GetDivisors(int n)
        {
            Contract.Requires(n > 1);

            var divisors = from a in Enumerable.Range(2, n / 2)
                           where n % a == 0
                           select a;
            Contract.Ensures(divisors.Count() < 1);
            return divisors;
        }

        //Exercise 2
        static void FindLongestMonotoneSegment()
        {
            Console.WriteLine("~~ Find Longest Monotone Segment From Array ~~");
            int[] array = InputArray();

            var highestlenght = HighestSequence(array);
            Console.WriteLine("The longest sequence is: {0}", highestlenght);
            Console.ReadKey();
        }

        //Helper method to exercise 2
        static int HighestSequence(int[] values)
        {
            IList<int> sequenceCounts = new List<int>();

            var currentSequence = 0;
            for (var i = 0; i < values.Length; i++)
            {
                if (i == (values.Length - 1)) //End edge case
                {
                    currentSequence++;
                    sequenceCounts.Add(currentSequence);
                }
                else if ((values[i]) < values[i + 1])
                {
                    currentSequence++;
                }
                else
                {
                    currentSequence++;
                    sequenceCounts.Add(currentSequence);
                    currentSequence = 0;
                    continue;
                }
                sequenceCounts.Add(currentSequence);
            }
            return sequenceCounts.Max();
        }

        //Helper method to input an array and return it
        static int[] InputArray()
        {
            Console.WriteLine("Input array, seperated by space");
            var numbers = Console.ReadLine().Split(' ').Select(token => int.Parse(token));

            // if you must have it as an array...
            int[] arr = numbers.ToArray();

            return arr;
        }


        //Exercise 3
        static void FindNearestIndexInArray()
        {
            Console.WriteLine("~~ Find Nearest Index to value in Array ~~ ");
            int[] sortedArray = InputArray().OrderBy(x => x).ToArray();
            Console.Write("Input the number you want to search for: ");
            int realNumber = Convert.ToInt32(Console.ReadLine());

            var bestIndex = BestIndex(sortedArray, realNumber);

            Console.Write("Your array is: ");
            int i = 0;
            foreach (var item in sortedArray)
            {
                Console.Write("[" + i + "]=" + item + " ");
                i++;
            }
            Console.WriteLine();
            Console.WriteLine("The index that is closest to your number is: {0}", bestIndex);
            Console.ReadLine();
        }


        //Helper method to exercise 3
        static int BestIndex(int[] sortedArray, int realNumber)
        {
            decimal minDistance = 0; //0 is fine here it is never read, it is just to make the compiler happy.
            int minIndex = -1;

            for (int i = 0; i < sortedArray.Length; i++)
            {
                var distance = Math.Abs(realNumber - sortedArray[i]);
                if (minIndex == -1 || distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = i;

                    //Optional, stop testing if we find a exact match.
                    if (minDistance == 0)
                        break;
                }
            }

            return minIndex;
        }

        //Exercise 4
        static void UnionAndFindIntersectOfTwoArrays()
        {
            Console.WriteLine("~~ Find Intersect and Union of two arrays ~~");
            int[] arr1 = InputArray();
            Console.WriteLine();
            int[] arr2 = InputArray();

            Console.WriteLine("Union of your arrays.");
            Union(arr1, arr2);
            Console.WriteLine();
            Console.WriteLine("Intersects of your arrays.");
            Intersection(arr1, arr2);
            Console.ReadLine();
        }

        //Helper method for exercise 4 - union
        static void Union(int[] arr1, int[] arr2)
        {
            var union = arr1.Union(arr2);

            foreach (var item in union)
            {
                Console.Write(item + " ");
            }
        }

        //Helper method for exercise 4 - union
        static void UnionManual(int[] arr1, int[] arr2)
        {
            arr1.OrderBy(x => x).ToArray();
            arr2.OrderBy(x => x).ToArray();
            int m = arr1.Length;
            int n = arr2.Length;

            int i = 0, j = 0;

            int[] union = new int[100];
            int[] intersect = new int[100];

            while (i < m && j < n)
            {
                if (arr1[i] < arr2[j])
                {
                    Console.Write("{0} ", arr1[i++]);
                }
                else if (arr2[j] < arr1[i])
                {
                    Console.Write("{0} ", arr2[j++]);
                }
                else /* if arr1[i] == arr2[j] */
                {
                    Console.Write("{0} ", arr2[j++]);
                    i++;
                }
            }

            /* Print remaining elements of the larger array */
            while (i < m)
            {
                Console.Write("{0} ", arr1[i++]);
            }
            while (j < n)
            {
                Console.Write("{0} ", arr2[j++]);
            }
        }

        //Helper method for exercise 4 - Intersection
        static void Intersection(int[] arr1, int[] arr2)
        {
            var intersection = arr1.Intersect(arr2);

            foreach (var item in intersection)
            {
                Console.Write(item + " ");
            }
        }

        //Helper method for exercise 4 - Intersection
        static void IntersectionManual(int[] arr1, int[] arr2)
        {
            arr1.OrderBy(x => x).ToArray();
            arr2.OrderBy(x => x).ToArray();
            int m = arr1.Length;
            int n = arr2.Length;

            int i = 0, j = 0;
            while (i < m && j < n)
            {
                if (arr1[i] < arr2[j])
                    i++;
                else if (arr2[j] < arr1[i])
                    j++;
                else /* if arr1[i] == arr2[j] */
                {
                    Console.Write("{0} ", arr2[j++]);
                    i++;
                }
            }
        }
    }

}
