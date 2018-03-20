using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3DGraphicsCompression;

namespace _3DGraphicsCompressionTest
{
    [TestClass]
    public class ListBufferTest
    {
        [TestMethod]
        public void TestCapacity()
        {
            ListBuffer<int> listBuffer = new ListBuffer<int>();
            listBuffer.Capacity = 2;
            Assert.AreEqual(2, listBuffer.Buffer.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCapacityNegativeException()
        {
            ListBuffer<int> listBuffer = new ListBuffer<int>();
            listBuffer.Capacity = -2;
        }

        [TestMethod]
        public void TestAdd()
        {
            ListBuffer<int> listBuffer = new ListBuffer<int>();
            listBuffer.Add(1);
            Assert.AreEqual(1, listBuffer.Buffer[0]);
        }

        [TestMethod]
        public void TestAddResize()
        {
            ListBuffer<int> listBuffer = new ListBuffer<int>();
            listBuffer.Add(1);
            listBuffer.Add(2);
            listBuffer.Add(3);
            listBuffer.Add(4);
            listBuffer.Add(5);
            Assert.AreEqual(8, listBuffer.Capacity);
        }

        [TestMethod]
        public void TestAddRange()
        {
            ListBuffer<int> listBuffer = new ListBuffer<int>();
            listBuffer.AddRange(new int[] { 1, 2 });
            Assert.AreEqual(1, listBuffer.Buffer[0]);
            Assert.AreEqual(2, listBuffer.Buffer[1]);
        }

        [TestMethod]
        public void TestAddRangeResize()
        {
            ListBuffer<int> listBuffer = new ListBuffer<int>();
            listBuffer.AddRange(new int[] { 1, 2, 3, 4, 5, 6 });
            Assert.AreEqual(6, listBuffer.Capacity);
        }
    }
}
