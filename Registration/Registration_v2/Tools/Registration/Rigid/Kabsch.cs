namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Factorization;

    /// <summary>
    /// An instance of the <see cref="Kabsch"/> class represents a Kabsch algorithm used for calculation
    /// of the rotation matrix with singular value decomposition.
    /// </summary>
    public class Kabsch : IRotation
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Calculates a rotation matrix. Points in given buffers have to be translated into origin of a coordinate system.
        /// Algorithm can behave incorectly if they would not be translated.
        /// </summary>
        /// <param name="sourcePoints">Source points (points that are rotated).</param>
        /// <param name="">Target points.</param>
        /// <returns>The rotation matrix.</returns>
        public Matrix<float> CalculateRotation(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints)
        {
            Log.Info("Calculating rotation matrix.");
            // Creates matrices from points in origin.
            Matrix<float> sourceMatrix = Matrix<float>.Build.DenseOfColumnVectors(sourcePoints.ToArray());
            Matrix<float> targetMatrix = Matrix<float>.Build.DenseOfColumnVectors(targetPoints.ToArray());

            // Mutiply target and source matrices and calculates singular value decomposition of the result.
            Svd<float> singularDecomposition = (targetMatrix * sourceMatrix.Transpose()).Svd();
            Matrix<float> rotationMatrix = singularDecomposition.U * singularDecomposition.VT;
            rotationMatrix[3, 0] = 0;
            rotationMatrix[3, 1] = 0;
            rotationMatrix[3, 2] = 0;
            rotationMatrix[3, 3] = 1;

            Log.Debug("Found rotation matrix: " + rotationMatrix);

            return rotationMatrix;
        }
    }
}
