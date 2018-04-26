using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Nonrigid
{
    class Rbf
    {
        private readonly Func<float, float> basisFunction = value => value * value * value;

        public Rbf()
        {

        }

        public List<Vector<float>> Interpolate(List<Vector<float>> controlPoints, List<Vector<float>> functionValues, List<Vector<float>> referPoints)
        {
        }
    }
}
