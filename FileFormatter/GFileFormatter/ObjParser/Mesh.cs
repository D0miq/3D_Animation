namespace GFileFormatter
{
    using System.Collections.Generic;
    using GFileFormatter.ObjParser;

    /// <summary>
    /// An instance of the <see cref="Mesh"/> class contains characteristics of a mesh.
    /// </summary>
    internal class Mesh
    {
        /// <summary>
        /// Faces of a mesh.
        /// </summary>
        private List<Face> faces;

        /// <summary>
        /// Texture coordinates map a texture to faces.
        /// </summary>
        private List<float> textureCoords;

        /// <summary>
        /// Normals.
        /// </summary>
        private List<float> normals;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh"/> class.
        /// </summary>
        /// <param name="faces">Faces of the mesh.</param>
        /// <param name="textureCoords">Texture coordinates that map a texture to faces.</param>
        /// <param name="normals">Normals.</param>
        public Mesh(List<Face> faces, List<float> textureCoords, List<float> normals)
        {
            this.faces = faces;
            this.textureCoords = textureCoords;
            this.normals = normals;
        }

        /// <summary>
        /// Gets faces of a mesh.
        /// </summary>
        public List<Face> Faces => this.faces;

        /// <summary>
        /// Gets texture coordinates.
        /// </summary>
        public List<float> TextureCoords => this.textureCoords;

        /// <summary>
        /// Gets normals.
        /// </summary>
        public List<float> Normals => this.normals;
    }
}