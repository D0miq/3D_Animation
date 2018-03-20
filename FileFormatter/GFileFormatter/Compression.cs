namespace GFileFormatter
{
    using System.IO;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="Compression"/> class compresses given files that contains 3D objects. 
    /// <see cref="Compression"/> assumes all files have the same number of vertices, same triangles and texture coordinates.
    /// </summary>
    /// <seealso cref="IFileReader"/>
    internal class Compression
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The list buffer of vertices.
        /// </summary>
        private ListBuffer<float> verticesList;

        /// <summary>
        /// The number of files ready to compression.
        /// </summary>
        private int filesCount;

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
        /// The compressed faces, textures and normals taken from a single file.
        /// </summary>
        private Mesh compressedMesh;

        /// <summary>
        /// Initializes a new instance of the <see cref="Compression"/> class.
        /// </summary>
        public Compression()
        {
            this.verticesList = new ListBuffer<float>();
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
        /// Gets the compressed faces, textures and normals taken from a single file.
        /// </summary>
        public Mesh CompressedMesh => this.compressedMesh;

        /// <summary>
        /// Adds a file for the compression.
        /// </summary>
        /// <param name="file">Path of a file.</param>
        internal void AddFile(string file)
        {
            Log.Debug("Path of a file: " + file);

            try
            {
                IFileReader fileReader = new ObjFileReader(file);
                Log.Debug("File count: " + this.filesCount);
                if (this.filesCount != 0)
                {
                    fileReader.ReadVertices(this.verticesList);
                } else
                {
                    this.compressedMesh = fileReader.Read(this.verticesList);
                }

                fileReader.Close();
                this.filesCount++;
            } catch (IOException e)
            {
                Log.Error("Unable to read " + file + ". " + e.Message);
                FileFormatterForm.statusBar.Text = "Unable to read " + file + ".";
            }
        }

        /// <summary>
        /// Compresses vertices of all files.
        /// </summary>
        /// <param name="controlTrajectoriesCount">The number of control trajectories.</param>
        internal void CompressVertices(int controlTrajectoriesCount)
        {
            Log.Debug("Number of control trajectories: " + controlTrajectoriesCount);

            var matrixBuilder = Matrix<float>.Build;
            var vectorBuilder = Vector<float>.Build;
            this.verticesList.Capacity = this.verticesList.Size;
            var matrix = matrixBuilder.Dense(this.verticesList.Size / this.filesCount, this.filesCount, this.verticesList.Buffer);
            this.averageTrajectory = matrix.RowSums().Divide(matrix.ColumnCount);

            foreach (var column in matrix.EnumerateColumnsIndexed())
            {
                var subtColumn = column.Item2.Subtract(this.averageTrajectory);
                matrix.SetColumn(column.Item1, subtColumn);
            }

            var autocorelation = matrix.Multiply(matrix.Transpose());
            var evd = autocorelation.Evd();
            var eigenValues = evd.EigenValues;
            var eigenVectors = evd.EigenVectors;
            this.subEigenVectors = eigenVectors.SubMatrix(0, eigenVectors.RowCount, controlTrajectoriesCount, eigenVectors.ColumnCount - controlTrajectoriesCount);
            this.controlTrajectories = this.subEigenVectors.Transpose() * matrix;
        }
    }
}
