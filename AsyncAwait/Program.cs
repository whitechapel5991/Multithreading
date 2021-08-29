using AsyncAwait1;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            var nmHelper = new NumberHelper();
            var calc = new Calc();
            Task task = null;
            
            while (true)
            {
                nmHelper.TypeNumber();
                if (task != null && !task.IsCompleted)
                {
                    cts.Cancel();
                    cts.Dispose();
                    cts = new CancellationTokenSource();
                    token = cts.Token;
                    Console.Clear();
                }
                task = calc.PrintSumAsync(nmHelper.Number, token);
            }
        }
    }
}
