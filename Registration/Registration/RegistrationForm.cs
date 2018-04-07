namespace Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using log4net;
    using MathNet.Numerics.LinearAlgebra;
    using Registration.Rigid;

    /// <summary>
    /// An instance of the <see cref="RegistrationForm"/> class creates a window of the application and processes events from controls.
    /// </summary>
    public partial class RegistrationForm : Form
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gives permission to change a tab of the tabControl.
        /// </summary>
        private bool disallowSelect = true;

        /// <summary>
        /// The path of the referential file.
        /// </summary>
        private string referenceFile = string.Empty;

        /// <summary>
        /// The path of the target direction where files are going to be saved.
        /// </summary>
        private string saveDirectory = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationForm"/> class.
        /// </summary>
        public RegistrationForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Checks all necessary conditions, like referential and source file existence or save directory.
        /// Then it starts a registration process.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"start registration\" button.");

            /*
             * Checks if referential file and at least one source file have been selected for registration.
             * If any of these were not selected, show an error NessageBox.
             */
            if (this.referenceFile == string.Empty || this.checkedListBox.CheckedItems.Count == 0)
            {
                Log.Warn("The referential file or any source files have not been selected.");
                MessageBox.Show(
                    "The referential file or any source files have not been selected.",
                    "Files not selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            /*
             * Creates a folder browser dialog and allows user to choose folder
             * where mapped objects are going to be saved.
             */
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                Description = "Select where mapped objects are going to be saved."
            };

            /*
             * Checks if a folder has been selected and saves its path into instance member saveDirectory.
             * If a folder has not been selected show an error NessageBox.
             */
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Log.Debug("Target folder has been selected: " + folderBrowserDialog.SelectedPath);
                this.saveDirectory = folderBrowserDialog.SelectedPath;
            }
            else
            {
                Log.Warn("Target directory has not been selected.");
                MessageBox.Show(
                    "Target directory has not been selected.",
                    "Directory not selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Do not let user change settings until registration is finished.
            this.tabControl.Enabled = false;

            // Starts registration process in a new thread.
            this.registrationWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Returns user to the first tab and clears selected referential and all source files.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"reset form\" button.");

            // Returns user to the first tab.
            this.disallowSelect = false;
            this.tabControl.SelectTab(0);

            // Clears selected referential and all source files.
            this.referenceFile = string.Empty;
            this.checkedListBox.Items.Clear();

            // Clears status text.
            this.statusLabel.Text = string.Empty;
        }

        /// <summary>
        /// Creates a file browser dialog and allows user to choose source meshes.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void SourceButton_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"add source files\" button.");

            /*
             * Creates a file browser dialog and allows user to choose source meshes.
             * It forbids everything else than obj file formats and enables multiselect.
             */
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Obj Files|*.obj",
                Title = "Select source files",
                Multiselect = true
            };

            // Checks if files have been selected and saves their paths into checkedListBox.
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Log.Debug("Source files has been chosen: " + openFileDialog.FileNames);
                foreach (string fileName in openFileDialog.FileNames)
                {
                    this.checkedListBox.Items.Add(fileName, true);
                }
            }
        }

        /// <summary>
        /// Creates a file browser dialog and allows user to choose referential mesh.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void ReferenceButton_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"add referential file\" button.");

            /*
             * Creates a file browser dialog and allows user to choose referential mesh.
             * It prohibits everything else than obj file formats.
             */
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Obj Files|*.obj",
                Title = "Select referential file"
            };

            // Checks if a file has been selected and saves its path into referenceFile member.
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Log.Debug("Reference button has been selected: " + openFileDialog.FileName);
                this.statusLabel.Text = "Referential mesh: " + openFileDialog.FileName;
                this.referenceFile = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Checks if every necessary nonrigid settings have been selected and moves user to the last tab.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void NextNonrigid_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"next tab\" button on the nonrigid settings tab.");

            /*
             * Checks if every necessary nonrigid settings have been selected and moves user to the last tab.
             * If something has been set wrong, it shows an error MessageBox.
             */
            if (true)
            {
                Log.Info("Everything has been set right, continue to the last tab.");
                this.disallowSelect = false;
                this.tabControl.SelectTab(3);
            }
            else
            {
                Log.Warn("An important setting has not been set right.");
                MessageBox.Show("Select item from all groups.", "Anything not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks if every necessary rigid settings have been selected and moves user to the last tab.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void NextRigid_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"next tab\" button on the rigid settings tab.");

            /*
             * Checks if every necessary rigid settings have been selected and moves user to the last tab.
             * If something has been set wrong, it shows an error MessageBox.
             */
            if ((this.bruteForceRadioButton.Checked || this.kdTreeRadioButton.Checked) && this.kabschRadioButton.Checked)
            {
                Log.Info("Everything has been set right, continue to the last tab.");
                this.disallowSelect = false;
                this.tabControl.SelectTab(3);
            }
            else
            {
                Log.Warn("An important setting has not been set right.");
                MessageBox.Show("Select item from all groups.", "Anything not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks if a registration method has been selected and moves user to its own setting tab.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void NextRegistration_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on the \"next tab\" button on the registration tab.");

            /*
            * Checks if a registration method has been selected and moves user to its own setting tab.
            * If the registration method has not been selected it shows an error MessageBox.
            */
            if (this.rigidRadioButton.Checked)
            {
                Log.Info("Everything has been set right, continue to the rigid settings tab.");
                this.disallowSelect = false;
                this.tabControl.SelectTab(1);
            }
            else if (this.nonrigidRadioButton.Checked)
            {
                Log.Info("Everything has been set right, continue to the nonrigid settings tab.");
                this.disallowSelect = false;
                this.tabControl.SelectTab(2);
            }
            else
            {
                Log.Warn("A registration method has to be selected.");
                MessageBox.Show("A registration method has to be selected.", "Nothing selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Allows user to change a tab only when he clicks on a prepared button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            Log.Info("Clicked on a tab.");
            Log.Debug("Do not allow selection: " + this.disallowSelect);

            /*
             * Enables change of tab only when disallowSelect is false.
             * It is set only when user clicks on next button.
             */
            e.Cancel = this.disallowSelect;
            this.disallowSelect = true;
        }

        private void RegistrationWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IFileReader fileReader = new ObjFileReader(this.referenceFile);
            List<Vector<float>> referencePoints = fileReader.ReadVertices();

            if (this.rigidRadioButton.Checked)
            {
                IRotation rotationAlgorithm;
                IPointMapping pointMapping;

                if (this.bruteForceRadioButton.Checked)
                {
                    pointMapping = new BruteForceMapping(referencePoints);
                }
                else if (this.kdTreeRadioButton.Checked)
                {
                    pointMapping = new KdTreeMapping(referencePoints);
                }

                if (this.kabschRadioButton.Checked)
                {
                    rotationAlgorithm = new Kabsch();
                }


            }
            else
            {
                this.statusLabel.Text = "Nonrigid registration has not been implemented yet.";
            }
        }

        private void RegistrationWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.Debug("Progress percentage: " + e.ProgressPercentage);
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void RegistrationWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Info("Registration of all meshes is finished.");
            this.progressBar.Value = 0;
            this.statusLabel.Text = "Registration of all meshes is finished.";
            this.tabControl.Enabled = true;
        }
    }
}
