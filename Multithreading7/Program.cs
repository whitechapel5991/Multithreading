using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var token = cts.Token;
            var task = Task.Factory.StartNew(() => 
            {
                Task.Delay(2000);
                token.ThrowIfCancellationRequested();
                Console.WriteLine($"Main task thread id : {Thread.CurrentThread.ManagedThreadId}");
                throw new Exception();
                return "parent task"; 
            }, token);
            cts.Cancel();

            await task.ContinueWith((result) => 
            {
                Console.WriteLine($"Success thread id : {Thread.CurrentThread.ManagedThreadId}");
                var str = result.IsCompletedSuccessfully ? result.Result : string.Empty;
                Console.WriteLine($"independ parent result {str}"); 
            });

            var faildTask = task.ContinueWith((result) => 
            {
                Console.WriteLine($"Fault task thread id : {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"fault!"); 
            }, TaskContinuationOptions.OnlyOnFaulted);

            if (!task.IsCanceled)
            {
                await task.ContinueWith((result) =>
                {
                    Console.WriteLine($"Fault synchronous task thread id : {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"fault in the same thread!");
                }, (TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted));
            }

            await task.ContinueWith((result) =>
            {
                Console.WriteLine($"Canceled task thread id : {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.LongRunning | TaskContinuationOptions.OnlyOnCanceled);
        }

        private static void MainTask(CancellationToken token)
        {

        }
    }

}
