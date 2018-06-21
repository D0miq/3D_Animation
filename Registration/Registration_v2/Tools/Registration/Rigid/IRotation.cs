namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// The interface <see cref="IRotation"/> represents an algorithm that computes rotation matrix and applies it on the given points.
    /// </summary>
    public interface IRotation
    {
        /// <summary>
        /// Calculates a rotation matrix.
        /// </summary>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="sourcePoints">Source points.</param>
        /// <returns>The rotation matrix.</returns>
        Matrix<float> CalculateRotation(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints);
    }
}
