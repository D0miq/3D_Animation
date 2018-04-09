namespace Registration.Rigid
{
    using System.Collections.Generic;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    public interface IPointMapping
    {
        List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints);
    }

    public class BruteForceMapping : IPointMapping
    {
        private List<Vector<float>> referPoints;

        public BruteForceMapping(List<Vector<float>> referPoints)
        {
            this.referPoints = referPoints;
        }

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

    public class KdTreeMapping : IPointMapping
    {
        private KdTree referTree;

        public KdTreeMapping(List<Vector<float>> referPoints)
        {
            this.referTree = new KdTree(referPoints);
        }

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