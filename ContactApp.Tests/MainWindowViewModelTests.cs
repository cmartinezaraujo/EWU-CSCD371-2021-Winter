using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactApp.Tests
{
    [TestClass]

    public class MainWindowViewModelTests
    {
        [TestMethod]

        public void OnNewContact_AddsNewContactToList()
        {
            var vm = new MainWindowViewModel();

            int before = vm.Contacts.Count;

            vm.NewContactCommand.Execute(null);

            Assert.AreEqual<int>(before + 1, vm.Contacts.Count);
        }

    }
}
