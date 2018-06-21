namespace Registration_v2.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// An instance of the <see cref="Model3DBean"/> class stores all models imported to the application.
    /// </summary>
    public class Model3DBean
    {
        /// <summary>
        /// The models the application cares about.
        /// </summary>
        private List<Model3DFile> modelListBean;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model3DBean"/> class.
        /// </summary>
        public Model3DBean()
        {
            this.modelListBean = new List<Model3DFile>();
        }

        /// <summary>
        /// Gets a list of all models imported to the application.
        /// </summary>
        public List<Model3DFile> ModelListBean
        {
            get => this.modelListBean;
        }
    }
}
