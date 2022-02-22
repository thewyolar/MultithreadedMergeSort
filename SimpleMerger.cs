using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadedMergeSort
{
    class SimpleMerger
    {
        private int[] sorted;

        public SimpleMerger(int[] unsorted)
        {
            this.sorted = unsorted;
        }

        public int[] Sorted() => this.sorted;

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

            for (int i = right; i <= highIndex; i++)
            {
                tempArray[index] = sorted[i];
                index++;
            }

            for (int i = 0; i < tempArray.Length; i++)
            {
                sorted[lowIndex + i] = tempArray[i];
            }
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

        public void MergeSort()
        {
            MergeSort(0, sorted.Length - 1);
        }
    }
}
