using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using Registration.Rigid;

namespace RegistrationTest.Rigid
{
    [TestClass]
    public class MappingTest
    {
        [TestMethod]
        public void TestMapping()
        {
            // Referenční soubor
            IFileReader fileReader = new ObjFileReader("");
            List<Vector<float>> referPoints = fileReader.ReadVertices();
            // Zdrojový soubor
            fileReader = new ObjFileReader("");
            List<Vector<float>> sourcePoints = fileReader.ReadVertices();

            // Doplnit maximální vzdálenost
            IPointMapping bruteForceMapping = new BruteForceMapping(referPoints, 50f);
            List<Vector<float>> bruteForceMappedPoints = bruteForceMapping.MapPoints(sourcePoints, out List<Vector<float>> mappedSourcePoints);

            // Doplnit maximální vzdálenost
            IPointMapping kdMapping = new KdTreeMapping(referPoints, 50f);
            List<Vector<float>> kdMappedPoints = kdMapping.MapPoints(sourcePoints, out mappedSourcePoints);

            Assert.AreEqual(bruteForceMappedPoints.Count, kdMappedPoints.Count);

            bool allEqual = true;
            for(int i = 0; i < bruteForceMappedPoints.Count; i++)
            {
                if (!bruteForceMappedPoints[i].Equals(kdMappedPoints[i]))
                {
                    allEqual = false;
                    break;
                }
            }

            Assert.IsTrue(allEqual);
        }
    }
}
