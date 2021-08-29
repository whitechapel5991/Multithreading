using System;
using System.Threading.Tasks;

namespace Multithreading3
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowCount1 = 5;
            int columnCount1 = 6;
            int columnCount2 = 4;

            var array1 = InitMatrix(rowCount1, columnCount1);
            var array2 = InitMatrix(columnCount1, columnCount2);

            PrintMatrix(array1, "Matrix 1");
            PrintMatrix(array2, "Matrix 2");

            var matARows = array1.GetLength(0);
            var matACols = array1.GetLength(1);
            var matBCols = array2.GetLength(1);
            var result = new double[matARows, matBCols];
            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += array1[i, k] * array2[k, j];
                    }
                    result[i, j] = temp;
                }
            });

            PrintMatrix(result, "Result");

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}
        }

        private static double[,] InitMatrix(int rowCount, int columnCount)
        {
            var random = new Random();
            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = random.Next(1, 10);
                }
            }

            return result;
        }

        private static void PrintMatrix(double[,] array, string message)
        {
            var matARows = array.GetLength(0);
            var matACols = array.GetLength(1);
            Console.WriteLine(message);
            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matACols; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine(Environment.NewLine);
        }
    }
}
