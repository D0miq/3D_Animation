namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// 
    /// </summary>
    interface IRotation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referPoints"></param>
        /// <param name="sourcePoints"></param>
        void CalculateRotation(List<Vector<float>> referPoints, List<Vector<float>> sourcePoints);
    }
}
