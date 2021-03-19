using System.Windows;
using System.Windows.Data;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactApp.Tests
{
    [TestClass]

    public class EmptyStringToVisibilityConverterTests
    {
        [TestMethod]

        public void Convert_PassedInEmptyStringConvertsToCollapsed()
        {
            var converter = new EmptyStringToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert("", null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Collapsed, visibility);
        }

        [TestMethod]
        public void Convert_PassedInNullStringConvertsToCollapsed()
        {
            var converter = new EmptyStringToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert(null, null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Collapsed, visibility);
        }

        [TestMethod]
        public void Convert_PassedInValidStringConvertsToVisible()
        {
            var converter = new EmptyStringToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert("Test", null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Visible, visibility);
        }
    }
}
