namespace Registration_v2.Data
{
    using System.Collections.Generic;
    using System.Windows.Media.Media3D;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the class <see cref="Model3DData"/> provides access to geometry and other characteristics of a 3D model.
    /// </summary>
    public class Model3DData
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The 3D model.
        /// </summary>
        private Model3DGroup model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model3DData"/> class which provides an access to geometry of the given model.
        /// </summary>
        /// <param name="model">The given model.</param>
        public Model3DData(Model3DGroup model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets a model whose data an instance of the <see cref="Model3DData"/> extracts.
        /// </summary>
        public Model3DGroup Model
        {
            get => this.model;
        }

        /// <summary>
        /// Gets a list of vertices from a 3D model.
        /// </summary>
        /// <returns>The list of vertices.</returns>
        public List<Vector<float>> GetVertices()
        {
            Log.Info("Reading vertices from Model3D");

            // Get a geometry from the model.
            GeometryModel3D modelScull = (GeometryModel3D)this.model.Children[0];
            MeshGeometry3D meshGeometry = (MeshGeometry3D)modelScull.Geometry;

            // Read vertices from the geometry and copy it to a new vertex buffer.
            List<Vector<float>> vertexBuffer = new List<Vector<float>>();
            foreach (Point3D point in meshGeometry.Positions)
            {
                Vector<float> vec = Vector<float>.Build.Dense(4);
                vec[0] = (float)point.X;
                vec[1] = (float)point.Y;
                vec[2] = (float)point.Z;
                vec[3] = 1f;
                vertexBuffer.Add(vec);
            }

            return vertexBuffer;
        }

        /// <summary>
        /// Sets the given list of vertices to a 3D model.
        /// </summary>
        /// <param name="vertices">The list of vertices.</param>
        public void SetVertices(List<Vector<float>> vertices)
        {
            Log.Info("Setting vertices to a model");

            // Copy vertices to a new collection of points.
            Point3DCollection point3DCollection = new Point3DCollection();
            foreach (Vector<float> vec in vertices)
            {
                Point3D point = new Point3D(vec[0], vec[1], vec[2]);
                point3DCollection.Add(point);
            }

            // Set the collection of added points to a geometry of the model.
            GeometryModel3D modelScull = (GeometryModel3D)this.model.Children[0];
            MeshGeometry3D meshGeometry = (MeshGeometry3D)modelScull.Geometry;
            meshGeometry.Positions = point3DCollection;
        }
    }
}
