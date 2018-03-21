namespace _3DGraphicsCompression.ObjParser
{
    using System.Collections.Generic;

    /// <summary>
    /// An instance of the <see cref="Frame"/> class contains characteristics of a mesh.
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class with the given vertices. Other fields are empty lists.
        /// </summary>
        /// <param name="vertices">Vertices of the mesh.</param>
        public Frame(List<float> vertices)
            : this(vertices, new List<Face>(), new List<float>(), new List<float>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="vertices">Vertices of the mesh.</param>
        /// <param name="faces">Faces of the mesh.</param>
        /// <param name="textureCoords">Texture coordinates that map a texture to faces.</param>
        /// <param name="normals">Normals.</param>
        public Frame(List<float> vertices, List<Face> faces, List<float> textureCoords, List<float> normals)
        {
            this.Vertices = vertices;
            this.Faces = faces;
            this.TextureCoords = textureCoords;
            this.Normals = normals;
        }

        /// <summary>
        /// Gets vertices of a mesh.
        /// </summary>
        public List<float> Vertices { get; }

        /// <summary>
        /// Gets or sets faces of a mesh.
        /// </summary>
        public List<Face> Faces { get; set; }

        /// <summary>
        /// Gets or sets texture coordinates of a mesh.
        /// </summary>
        public List<float> TextureCoords { get; set; }

        /// <summary>
        /// Gets or sets normals of a mesh.
        /// </summary>
        public List<float> Normals { get; set; }
    }
}