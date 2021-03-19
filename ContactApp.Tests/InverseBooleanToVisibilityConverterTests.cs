using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactApp.Tests
{
    [TestClass]
    public class InverseBooleanToVisibilityConverterTests
    {
        [TestMethod]

        public void Convert_ConvertsTrueToCollapsed()
        {
            var converter = new InverseBooleanToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert(true, null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Collapsed, visibility);
        }

        [TestMethod]

        public void Convert_ConvertsfalseToVisible()
        {
            var converter = new InverseBooleanToVisibilityConverter();

            Visibility visibility = (Visibility)converter.Convert(false, null, null, null);

            Assert.AreEqual<Visibility>(Visibility.Visible, visibility);
        }

        [TestMethod]

        public void ConvertBack_ConvertsVisibleToFalse()
        {
            var converter = new InverseBooleanToVisibilityConverter();

            bool visibility = (bool)converter.ConvertBack(Visibility.Visible, null, null, null);

            Assert.AreEqual<bool>(false, visibility);
        }

        [TestMethod]

        public void ConvertBack_ConvertsCollapsedToTrue()
        {
            var converter = new InverseBooleanToVisibilityConverter();

            bool visibility = (bool)converter.ConvertBack(Visibility.Collapsed, null, null, null);

            Assert.AreEqual<bool>(true, visibility);
        }
    }
}
