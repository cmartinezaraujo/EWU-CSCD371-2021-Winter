using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactApp.Tests
{
    [TestClass]
    public class BooleanToVisibilityConverterTests
    {
        [TestMethod]

        public void Convert_ConvertsFalseToCollapsed()
        {
            var converter = new BooleanToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert(false, null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Collapsed, visibility);
        }

        [TestMethod]

        public void Convert_ConvertsTrueToVisible()
        {
            var converter = new BooleanToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert(true, null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Visible, visibility);
        }

        [TestMethod]

        public void ConvertBack_ConvertsVisibleToTrue()
        {
            var converter = new BooleanToVisibilityConverter();

            bool visibility = (bool)converter.ConvertBack(Visibility.Visible, null, null, null);

            Assert.AreEqual<bool>(true, visibility);
        }

        [TestMethod]

        public void ConvertBack_ConvertsCollapsedToFalse()
        {
            var converter = new BooleanToVisibilityConverter();

            bool visibility = (bool)converter.ConvertBack(Visibility.Collapsed, null, null, null);

            Assert.AreEqual<bool>(false, visibility);
        }
    }

}
