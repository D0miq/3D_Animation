namespace Registration_v2.Tools.Registration.Nonrigid
{
    using System;
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="Rbf"/> class provides radial basis function interpolation algorithm.
    /// </summary>
    public class Rbf
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The basis function r^3 which is used during interpolation.
        /// </summary>
        private readonly Func<float, float> basisFunction = value => value * value * value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rbf"/> class.
        /// </summary>
        public Rbf()
        {
            Log.Info("Creating rbg.");
        }

        /// <summary>
        /// Interpolates correction vectors of control points for each source point.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="controlPoints">Control points.</param>
        /// <param name="correctionVectors">Correction vectors.</param>
        /// <returns>Interpolated correction vectors.</returns>
        public List<Vector<float>> Interpolate(List<Vector<float>> sourcePoints, List<Vector<float>> controlPoints, List<Vector<float>> correctionVectors)
        {
            Log.Info("Interpolating the list of the correction vectors.");

            // Compute a matrix which contains values of a computed basis function.
            Matrix<float> basisMatrix = this.CreateBasisMatrix(controlPoints);

            // Solve linear equation system for each coordinate of correction vectors. It gives 3*correction vectors count weights.
            Vector<float> xWeightVector = basisMatrix.Solve(this.GetCorrectionElements(0, correctionVectors));
            Vector<float> yWeightVector = basisMatrix.Solve(this.GetCorrectionElements(1, correctionVectors));
            Vector<float> zWeightVector = basisMatrix.Solve(this.GetCorrectionElements(2, correctionVectors));

            /*
             * Interpolate correction vectors for each source point. 
             * It sums each solved weight with result of the basis function from an L2-norm between source point and control point.
             */
            List<Vector<float>> interpolatedCorrectionVectors = new List<Vector<float>>(sourcePoints.Count);
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                Vector<float> interpolatedVector = Vector<float>.Build.Dense(4);
                for (int j = 0; j < xWeightVector.Count; j++)
                {
                    float basisValue = this.basisFunction((float)Distance.Euclidean(sourcePoints[i], controlPoints[j]));
                    interpolatedVector[0] += xWeightVector[j] * basisValue;
                    interpolatedVector[1] += yWeightVector[j] * basisValue;
                    interpolatedVector[2] += zWeightVector[j] * basisValue;
                }

                interpolatedCorrectionVectors.Add(interpolatedVector);
            }

            return interpolatedCorrectionVectors;
        }

        /// <summary>
        /// Creates a basis matrix from a basis function between all pairs of control points.
        /// </summary>
        /// <param name="controlPoints">Control points.</param>
        /// <returns>The basis matrix.</returns>
        private Matrix<float> CreateBasisMatrix(List<Vector<float>> controlPoints)
        {
            // Compute a basis function between all pairs of control points.
            Log.Info("Creating basis matrix.");
            Matrix<float> basisMatrix = Matrix<float>.Build.Dense(controlPoints.Count, controlPoints.Count);

            for (int i = 0; i < controlPoints.Count; i++)
            {
                for (int j = 0; j < controlPoints.Count; j++)
                {
                    basisMatrix[i, j] = this.basisFunction((float)Distance.Euclidean(controlPoints[i], controlPoints[j]));
                }
            }

            return basisMatrix;
        }

        /// <summary>
        /// Obtains elements of the given correction vectors on the given index.
        /// </summary>
        /// <param name="elementIndex">The element index.</param>
        /// <param name="correctionVectors">Correction vectors.</param>
        /// <returns>Vector of the element from each correction vector.</returns>
        private Vector<float> GetCorrectionElements(int elementIndex, List<Vector<float>> correctionVectors)
        {
            // Obtain elements of the given correction vectors on the given index.
            Log.Info("Obtaining elements of correction vectors.");
            Vector<float> elements = Vector<float>.Build.Dense(correctionVectors.Count);
            for (int i = 0; i < correctionVectors.Count; i++)
            {
                elements[i] = correctionVectors[i][elementIndex];
            }

            return elements;
        }
    }
}
