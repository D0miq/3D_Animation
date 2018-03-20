namespace GFileFormatter
{
    /// <summary>
    /// An instance of the <see cref="IFileReader"/> interface represents a reader of a mesh saved in a file.
    /// </summary>
    /// <seealso cref="ObjFileReader"/>
    internal interface IFileReader
    {
        /// <summary>
        /// Reads all data from a file. Read data are vertices, texture coordinates, faces and normals.
        /// Vertices are saved into the given <paramref name="verticesBuffer"/> and rest is returned in a <see cref="Mesh"/>.
        /// If file does not contain textures or normals returned lists in the <see cref="Mesh"/> are empty except faces, they have to be present in a file.
        /// </summary>
        /// <param name="verticesBuffer">The buffer that contains vertices.</param>
        /// <returns>Mesh contains texture coordinates, faces and normals.</returns>
        Mesh Read(ListBuffer<float> verticesBuffer);

        /// <summary>
        /// Reads vertices from a file and saves them into the given <paramref name="verticesBuffer"/>.
        /// </summary>
        /// <param name="verticesBuffer">The buffer that contains vertices.</param>
        void ReadVertices(ListBuffer<float> verticesBuffer);

        /// <summary>
        /// Closes the reader.
        /// </summary>
        void Close();
    }
}
