using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration.Rigid;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace RegistrationTest.Rigid
{
    [TestClass]
    public class KdTreeTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestFindNearestPoint()
        {
            List<Vector<float>> list = new List<Vector<float>>();
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 3, -5, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -7, -2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 12, 1, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -3, 4, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -1, -7, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 8, 2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 10, -9, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -5, 6, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 5, 8, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 7, -11, 0 }));

            KdTree tree = new KdTree(list);

            Vector<float> vector = tree.FindNearestPoint(Vector<float>.Build.DenseOfArray(new float[] { -2, -7, 0 }));

            Assert.AreEqual(-1, vector[0]);
            Assert.AreEqual(-7, vector[1]);
            Assert.AreEqual(0, vector[2]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestFindNearestPoint1()
        {
            List<Vector<float>> list = new List<Vector<float>>();
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 3, -5, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -7, -2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 12, 1, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -3, 4, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -1, -7, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 8, 2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 10, -9, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -5, 6, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 5, 8, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 7, -11, 0 }));

            KdTree tree = new KdTree(list);

            Vector<float> vector = tree.FindNearestPoint(Vector<float>.Build.DenseOfArray(new float[] { 5, 7, 0 }));

            Assert.AreEqual(5, vector[0]);
            Assert.AreEqual(8, vector[1]);
            Assert.AreEqual(0, vector[2]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestFindNearestPoint2()
        {
            List<Vector<float>> list = new List<Vector<float>>();
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 3, -5, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -7, -2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 12, 1, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -3, 4, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -1, -7, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 8, 2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 10, -9, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -5, 6, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 5, 8, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 7, -11, 0 }));

            KdTree tree = new KdTree(list);

            Vector<float> vector = tree.FindNearestPoint(Vector<float>.Build.DenseOfArray(new float[] { 7, 2, 0 }));

            Assert.AreEqual(8, vector[0]);
            Assert.AreEqual(2, vector[1]);
            Assert.AreEqual(0, vector[2]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestFindNearestPoint3()
        {
            List<Vector<float>> list = new List<Vector<float>>();
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 3, -5, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -7, -2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 12, 1, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -3, 4, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -1, -7, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 8, 2, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 10, -9, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { -5, 6, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 5, 8, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 7, -11, 0 }));
            list.Add(Vector<float>.Build.DenseOfArray(new float[] { 2, -10, 0 }));

            KdTree tree = new KdTree(list);
            

            Vector<float> vector = tree.FindNearestPoint(Vector<float>.Build.DenseOfArray(new float[] { 4, -10, 0 }));

            Assert.AreEqual(2, vector[0]);
            Assert.AreEqual(-10, vector[1]);
            Assert.AreEqual(0, vector[2]);
        }
    }
}
