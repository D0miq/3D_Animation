namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Factorization;

    class Kabsch : IRotation
    {
        public Matrix<float> CalculateRotation(List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            Centroid referCentroid = new Centroid(referPoints);
            Centroid sourceCentroid = new Centroid(sourcePoints);

            Matrix<float> referMatrix = Matrix<float>.Build.DenseOfColumnVectors(referCentroid.NormalizePoints().ToArray());
            Matrix<float> sourceMatrix = Matrix<float>.Build.DenseOfColumnVectors(sourceCentroid.NormalizePoints().ToArray());

            Svd<float> singularDecomposition = (referMatrix * sourceMatrix.Transpose()).Svd();
            return singularDecomposition.U * singularDecomposition.VT;
        }
    }
}
