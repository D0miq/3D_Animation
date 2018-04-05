namespace Registration
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
        /// <exception cref="IOException">Unable to read the file.</exception>
        /// <exception cref="ArgumentException">Invalid argument.</exception>
        public ObjFileReader(string path)
        {
            Log.Debug("Path of a file: " + path);
            this.reader = new StreamReader(path);
            this.path = path;
        }

        /// <summary>
        /// Gets the path of a read file.
        /// </summary>
        public string Path => this.path;

        /// <summary>
        /// Reads vertices from an obj file. It goes through the whole file.
        /// </summary>
        /// <returns>The list that contains vertices.</returns>
        /// <exception cref="IOException">Unable to read from the file.</exception>
        public List<float> ReadVertices()
        {
            List<float> vertices = new List<float>();

            string line;
            while ((line = this.reader.ReadLine()) != null)
            {
                this.CheckVertex(line, vertices);
            }

            return vertices;
        }

        /// <summary>
        /// Closes the reader.
        /// </summary>
        public void Close()
        {
            this.reader.Close();
        }

        /// <summary>
        /// Checks if the given line contains a vertex and if so, adds it to the list.
        /// </summary>
        /// <param name="line">The line of a file.</param>
        /// <param name="vertices">The list of vertices.</param>
        private void CheckVertex(string line, List<float> vertices)
        {
            if (line.StartsWith("v "))
            {
                string[] stringValues = line.Split(' '); // Values in a read line.

                float.TryParse(stringValues[1], NumberStyles.Float, CultureInfo, out float vertexCoord);
                vertices.Add(vertexCoord);

                float.TryParse(stringValues[2], NumberStyles.Float, CultureInfo, out vertexCoord);
                vertices.Add(vertexCoord);

                float.TryParse(stringValues[3], NumberStyles.Float, CultureInfo, out vertexCoord);
                vertices.Add(vertexCoord);
            }
        }
    }
}