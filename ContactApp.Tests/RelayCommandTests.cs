using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void CanExecuteDelegate_CanExecuteReturnsFalseWhenNotAbleToExecute()
        {
            RelayCommand relayCommand = new RelayCommand(() => System.Console.WriteLine("Test") , () => false);

            Assert.IsFalse(relayCommand.CanExecute(null));
        }

        [TestMethod]

        public void CanExecuteDelegate_CanExecuteReturnsTrueWhenNotAbleToExecute()
        {
            RelayCommand relayCommand = new RelayCommand(() => System.Console.WriteLine("Test"), () => true);

            Assert.IsTrue(relayCommand.CanExecute(null));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Execute_WillExecuteAndThrowAnException()
        {
            RelayCommand relayCommand = new RelayCommand(() => throw new NotImplementedException(), () => true);
            relayCommand.Execute(null);
        }
    }
}
