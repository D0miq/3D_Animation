namespace _3DGraphicsCompression.ObjParser
{
    using System.Collections.Generic;

    /// <summary>
    /// An instance of the <see cref="IFileReader"/> interface represents a reader of a mesh saved in a file.
    /// </summary>
    /// <seealso cref="ObjFileReader"/>
    public interface IFileReader
    {
        /// <summary>
        /// Gets path of a read file.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Reads all data from an obj file. Read data are vertices, texture coordinates, faces and normals.
        /// If the file does not contain textures or normals, returned lists in the <see cref="Frame"/> are empty, except faces, they always have to be present in the file.
        /// </summary>
        /// <returns>The mesh contains vertices, texture coordinates, faces and normals.</returns>
        /// <exception cref="System.IO.IOException">Unable to read from the file.</exception>
        Frame ReadAll();

        /// <summary>
        /// Reads vertices from an obj file. It reads the whole file.
        /// </summary>
        /// <returns>The list that contains vertices.</returns>
        /// <exception cref="System.IO.IOException">Unable to read from the file.</exception>
        List<float> ReadVertices();

        /// <summary>
        /// Closes the reader.
        /// </summary>
        void Close();
    }
}
