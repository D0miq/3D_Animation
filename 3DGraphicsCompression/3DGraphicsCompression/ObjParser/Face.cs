﻿namespace _3DGraphicsCompression.ObjParser
{
    using log4net;

    /// <summary>
    /// An instance of the <see cref="Face"/> class encapsulates data of a face.
    /// </summary>
    public class Face
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Number of values of a face.
        /// </summary>
        public const int VALUES_COUNT = 3;

        /// <summary>
        /// Delimeter between values of a face.
        /// </summary>
        private const char DELIMETER = '/';

        /// <summary>
        /// Vertices of a face.
        /// </summary>
        private int[] vertices;

        /// <summary>
        /// Textures of a face.
        /// </summary>
        private int[] textures;

        /// <summary>
        /// Normals of a face.
        /// </summary>
        private int[] normals;

        /// <summary>
        /// Initializes a new instance of the <see cref="Face"/> class.
        /// </summary>
        public Face()
        {
            this.vertices = new int[VALUES_COUNT];
            this.textures = new int[VALUES_COUNT];
            this.normals = new int[VALUES_COUNT];
        }

        /// <summary>
        /// Gets vertices of a face.
        /// </summary>
        public int[] Vertices => this.vertices;

        /// <summary>
        /// Gets textures of a face.
        /// </summary>
        public int[] Textures => this.textures;

        /// <summary>
        /// Gets normals of a face.
        /// </summary>
        public int[] Normals => this.normals;

        /// <summary>
        /// Sets face values from a string representation.
        /// </summary>
        /// <param name="values">Three strings where each represents a vertex in a face.</param>
        /// <returns>Created face.</returns>
        public Face FromString(string[] values)
        {
            for (int i = 0; i < VALUES_COUNT; i++)
            {
                this.ParseVertex(values[i], i);
            }

            return this;
        }

        /// <summary>
        /// Parses vertex, texture and normal from a string.
        /// </summary>
        /// <param name="vertex">The string with face data.</param>
        /// <param name="index">Order of the given string.</param>
        private void ParseVertex(string vertex, int index)
        {
            string[] values = vertex.Split(DELIMETER);
            if (values.Length >= 1)
            {
                int.TryParse(values[0], out this.vertices[index]);
            }

            if (values.Length >= 2)
            {
                int.TryParse(values[1], out this.textures[index]);
            }

            if (values.Length == 3)
            {
                int.TryParse(values[2], out this.normals[index]);
            }
        }
    }
}
