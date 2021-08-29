using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading6
{
    class Program
    {
        public static List<int> _sharedCollection = new List<int>();
        static SemaphoreSlim _mutex = new SemaphoreSlim(1,1);
        static async Task Main(string[] args)
        {
            var comparingResult = DateTime.Compare(DateTime.Now.AddMinutes(-1), DateTime.Now);

            var task1 = Task.Factory.StartNew(FillCollection, TaskCreationOptions.AttachedToParent);
            var task2 = Task.Factory.StartNew(PrintCollection, TaskCreationOptions.AttachedToParent);
            await Task.WhenAll(task1, task2);
        }

        private static void FillCollection()
        {
            for (int i = 0; i < 10; i++)
            {
                _mutex.Wait();
                _sharedCollection.Add(i);
            }
        }

        private static void PrintCollection()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Console.WriteLine($"Count {_sharedCollection.Count}");
                    foreach (var item in _sharedCollection)
                    {
                        Console.WriteLine(item);
                    }
                }
                finally
                {
                    _mutex.Release();
                }
            }
        }

    }
}
