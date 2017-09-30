using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractIntro
{
    public class Comp1
    {
        /* Compolsory Assignment 1*/

        public void RunMenu()
        {
            var running = true;
            do
            {
                DisplayMenu();
                var input = Convert.ToInt32(Console.ReadLine());
                switch (input.ToString())
                {
                    case "0":
                        Program.RunMenu();
                        break;
                    case "1":
                        ReturnNonTrivialDivisors();
                        break;
                    case "2":
                        FindLongestMonotoneSegment();
                        break;
                    case "3":
                        FindNearestIndexInArray();
                        break;
                    case "4":
                        UnionAndFindIntersectOfTwoArrays();
                        break;
                }
            } while (running);

        }

        /// <summary>
        /// Simply displays the menu - purely visual
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("| Compulsory Assignment 1");
            Console.WriteLine("| [1] Exercise 1 - Non-trivial divisors.");
            Console.WriteLine("| [2] Exercise 2 - Longest sequence in array.");
            Console.WriteLine("| [3] Exercise 3 - Nearest Index to value in array.");
            Console.WriteLine("| [4] Exercise 4 - Intersect and Union of two Arrays.");
            Console.WriteLine("|");
            Console.WriteLine("| [0] Back");
            Console.WriteLine();
        }

        //Exercise 1
        /// <summary>
        /// Returns the non-trivial divisors of an int
        /// </summary>
        /// <returns>Array containing the trivial divisors of the given int</returns>
        public int[] ReturnNonTrivialDivisors()
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
        /// <summary>
        /// Find the divisors for a given int and returns them as an int[]
        /// </summary>
        /// <param name="n">The given int to find the divisors for</param>
        /// <returns>an array containing the divisors for the given int</returns>
        /*
         Pre:
         a > 1
         
         Post:
         P(x):y(1<y<x  x mod y  0  yresult)
         and z(1<z<x  x mod y  0  zresult) 
             */
        public int[] GetDivisors(int n)
        {
            Contract.Requires(n > 1);
            Contract.Ensures(Contract.ForAll(Contract.Result<int[]>(),
                value => n % value == 0 && value > 1 && value < n));
            Contract.Ensures(Contract.ForAll(2, n,
                idx => (n % idx == 0) == Contract.Result<int[]>().Contains(idx)));

            var divisors = from a in Enumerable.Range(2, n / 2)
                           where n % a == 0
                           select a;
            return divisors.ToArray();
        }

        //Exercise 2
        /// <summary>
        /// Find the longest continous sequence in an int array
        /// </summary>
        public void FindLongestMonotoneSegment()
        {
            Console.WriteLine("~~ Find Longest Monotone Segment From Array ~~");
            int[] array = InputArray();

            var highestlenght = HighestSequence(array);
            Console.WriteLine("The longest sequence is: {0}", highestlenght);
            Console.ReadKey();
        }

        //Helper method to exercise 2
        /// <summary>
        /// The method containing the logic for exercise 2
        /// Finding the longest continous sequence in an int array
        /// </summary>
        /// <param name="values">The given int array</param>
        /// <returns>The length of the longest continous sequence</returns>
        public int HighestSequence(int[] values)
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
        /// <summary>
        /// Helper method for reading an int array since that was used alot
        /// </summary>
        /// <returns>the int array that you typed in</returns>
        public int[] InputArray()
        {
            Console.WriteLine("Input array, seperated by space");
            var numbers = Console.ReadLine().Trim().Split(' ').Select(token => Int32.Parse(token));

            // if you must have it as an array...
            int[] arr = numbers.ToArray();

            return arr;
        }


        //Exercise 3
        /// <summary>
        /// Find the 'best' index in an array compared to an int
        /// </summary>
        public void FindNearestIndexInArray()
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
        /// <summary>
        /// Find the index of the closest value to the given int in the given array
        /// </summary>
        /// <param name="sortedArray">the given array sorted</param>
        /// <param name="realNumber">the given number</param>
        /// <returns>the best index (the one closest to the given int)</returns>
        public int BestIndex(int[] sortedArray, int realNumber)
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
        /// <summary>
        /// Make a union and find the intersects of to int[]s
        /// </summary>
        public void UnionAndFindIntersectOfTwoArrays()
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
        /// <summary>
        /// The built in union function
        /// </summary>
        /// <param name="arr1">the first array</param>
        /// <param name="arr2">the second array</param>
        public void Union(int[] arr1, int[] arr2)
        {
            var union = arr1.Union(arr2);

            foreach (var item in union)
            {
                Console.Write(item + " ");
            }
        }

        //Helper method for exercise 4 - union
        /// <summary>
        /// A manual union (and sorted, wasnt sure which you meant)
        /// </summary>
        /// <param name="arr1">first array</param>
        /// <param name="arr2">second array</param>
        public void UnionManual(int[] arr1, int[] arr2)
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
        /// <summary>
        /// The built in intersection function
        /// </summary>
        /// <param name="arr1">first array</param>
        /// <param name="arr2">second array</param>
        public void Intersection(int[] arr1, int[] arr2)
        {
            var intersection = arr1.Intersect(arr2);

            foreach (var item in intersection)
            {
                Console.Write(item + " ");
            }
        }

        //Helper method for exercise 4 - Intersection
        /// <summary>
        /// Manual intersection (and sorted), wasnt sure which one you wanted
        /// </summary>
        /// <param name="arr1">first array</param>
        /// <param name="arr2">second array</param>
        public void IntersectionManual(int[] arr1, int[] arr2)
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
