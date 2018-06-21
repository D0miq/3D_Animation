namespace Registration_v2.Data
{
    using System.IO;
    using log4net;

    /// <summary>
    /// An instance of the <see cref="Model3DFile"/> class stores informations about files where a model is saved. It contains its original location provided from a user,
    /// its name which is shown to the user and a location of a file that is used for editation without affecting the original file.
    /// </summary>
    public class Model3DFile
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The path of an original file.
        /// </summary>
        private string originalPath;

        /// <summary>
        /// The name which is used for identification.
        /// </summary>
        private string name;

        /// <summary>
        /// The index of an imported model. It should be incremented when multiple files with the same name are imported.
        /// </summary>
        private int index = 0;

        /// <summary>
        /// The path of a file that is used for editation.
        /// </summary>
        private string editPath;

        /// <summary>
        /// The indication of a previous editation of a file.
        /// </summary>
        private bool changed = false;

        /// <summary>
        /// The indication of storing changes to an original file.
        /// </summary>
        private bool exported;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model3DFile"/> class with the path of an original file. It sets the name with its original name and creates
        /// a temp file which is used for editation of a model.
        /// </summary>
        /// <param name="originalPath">The path of an original file.</param>
        public Model3DFile(string originalPath)
        {
            Log.Info("Creating a new model file.");
            this.originalPath = originalPath;
            this.name = Path.GetFileNameWithoutExtension(this.OriginalPath);
            this.editPath = Path.ChangeExtension(Path.GetTempPath() + Path.GetRandomFileName(), Path.GetExtension(originalPath));
            File.Copy(this.originalPath, this.editPath, true);
            this.exported = true;
            Log.Debug("Original file: " + this.originalPath);
            Log.Debug("Temp file: " + this.editPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model3DFile"/> class and only creates a new temp file.
        /// </summary>
        public Model3DFile()
        {
            Log.Info("Creating a new model file.");
            this.editPath = Path.ChangeExtension(Path.GetTempPath() + Path.GetRandomFileName(), ".obj");
            this.name = Path.GetFileNameWithoutExtension(this.EditPath);
            this.exported = false;
            Log.Debug("Temp file:" + this.editPath);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Model3DFile"/> class. It also deletes a temp file that was used for editation.
        /// </summary>
        ~Model3DFile()
        {
            Log.Info("Deleting a temp file.");
            Log.Debug("Temp file: " + this.editPath);
            File.Delete(this.EditPath);
        }

        /// <summary>
        /// Gets a path of an original file.
        /// </summary>
        public string OriginalPath
        {
            get => this.originalPath;
        }

        /// <summary>
        /// Gets a name which is used for identification.
        /// </summary>
        public string Name
        {
            get => this.name;
        }

        /// <summary>
        /// Gets a path of a file that is used for editation.
        /// </summary>
        public string EditPath
        {
            get => this.editPath;
        }

        /// <summary>
        /// Gets or sets index of imported model. It should be incremented when multiple files with the same name are imported.
        /// </summary>
        public int Index
        {
            get => this.index;
            set => this.index = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a model was edited.
        /// </summary>
        public bool Changed
        {
            get => this.changed;
            set => this.changed = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a model was saved in its original file.
        /// </summary>
        public bool Exported
        {
            get => this.exported;
            set => this.exported = value;
        }

        /// <summary>
        /// Provides a string representation of an instance. The string is composed of a name, index and indication of editation.
        /// </summary>
        /// <returns>The string representation of an instance of the <see cref="Model3DFile"/> class.</returns>
        public override string ToString()
        {
            string value = this.exported ? this.name : "*" + this.name;
            if (this.index != 0)
            {
                value += "(" + this.index + ")"; 
            }

            return value;
        }
    }
}
