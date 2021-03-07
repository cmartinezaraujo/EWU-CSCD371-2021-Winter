using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment7
{
    [TestClass]
    public class Assignment7Tests
    {
        string[] urls = { "https://google.com", "https://google.com", "https://google.com", "https://google.com", "https://google.com", "https://google.com" };
        WebClient client = new WebClient();

        public int RepetitionsCallHelper(int repetitions)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;

            var progress = new Progress<int>(progressPercent => Console.WriteLine(progressPercent));
            int result = (Assignment7.DownloadTextRepeatedlyAsynch(progress, parallelOptions, repetitions, urls)).Result;
            return result;
        }

        [TestMethod]
        public void DownloadTextAsyncCountsCharacter_ReturnsMoreThan0()
        {
            Assert.IsTrue(Assignment7.DownloadTextAsync("https://google.com").Result > 0);
        }

        [TestMethod]
        public void DownloadTextAsyncCounts_ReturnsAccurateCountWithin1000Characters()
        {
            int characterExpected = urls.Aggregate(0, (total, next) => total + client.DownloadString(next).Length);

            Assert.IsTrue((Assignment7.DownloadTextAsync(urls).Result-characterExpected) < 1000);
        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynchCountsCharacters_ReturnsMoreThan0()
        {
            Assert.IsTrue(RepetitionsCallHelper(1) > 0);
        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynch_ReturnsAccurateCountWithin10000Characters()
        {
            int characterExpected = urls.Aggregate(0, (total, next) => total + client.DownloadString(next).Length);
            characterExpected += urls.Aggregate(0, (total, next) => total + client.DownloadString(next).Length);

            Assert.IsTrue((RepetitionsCallHelper(2) - characterExpected) < 10000);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DownloadTextRepeatedlyAsynch_Returns0WhenCanceledBeforCall()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel();

            var progress = new Progress<int>(progressPercent => Console.WriteLine(progressPercent));


            int result = (Assignment7.DownloadTextRepeatedlyAsynch(progress, parallelOptions, 100, urls)).Result;

            Assert.AreEqual<int>(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DownloadTextRepeatedlyAsynch_GetsCancelledAfter10Milliseconds()
        {
            CancellationTokenSource cancellationTokenSource1 = new CancellationTokenSource();
            CancellationTokenSource cancellationTokenSource2 = new CancellationTokenSource();

            ParallelOptions parallelOptions1 = new ParallelOptions();
            parallelOptions1.CancellationToken = cancellationTokenSource1.Token;

            ParallelOptions parallelOptions2 = new ParallelOptions();
            parallelOptions2.CancellationToken = cancellationTokenSource2.Token;


            cancellationTokenSource2.CancelAfter(10);

            var progress = new Progress<int>(progressPercent => Console.WriteLine(progressPercent));

            int expected = (Assignment7.DownloadTextRepeatedlyAsynch(progress, parallelOptions1, 5, urls)).Result;
            int result = (Assignment7.DownloadTextRepeatedlyAsynch(progress, parallelOptions2, 5, urls)).Result;

            Assert.IsTrue(expected > result);

        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynch_ProgressGetsTriggeredEachRepetition()
        {
            int progressTriggered = 0;

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;

            var progress = new Progress<int>(progressPercent => progressTriggered++);

            
            int expected = (Assignment7.DownloadTextRepeatedlyAsynch(progress, parallelOptions, 3, urls)).Result;

            Assert.AreEqual<int>(3, progressTriggered);
        }
    }
}
