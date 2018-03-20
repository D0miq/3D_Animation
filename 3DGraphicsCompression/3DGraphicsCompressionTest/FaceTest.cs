using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3DGraphicsCompression.ObjParser;
using System.Linq;

namespace _3DGraphicsCompressionTest
{
    [TestClass]
    public class FaceTest
    {
        [TestMethod]
        public void TestFromString()
        {
            string[] faceString = { "1/2/3", "4/5/6", "7/8/9" };
            Face face = new Face();
            face.FromString(faceString);
            Assert.IsTrue(new int[] { 1, 4, 7 }.SequenceEqual(face.Vertices));
            Assert.IsTrue(new int[] { 2, 5, 8 }.SequenceEqual(face.Textures));
            Assert.IsTrue(new int[] { 3, 6, 9 }.SequenceEqual(face.Normals));
        }

        [TestMethod]
        public void TestFromString2()
        {
            string[] faceString = { "1//3", "4//6", "7//9" };
            Face face = new Face();
            face.FromString(faceString);
            Assert.IsTrue(new int[] { 1, 4, 7 }.SequenceEqual(face.Vertices));
            Assert.IsTrue(new int[] { 0, 0, 0 }.SequenceEqual(face.Textures));
            Assert.IsTrue(new int[] { 3, 6, 9 }.SequenceEqual(face.Normals));
        }

        [TestMethod]
        public void TestFromString3()
        {
            string[] faceString = { "1/2", "4/5", "7/8" };
            Face face = new Face();
            face.FromString(faceString);
            Assert.IsTrue(new int[] { 1, 4, 7 }.SequenceEqual(face.Vertices));
            Assert.IsTrue(new int[] { 2, 5, 8 }.SequenceEqual(face.Textures));
            Assert.IsTrue(new int[] { 0, 0, 0 }.SequenceEqual(face.Normals));
        }

        [TestMethod]
        public void TestFromStringWrongFormat()
        {
            string[] faceString = { "afsdsv", "ada/afv", "asd///fsv" };
            Face face = new Face();
            face.FromString(faceString);
            Assert.IsTrue(new int[] { 0, 0, 0 }.SequenceEqual(face.Vertices));
            Assert.IsTrue(new int[] { 0, 0, 0 }.SequenceEqual(face.Textures));
            Assert.IsTrue(new int[] { 0, 0, 0 }.SequenceEqual(face.Normals));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestFromStringException()
        {
            string[] faceString = { "1/2/3", "4/5/6" };
            Face face = new Face();
            face.FromString(faceString);
        }

        
    }
}
