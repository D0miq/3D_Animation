using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3DGraphicsCompression;
using Rhino.Mocks;
using _3DGraphicsCompression.ObjParser;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace _3DGraphicsCompressionTest
{
    [TestClass]
    public class CompressionTest
    {
        [TestMethod]
        public void TestAddFrane()
        {
            Compression compression = new Compression();
            Frame mesh = new Frame(new List<float>() { 0.5f, 0.3f }, new List<Face>() { new Face() }, new List<float>() { 1, 2 }, new List<float>() { 3, 4 });
            compression.AddFrame(mesh);
            CollectionAssert.AreEqual(mesh.Vertices, compression.Frame.Vertices);
            CollectionAssert.AreEqual(mesh.Faces, compression.Frame.Faces);
            CollectionAssert.AreEqual(mesh.TextureCoords, compression.Frame.TextureCoords);
            CollectionAssert.AreEqual(mesh.Normals, compression.Frame.Normals);
        }

        [TestMethod]
        public void TestAddFrameVertices()
        {
            Compression compression = new Compression();
            List<float> verticesBuffer = new List<float>() { 3, 2, -1, 0 };
            compression.AddFrame(verticesBuffer);
            CollectionAssert.AreEqual(verticesBuffer, compression.Frame.Vertices);
        }

        [TestMethod]
        public void TestCompressVertices()
        {
            List<float> verticesBuffer = new List<float>() { 3, 2, -1, 0, -5 ,2};

            Compression compression = new Compression(verticesBuffer, 1);

            compression.CompressFrames(1);

            Assert.AreEqual(Vector<float>.Build.DenseOfArray(new float[] { 1.5f, -1.5f, 0.5f }), compression.AverageTrajectory);
            Assert.AreEqual(Matrix<float>.Build.DenseOfArray(new float[,] { { -0.2509f }, { -0.935f }, { 0.2509f } }), compression.SubEigenVectors);
            Assert.AreEqual(Matrix<float>.Build.DenseOfArray(new float[,] { { -1.2202f, 6.8302f } }), compression.ControlTrajectories);
        }
    }
}
