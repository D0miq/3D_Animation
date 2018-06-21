namespace Registration_v2.UI
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using log4net;
    using Registration_v2.IO;
    using Registration_v2.Tools.Registration.Rigid;
    using Registration_v2.Data;

    public partial class NonrigidSettingsForm : Form
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<Model3DFile> sourceModels;

        private List<Model3DFile> targetModels;

        private int maxIterations;

        public NonrigidSettingsForm(List<Model3DFile> fileList)
        {
            this.InitializeComponent();
            this.sourceListBox.Items.AddRange(fileList.Cast<object>().ToArray());
            this.targetListBox.Items.AddRange(fileList.Cast<object>().ToArray());
        }

        public List<Model3DFile> SourceModels { get => this.sourceModels; }
        public List<Model3DFile> TargetModels { get => this.targetModels; }
        public int MaxIterations { get => this.maxIterations; }

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
