namespace Registration_v2.Tools.Registration
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// The interface <see cref="IRotation"/> represents an algorithm that computes registrations between two sets of points.
    /// </summary>
    /// <seealso cref="NonrigidRegistration"/>
    /// <seealso cref="RigidRegistration"/>
    public interface IRegistration
    {
        /// <summary>
        /// Registers source points to target points.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <returns>Registered source points.</returns>
        List<Vector<float>> ComputeRegistration(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints);
    }
}
