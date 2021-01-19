using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {


        [TestMethod]
        public void FileLogger_Makes_Successful_Logs()
        {
            //Arrange
            string testPath = Path.GetRandomFileName();

            LogFactory logFactory = new LogFactory();
            logFactory.ConfigureFileLogger(testPath);

            var logger = logFactory.CreateLogger(nameof(FileLoggerTests));

            //Act
            logger.Log(LogLevel.Debug, "Testing");
            logger.Log(LogLevel.Warning, "Testing");
            logger.Debug("Msg: {0}", 34);

            //Assert
            string[] lines = File.ReadAllLines(testPath);
            File.Delete(testPath);
            Assert.AreEqual(3, lines.Length);
        }


    }
}
