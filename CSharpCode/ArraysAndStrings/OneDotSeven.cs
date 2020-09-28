using System;
using System.Collections.Generic;
using System.Text;

namespace ArraysAndStrings
{
    public class OneDotSeven
    {
        //L'idea è che X[i, j] = Xrot[j, N -1 - i]
        public void Run()
        {
            int N = 3;
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5,  6}, { 7, 8, 9 } };
            int[,] newmatrix = new int[N,N]; // { { 1, 2, 3 }, { 4, 5,  6}, { 7, 8, 9 } };

            //for (int i = 0; i < N; i++)
            //{
            //    for (int j = 0; j < N; j++)
            //    {
            //        newmatrix[j, N - 1 - i] = matrix[i, j];
            //    }
            //}

            for (int j = 0; j < N - 1; j++)
            {
                int item = matrix[0, j];
                var nextCell = matrix[j, N - 1 - 0];
                matrix[j, N - 1 - 0] = item;

                var nextCell2 = matrix[N - 1 - 0, N - 1 - j];
                matrix[N - 1 - 0, N - 1 - j] = nextCell;

                var nextCell3 = matrix[N - 1 - j, N - 1 - (N - 1 - 0)];
                matrix[N - 1 - j, N - 1 - (N - 1 - 0)] = nextCell2;

                var nextCell4 = matrix[N - 1 - (N - 1 - 0), N - 1 - (N - 1 - j)];
                matrix[N - 1 - (N - 1 - 0), N - 1 - (N - 1 - j)] = nextCell3;
            }

            Console.ReadLine();
        }
    }
}
