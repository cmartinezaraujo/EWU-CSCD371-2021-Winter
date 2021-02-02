using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberSet.Tests
{
    [TestClass]
    public class NumberSetTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NumSetConstructor_PassedNullParamsArray()
        {
            NumSet nums = new NumSet(null);

        }

        [TestMethod]
        public void ToString_ReturnsHashSetAsAString()
        {
            NumSet nums = new NumSet(1, 2, 3, 3, 2, 1);

            string expected = "1,2,3";
            string returned = nums.ToString();

            Assert.AreEqual(expected, returned);
        }

        [TestMethod]

        public void Equals_ReturnsFalse_OnNonMatchingSets()
        {
            NumSet nums1 = new NumSet(1, 2, 3, 4, 5);

            NumSet nums2 = new NumSet(1, 2, 3, 4);

            Assert.IsFalse(nums1.Equals(nums2));
        }

        [TestMethod]
        public void Equals_ReturnsTrue_OnNonMatchingSets()
        {
            NumSet nums1 = new NumSet(1, 2, 3, 3, 2, 1);

            NumSet nums2 = new NumSet(3, 2, 1);

            Assert.IsTrue(nums1.Equals(nums2));
        }

        [TestMethod]

        public void GetHashCode_ReturnsSameHashCode()
        {
            NumSet nums1 = new NumSet(1, 2, 3);

            NumSet nums2 = new NumSet(3, 2, 1);

            int hashOne = nums1.GetHashCode();
            int hashTwo = nums2.GetHashCode();

            Assert.AreEqual(hashOne, hashTwo);

        }

        [TestMethod]

        public void EuqalityOperator_ReturnsTrue_WithEqualSets()
        {
            NumSet nums1 = new NumSet(1, 2, 3, 3, 2, 1);

            NumSet nums2 = new NumSet(3, 2, 1);

            Assert.IsTrue(nums1==nums2);
        }

        [TestMethod]
        public void EuqalityOperator_ReturnsFalse_WithNonMatchingSets()
        {
            NumSet nums1 = new NumSet(1, 2, 3, 3, 2, 1);

            NumSet nums2 = new NumSet(3, 2, 1, 88);

            Assert.IsFalse(nums1==nums2);
        }

        [TestMethod]

        public void NotEqualOperator_ReturnsTrue_WithNonMatchingSets()
        {
            NumSet nums1 = new NumSet(1, 2, 3, 3, 2, 1);

            NumSet nums2 = new NumSet(3, 2, 1, 88);

            Assert.IsTrue(nums1!=nums2);
        }

        [TestMethod]

        public void NotEqualOperator_ReturnsFalse_WithMatchingSets()
        {
            NumSet nums1 = new NumSet(1, 2, 3, 3, 2, 1);

            NumSet nums2 = new NumSet(3, 2, 1);

            Assert.IsFalse(nums1!=nums2);
        }

        [TestMethod]
        public void ReturnArray_ReturnsSetAsArray()
        {
            NumSet tempSet = new NumSet(1, 2, 3, 3, 2, 1);

            int[] setArray1 = tempSet.returnArray();
            int[] setArray2 = {1, 2, 3};

            for(int x = 0; x < setArray1.Length; x++)
            {
                Assert.AreEqual(setArray1[x], setArray2[x]);
            }

            Assert.AreEqual(setArray1.Length, setArray2.Length);

        }

        [TestMethod]

        public void ReturnArray_ModifyingReturnedArray_DoesNotModifySet()
        {
            NumSet tempSet1 = new NumSet(1, 2, 3, 3, 2, 1);
            NumSet tempSet2 = new NumSet(1, 2, 3, 3, 2, 1);

            int[] setArray = tempSet1.returnArray();
            setArray[0] = 500;
            setArray[1] = 60;
            setArray[2] = 800;

            Assert.IsTrue(tempSet1 == tempSet2);
        }








    }
}
