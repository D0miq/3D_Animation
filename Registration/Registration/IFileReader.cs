namespace Registration
{
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;

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
        /// Reads vertices from an obj file. It goes through the whole file.
        /// </summary>
        /// <returns>The list that contains vertices.</returns>
        /// <exception cref="System.IO.IOException">Unable to read from the file.</exception>
        List<Vector<float>> ReadVertices();

        /// <summary>
        /// Reads faces from an obj file. It goes through the whole file.
        /// </summary>
        /// <returns>The string that contains all faces.</returns>
        /// <exception cref="System.IO.IOException">Unable to read from the file.</exception>
        string ReadFaces();

        /// <summary>
        /// Closes the reader.
        /// </summary>
        void Close();
    }
}
