namespace Registration_v2.IO
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Media.Media3D;
    using log4net;

    /// <summary>
    /// An instance of the <see cref="ObjFileWriter"/> class represents a writer that creates a new .obj file.
    /// </summary>
    public class ObjFileWriter
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The writer that is used to access a file.
        /// </summary>
        private StreamWriter stream;

        /// <summary>
        /// The format of numbers in a generated file.
        /// </summary>
        private NumberFormatInfo numberFormatInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjFileWriter"/> class.
        /// </summary>
        /// /// <param name="path">The path of a file that is going to be written.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        /// <exception cref="ArgumentException">Invalid argument.</exception>
        /// <exception cref="System.Security.SecurityException">It does not have a permission to write.</exception>
        public ObjFileWriter(string path)
        {
            Log.Info("Creating an ObjFileWriter.");
            this.stream = new StreamWriter(path);
            this.numberFormatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = string.Empty,
                NumberDecimalDigits = 10
            };
        }

        /// <summary>
        /// Writes vertices and faces from the given model into an .obj file.
        /// </summary>
        /// <param name="model">Model that is going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        /// <exception cref="InvalidOperationException">The file format is not supported.</exception>
        public void WriteModel(Model3DGroup model)
        {
            try
            {
                Log.Info("Writing a model");
                GeometryModel3D geometry = model.Children[0] as GeometryModel3D;
                MeshGeometry3D meshGeometry = geometry.Geometry as MeshGeometry3D;
                this.WriteVertices(meshGeometry);
                this.WriteIndices(meshGeometry);
            } catch (InvalidOperationException e)
            {
                Log.Error("The file format is not supported.", e);
                throw e;
            } catch (IOException e)
            {
                Log.Error("Error during writing to the file.", e);
                throw e;
            }
        }

        /// <summary>
        /// Closes a stream.
        /// </summary>
        public void Close()
        {
            Log.Info("Closing a stream.");
            this.stream.Close();
        }


        /// <summary>
        /// Writes vertices into an .obj file.
        /// </summary>
        /// <param name="geometry">The geometry that contains vertices.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        private void WriteVertices(MeshGeometry3D geometry)
        {
            Log.Info("Writing vertices.");
            foreach (Point3D point in geometry.Positions)
            {
                this.stream.WriteLine("v {0} {1} {2}", point.X.ToString("N", this.numberFormatInfo), point.Y.ToString("N", this.numberFormatInfo), point.Z.ToString("N", this.numberFormatInfo));
            }
        }

        /// <summary>
        /// Writes faces into an .obj file.
        /// </summary>
        /// <param name="geometry">The geometry that contains faces.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        private void WriteIndices(MeshGeometry3D geometry)
        {
            Log.Info("Writing indices.");
            for (int i = 0; i < geometry.TriangleIndices.Count; i += 3)
            {
                this.stream.WriteLine("f {0} {1} {2}", geometry.TriangleIndices[i] + 1, geometry.TriangleIndices[i + 1] + 1, geometry.TriangleIndices[i + 2] + 1);
            }
        }
    }
}
