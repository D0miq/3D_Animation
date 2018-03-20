namespace _3DGraphicsCompression.ObjParser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using log4net;

    /// <summary>
    /// An instance of the <see cref="ObjFileReader"/> class represents a reader of a mesh saved in an obj file.
    /// </summary>
    /// <seealso cref="IFileReader"/>
    public class ObjFileReader : IFileReader
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// En-GB specific number format.
        /// </summary>
        private static readonly CultureInfo CultureInfo = CultureInfo.CreateSpecificCulture("en-GB");

        /// <summary>
        /// The reader that is used to access a file.
        /// </summary>
        private StreamReader reader;

        /// <summary>
        /// The path of a file.
        /// </summary>
        private string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjFileReader"/> class.
        /// </summary>
        /// <param name="path">Path of a file that is going to be read.</param>
        /// <exception cref="IOException">File can not be read.</exception>
        /// <exception cref="ArgumentException">Invalid argument.</exception>
        public ObjFileReader(string path)
        {
            Log.Debug("Path of a file: " + path);
            this.reader = new StreamReader(path);
            this.path = path;
        }

        /// <summary>
        /// Gets path of a read file.
        /// </summary>
        public string Path => this.path;

        /// <summary>
        /// Reads all data from an obj file. Read data are vertices, texture coordinates, faces and normals.
        /// Vertices are saved into the given <paramref name="verticesBuffer"/> and rest is returned in a <see cref="Mesh"/>.
        /// If file does not contain textures or normals returned lists in the <see cref="Mesh"/> are empty except faces, they have to be present in a file.
        /// </summary>
        /// <param name="verticesBuffer">The buffer that contains vertices.</param>
        /// <returns>Mesh contains texture coordinates, faces and normals.</returns>
        /// <exception cref="IOException">File cannot be read.</exception>
        public Mesh Read(ListBuffer<float> verticesBuffer)
        {
            List<Face> faces = new List<Face>();
            List<float> textureCoords = new List<float>();
            List<float> normals = new List<float>();

            string line;
            while ((line = this.reader.ReadLine()) != null)
            {
                this.CheckVertex(line, verticesBuffer);
                this.CheckFace(line, faces);
                this.CheckTextureCoords(line, textureCoords);
                this.CheckNormal(line, normals);
            }

            return new Mesh(faces, textureCoords, normals);
        }

        /// <summary>
        /// Reads vertices from an obj file and saves them into the given <paramref name="verticesBuffer"/>.
        /// </summary>
        /// <param name="verticesBuffer">The buffer that contains vertices.</param>
        /// <exception cref="IOException">File cannot be read.</exception>
        public void ReadVertices(ListBuffer<float> verticesBuffer)
        {
            string line;
            while ((line = this.reader.ReadLine()) != null)
            {
                this.CheckVertex(line, verticesBuffer);
            }
        }

        /// <summary>
        /// Closes the reader.
        /// </summary>
        public void Close()
        {
            this.reader.Close();
        }

        /// <summary>
        /// Checks if the given line contains a vertex and if so, adds it to the buffer.
        /// </summary>
        /// <param name="line">Line of a file.</param>
        /// <param name="verticesBuffer">Buffer of vertices.</param>
        private void CheckVertex(string line, ListBuffer<float> verticesBuffer)
        {
            if (line.StartsWith("v "))
            {
                string[] stringValues = line.Split(' '); // Values in a read line.

                float.TryParse(stringValues[1], NumberStyles.Float, CultureInfo, out float vertexCoord);
                verticesBuffer.Add(vertexCoord);

                float.TryParse(stringValues[2], NumberStyles.Float, CultureInfo, out vertexCoord);
                verticesBuffer.Add(vertexCoord);

                float.TryParse(stringValues[3], NumberStyles.Float, CultureInfo, out vertexCoord);
                verticesBuffer.Add(vertexCoord);
            }
        }

        /// <summary>
        /// Checks if the given line contains a face and if so, adds it to the list.
        /// </summary>
        /// <param name="line">Line of a file.</param>
        /// <param name="faces">List of faces.</param>
        private void CheckFace(string line, List<Face> faces)
        {
            if (line.StartsWith("f "))
            {
                string[] stringValues = line.Remove(0, 2).Split(' '); // Values in a read line.
                faces.Add(new Face().FromString(stringValues));
            }
        }

        /// <summary>
        /// Checks if the given line contains a texture coordinates and if so, adds it to the list.
        /// </summary>
        /// <param name="line">Line of a file.</param>
        /// <param name="textureCoords">List of texture coordinates.</param>
        private void CheckTextureCoords(string line, List<float> textureCoords)
        {
            if (line.StartsWith("vt "))
            {
                string[] stringValues = line.Split(' '); // Values in a read line.

                float.TryParse(stringValues[1], NumberStyles.Float, CultureInfo, out float textureCoord);
                textureCoords.Add(textureCoord);

                float.TryParse(stringValues[2], NumberStyles.Float, CultureInfo, out textureCoord);
                textureCoords.Add(textureCoord);
            }
        }

        /// <summary>
        /// Checks if the given line contains a normal and if so, adds it to the list.
        /// </summary>
        /// <param name="line">Line of a file.</param>
        /// <param name="normals">List of normals.</param>
        private void CheckNormal(string line, List<float> normals)
        {
            if (line.StartsWith("vn "))
            {
                string[] stringValues = line.Split(' '); // Values in a read line.

                float.TryParse(stringValues[1], NumberStyles.Float, CultureInfo, out float normalCoord);
                normals.Add(normalCoord);

                float.TryParse(stringValues[2], NumberStyles.Float, CultureInfo, out normalCoord);
                normals.Add(normalCoord);

                float.TryParse(stringValues[3], NumberStyles.Float, CultureInfo, out normalCoord);
                normals.Add(normalCoord);
            }
        }
    }
}
