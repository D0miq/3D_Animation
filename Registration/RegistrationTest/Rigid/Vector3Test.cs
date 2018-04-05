using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration.Rigid;

namespace RegistrationTest.Rigid
{
    /// <summary>
    /// Souhrnný popis pro Vector3Test
    /// </summary>
    [TestClass]
    public class Vector3Test
    {
        [TestMethod]
        public void TestGetValue()
        {
            Vector3 vector = new Vector3(1, 2, 3);
            Assert.AreEqual(1, vector.GetValue(0));
        }

        [TestMethod]
        public void TestDistance()
        {
            Vector3 vector1 = new Vector3(1, 2, 5);
            Vector3 vector2 = new Vector3(2, 0, 3);
            Assert.AreEqual(3, vector1.Distance(vector2));
        }
    }
}
