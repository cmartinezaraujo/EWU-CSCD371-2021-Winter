using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ContactApp.Tests
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RelayCommand_PassedNullValues()
        {
            RelayCommand NullCommand = new RelayCommand(null, null);
        }

    }

}
