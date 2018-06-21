namespace Registration_v2.Tools.Registration.Rigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="RigidRegistration"/> class provides a rigid registration which maps source points to target points without deformations.
    /// </summary>
    /// <seealso cref="IRegistration"/>
    /// <seealso cref="NonrigidRegistration"/>
    /// <seealso cref="IRotation"/>
    /// <seealso cref="IPointMapping"/>
    public class RigidRegistration : IRegistration
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The mapping algorithm.
        /// </summary>
        private IPointMapping pointMapping;

        /// <summary>
        /// The rotation algorithm.
        /// </summary>
        private IRotation rotation;

        /// <summary>
        /// The max number of iterations.
        /// </summary>
        private int numberOfIterations;

        /// <summary>
        /// The max distance which ends the mapping.
        /// </summary>
        private float maxDistance = 0.1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="RigidRegistration"/> class and sets a mapping algorithm and max number of iterations.
        /// </summary>
        /// <param name="pointMapping">The mapping algorithm.</param>
        /// <param name="numberOfIterations">The number of iterations.</param>
        public RigidRegistration(IPointMapping pointMapping, int numberOfIterations)
        {
            Log.Info("Creating rigid registration.");
            this.pointMapping = pointMapping;
            this.rotation = new Kabsch();
            this.numberOfIterations = numberOfIterations;
        }

        /// <summary>
        /// Registers source points to target points. It only translates and rotates whole mesh.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <returns>Registered source points.</returns>
        public List<Vector<float>> ComputeRegistration(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints)
        {
            Log.Info("Computing registration.");
            Log.Info("Cloning the given source.");
            List<Vector<float>> copySourcePoints = new List<Vector<float>>();
            sourcePoints.ForEach(point => copySourcePoints.Add(point.Clone()));

            Matrix<float> transformationMatrix = this.ComputeRegistrationMatrix(sourcePoints, targetPoints);
            Transformation3D.ApplyTransformation(copySourcePoints, transformationMatrix);

            return copySourcePoints;
        }

        /// <summary>
        /// Registers source points to target points. It only translates and rotates whole mesh.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <returns>Transformation matrix.</returns>
        public Matrix<float> ComputeRegistrationMatrix(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints)
        {
            Log.Info("Computing registration matrix.");
            Log.Info("Cloning the given lists.");

            // Copy points.
            List<Vector<float>> copySourcePoints = new List<Vector<float>>();
            sourcePoints.ForEach(point => copySourcePoints.Add(point.Clone()));
            List<Vector<float>> copyTargetPoints = new List<Vector<float>>();
            targetPoints.ForEach(point => copyTargetPoints.Add(point.Clone()));

            int iteration = 0;
            Matrix<float> transformationMatrix = Matrix<float>.Build.DenseIdentity(4);
            List<Vector<float>> mappedSourcePoints;
            List<Vector<float>> mappedTargetPoints;
            Centroid mappedTargetCentroid;

            do
            {
                Log.Debug("Number of iterations: " + iteration);

                // Map points.
                this.pointMapping.MapPoints(copySourcePoints, copyTargetPoints, out mappedSourcePoints, out mappedTargetPoints);

                // Compute centroid of target points and translate them.
                mappedTargetCentroid = new Centroid(mappedTargetPoints);
                Transformation3D.Translate(copyTargetPoints, -mappedTargetCentroid.Value);

                // Compute centroid of source points, translate them and add translation to a transformation matrix.
                Centroid mappedSourceCentroid = new Centroid(mappedSourcePoints);
                Transformation3D.Translate(copySourcePoints, -mappedSourceCentroid.Value);
                transformationMatrix = Transformation3D.CreateTranslationMatrix(-mappedSourceCentroid.Value) * transformationMatrix;

                // Compute rotation matrix, apply it on source points and add it to the transformation matrix.
                Matrix<float> rotationMatrix = this.rotation.CalculateRotation(mappedSourcePoints, mappedTargetPoints);
                Transformation3D.ApplyTransformation(copySourcePoints, rotationMatrix);
                transformationMatrix = rotationMatrix * transformationMatrix;
            } while (!this.CheckDistance(mappedSourcePoints, mappedTargetPoints) && ++iteration < this.numberOfIterations);

            // Compute transformation from origin to a target position.
            Centroid targetCentroid = new Centroid(targetPoints);
            Centroid copyTargetCentroid = new Centroid(copyTargetPoints);
            mappedTargetCentroid = new Centroid(mappedTargetPoints);
            return Transformation3D.CreateTranslationMatrix(targetCentroid.Value - (copyTargetCentroid.Value - mappedTargetCentroid.Value)) * transformationMatrix;
        }

        /// <summary>
        /// Checks if distance between target points and source points is smaller than the given max distance.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <returns>True - if all pairs fulfils the limit, false otherwise.</returns>
        private bool CheckDistance(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints)
        {
            Log.Info("Checking distance.");
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                if (Distance.Euclidean(sourcePoints[i], targetPoints[i]) > this.maxDistance)
                {
                    Log.Debug("Distance is greater than allowed limit.");
                    return false;
                }
            }

            Log.Debug("Distance is lesser than allowed limit.");
            return true;
        }
    }
}
