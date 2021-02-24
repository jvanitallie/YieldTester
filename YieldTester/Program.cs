using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace YieldTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            
            sw.Restart();
            var myList1 = GetLazyListOfNumbers();
            Console.WriteLine($"Got Lazy List in {sw.ElapsedMilliseconds} ms");
            WaitForEnter();

            sw.Restart();
            var myList2 = myList1.ToList();
            Console.WriteLine($"Enumerated Lazy List in {sw.ElapsedMilliseconds} ms");
            WaitForEnter();

            sw.Restart();
            myList1 = GetNotLazyListOfNumbers();
            Console.WriteLine($"Got NonLazy List in {sw.ElapsedMilliseconds} ms");
            WaitForEnter();

            sw.Restart();
            myList2 = myList1.ToList();
            Console.WriteLine($"Enumerated NonLazy List in {sw.ElapsedMilliseconds} ms");
            WaitForEnter();

            sw.Restart();
            myList1 = GetLazyListOfNumbers();
            Console.WriteLine($"Got Lazy List in {sw.ElapsedMilliseconds} ms");

            WaitForEnter();
            EnumerateList(sw, myList1);
            WaitForEnter();
            EnumerateList(sw, myList1);

            WaitForEnter();
            sw.Restart();
            myList1 = GetNotLazyListOfNumbers();
            Console.WriteLine($"Got NonLazy List in {sw.ElapsedMilliseconds} ms");

            WaitForEnter();
            EnumerateList(sw, myList1);
            WaitForEnter();
            EnumerateList(sw, myList1);

        }

        private static void EnumerateList(Stopwatch sw, IEnumerable<long> myList1)
        {
            sw.Restart();
            foreach (var item in myList1)
            {
                Console.WriteLine($"Retrieved item {item} in {sw.ElapsedMilliseconds} ms");
                sw.Restart();
            }
        }



        public static IEnumerable<long> GetLazyListOfNumbers()
        {

            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(random.Next(1, 100));
                yield return i;
            }

        }


        public static IEnumerable<long> GetNotLazyListOfNumbers()
        {
            List<long> myList = new List<long>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(random.Next(1, 100));
                myList.Add(i);
            }
            return myList;

        }


        private static void WaitForEnter()
        {
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

    }
}
