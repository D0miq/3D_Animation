namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// The interface <see cref="IPointMapping"/> represents a mapping algorithm.
    /// </summary>
    /// <seealso cref="KdTreeMapping"/>
    /// <seealso cref="BruteForceMapping"/>
    public interface IPointMapping
    {
        /// <summary>
        /// Finds correspoding pairs of the closest points for the given lists.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <param name="mappedSourcePoints">Mapped source points contains points that have found neighbor.</param>
        /// <param name="mappedTargetPoints">Closest target points to the given source points.</param>
        void MapPoints(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints, out List<Vector<float>> mappedSourcePoints, out List<Vector<float>> mappedTargetPoints);
    }
}