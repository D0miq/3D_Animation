namespace _3DGraphicsCompression
{
    using System.Collections.Generic;
    using System.IO;
    using _3DGraphicsCompression.ObjParser;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="Compression"/> class compresses given files that contains 3D objects.
    /// <see cref="Compression"/> assumes all files have the same number of vertices, same triangles and texture coordinates.
    /// </summary>
    /// <seealso cref="IFileReader"/>
    public class Compression
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The number of frames ready to compression.
        /// </summary>
        private int framesCount;

        /// <summary>
        /// The vector of average trajectory contains average values for each vertex coordinates.
        /// </summary>
        private Vector<float> averageTrajectory;

        /// <summary>
        /// The submatrix of eigen vectors contains selected number of eigen vectors with the biggest eigen values.
        /// </summary>
        private Matrix<float> subEigenVectors;

        /// <summary>
        /// The matrix of control trajectories from submatrix of eigen vectors.
        /// </summary>
        private Matrix<float> controlTrajectories;

        /// <summary>
        /// The vertices, added from all frames, and compressed faces, textures and normals.
        /// </summary>
        private Frame frame;

        /// <summary>
        /// Initializes a new instance of the <see cref="Compression"/> class.
        /// </summary>
        public Compression()
            : this(new List<float>(), 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Compression"/> class with the vertices data of frames and number of them.
        /// Vertices should be sorted (point by point, frame by frame).
        /// </summary>
        /// <param name="data">Vertices data of frames.</param>
        /// <param name="framesCount">The number of frames data are from.</param>
        public Compression(List<float> data, int framesCount)
        {
            this.frame = new Frame(data);
            this.framesCount = framesCount;
        }

        /// <summary>
        /// Gets the vector of average trajectory that contains average values for each vertex coordinates.
        /// </summary>
        public Vector<float> AverageTrajectory => this.averageTrajectory;

        /// <summary>
        /// Gets the submatrix of eigen vectors that contains selected number of eigen vectors with the biggest eigen values.
        /// </summary>
        public Matrix<float> SubEigenVectors => this.subEigenVectors;

        /// <summary>
        /// Gets the matrix of control trajectories from submatrix of eigen vectors.
        /// </summary>
        public Matrix<float> ControlTrajectories => this.controlTrajectories;

        /// <summary>
        /// Gets the vertices, added from all frames, and compressed faces, textures and normals.
        /// </summary>
        public Frame Frame => this.frame;

        /// <summary>
        /// Gets the number of added frames.
        /// </summary>
        public int FramesCount => this.framesCount;

        /// <summary>
        /// Adds vertices of a frame and sets other values from the frame, like faces or normals, for the compression.
        /// Vertices should be sorted (point by point, frame by frame).
        /// </summary>
        /// <param name="frame">The frame.</param>
        public void AddFrame(Frame frame)
        {
            this.frame.Vertices.AddRange(frame.Vertices);
            this.frame.Faces = frame.Faces;
            this.frame.TextureCoords = frame.TextureCoords;
            this.frame.Normals = frame.Normals;
            this.framesCount++;
        }

        /// <summary>
        /// Adds vertices for the compression. Vertices should be sorted (point by point, frame by frame).
        /// </summary>
        /// <param name="vertices">Vertices of the frame.</param>
        public void AddFrame(List<float> vertices)
        {
            this.frame.Vertices.AddRange(vertices);
            this.framesCount++;
        }

        /// <summary>
        /// Compresses vertices of all frames.
        /// </summary>
        /// <param name="controlTrajectoriesCount">The number of control trajectories.</param>
        public void CompressFrames(int controlTrajectoriesCount)
        {
            Log.Debug("Number of control trajectories: " + controlTrajectoriesCount);

            var matrixBuilder = Matrix<float>.Build;
            var vectorBuilder = Vector<float>.Build;

            var matrix = matrixBuilder.Dense(this.framesCount * 3, (this.frame.Vertices.Count / 3) / this.framesCount);

            for (int i = 0; i < this.frame.Vertices.Count; i += 3)
            {
                matrix[(i / 3 / matrix.ColumnCount) * 3, (i / 3) % matrix.ColumnCount] = this.frame.Vertices[i];
                matrix[((i / 3 / matrix.ColumnCount) * 3) + 1, (i / 3) % matrix.ColumnCount] = this.frame.Vertices[i + 1];
                matrix[((i / 3 / matrix.ColumnCount) * 3) + 2, (i / 3) % matrix.ColumnCount] = this.frame.Vertices[i + 2];
            }

            this.averageTrajectory = matrix.RowSums().Divide(matrix.ColumnCount);

            foreach (var column in matrix.EnumerateColumnsIndexed())
            {
                var subtColumn = column.Item2 - this.averageTrajectory;
                matrix.SetColumn(column.Item1, subtColumn);
            }

            var autocorelation = matrix * matrix.Transpose();
            var evd = autocorelation.Evd(Symmetricity.Symmetric);
            var eigenValues = evd.EigenValues;
            var eigenVectors = evd.EigenVectors;
            this.subEigenVectors = eigenVectors.SubMatrix(0, eigenVectors.RowCount, eigenVectors.ColumnCount - controlTrajectoriesCount, controlTrajectoriesCount);
            this.controlTrajectories = this.subEigenVectors.Transpose() * matrix;
        }
    }
}
