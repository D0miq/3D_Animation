namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Factorization;

    /// <summary>
    /// 
    /// </summary>
    class Kabsch : IRotation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referPoints"></param>
        /// <param name="sourcePoints"></param>
        public void CalculateRotation(List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            Centroid referCentroid = new Centroid(referPoints);
            Centroid sourceCentroid = new Centroid(sourcePoints);

            List<Vector<float>> originReferPoints = referCentroid.ToOrigin();
            List<Vector<float>> originSourcePoints = sourceCentroid.ToOrigin();

            Matrix<float> referMatrix = Matrix<float>.Build.DenseOfColumnVectors(originReferPoints.ToArray());
            Matrix<float> sourceMatrix = Matrix<float>.Build.DenseOfColumnVectors(originSourcePoints.ToArray());

            Svd<float> singularDecomposition = (referMatrix * sourceMatrix.Transpose()).Svd();
            Matrix<float> rotationMatrix = singularDecomposition.U * singularDecomposition.VT;

            for (int j = 0; j < originSourcePoints.Count; j++)
            {
                sourcePoints[j] = (rotationMatrix * originSourcePoints[j]) + sourceCentroid.Value;
            }
        }
    }
}
