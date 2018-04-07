namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    interface IRotation
    {
        Matrix<float> CalculateRotation(List<Vector<float>> referPoints, List<Vector<float>> sourcePoints);
    }
}
