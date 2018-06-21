namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// The <see cref="Transformation3D"/> class provides operations with transformation, like applying transformation to a set of points or creating a matrix.
    /// </summary>
    public class Transformation3D
    {
        /// <summary>
        /// Applies transformation matrix to a set of points.
        /// </summary>
        /// <param name="points">The list of points.</param>
        /// <param name="transformationMatrix">The transformation matrix.</param>
        public static void ApplyTransformation(List<Vector<float>> points, Matrix<float> transformationMatrix)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = transformationMatrix * points[i];
            }
        }

        /// <summary>
        /// Translates a list of points by a vector
        /// </summary>
        /// <param name="points">The list of points.</param>
        /// <param name="translationVector">The translation vector.</param>
        public static void Translate(List<Vector<float>> points, Vector<float> translationVector)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i][0] = points[i][0] + translationVector[0];
                points[i][1] = points[i][1] + translationVector[1];
                points[i][2] = points[i][2] + translationVector[2];
            }
        }

        /// <summary>
        /// Creates a translation matrix from a translation vector.
        /// </summary>
        /// <param name="translationVector">The translation vector.</param>
        /// <returns>The translation matrix.</returns>
        public static Matrix<float> CreateTranslationMatrix(Vector<float> translationVector)
        {
            Matrix<float> translationMatrix = Matrix<float>.Build.DenseIdentity(4);
            translationMatrix[0, 3] = translationVector[0];
            translationMatrix[1, 3] = translationVector[1];
            translationMatrix[2, 3] = translationVector[2];
            return translationMatrix;
        }
    }
}
