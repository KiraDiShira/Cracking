using System;
using System.Collections.Generic;
using System.Text;

namespace ArraysAndStrings
{
    public class OneDotEight
    {
        public void Run()
        {
            const int M = 3;
            const int N = 3;
                
            int[,] matrix = new int[M,N] { { 1, 2, 0 }, { 4, 5, 6 }, { 7, 8, 9 } };

            ISet<int> zeroRows = new HashSet<int>();
            ISet<int> zeroColumns = new HashSet<int>();
                
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if(matrix[i, j] == 0)
                    {
                        zeroRows.Add(i);
                        zeroColumns.Add(j);
                    }
                }
            }

            foreach (var row in zeroRows)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[row, j] = 0;
                }
            }

            foreach (var column in zeroColumns)
            {
                for (int i = 0; i < M; i++)
                {
                    matrix[i, column] = 0;
                }
            }            

            Console.ReadLine();
        }
    }
}
