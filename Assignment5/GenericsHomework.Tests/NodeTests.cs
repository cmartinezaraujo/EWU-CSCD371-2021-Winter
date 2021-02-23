using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericsHomework.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NodeConstructor_ValueIsSet()
        {
            Node<int> nodeOne = new Node<int>(24);
            Node<string> nodeTwo = new Node<string>("Hello");
            Node<string> node = new Node<string>(null);

            Assert.AreEqual<int>(24, nodeOne.Value);
            Assert.AreEqual<string>("Hello", nodeTwo.Value);
            Assert.AreEqual<string>(null, node.Value);
        }

        [TestMethod]
        public void NodeToStringReturnsCorrectValueAsString()
        {
            int integer = 24;
            string word = "Hello";

            Node<int> nodeOne = new Node<int>(integer);
            Node<string> nodeTwo = new Node<string>(word);

            Assert.AreEqual<string>(integer.ToString(), nodeOne.ToString());
            Assert.AreEqual<string>(word, nodeTwo.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NodeToStringThrowsExceptionWithNullValue()
        {
            Node<string> node = new Node<string>(null);
            node.ToString();
        }

        [TestMethod]

        public void Verify_NextPropertyOnConstructedNode()
        {
            Node<int> node = new Node<int>(25);

            Node<int> nodeReference = node.Next;

            Assert.AreEqual<Node<int>>(node, nodeReference);
        }

        [TestMethod]
        public void Verify_NextPropertyChecksReference()
        {
            Node<int> node = new Node<int>(25);

            Node<int> differentNodeReference = new Node<int>(25);

            Assert.AreNotEqual<Node<int>>(node, differentNodeReference);
        }

        [TestMethod]

        public void NodeInsertsNewNodeToListAndLoopsBack()
        {
            Node<string> node = new Node<string>("StringOne");

            node.Insert("StringTwo");

            Assert.AreEqual<string>("StringOne", node.ToString());
            Assert.AreEqual<string>("StringTwo", node.Next.ToString());
            Assert.AreEqual<string>("StringOne", node.Next.Next.ToString());

        }

        [TestMethod]

        public void CallingClearOnCurrentNodeClearsAllOtherNodes()
        {
            Node<string> node = new Node<string>("One");

            node.Insert("Two");
            node.Insert("Three");
            node.Insert("Four");
            node.Insert("Five");

            node.Clear();

            Assert.AreEqual<string>("One", node.Value);
            Assert.AreEqual<string>("One", node.Next.Value);
        }

        [TestMethod]
        public void GetEnumerator_ReturnsWorkingEnumerator()
        {
            Node<string> node = new Node<string>("One");

            node.Insert("Two");
            node.Insert("Three");
            node.Insert("Four");
            node.Insert("Five");

            int count = 0;

            foreach(string data in node)
            {
                count++;
            }

            Assert.AreEqual<int>(count, 5);
        }

        [TestMethod]
        public void childItems_ReturnsOnlyThreeChildrenEnumerable()
        {
            Node<string> node = new Node<string>("Six");

            node.Insert("Five");
            node.Insert("Four");
            node.Insert("Three");
            node.Insert("Two");
            node.Insert("One");

            List<string> numbers = new List<string>();

            foreach(string number in node.childItems(3))
            {
                numbers.Add(number);
            }

            Assert.IsTrue(numbers.Contains("One"));
            Assert.IsTrue(numbers.Contains("Two"));
            Assert.IsTrue(numbers.Contains("Three"));
            Assert.AreEqual<int>(3, numbers.Count());

        }


    }
}
