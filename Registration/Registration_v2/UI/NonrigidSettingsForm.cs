namespace Registration_v2.UI
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using log4net;
    using Registration_v2.Data;

    /// <summary>
    /// An instance of the <see cref="NonrigidSettingsForm"/> class represents a form which enables a user set parameters of a nonrigid registration.
    /// </summary>
    public partial class NonrigidSettingsForm : Form
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Selected source models.
        /// </summary>
        private List<Model3DFile> sourceModels;

        /// <summary>
        /// Selected target models.
        /// </summary>
        private List<Model3DFile> targetModels;

        /// <summary>
        /// The maximal number of iterations.
        /// </summary>
        private int maxIterations;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonrigidSettingsForm"/> class with a list of models.
        /// </summary>
        /// <param name="fileList">The list of models.</param>
        public NonrigidSettingsForm(List<Model3DFile> fileList)
        {
            this.InitializeComponent();
            this.sourceListBox.Items.AddRange(fileList.Cast<object>().ToArray());
            this.targetListBox.Items.AddRange(fileList.Cast<object>().ToArray());
        }

        /// <summary>
        /// Gets selected source models.
        /// </summary>
        public List<Model3DFile> SourceModels { get => this.sourceModels; }

        /// <summary>
        /// Gets selected target models.
        /// </summary>
        public List<Model3DFile> TargetModels { get => this.targetModels; }

        /// <summary>
        /// Gets the maximal number of iterations.
        /// </summary>
        public int MaxIterations { get => this.maxIterations; }

        /// <summary>
        /// Checks if all values in a form are set correctly.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">The arguments of na event.</param>
        private void NonrigidSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                return;
            }

            int.TryParse(this.iterationsTextBox.Text, out this.maxIterations);
            if (this.maxIterations <= 0)
            {
                Log.Error("Wrong input number of iterations");
                MessageBox.Show(this, "Number of iterations is not valid!\nPlease enter a value greater than 0.", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (this.sourceListBox.SelectedItems.Count == 0 || this.targetListBox.SelectedItems.Count == 0)
            {
                Log.Error("Source or referential models have not been selected.");
                MessageBox.Show(this, "Please select a source model that should be registered to checked referential models.", "Source or referential models not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            else
            {
                this.sourceModels = this.sourceListBox.SelectedItems.Cast<Model3DFile>().ToList();
                this.targetModels = this.targetListBox.SelectedItems.Cast<Model3DFile>().ToList();
            }
        }
    }
}
