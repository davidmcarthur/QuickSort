using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int fib;
            TimeSpan span;
            // Using DateTime to show the speed differeneces between the normal process of 
            // calculating Fib numbers and calculating using QuickSort
            DateTime start = DateTime.Now;
            DateTime end;
            for (int i = 30; i < 40; i++)
            {
                fib = Fibonacci(i);
                Console.WriteLine($"Fib result was {fib}");
                
            }
            end = DateTime.Now;
            span = end - start;
            Console.WriteLine($"The Fib sequence took {span}");

            // Code to test our optimized method Fibonacci with Memoization
            start = DateTime.Now;
            for (int i = 30; i < 40; i++)
            {
                fib = FibonacciMem(i);
                Console.WriteLine($"Fib result was {fib}");
            }
            end = DateTime.Now;
            span = end - start;
            Console.WriteLine($"The optomized Fib sequence took {span}");
            Console.ReadLine();

        }

        /// <summary>
        /// User friendly method allows user to simply pass an array to sort
        /// </summary>
        /// <param name="arr">passed array from user.</param>
        public static void QSort(int[] arr)
        {
            QuickSortHelper(arr, 0, arr.Length-1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        private static void QuickSortHelper(int[] arr, int startIdx, int endIdx)
        {
            if(startIdx < endIdx) // only need to sort if at least 2 elements
            {
                // choose a pivot
                int pivot = Partition(arr, startIdx, endIdx);

                // recursively sort values on each side of pivot
                QuickSortHelper(arr, startIdx, pivot - 1);
                QuickSortHelper(arr, pivot + 1, endIdx);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        /// <returns></returns>
        private static int Partition(int[] arr, int startIdx, int endIdx)
        {
            int pivot = arr[endIdx];      // last value in arr is pivot value
            int k = startIdx - 1;
            int tmp;

            // loop to sort partition
            for (int i = startIdx; i < endIdx; i++)
            {
                if(arr[i] < pivot)
                {
                    k++;
                    // swap arr k with arr i
                    tmp = arr[i];
                    arr[i] = arr[k];
                    arr[k] = tmp;

                }
            }
            // move pivot in place
            k++;
            tmp = arr[endIdx];
            arr[endIdx] = arr[k];
            arr[k] = tmp;

            return k;
        }

        // creating a static Dictionary to store key values of the previously calculated 
        // Fib N numbers.
        public static Dictionary<int, int> calcVals = new Dictionary<int, int>();
        /// <summary>
        /// Fibonacci Memoization optimizes our brute force Fibonacci calculation using the
        /// Dictionary 
        /// </summary>
        /// <param number="n">Is the number of the Fib(N) that we are asking to calculate</param>
        /// <returns>The value of the Fibonacci number passed.</returns>
        public static int FibonacciMem(int n)
        {
            // This part is key to optimization. Rather than calculating each value every time it
            // is required to be calculated we check our Dict item calcVals to see if the item
            // has already been calculate using the key of the N number we want to calculate to 
            // retrieve the Dict value of the result (vice calculation).
            if (calcVals.Keys.Contains(n))
            {
                return calcVals[n];
            }
            else
            {
                // else it hasn't been calculated so we calculate using our brute force method
                if (n < 0)
                    throw new Exception("n cannot be 0");
                else if(n == 0)
                {
                    return 0;
                }
                else if(n == 1)
                {
                    return 1;
                }
                else
                {
                    int ret = Fibonacci(n - 1) + Fibonacci(n - 2);
                    calcVals[n] = ret;
                    return ret;
                }
            }
        }
        /// <summary>
        /// Brute Force Fibonacci method to calculate any Fib(N) number by calculating the value of Fib(n) for each
        /// layer in the Fib(N) number.
        /// </summary>
        /// <param number="n">Input number of Fibonacci to be calculated recursively</param>
        /// <returns>The value of the Fibonacci number passed.</returns>
        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new Exception("n cannot be 0");
            else if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else
            {
                int ret = Fibonacci(n - 1) + Fibonacci(n - 2);
                calcVals[n] = ret;
                return ret;
            }
        }


    }
}
/**** SAMPLE OUTPUT ****
 * Fib result was 832040
 * Fib result was 1346269
 * Fib result was 2178309
 * Fib result was 3524578
 * Fib result was 5702887
 * Fib result was 9227465
 * Fib result was 14930352
 * Fib result was 24157817
 * Fib result was 39088169
 * Fib result was 63245986
 * The Fib sequence took 00:00:08.1069157
 * 
 * Fib result was 832040
 * Fib result was 1346269
 * Fib result was 2178309
 * Fib result was 3524578
 * Fib result was 5702887
 * Fib result was 9227465
 * Fib result was 14930352
 * Fib result was 24157817
 * Fib result was 39088169
 * Fib result was 63245986
 * The optomized Fib sequence took 00:00:00.0081408
 */
