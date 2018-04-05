using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration.Rigid;

namespace RegistrationTest.Rigid
{
    [TestClass]
    public class KdTreeTest
    {
        [TestMethod]
        public void TestFindNearestPoint()
        {
            KdTree<Vector3> tree = new KdTree<Vector3>(new Vector3(3, -5, 0));
            tree.Add(new Vector3(-7, -2, 0));
            tree.Add(new Vector3(12, 1, 0));
            tree.Add(new Vector3(-3, 4, 0));
            tree.Add(new Vector3(-1, -7, 0));
            tree.Add(new Vector3(8, 2, 0));
            tree.Add(new Vector3(10, -9, 0));
            tree.Add(new Vector3(-5, 6, 0));
            tree.Add(new Vector3(5, 8, 0));
            tree.Add(new Vector3(7, -11, 0));

            Vector3 vector = tree.FindNearestPoint(new Vector3(-2, -7, 0));

            Assert.AreEqual(-1, vector.X);
            Assert.AreEqual(-7, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [TestMethod]
        public void TestFindNearestPoint1()
        {
            KdTree<Vector3> tree = new KdTree<Vector3>(new Vector3(3, -5, 0));
            tree.Add(new Vector3(-7, -2, 0));
            tree.Add(new Vector3(12, 1, 0));
            tree.Add(new Vector3(-3, 4, 0));
            tree.Add(new Vector3(-1, -7, 0));
            tree.Add(new Vector3(8, 2, 0));
            tree.Add(new Vector3(10, -9, 0));
            tree.Add(new Vector3(-5, 6, 0));
            tree.Add(new Vector3(5, 8, 0));
            tree.Add(new Vector3(7, -11, 0));

            Vector3 vector = tree.FindNearestPoint(new Vector3(5, 7, 0));

            Assert.AreEqual(5, vector.X);
            Assert.AreEqual(8, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [TestMethod]
        public void TestFindNearestPoint2()
        {
            KdTree<Vector3> tree = new KdTree<Vector3>(new Vector3(3, -5, 0));
            tree.Add(new Vector3(-7, -2, 0));
            tree.Add(new Vector3(12, 1, 0));
            tree.Add(new Vector3(-3, 4, 0));
            tree.Add(new Vector3(-1, -7, 0));
            tree.Add(new Vector3(8, 2, 0));
            tree.Add(new Vector3(10, -9, 0));
            tree.Add(new Vector3(-5, 6, 0));
            tree.Add(new Vector3(5, 8, 0));
            tree.Add(new Vector3(7, -11, 0));

            Vector3 vector = tree.FindNearestPoint(new Vector3(7, 2, 0));

            Assert.AreEqual(8, vector.X);
            Assert.AreEqual(2, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [TestMethod]
        public void TestFindNearestPoint3()
        {
            KdTree<Vector3> tree = new KdTree<Vector3>(new Vector3(3, -5, 0));
            tree.Add(new Vector3(-7, -2, 0));
            tree.Add(new Vector3(12, 1, 0));
            tree.Add(new Vector3(-3, 4, 0));
            tree.Add(new Vector3(-1, -7, 0));
            tree.Add(new Vector3(8, 2, 0));
            tree.Add(new Vector3(10, -9, 0));
            tree.Add(new Vector3(-5, 6, 0));
            tree.Add(new Vector3(5, 8, 0));
            tree.Add(new Vector3(7, -11, 0));
            tree.Add(new Vector3(2, -10, 0));

            Vector3 vector = tree.FindNearestPoint(new Vector3(4, -10, 0));

            Assert.AreEqual(2, vector.X);
            Assert.AreEqual(-10, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }
    }
}
