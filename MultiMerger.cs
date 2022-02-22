using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadedMergeSort
{
    class MultiMerger
    {
        public int[] sorted { get; private set; }

        public MultiMerger(int[] unsorted)
        {
            this.sorted = unsorted;
        }

        //метод для слияния массивов
        public void Merge(int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            int[] tempArray = new int[highIndex - lowIndex + 1];
            int index = 0;

            while (left <= middleIndex && right <= highIndex)
            {
                if (sorted[left] < sorted[right])
                {
                    tempArray[index] = sorted[left];
                    left++;
                }
                else
                {
                    tempArray[index] = sorted[right];
                    right++;
                }

                index++;
            }

            for (int i = left; i <= middleIndex; i++)
            {
                tempArray[index] = sorted[i];
                index++;
            }

            for (int j = right; j <= highIndex; j++)
            {
                tempArray[index] = sorted[j];
                index++;
            }

            for (int i = 0; i < tempArray.Length; i++)
                sorted[lowIndex + i] = tempArray[i];
        }

        //однопоточная сортировка слиянием
        public void MergeSort(int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(lowIndex, middleIndex);
                MergeSort(middleIndex + 1, highIndex);
                Merge(lowIndex, middleIndex, highIndex);
            }
        }

        //многопоточная сортировка слиянием
        public void MergeSortMT(int lowIndex, int highIndex, int mtCount)
        {
            int middleIndex = (lowIndex + highIndex) / 2;

            if (lowIndex < highIndex)
            {
                if (mtCount > 0)
                {
                    Task t = Task.Run(() => MergeSortMT(lowIndex, middleIndex, mtCount - 1));

                    MergeSortMT(middleIndex + 1, highIndex, mtCount - 1);

                    t.Wait();
                }
                else
                {
                    MergeSort(lowIndex, middleIndex);
                    MergeSort(middleIndex + 1, highIndex);
                }

                Merge(lowIndex, middleIndex, highIndex);
            }
        }

        public void MergeSortMT(int mtCount)
        {
            MergeSortMT(0, sorted.Length - 1, mtCount);
        }
    }
}
