using System;
using GenericsHomework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Node.Tests
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

        public void NodeNextPropertySetsNewNodesNextToItself()
        {
            Node<int> node = new Node<int>(25);

            Node<int> nodeReference = node.Next;

            Assert.AreEqual<Node<int>>(node, nodeReference);
        }

        [TestMethod]
        public void NodeNextPropertySetsNewNodesNextToItselfFalse()
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
    }
}
