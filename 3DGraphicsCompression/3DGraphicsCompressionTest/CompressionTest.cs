using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3DGraphicsCompression;
using Rhino.Mocks;
using _3DGraphicsCompression.ObjParser;
using System.IO;
using MathNet.Numerics.LinearAlgebra;

namespace _3DGraphicsCompressionTest
{
    [TestClass]
    public class CompressionTest
    {
        [TestMethod]
        public void TestAdd()
        {
            Compression compression = new Compression();
            var fileReader = MockRepository.GenerateStub<IFileReader>();
            compression.AddFile(fileReader);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void TestAddException()
        {
            Compression compression = new Compression();
            var fileReader = MockRepository.GenerateMock<IFileReader>();
            fileReader.Expect(x => x.Read(null)).IgnoreArguments().Throw(new IOException());
            fileReader.Expect(x => x.ReadVertices(null)).IgnoreArguments().Throw(new IOException());
            compression.AddFile(fileReader);
        }

        [TestMethod]
        public void TestCompressVertices()
        {
            ListBuffer<float> verticesBuffer = new ListBuffer<float>();
            verticesBuffer.AddRange(new float[] { 3, 2, -1, 0 });

            Compression compression = new Compression(verticesBuffer, 2);

            compression.CompressVertices(1);

            Assert.AreEqual(Vector<float>.Build.DenseOfArray(new float[] { 1, 1 }), compression.AverageTrajectory);
            Assert.AreEqual(compression.SubEigenVectors.At(0, 0) / 2, compression.SubEigenVectors.At(1,0));
            Assert.AreEqual(-compression.ControlTrajectories.At(0,0), compression.ControlTrajectories.At(1,0));
        }
    }
}
