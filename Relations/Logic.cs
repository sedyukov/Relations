using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations
{
    class Logic
    {
        bool[,] m;
        int n;
        int[] counts;
        int max;
        List<int> maxCounts;
        List<int> result = new List<int>();
        List<int> prevs;
        public Logic(bool[,] m, int n)
        {
            this.m = m;
            this.n = n;
            counts = new int[n];
            maxCounts = new List<int>();
            for (int i = 0; i < n; i++)
            {
                counts[i] = -1;
            }
        }

        private void printMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine(m[i, j]);
                }
            }
        }
        private bool calcRow(int col, int prev)
        {
            prevs.Add(col);
            counts[col] = 0;
            for (int i = 0; i < n; i++)
            {
                if (m[i, col])
                {
                    for (int j = 0; j < prevs.Count; j++)
                    {
                        if (i == prevs[j]) return false;
                    }
                    if (!calcRow(i, col)) return false;
                    counts[col] += 1 + counts[i];
                }
            }
            return true;
        }
        public bool calcMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                prevs = new List<int>();
                if (!calcRow(i, i)) return false;
            }
            return true;
        }

        private void findMaxCount()
        {
            max = counts[0];
            for (int i = 1; i < n; i++)
            {
                if (counts[i] > max)
                {
                    max = counts[i];
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (counts[i] == max)
                {
                    maxCounts.Add(i);
                }
            }
        }

        private void printResCol(int col, int prev)
        {
            Console.WriteLine(col);
            for (int i = 0; i < n; i++)
            {
                if (i == prev) continue;
                if (m[i, col])
                {
                    printResCol(i, col);
                }
            }
        }

        private void getResCol(int col, int prev)
        {
            result.Add(col);
            for (int i = 0; i < n; i++)
            {
                if (i == prev) continue;
                if (m[i, col])
                {
                    getResCol(i, col);
                }
            }
        }

        private void printRes()
        {
            calcMatrix();
            findMaxCount();
            for (int i = 0; i < maxCounts.Count; i++)
            {
                printResCol(maxCounts[i], maxCounts[i]);
                Console.WriteLine();
            }
        }

        public List<int> getResult()
        {
            findMaxCount();
            for (int i = 0; i < maxCounts.Count; i++)
            {
                result.Add(-1);
                getResCol(maxCounts[i], maxCounts[i]);
            }
            result.Add(-1);
            return result;
        }

    }

        
}
