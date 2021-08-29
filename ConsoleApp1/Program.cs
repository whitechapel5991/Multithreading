using System;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 100;
            Func<object, int> action = (object obj) =>
            {
                int i = (int)obj;
                for (int j = 0; j < 1000; j++)
                {
                    Console.WriteLine($"Task #{i} - {j}");
                }

                return i;
            };

            Task<int>[] taskArray = new Task<int>[count];
            for (int i = 0; i < count; i++)
            {
                int index = i;
                taskArray[i] = Task<int>.Factory.StartNew(action, index);
            }

            Task.WaitAll(taskArray);
            
            Console.WriteLine("Hello World!");
        }
    }
}
