namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="BruteForceMapping"/> class represents a brute force mapping algorithm.
    /// </summary>
    /// <seealso cref="IPointMapping"/>
    /// <seealso cref="KdTreeMapping"/>
    public class BruteForceMapping : IPointMapping
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Finds correspoding pairs of the closest points for the given lists.
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
        /// Function uses brute force algorithm to map points.
        /// </summary>
        /// <param name="smallerList">The smaller collection.</param>
        /// <param name="biggerList">The bigger collection.</param>
        /// <param name="mappedSmallerList">Mapped smaller collection contains points that have found neighbor.</param>
        /// <param name="mappedBiggerList">Closest points from the bigger collection to the given points from the smaller collection.</param>
        private void Map(List<Vector<float>> smallerList, List<Vector<float>> biggerList, out List<Vector<float>> mappedSmallerList, out List<Vector<float>> mappedBiggerList)
        {
            mappedSmallerList = new List<Vector<float>>();
            mappedBiggerList = new List<Vector<float>>();

            for (int i = 0; i < smallerList.Count; i++)
            {
                int closestIndex = 0;

                for (int j = 0; j < biggerList.Count; j++)
                {
                    if (Distance.Euclidean(biggerList[j], smallerList[i]) < Distance.Euclidean(biggerList[closestIndex], smallerList[i]))
                    {
                        closestIndex = j;
                    }
                }

                Log.Debug("Mapped point: " + smallerList[i]);
                Log.Debug("Found point: " + biggerList[closestIndex]);
                Log.Debug("Index of the found point: " + closestIndex);

                mappedSmallerList.Add(smallerList[i]);
                mappedBiggerList.Add(biggerList[closestIndex]);
            }
        }
    }
}
