using System;
using System.Diagnostics;

namespace MultithreadedMergeSort
{
    class Program
    {
        public static void Test(int length)
        {
            Console.WriteLine();

            int[] array1 = new int[length];
            Generate(array1);
            int[] array2 = new int[array1.Length];
            Array.Copy(array1, array2, array1.Length);

            Console.WriteLine($"Тест. Кол-во элементов {array1.Length}:");
            Stopwatch sw = new Stopwatch();

            SimpleMerger simpleMerger = new SimpleMerger(array1);
            sw.Start();
            simpleMerger.MergeSort();
            sw.Stop();
            Console.WriteLine("[Merge Sort] Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms");

            MultiMerger multiMerger = new MultiMerger(array2);
            /*for (int i = 1; i < 13; i++)
            {
                sw.Restart();
                multiMerger.MergeSortMT(i);
                sw.Stop();
                Console.WriteLine($"Кол-во потоков: {i}");
                Console.WriteLine("[Multithreading Merge Sort] Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms");
            }*/
            sw.Restart();
            multiMerger.MergeSortMT(6);
            sw.Stop();
            Console.WriteLine("[Multithreading Merge Sort] Время, затраченное на выполнение: " + sw.ElapsedMilliseconds + "ms");
        }

        public static void Generate(int[] array)
        {
            Random r = new Random();

            for (int i = 0; i < array.Length; i++)
                array[i] = r.Next(0, 10000);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запуск тестов.");

            Test(1000);
            Test(10000);
            Test(100000);
            Test(1000000);
            Test(10000000);

            Console.WriteLine();
            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
