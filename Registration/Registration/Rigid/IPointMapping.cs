namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// 
    /// </summary>
    public interface IPointMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePoints"></param>
        /// <returns></returns>
        List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints);
    }

    /// <summary>
    /// 
    /// </summary>
    public class BruteForceMapping : IPointMapping
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Vector<float>> referPoints;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referPoints"></param>
        public BruteForceMapping(List<Vector<float>> referPoints)
        {
            this.referPoints = referPoints;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePoints"></param>
        /// <returns></returns>
        public List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints)
        {
            Vector<float>[] mappedPoints = new Vector<float>[this.referPoints.Count];
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                int closestIndex = 0;

                for (int j = 0; j < this.referPoints.Count; j++)
                {
                    if (Distance.Euclidean(this.referPoints[j], sourcePoints[i]) < Distance.Euclidean(this.referPoints[closestIndex], sourcePoints[i]))
                    {
                        closestIndex = j;
                    }
                }

                mappedPoints[i] = this.referPoints[closestIndex];
            }

            return new List<Vector<float>>(mappedPoints);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class KdTreeMapping : IPointMapping
    {
        /// <summary>
        /// 
        /// </summary>
        private KdTree referTree;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referPoints"></param>
        public KdTreeMapping(List<Vector<float>> referPoints)
        {
            this.referTree = new KdTree(referPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePoints"></param>
        /// <returns></returns>
        public List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints)
        {
            Vector<float>[] mappedPoints = new Vector<float>[sourcePoints.Count];
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                mappedPoints[i] = this.referTree.FindNearestPoint(sourcePoints[i]);
            }

            return new List<Vector<float>>(mappedPoints);
        }
    }
}