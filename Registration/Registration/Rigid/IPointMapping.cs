namespace Registration.Rigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The interface <see cref="IPointMapping"/> represents 
    /// </summary>
    /// <seealso cref="KdTreeMapping"/>
    /// <seealso cref="BruteForceMapping"/>
    public interface IPointMapping
    {
        /// <summary>
        /// Finds correspoding pairs of the closest points for the given list.
        /// If distance between found neighbor and source points is greater than limit distance, points are not added to output lists.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <returns>Closest referential points to the given source points.</returns>
        List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints, out List<Vector<float>> mappedSourcePoints);
    }

    /// <summary>
    /// An instance of the <see cref="BruteForceMapping"/> class 
    /// </summary>
    /// <seealso cref="IPointMapping"/>
    /// <seealso cref="KdTreeMapping"/>
    public class BruteForceMapping : IPointMapping
    {
        /// <summary>
        /// Referential points.
        /// </summary>
        private List<Vector<float>> referPoints;

        /// <summary>
        /// The max distance on what pairs are going to be created.
        /// </summary>
        private float maxDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="BruteForceMapping"/> class.
        /// </summary>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="maxDistance">The max distance on what pairs are going to be created.</param>
        public BruteForceMapping(List<Vector<float>> referPoints, float maxDistance)
        {
            this.referPoints = referPoints;
            this.maxDistance = maxDistance;
        }

        /// <summary>
        /// Finds correspoding pairs of the closest points for the given list.
        /// Function uses brute force algorithm to map points.
        /// If distance between found neighbor and source points is greater than limit distance, points are not added to output lists.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// /// <param name="mappedSourcePoints">Mapped source points contains points that have found neighbor.</param>
        /// <returns>Closest referential points to the given source points.</returns>
        public List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints, out List<Vector<float>> mappedSourcePoints)
        {
            List<Vector<float>> mappedPoints = new List<Vector<float>>();
            mappedSourcePoints = new List<Vector<float>>();

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

                if (Distance.Euclidean(this.referPoints[closestIndex], sourcePoints[i]) < this.maxDistance)
                {
                    mappedPoints.Add(this.referPoints[closestIndex]);
                    mappedSourcePoints.Add(sourcePoints[i]);
                }
            }

            return mappedPoints;
        }
    }

    /// <summary>
    /// An instance of the <see cref="KdTreeMapping"/> class maps points between two 3D objects. It uses algorithm  
    /// </summary>
    /// <seealso cref="KdTree"/>
    /// <seealso cref="IPointMapping"/>
    /// <seealso cref="BruteForceMapping"/>
    public class KdTreeMapping : IPointMapping
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The kd-tree.
        /// </summary>
        private KdTree referTree;

        /// <summary>
        /// The max distance on what pairs are going to be created
        /// </summary>
        private float maxDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="KdTreeMapping"/> class and creates
        /// a kd-tree from the given referential points.
        /// </summary>
        /// <param name="referPoints">Referential points.</param>
        /// <param name="maxDistance">The max distance on what pairs are going to be created.</param>
        public KdTreeMapping(List<Vector<float>> referPoints, float maxDistance)
        {
            this.referTree = new KdTree(referPoints);
            this.maxDistance = maxDistance;
            Log.Debug("Depth of the referential kd-tree " + this.referTree.MaxDepth);
        }

        /// <summary>
        /// Finds correspoding pairs of the closest points for the given list.
        /// Function uses kd-tree structure and finds the nearest neighbor from it.
        /// If distance between found neighbor and source points is greater than limit distance, points are not added to output lists.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="mappedSourcePoints">Mapped source points contains points that have found neighbor.</param>
        /// <returns>Closest referential points to the given source points.</returns>
        public List<Vector<float>> MapPoints(List<Vector<float>> sourcePoints, out List<Vector<float>> mappedSourcePoints)
        {
            List<Vector<float>> mappedPoints = new List<Vector<float>>();
            mappedSourcePoints = new List<Vector<float>>();

            for (int i = 0; i < sourcePoints.Count; i++)
            {
                Vector<float> tempPoint = this.referTree.FindNearestPoint(sourcePoints[i]);
                if (Distance.Euclidean(tempPoint, sourcePoints[i]) < this.maxDistance)
                {
                    mappedPoints.Add(tempPoint);
                    mappedSourcePoints.Add(sourcePoints[i]);
                }

                Log.Debug("Number of checked nodes in " + i + ". iteration = " + this.referTree.CheckedNodeCount);
            }

            return mappedPoints;
        }
    }
}