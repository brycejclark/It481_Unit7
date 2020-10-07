using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.CodeDom.Compiler;

namespace It481_Unit7
{
    class Sorting
    {

        private static Stopwatch stopwatch;

        private static bool debug = false;

        static void Main(String [] args)
        {
            //BubbleSort
            int type = 1;

            //Small
            int[] smallArray = getArray(10, 100);

            int[] newSmallArray = new int[smallArray.Length];
            Array.Copy(smallArray, 0, newSmallArray, 0, newSmallArray.Length);

            int[] quickSmallArray = new int[newSmallArray.Length];
            Array.Copy(newSmallArray, 0, quickSmallArray, 0, quickSmallArray.Length);

            String size = "small";
            runSortArray(smallArray, size, type);


            //Medium
            int[] mediumArray = getArray(1000, 100);

            int [] newMediumArray = new int[mediumArray.Length];
            Array.Copy(mediumArray, 0, newMediumArray, 0, newMediumArray.Length);

            int[] quickMediumArray = new int[newMediumArray.Length];
            Array.Copy(newMediumArray, 0, quickMediumArray, 0, quickMediumArray.Length);

            size = "medium";
            runSortArray(mediumArray, size, type);


            //Large
            int[] largeArray = getArray(10000, 100);

            int[] newLargeArray = new int[largeArray.Length];
            Array.Copy(largeArray, 0, newLargeArray, 0, newLargeArray.Length);

            int[] quickLargeArray = new int[newLargeArray.Length];
            Array.Copy(newLargeArray, 0, quickLargeArray, 0, quickLargeArray.Length);

            size = "large";
            runSortArray(largeArray, size, type);



            newSmallArray = onlyUniqueElements(newSmallArray);
            size = "new small unique";
            runSortArray(newSmallArray, size, type);

            newMediumArray = onlyUniqueElements(newMediumArray);
            size = "new medium unique";
            runSortArray(newMediumArray, size, type);

            newLargeArray = onlyUniqueElements(newLargeArray);
            size = "new large unique";
            runSortArray(newLargeArray, size, type);


            //Quicksort
            type = 2;

            size = "quick small";
            runSortArray(quickSmallArray, size, type);

            size = "quick medium";
            runSortArray(quickMediumArray, size, type);

            size = "quick large";
            runSortArray(quickLargeArray, size, type);

            Console.Read();
        }


        private static int[] getArray(int size, int randomMaxSize)
        {
            int[] myArray = new int[size];

            for (int i=0; i<myArray.Length; i++)
            {
                myArray[i] = GetRandomNumber(1, randomMaxSize);
            }
            return myArray;
        }

        private static void runSortArray(int[] arr, String size, int type)
        {
            long elapsedTime = 0;

            String sort = null;

            if(type == 1)
            {
                sort = "bubble";
            }
            else if(type == 2)
            {
                sort = "quick";
            }

            if(debug)
            {
                Console.WriteLine("Array before the " + sort + " sort");
                for(int i=0; i<arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }

            stopwatch = Stopwatch.StartNew();

            if(type == 1)
            {
                bubbleSort(arr);
            }
            else if(type == 2)
            {
                int low = 0;
                int high = arr.Length - 1;
                sortAsc(arr, low, high);
            }
            Console.WriteLine();

            if (debug)
            {
                Console.WriteLine("Array after the " + sort + " sort");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }

            stopwatch.Stop();
            elapsedTime = stopwatch.ElapsedTicks;

            long frequency = Stopwatch.Frequency;

            long nanosecondPerTick = (1000L * 1000L * 1000L) / frequency;
            elapsedTime = elapsedTime * nanosecondPerTick;

            Console.WriteLine("\n");
            Console.WriteLine("The run time is for the " + size + " array in nanoseconds is " + elapsedTime);

            Console.WriteLine("\n\n");
        }

        private static void bubbleSort(int[] intArray)
        {
            int temp = 0;

            for(int i=0; i<intArray.Length; i++)
            {
                for(int j = 0; j<intArray.Length-1; j++)
                {
                    if(intArray[j]>intArray[j+1])
                    {
                        temp = intArray[j + 1];
                        intArray[j + 1] = intArray[j];
                        intArray[j] = temp;
                    }
                }
            }
        }

        private static int[] onlyUniqueElements(int[] inputArray)
        {
            HashSet<int> set = new HashSet<int>();

            int[] tmp = new int[inputArray.Length];
            int index = 0;

            foreach (int i in inputArray)
                if (set.Add(i))
                    tmp[index++] = 1;

            return set.ToArray();
        }

        public static void sortAsc(int[] x, int low, int high)
        {
            if (x == null || x.Length == 0)
                return;

            if (low >= high)
                return;

            int middle = low + (high - low) / 2;
            int pivot = x[middle];
            int i = low, j = high;

            while(i <= j )
            {
                while(x[i]<pivot)
                {
                    i++;
                }
                while(x[j]>pivot)
                {
                    j--;
                }
                if(i<=j)
                {
                    int temp = x[i];
                    x[i] = x[j];
                    x[j] = temp;
                    i++;
                    j--;
                }
            }

            if (low < j)
                sortAsc(x, low, j);
            if (high > i)
                sortAsc(x, i, high);
        }

        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
    }
}
