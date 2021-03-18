using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactApp.Tests
{
    [TestClass]

    public class MainWindowViewModelTests
    {
        [TestMethod]

        public void NewContactCommand_AddsNewContactToList()
        {
            var viewModel = new MainWindowViewModel();

            int before = viewModel.Contacts.Count;

            viewModel.NewContactCommand.Execute(null);

            Assert.AreEqual<int>(before + 1, viewModel.Contacts.Count);
        }

        [TestMethod]
        public void DeleteContactCommand_RemovesSelectedContactFromList()
        {
            var viewModel = new MainWindowViewModel();

            int before = viewModel.Contacts.Count;

            var contact = viewModel.Contacts.Last();

            viewModel.SelectedContact = contact;

            viewModel.DeleteContactCommand.Execute(null);

            Assert.AreEqual<int>(before - 1, viewModel.Contacts.Count);
            Assert.IsFalse(viewModel.Contacts.Contains(contact));
        }

        [TestMethod]

        public void EditContactCommand_SetsIsBeingEditedToTrue()
        {
            var vieModel = new MainWindowViewModel();

            vieModel.IsBeingEdited = false;

            vieModel.EditContactCommand.Execute(null);

            Assert.IsTrue(vieModel.IsBeingEdited);
        }

        [TestMethod]
        public void SaveContactCommand_SetsIsBeingEditedToFalse()
        {
            var viewModel = new MainWindowViewModel();

            viewModel.IsBeingEdited = true;

            viewModel.SelectedContact = viewModel.Contacts.First();

            viewModel.SaveContactCommand.Execute(null);

            Assert.IsFalse(viewModel.IsBeingEdited);
        }

        [TestMethod]
        public void SaveContactCommand_UpdatesLastModifiedDate()
        {
            var viewModel = new MainWindowViewModel();

            viewModel.SelectedContact = viewModel.Contacts.First();

            DateTime originalDate = DateTime.Now;

            viewModel.SelectedContact = viewModel.Contacts.First();
            viewModel.SelectedContact.LastModified = originalDate;

            viewModel.SaveContactCommand.Execute(null);

            Assert.AreNotEqual<DateTime>(originalDate, viewModel.SelectedContact.LastModified);
        }

    }
}
