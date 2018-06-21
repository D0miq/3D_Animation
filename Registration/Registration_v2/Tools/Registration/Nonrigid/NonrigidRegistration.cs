namespace Registration_v2.Tools.Registration.Nonrigid
{
    using System.Collections.Generic;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;
    using Registration_v2.Tools.Registration.Rigid;

    /// <summary>
    /// An instance of the <see cref="NonrigidRegistration"/> class provides a nonrigid registration which maps source points to target points and deforms them.
    /// </summary>
    /// <seealso cref="IRegistration"/>
    /// <seealso cref="RigidRegistration"/>
    /// <seealso cref="Rbf"/>
    /// <seealso cref="ControlPointsGenerator"/>
    public class NonrigidRegistration : IRegistration
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The rigid registration which is used during a nonrigid algorithm.
        /// </summary>
        private RigidRegistration rigidRegistration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonrigidRegistration"/> class and sets a rigid registration which will be used during a nonrigid algorithm.
        /// </summary>
        /// <param name="rigidRegistration">The rigid registration.</param>
        public NonrigidRegistration(RigidRegistration rigidRegistration)
        {
            this.rigidRegistration = rigidRegistration;
        }

        /// <summary>
        /// Registers source points to target points. It does not change just a position of source points but also deforms it.
        /// </summary>
        /// <param name="sourcePoints">Source points.</param>
        /// <param name="targetPoints">Target points.</param>
        /// <returns>Registered source points.</returns>
        public List<Vector<float>> ComputeRegistration(List<Vector<float>> sourcePoints, List<Vector<float>> targetPoints)
        {
            // Compute a rigid registration between source and target points so they overlap.
            Log.Info("Registering points.");
            List<Vector<float>> registeredSourcePoints = this.rigidRegistration.ComputeRegistration(sourcePoints, targetPoints);

            /*
             * Find mapped pairs between registered source points and target points (they overlap) because one can be smaller than the other and control points should
             * be generated only on the mapped part. For example imagine a head as source points and a face as target points. Control points should be generated
             * only on the face of source points.
             */
            Log.Info("Mapping points.");
            KdTreeMapping treeMapping = new KdTreeMapping();
            List<Vector<float>> mappedSourcePoints;
            List<Vector<float>> mappedTargetPoints;
            treeMapping.MapPoints(registeredSourcePoints, targetPoints, out mappedSourcePoints, out mappedTargetPoints);

            // Selects random points (control points) from the mapped source points. Control points are used for mapping of deformed parts of a mesh.
            Log.Info("Generating control points.");
            ControlPointsGenerator pointsGenerator = new ControlPointsGenerator(mappedSourcePoints);
            List<Vector<float>> controlPoints = pointsGenerator.GetRandomPoints();

            /*
             * Compute correction vectors for each control point. It is done by searching points in an area around the control point. Afterwards these searched points are
             * passed to the rigid registration which returns a transformation matrix for related to mapping between the area and the target points. This matrix is then used
             * to transform the control point and the correction vector is computed as difference between transformed control point and its original value.
             */
            Log.Info("Computing correction vectors.");
            List<Vector<float>> correctionVectors = new List<Vector<float>>();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                List<Vector<float>> closePoints = pointsGenerator.FindClosePoints(controlPoints[i]);
                Matrix<float> transformMatrix = this.rigidRegistration.ComputeRegistrationMatrix(closePoints, targetPoints);
                Vector<float> copyPoint = controlPoints[i].Clone();
                copyPoint = transformMatrix * copyPoint;
                correctionVectors.Add(copyPoint - controlPoints[i]);
            }

            // Interpolate correction vectors for a whole registered source point cloud. Each point in the point cloud will get own correction vector.
            Log.Info("Interpolating correction vectors.");
            Rbf rbf = new Rbf();
            List<Vector<float>> interpolatedCorrectionVectors = rbf.Interpolate(registeredSourcePoints, controlPoints, correctionVectors);
            for (int i = 0; i < registeredSourcePoints.Count; i++)
            {
                registeredSourcePoints[i] = registeredSourcePoints[i] + interpolatedCorrectionVectors[i];
            }

            return registeredSourcePoints;
        }
    }
}
