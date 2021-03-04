using System;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment7
{
    [TestClass]
    public class Assignment7Tests
    {
        string[] urls = { "https://google.com", "https://google.com", "https://google.com", "https://google.com", "https://google.com", "https://google.com" };
        WebClient client = new WebClient();

        public int repetitionsCall(int repetitions)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            var progress = new Progress<int>(progressPercent => Console.WriteLine(progressPercent));
            int result = (Assignment7.DownloadTextRepeatedlyAsynch(progress, cancellationToken, repetitions, urls)).Result;
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
            Assert.IsTrue(repetitionsCall(1) > 0);
        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynch_ReturnsAccurateCountWithin10000Characters()
        {
            int characterExpected = urls.Aggregate(0, (total, next) => total + client.DownloadString(next).Length);
            characterExpected += urls.Aggregate(0, (total, next) => total + client.DownloadString(next).Length);

            Assert.IsTrue((repetitionsCall(2) - characterExpected) < 10000);
        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynch_Returns0WhenCanceledBeforCall()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel();
            var progress = new Progress<int>(progressPercent => Console.WriteLine(progressPercent));


            int result = (Assignment7.DownloadTextRepeatedlyAsynch(progress, cancellationToken, 100, urls)).Result;

            Assert.AreEqual<int>(0, result);
        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynch_GetsCancelledAfter10Milliseconds()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.CancelAfter(10);
            var progress = new Progress<int>(progressPercent => Console.WriteLine(progressPercent));

            int expected = (Assignment7.DownloadTextRepeatedlyAsynch(progress, cancellationToken, 5, urls)).Result;
            int result = (Assignment7.DownloadTextRepeatedlyAsynch(progress, cancellationToken, 5, urls)).Result;

            Assert.IsTrue(expected > result);

        }

        [TestMethod]
        public void DownloadTextRepeatedlyAsynch_ProgressGetsTriggeredEachRepetition()
        {
            int progressTriggered = 0;

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            var progress = new Progress<int>(progressPercent => progressTriggered++);

            
            int expected = (Assignment7.DownloadTextRepeatedlyAsynch(progress, cancellationToken, 3, urls)).Result;

            Assert.AreEqual<int>(3, progressTriggered);
        }
    }
}
