namespace Registration_v2.Tools.Registration.Nonrigid
{
    using System;
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="ControlPointsGenerator"/> class lets generate random points from a vertex buffer. It calculates a diameter which separates points from each other.
    /// The diamater can be also used to find nearest points in a area.
    /// </summary>
    public class ControlPointsGenerator
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The diameter of an area around a control point.
        /// </summary>
        private float areaSize;

        /// <summary>
        /// The buffer from which control points are generated.
        /// </summary>
        private List<Vector<float>> vertexBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlPointsGenerator"/> class and sets a buffer from which control points are selected.
        /// </summary>
        /// <param name="vertexBuffer">The buffer that contains future control points.</param>
        public ControlPointsGenerator(List<Vector<float>> vertexBuffer)
        {
            this.vertexBuffer = vertexBuffer;

            // Set the diameter as a hundredth of the longest distance between two points in the vertex buffer.
            this.areaSize = this.FindLongestDistance() / 100;
            Log.Debug("Area size: " + this.areaSize);
        }

        /// <summary>
        /// Selects random points from the buffer assigned to the instance. Points are not deep copied or copied at all. References points to the vertex buffer of the instance.
        /// </summary>
        /// <returns>The list of generated points.</returns>
        public List<Vector<float>> GetRandomPoints()
        {
            Log.Info("Selecting random points from the given buffer.");
            List<Vector<float>> controlPoints = new List<Vector<float>>();

            // Set number of points which are selected. It selects 10% of total points.
            int controlPointsCount = this.vertexBuffer.Count > 0 && this.vertexBuffer.Count < 10 ? 1 : this.vertexBuffer.Count / 10;
            int index = 0;

            Random random = new Random();

            while (index < controlPointsCount)
            {
                Log.Debug("Index: " + index);

                // Select a random point from the buffer.
                Vector<float> tempPoint = this.vertexBuffer[random.Next(this.vertexBuffer.Count)];

                // Check if the point is not already in the list of generated control points.
                if (!controlPoints.Contains(tempPoint))
                {
                    bool isOk = true;
                    for (int i = 0; i < controlPoints.Count; i++)
                    {
                        // Check if point is further than the specified diameter areaSize from all other control points.
                        if (Distance.Euclidean(controlPoints[i], tempPoint) < this.areaSize)
                        {
                            isOk = false;
                            break;
                        }
                    }

                    // Add point to control points list if everything went well.
                    if (isOk)
                    {
                        controlPoints.Add(tempPoint);
                        Log.Debug("Found point: " + tempPoint);
                        index++;
                    }
                }
            }

            return controlPoints;
        }

        /// <summary>
        /// Find points from the buffer, which is assigned to the instance, that are close to the given point. Distance is defined by <see cref="areaSize"/>.
        /// </summary>
        /// <param name="controlPoint">The given point which look for its neighbors.</param>
        /// <returns>The list of points that are close to the given point.</returns>
        public List<Vector<float>> FindClosePoints(Vector<float> controlPoint)
        {
            Log.Info("Finding points from the given buffer.");
            Log.Debug("Given control point: " + controlPoint);
            return this.vertexBuffer.FindAll(point => Distance.Euclidean(point, controlPoint) < this.areaSize);
        }

        /// <summary>
        /// Computes the longest distance between points in the buffer which is assigned to the instance.
        /// </summary>
        /// <returns>The maximal distance between two points.</returns>
        private float FindLongestDistance()
        {
            Log.Info("Finding longest distance.");
            float maxDistance = 0f;
            for (int i = 0; i < this.vertexBuffer.Count; i++)
            {
                for (int j = i; j < this.vertexBuffer.Count; j++)
                {
                    float tempDistance = (float)Distance.Euclidean(this.vertexBuffer[i], this.vertexBuffer[j]);
                    if (tempDistance > maxDistance)
                    {
                        maxDistance = tempDistance;
                    }
                }
            }

            Log.Debug("Found distance: " + maxDistance);
            return maxDistance;
        }
    }
}
