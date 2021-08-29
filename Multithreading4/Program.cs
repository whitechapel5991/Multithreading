using System;
using System.Threading;

namespace Multithreading4
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(CreateThreadRecursively);
            thread.Start(10);
            thread.Join();
        }

        private static void CreateThreadRecursively(object threadCount)
        {
            var currentThread = (int)threadCount;
            if (currentThread == 0)
            {
                return;
            }

            currentThread--;
            Console.WriteLine(currentThread);
            Thread thread = new Thread(CreateThreadRecursively);
            thread.Start(currentThread);
            thread.Join();
        }
    }
}
