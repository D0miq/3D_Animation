namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// And instance of the class <see cref="Icp"/> represents the iterative close points algorithm used
    /// to calculate optimal rigid transformations between two different 3D objects. Objects should not
    /// been very far from each other, otherwise registration can fail.
    /// </summary>
    public class Icp
    {
        /// <summary>
        /// The rotation algorithm.
        /// </summary>
        private IRotation rotation;

        /// <summary>
        /// The mapping algorithm.
        /// </summary>
        private IPointMapping mapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="Icp"/> class with given algorithms.
        /// </summary>
        /// <param name="rotationAlgorithm">The algorithm used for calculation of the rotation matrix.</param>
        /// <param name="mapping">The algorithm used for mapping pairs of points.</param>
        public Icp(IRotation rotationAlgorithm, IPointMapping mapping)
        {
            this.rotation = rotationAlgorithm;
            this.mapping = mapping;
        }

        /// <summary>
        /// Finds and applies transformations on the given source points.
        /// Its target is to register source points on referential points.
        /// Function applies transformation on the given source points and edits it.
        /// Points are trasformed until source points and referential points are closer than the given distance limit.
        /// </summary>
        /// <param name="maxDistance">The max possible distance between pairs of points.</param>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="sourcePoints">Source points.</param>
        public void ComputeTransformation(float maxDistance, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            List<Vector<float>> mappedReferPoints = this.mapping.MapPoints(sourcePoints, out List<Vector<float>> mappedSourcePoints);
            this.Translate(mappedReferPoints, sourcePoints);

            Centroid sourceCentroid = new Centroid(sourcePoints);

            do
            {

                Matrix<float> rotationMatrix = this.rotation.CalculateRotation(mappedReferPoints, mappedSourcePoints);

                List<Vector<float>> originSourcePoints = sourceCentroid.TranslatePointsToOrigin(sourcePoints);

                // Applies rotation on source points and translates them back from the origin.
                for (int i = 0; i < originSourcePoints.Count; i++)
                {
                    sourcePoints[i] = (rotationMatrix * originSourcePoints[i]) + sourceCentroid.Value;
                }

                mappedReferPoints = this.mapping.MapPoints(sourcePoints, out mappedSourcePoints);
            } while (!this.CheckDistance(maxDistance, mappedReferPoints, sourcePoints));
        }

        /// <summary>
        /// Finds and applies transformations on the given source points.
        /// Its target is to register source points on referential points.
        /// Function applies transformation on the given source points and edits it.
        /// Points are transformed <paramref name="iterationCount"/> times.
        /// </summary>
        /// <param name="iterationCount">The number of iterations.</param>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="sourcePoints">Source points.</param>
        public void ComputeTransformation(int iterationCount, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            List<Vector<float>> mappedReferPoints = this.mapping.MapPoints(sourcePoints, out List<Vector<float>> mappedSourcePoints);
            this.Translate(mappedReferPoints, sourcePoints);

            Centroid sourceCentroid = new Centroid(sourcePoints);

            for (int i = 0; i < iterationCount; i++)
            {
                Matrix<float> rotationMatrix = this.rotation.CalculateRotation(mappedReferPoints, mappedSourcePoints);

                List<Vector<float>> originSourcePoints = sourceCentroid.TranslatePointsToOrigin(sourcePoints);

                // Applies rotation on source points and translates them back from the origin.
                for (int j = 0; j < originSourcePoints.Count; j++)
                {
                    sourcePoints[j] = (rotationMatrix * originSourcePoints[j]) + sourceCentroid.Value;
                }

                mappedReferPoints = this.mapping.MapPoints(sourcePoints, out mappedSourcePoints);
            }
        }

        /// <summary>
        /// Checks if distance between referential points and source points is smaller than the given max distance.
        /// </summary>
        /// <param name="maxDistance">The max suitable distance.</param>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="sourcePoints">Source points.</param>
        /// <returns>True - if all pairs fulfils the limit, false otherwise.</returns>
        private bool CheckDistance(float maxDistance, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            for (int i = 0; i < referPoints.Count; i++)
            {
                if (Distance.Euclidean(referPoints[i], sourcePoints[i]) > maxDistance)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Translates a centroid of the given source points to a centroid of the referential points.
        /// Function edits the input list of source points.
        /// </summary>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="sourcePoints">Source points.</param>
        private void Translate(List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            Centroid referCentroid = new Centroid(referPoints);
            Centroid sourceCentroid = new Centroid(sourcePoints);
            Vector<float> translation = referCentroid.Value - sourceCentroid.Value;

            for (int i = 0; i < sourcePoints.Count; i++)
            {
                sourcePoints[i] = sourcePoints[i] + translation;
            }
        }
    }
}
