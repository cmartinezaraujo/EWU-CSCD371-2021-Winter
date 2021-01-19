using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests
{
    [TestClass]
    public class LogFactoryTests
    {

        [TestMethod]
        public void Logger_Returns_Null()
        {
            LogFactory logFactory = new LogFactory();

            BaseLogger testLogger = logFactory.CreateLogger(nameof(testLogger));

            Assert.IsNull(testLogger);
        }

        [TestMethod]
        public void Logger_Returns_Configured_Logger()
        {
            string testPath = Path.GetRandomFileName();

            LogFactory logFactory = new LogFactory();
            logFactory.ConfigureFileLogger(testPath);

            BaseLogger testLogger = logFactory.CreateLogger(nameof(testLogger));

            Assert.IsNotNull(testLogger);

        }

       [TestMethod]
       public void Create_Logger_ClassName_Set()
        {
            string testPath = Path.GetRandomFileName();

            LogFactory logFactory = new LogFactory();
            logFactory.ConfigureFileLogger(testPath);

            BaseLogger testLogger = logFactory.CreateLogger(nameof(testLogger));

            Assert.AreEqual(testLogger.ClassName, nameof(testLogger));
        }

    }
}
