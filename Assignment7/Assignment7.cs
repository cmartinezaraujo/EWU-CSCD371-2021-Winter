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

        public static async Task<int> DownloadTextRepeatedlyAsynch(IProgress<int> progress, CancellationToken cancellationToken, int repetitions, params string[] urls)
        {

            return await Task.Run(async () =>
            {
                //int progress = 0;
                int total = 0;
                for (int i = 0; i < repetitions && !(cancellationToken.IsCancellationRequested); i++)
                {
                    total += await DownloadTextAsync(urls);
                    progress.Report((i*100) / repetitions);
                }
                return total;
            });
        }
    }
}
