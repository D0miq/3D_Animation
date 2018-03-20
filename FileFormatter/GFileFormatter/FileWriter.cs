namespace GFileFormatter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using GFileFormatter.ObjParser;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;

    /// <summary>
    /// An instance of the <see cref="FileWriter"/> class represents a binary writer that creates a new 3gbf file.
    /// </summary>
    internal class FileWriter
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The writer that is used to access a file.
        /// </summary>
        private BinaryWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriter"/> class.
        /// </summary>
        /// /// <param name="targetFileName">Name of a file that is going to be written.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        /// <exception cref="ArgumentException">Invalid argument.</exception>
        /// <exception cref="System.Security.SecurityException">Does not have permission to write.</exception>
        public FileWriter(string targetFileName)
        {
            this.writer = new BinaryWriter(new FileStream(targetFileName, FileMode.Create));
        }

        /// <summary>
        /// Writes faces into a 3gbf file as {vertex, texture, normal}.
        /// </summary>
        /// <param name="faces">Faces that are going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteFaces(List<Face> faces)
        {
            this.writer.Write(faces.Count);
            foreach (Face face in faces)
            {
                for (int i = 0; i < Face.VALUES_COUNT; i++)
                {
                    this.writer.Write(face.Vertices[i]);
                    this.writer.Write(face.Textures[i]);
                    this.writer.Write(face.Normals[i]);
                }
            }
        }

        /// <summary>
        /// Writes textures into a 3gbf file.
        /// </summary>
        /// <param name="textures">Textures that are going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteTextures(List<float> textures)
        {
            this.writer.Write(textures.Count);
            foreach (float value in textures)
            {
                this.writer.Write(value);
            }
        }

        /// <summary>
        /// Writes normals into a 3gbf file.
        /// </summary>
        /// <param name="normals">Normals that are going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteNormals(List<float> normals)
        {
            this.writer.Write(normals.Count);
            foreach (float value in normals)
            {
                this.writer.Write(value);
            }
        }

        /// <summary>
        /// Writes an average trajectory into a 3gbf file.
        /// </summary>
        /// <param name="averageTrajectory">The average trajectory that is going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteTrajectory(Vector<float> averageTrajectory)
        {
            this.writer.Write(averageTrajectory.Count);
            foreach (float value in averageTrajectory)
            {
                this.writer.Write(value);
            }
        }

        /// <summary>
        /// Writes control trajectories into a 3gbf file.
        /// </summary>
        /// <param name="controlTrajectories">Control trajectories that are going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteControlTrajectories(Matrix<float> controlTrajectories)
        {
            this.writer.Write(controlTrajectories.RowCount);
            this.writer.Write(controlTrajectories.ColumnCount);
            foreach (float value in controlTrajectories.Enumerate())
            {
                this.writer.Write(value);
            }
        }

        /// <summary>
        /// Writes selected part of the eigen vectors into a 3gbf file.
        /// </summary>
        /// <param name="eigenMatrix">The eigen matrix that is going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteEigenVectors(Matrix<float> eigenMatrix)
        {
            this.writer.Write(eigenMatrix.RowCount);
            this.writer.Write(eigenMatrix.ColumnCount);
            foreach (float value in eigenMatrix.Enumerate())
            {
                this.writer.Write(value);
            }
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        internal void Close()
        {
            this.writer.Close();
        }
    }
}