using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Multithreading2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var task1 = await Task.Run(() => 
            {
                var random = new Random();
                var array = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    array.Add(random.Next(10));
                }

                Console.WriteLine("First task:");
                foreach (var item in array)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(Environment.NewLine);

                return array;
            }).ContinueWith(result => 
            {
                var random = new Random();
                var array = result.Result;

                Console.WriteLine("Second task:");
                Console.WriteLine("Random numbers:");
                for (int i = 0; i < 10; i++)
                {
                    var randomNumber = random.Next(10);
                    array[i] *= randomNumber;
                    Console.WriteLine($"[{i}] = {randomNumber}");
                }

                Console.WriteLine("Result:");
                foreach (var item in array)
                {
                    Console.WriteLine($"{item}");
                }
                Console.WriteLine(Environment.NewLine);

                return array;
            }, TaskContinuationOptions.OnlyOnRanToCompletion)
            .ContinueWith(result => 
            {
                var array = result.Result;
                array.Sort();

                Console.WriteLine("Third task:");
                foreach (var item in array)
                {
                    Console.WriteLine($"{item}");
                }
                Console.WriteLine(Environment.NewLine);
                return array;
            }, TaskContinuationOptions.OnlyOnRanToCompletion)
            .ContinueWith(result =>
            {
                var averageResult = result.Result.Average();
                Console.WriteLine("Fourth task:");
                Console.WriteLine($"{averageResult}");
                return averageResult;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
