namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="KdTreeMapping"/> class maps points between two 3D objects. It uses a kd-tree to do so.
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
        /// Finds correspoding pairs of the closest points for the given source list.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <param name="mappedSourcePoints">Mapped source points contains points that have found neighbor.</param>
        /// <param name="mappedTargetPoints">Closest target points to the given source points.</param>
        public void MapPoints(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints, out List<Vector<float>> mappedSourcePoints, out List<Vector<float>> mappedTargetPoints)
        {
            Log.Info("Mapping points.");
            if (sourcePoints.Count > targetPoints.Count)
            {
                Log.Debug("Mapping target points to source points.");
                this.Map(targetPoints, sourcePoints, out mappedTargetPoints, out mappedSourcePoints);
            }
            else
            {
                Log.Debug("Mapping source points to target points.");
                this.Map(sourcePoints, targetPoints, out mappedSourcePoints, out mappedTargetPoints);
            }
        }

        /// <summary>
        /// Maps a smaller collection to the bigger one.
        /// Function uses kd-tree structure and finds the nearest neighbor from it.
        /// </summary>
        /// <param name="smallerList">The smaller collection.</param>
        /// <param name="biggerList">The bigger collection.</param>
        /// <param name="mappedSmallerList">Mapped smaller collection contains points that have found neighbor.</param>
        /// <param name="mappedBiggerList">Closest points from the bigger collection to the given points from the smaller collection.</param>
        private void Map(List<Vector<float>> smallerList, List<Vector<float>> biggerList, out List<Vector<float>> mappedSmallerList, out List<Vector<float>> mappedBiggerList)
        {
            KdTree kdTree = new KdTree(biggerList);
            mappedSmallerList = new List<Vector<float>>();
            mappedBiggerList = new List<Vector<float>>();

            for (int i = 0; i < smallerList.Count; i++)
            {
                Vector<float> tempPoint = kdTree.FindNearestPoint(smallerList[i]);
                mappedSmallerList.Add(smallerList[i]);
                mappedBiggerList.Add(tempPoint);
                // Log.Debug("Found point: " + tempPoint);
                Log.Debug("Number of checked nodes in " + i + ". iteration = " + kdTree.CheckedNodeCount);
            }
        }
    }
}
