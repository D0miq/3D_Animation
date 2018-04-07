namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    class Icp
    {
        private IRotation rotation;

        private IPointMapping mapping;

        public Icp(IRotation rotationAlgorithm, IPointMapping mapping)
        {
            this.rotation = rotationAlgorithm;
            this.mapping = mapping;
        }

        public void ComputeTrasformation(float maxError, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            this.Translate(referPoints, sourcePoints);
            List<Vector<float>> mappedReferPoints;

            do
            {
                mappedReferPoints = this.mapping.MapPoints(sourcePoints);
                Matrix<float> rotationMatrix = this.rotation.CalculateRotation(mappedReferPoints, sourcePoints);
                for (int j = 0; j < sourcePoints.Count; j++)
                {
                    sourcePoints[j] = rotationMatrix * sourcePoints[j];
                }
            } while (!this.CheckDistance(maxError, mappedReferPoints, sourcePoints));
        }

        /// <summary>
        /// Najde a aplikuje transformace na sourcePoints
        /// </summary>
        /// <param name="iterationCount"></param>
        /// <param name="referPoints"></param>
        /// <param name="sourcePoints"></param>
        public void ComputeTransformation(int iterationCount, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            this.Translate(referPoints, sourcePoints);

            for (int i = 0; i < iterationCount; i++)
            {
                List<Vector<float>> mappedReferPoints = this.mapping.MapPoints(sourcePoints);
                Matrix<float> rotationMatrix = this.rotation.CalculateRotation(mappedReferPoints, sourcePoints);
                for (int j = 0; j < sourcePoints.Count; j++)
                {
                    sourcePoints[j] = rotationMatrix * sourcePoints[j];
                }
            }
        }

        private bool CheckDistance(float maxError, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            for (int i = 0; i < referPoints.Count; i++)
            {
                if (Distance.Euclidean(referPoints[i], sourcePoints[i]) > maxError)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Přesune source points na referPoints
        /// </summary>
        /// <param name="referPoints"></param>
        /// <param name="sourcePoints"></param>
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
