namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// 
    /// </summary>
    class Icp
    {
        /// <summary>
        /// 
        /// </summary>
        private IRotation rotation;

        /// <summary>
        /// 
        /// </summary>
        private IPointMapping mapping;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotationAlgorithm"></param>
        /// <param name="mapping"></param>
        public Icp(IRotation rotationAlgorithm, IPointMapping mapping)
        {
            this.rotation = rotationAlgorithm;
            this.mapping = mapping;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxError"></param>
        /// <param name="referPoints"></param>
        /// <param name="sourcePoints"></param>
        public void ComputeTransformation(float maxError, List<Vector<float>> referPoints, List<Vector<float>> sourcePoints)
        {
            this.Translate(referPoints, sourcePoints);
            List<Vector<float>> mappedReferPoints;

            do
            {
                mappedReferPoints = this.mapping.MapPoints(sourcePoints);
                this.rotation.CalculateRotation(mappedReferPoints, sourcePoints);
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
                this.rotation.CalculateRotation(mappedReferPoints, sourcePoints);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxError"></param>
        /// <param name="referPoints"></param>
        /// <param name="sourcePoints"></param>
        /// <returns></returns>
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
