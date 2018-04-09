namespace Registration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;
    using System.Globalization;

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
        private StreamWriter writer;

        /// <summary>
        /// Format of numbers in generated file.
        /// </summary>
        private NumberFormatInfo numberFormatInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriter"/> class.
        /// </summary>
        /// /// <param name="targetFileName">Name of a file that is going to be written.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        /// <exception cref="ArgumentException">Invalid argument.</exception>
        /// <exception cref="System.Security.SecurityException">Does not have permission to write.</exception>
        public FileWriter(string targetFileName)
        {
            this.writer = new StreamWriter(targetFileName);
            this.numberFormatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = string.Empty,
                NumberDecimalDigits = 10
            };
        }

        /// <summary>
        /// Writes faces into a 3gbf file as {vertex, texture, normal}.
        /// </summary>
        /// <param name="faces">Faces that are going to be written to the file.</param>
        /// <exception cref="IOException">Error during writing to the file.</exception>
        internal void WriteAll(List<Vector<float>> vertices, string rest)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                this.writer.WriteLine("v {0} {1} {2}", vertices[i][0].ToString("N", this.numberFormatInfo), vertices[i][1].ToString("N", this.numberFormatInfo), vertices[i][2].ToString("N", this.numberFormatInfo));
            }

            this.writer.Write(rest);
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