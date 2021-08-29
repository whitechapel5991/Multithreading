using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait1
{
    class Calc
    {
        public async Task PrintSumAsync(int n, CancellationToken token)
        {
            int result = 0;
            for (int i = 0; i < n; i++)
            {
                await Task.Delay(100);
                token.ThrowIfCancellationRequested();
                result += i;
            }

            Console.WriteLine($"Sum numbers {n} = {result}");
        }
    }
}
