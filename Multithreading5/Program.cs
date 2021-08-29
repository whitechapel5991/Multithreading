using System;
using System.Threading;

namespace Multithreading5
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateThreadRecursively(10);
        }

        private static void CreateThreadRecursively(object threadCount)
        {
            Semaphore semaphore = new Semaphore(1,1);
            semaphore.WaitOne();
            var currentThread = (int)threadCount;
            if (currentThread == 0)
            {
                return;
            }

            currentThread--;
            Console.WriteLine(currentThread);
            ThreadPool.QueueUserWorkItem(CreateThreadRecursively, currentThread);

            semaphore.Release();
        }
    }
}
