using ContractIntro;
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
                switch (input)
                {
                    case 0:
                        running = false;
                        Environment.Exit(0);
                        break;
                    case 1:
                        new Comp1().RunMenu();
                        break;
                    case 2:
                        new Exercises41().RunMenu();
                        break;
                }
            } while (running);

        }

        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("| Compulsory Assignment");
            Console.WriteLine("| [1] Compulsory Assignment 1");
            Console.WriteLine("| [2] Exercises Week 41");
            Console.WriteLine("|");
            Console.WriteLine("| [0] Exit");
            Console.WriteLine();
        }

        /*   Contract Intro   */
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
    }
}
