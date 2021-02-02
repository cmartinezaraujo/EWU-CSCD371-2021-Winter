using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberSet.Writer;

namespace NumberSet.Tests
{
    [TestClass]
    public class SetWriterTests
    {
        [TestMethod]

        [ExpectedException(typeof(ArgumentNullException))]
        public void SetWriterConstructor_PassedInNullFile()
        {
            SetWriter writer = new SetWriter(null!);
        }

        [TestMethod]

        public void SetWriterConstructor_MakesFileWithStreamWriter()
        {
            string filePath = Path.GetTempFileName();
            SetWriter writer = new SetWriter(filePath);

            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]

        public void WriteSet_WritesPassedInSetToFile()
        {
            string filePath = Path.GetTempFileName();
            NumSet set = new NumSet(1, 2, 3, 4, 5, 6);

            using (SetWriter writer = new SetWriter(filePath))
            {
                writer.writeSet(set);
            }

            Assert.AreEqual(File.ReadAllText(filePath).Trim(), set.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Dispose_ThrowsExceptionWritingToClosedStrewamWriter()
        {
            string filePath = Path.GetTempFileName();
            SetWriter writer = new SetWriter(filePath);
            NumSet set = new NumSet(1, 2, 3, 4, 5, 6);

            writer.Dispose();

            writer.writeSet(set);
        }



    }
}
