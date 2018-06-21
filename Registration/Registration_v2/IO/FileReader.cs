namespace Registration_v2.IO
{
    using System;
    using HelixToolkit.Wpf;
    using log4net;
    using Registration_v2.Data;

    /// <summary>
    /// An instance of the <see cref="FileReader"/> class allows reading of a model from a file.
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Reads a 3D model from the given path.
        /// </summary>
        /// <param name="path">The path of a 3D model.</param>
        /// <returns>The read 3D model.</returns>
        /// <exception cref="InvalidOperationException">It is thrown when the file format is not supported.</exception>
        public Model3DData ReadModel(string path)
        {
            try
            {
                Log.Info("Reading a model");

                // Import a 3D model file.
                ModelImporter importer = new ModelImporter()
                {
                    DefaultMaterial = Materials.LightGray
                };

                return new Model3DData(importer.Load(path));
            }
            catch (InvalidOperationException e)
            {
                Log.Error("The file format is not supported.", e);
                throw e;
            }
        }
    }
}
