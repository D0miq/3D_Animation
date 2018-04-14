namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Factorization;

    /// <summary>
    /// An instance of the <see cref="Kabsch"/> class represents a Kabsch algorithm used for calculation
    /// of the rotation matrix with singular value decomposition.
    /// </summary>
    public class Kabsch : IRotation
    {
        /// <summary>
        /// Calculates a rotation matrix.
        /// </summary>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="sourcePoints">Source points.</param>
        /// <returns>The rotation matrix.</returns>
        public Matrix<float> CalculateRotation(List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            // Calculates centroid of both lists.
            Centroid referCentroid = new Centroid(referPoints);
            Centroid sourceCentroid = new Centroid(sourcePoints);

            // Translates points to the origin.
            List<Vector<float>> originReferPoints = referCentroid.TranslatePointsToOrigin(referPoints);
            List<Vector<float>> originSourcePoints = sourceCentroid.TranslatePointsToOrigin(sourcePoints);

            // Creates matrices from translated points.
            Matrix<float> referMatrix = Matrix<float>.Build.DenseOfColumnVectors(originReferPoints.ToArray());
            Matrix<float> sourceMatrix = Matrix<float>.Build.DenseOfColumnVectors(originSourcePoints.ToArray());

            // Mutiply referential and source matrices and calculates singular value decomposition of the result.
            Svd<float> singularDecomposition = (referMatrix * sourceMatrix.Transpose()).Svd();
            return singularDecomposition.U * singularDecomposition.VT;
        }
    }
}
