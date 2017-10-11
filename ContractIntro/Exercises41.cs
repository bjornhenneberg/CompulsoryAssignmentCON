using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractIntro
{
    class Exercises41
    {
        public void RunMenu()
        {
            var running = true;
            do
            {
                DisplayMenu();
                var input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        Program.RunMenu();
                        break;
                    case 1:
                        Exercise3();
                        break;
                }
            } while (running);

        }

        /// <summary>
        /// Simply displays the menu - purely visual
        /// </summary>
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("| Compulsory Assignment 1");
            Console.WriteLine("| [1] Exercise 3 - Limited Stack.");
            Console.WriteLine("|");
            Console.WriteLine("| [0] Back");
            Console.WriteLine();
        }

        public void Exercise3()
        {
            Console.Clear();
            Console.WriteLine("----- Exercise 3 -----");
            Random rand = new Random();

            FixedSizedQueue<int> list = new FixedSizedQueue<int>();
            list.Limit = 25;
            int amountofusers = 1000;
            Console.WriteLine("Getting {0} ints...",amountofusers);

            for (int i = 0; i < amountofusers; i++)
            {
                list.Enqueue(i);
            }

            list.PushTo(1000, 0);

            foreach (var item in list.q)
            {
                Console.WriteLine("Item: {0}", item);
            }
            Console.WriteLine("{0} ints in final list with limit {1}",list.q.Count,list.Limit);
            Console.ReadLine();
        }

        public class FixedSizedQueue<T>
        {
            public ConcurrentQueue<T> q = new ConcurrentQueue<T>();
            private object lockObject = new object();

            public int Limit { get; set; }
            public void Enqueue(T obj)
            {
                q.Enqueue(obj);
                lock (lockObject)
                {
                    T overflow;
                    while (q.Count > Limit && q.TryDequeue(out overflow)) ;
                }
            }
            public void PushTo(T obj, int index = 0)
            {
                var list = q.ToArray();
                T temp;
                temp = list[index];
                list[index] = obj;
                ConcurrentQueue<T> queue = new ConcurrentQueue<T>(list);
                q = queue;
            }
        }
    }
}
