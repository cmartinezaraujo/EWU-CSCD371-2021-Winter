using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment7
{
    public class Assignment7
    {
        public static async Task<int> DownloadTextAsync(params string[] urls)
        {

            WebClient client = new WebClient();

            return await Task.Run(() => urls.Aggregate(0, (total, next) => total + client.DownloadString(next).Length));
        }

        public static async Task<int> DownloadTextRepeatedlyAsynch(IProgress<int> progress, ParallelOptions parallelOptions, int repetitions, params string[] urls)
        {
            object sync = new Object();

            return await Task.Run(() =>
            {
                int current = 0;
                int total = 0;

                Parallel.For(0, repetitions, parallelOptions, index =>
                {
                    parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                    lock (sync)
                    {
                        total += DownloadTextAsync(urls).Result;
                    }
                    current++;
                    progress.Report((current * 100) / repetitions);
                });
                return total;
            });
        }
    }
}
