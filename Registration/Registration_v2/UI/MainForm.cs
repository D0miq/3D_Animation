namespace Registration_v2.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;
    using MathNet.Numerics.LinearAlgebra;
    using Registration_v2.Data;
    using Registration_v2.IO;
    using Registration_v2.Tools.Registration.Nonrigid;
    using Registration_v2.Tools.Registration.Rigid;

    /// <summary>
    /// An instance of the <see cref="MainForm"/> class represents a main form of the application.
    /// </summary>
    public partial class MainForm : System.Windows.Forms.Form, CheckListPanel.ICheckListPanelListener
    {
        /// <summary>
        /// The panel with a list of 3D models.
        /// </summary>
        private CheckListPanel listModel3DPanel;

        /// <summary>
        /// The bean used for a binding to imported models.
        /// </summary>
        private Model3DBean model3DBean;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="model3DBean">The bean used for a binding to imported models.</param>
        public MainForm(Model3DBean model3DBean)
        {
            this.InitializeComponent();
            this.model3DBean = model3DBean;
            this.listModel3DPanel = new CheckListPanel(this, this.viewModelsMenuItem.Text);
        }

        /// <summary>
        /// Called when an item is saved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        public void OnSaveClicked(object sender, EventArgs e)
        {
            Model3DFile file = this.listModel3DPanel.GetSelectedItem<Model3DFile>();
            if (file != null)
            {
                this.saveFileDialog.InitialDirectory = Path.GetDirectoryName(file.OriginalPath);
                this.saveFileDialog.FileName = file.Name;
                DialogResult dialogResult = this.saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    this.statusLabel.Text = "Writing " + file.Name + " to " + this.saveFileDialog.FileName + ".";
                    FileReader fileReader = new FileReader();
                    Model3DData model3D = fileReader.ReadModel(file.EditPath);

                    try
                    {
                        ObjFileWriter fileWriter = new ObjFileWriter(this.saveFileDialog.FileName);
                        fileWriter.WriteModel(model3D.Model);
                        fileWriter.Close();
                        this.statusLabel.Text = "Writing was finished.";
                        file.Exported = true;
                        this.listModel3DPanel.Refresh();
                    } catch
                    {
                        MessageBox.Show(this, "An error occurred when writing to a file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else
            {
                MessageBox.Show(this, "Please select a model that should be saved.", "Model not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when an item is checked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        public void OnItemCheckChanged(object sender, ItemCheckEventArgs e)
        {
            Model3DFile file = this.listModel3DPanel.GetSelectedItem<Model3DFile>();
            if (e.CurrentValue == CheckState.Unchecked)
            {
                this.helixViewer.AddModel(file.EditPath);
            } else
            {
                this.helixViewer.Remove(file.EditPath);
            }
        }

        /// <summary>
        /// Imports new models into the application.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">Event arguments.</param>
        private void fileImportMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                foreach (string fileName in this.openFileDialog.FileNames)
                {
                    Model3DFile model = new Model3DFile(fileName);
                    Model3DFile foundModel = this.model3DBean.ModelListBean.FindLast(item => item.Name == model.Name);
                    if (foundModel != null)
                    {
                        model.Index = foundModel.Index + 1;
                    }

                    this.model3DBean.ModelListBean.Add(model);
                    this.listModel3DPanel.Add(model);
                }
            }
        }

        /// <summary>
        /// Saves a selected model to a file.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">Event arguments.</param>
        private void fileSaveMenuItem_Click(object sender, EventArgs e)
        {
            this.OnSaveClicked(sender, e);
        }

        /// <summary>
        /// Shows a view with imported models.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">Event arguments.</param>
        private void viewModelsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.viewSplitContainer.Panel1.Contains(this.listModel3DPanel))
            {
                this.mainSplitContainer.Panel1Collapsed = true;
                this.listModel3DPanel.IsShown = false;
                this.viewSplitContainer.Panel1.Controls.Clear();
            }
            else
            {
                this.viewSplitContainer.Panel1.Controls.Clear();
                this.mainSplitContainer.Panel1Collapsed = false;
                this.listModel3DPanel.IsShown = true;
                this.viewSplitContainer.Panel1.Controls.Add(this.listModel3DPanel);
            }
        }

        /// <summary>
        /// Starts a rigid registration.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">Event arguments.</param>
        private void toolsRegistrationRigidMenuItem_Click(object sender, EventArgs e)
        {
            RigidSettingsForm rigidSettingsForm = new RigidSettingsForm(this.model3DBean.ModelListBean);
            rigidSettingsForm.ShowDialog(this);

            if (rigidSettingsForm.DialogResult == DialogResult.OK)
            {
                this.statusProgressBar.Visible = true;
                this.statusLabel.Text = "Registration task is in progress.";

                for (int i = 0; i < rigidSettingsForm.SourceModels.Count; i++)
                {
                    for (int j = 0; j < rigidSettingsForm.TargetModels.Count; j++)
                    {
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.RunWorkerCompleted += this.ReportTaskCompleted;
                        worker.DoWork += (obj, args) =>
                        {
                            RigidArgs rigidArgs = (RigidArgs)args.Argument;

                            FileReader fileReader = new FileReader();
                            Model3DData sourceModel = fileReader.ReadModel(rigidArgs.SourceModel.EditPath);
                            Model3DData targetModel = fileReader.ReadModel(rigidArgs.TargetModel.EditPath);

                            List<Vector<float>> sourceVertexBuffer = sourceModel.GetVertices();
                            List<Vector<float>> targetVertexBuffer = targetModel.GetVertices();

                            RigidRegistration rigidRegistration = new RigidRegistration(rigidArgs.MappingAlgorithm, rigidArgs.NumberOfIterations);
                            sourceVertexBuffer = rigidRegistration.ComputeRegistration(sourceVertexBuffer, targetVertexBuffer);

                            sourceModel.SetVertices(sourceVertexBuffer);
                            ObjFileWriter fileWriter = new ObjFileWriter(rigidArgs.SourceModel.EditPath);
                            fileWriter.WriteModel(sourceModel.Model);
                            fileWriter.Close();

                            rigidArgs.SourceModel.Changed = true;
                            rigidArgs.SourceModel.Exported = false;
                        };

                        worker.RunWorkerAsync(new RigidArgs(rigidSettingsForm.SourceModels[i], rigidSettingsForm.TargetModels[j], rigidSettingsForm.PointMappingAlgorithm, rigidSettingsForm.MaxIterations));
                    }
                }
            }
        }

        /// <summary>
        /// Starts a nonrigid registration.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">Event arguments.</param>
        private void toolsRegistrationNonrigidMenuItem_Click(object sender, EventArgs e)
        {
            NonrigidSettingsForm nonrigidSettingsForm = new NonrigidSettingsForm(this.model3DBean.ModelListBean);
            nonrigidSettingsForm.ShowDialog(this);

            if (nonrigidSettingsForm.DialogResult == DialogResult.OK)
            {
                this.statusProgressBar.Visible = true;
                this.statusLabel.Text = "Registration task is in progress.";

                for (int i = 0; i < nonrigidSettingsForm.SourceModels.Count; i++)
                {
                    for (int j = 0; j < nonrigidSettingsForm.TargetModels.Count; j++)
                    {
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.RunWorkerCompleted += this.ReportTaskCompleted;
                        worker.DoWork += (obj, args) =>
                        {
                            NonrigidArgs nonrigidArgs = (NonrigidArgs)args.Argument;

                            FileReader fileReader = new FileReader();
                            Model3DData sourceModel = fileReader.ReadModel(nonrigidArgs.SourceModel.EditPath);
                            Model3DData targetModel = fileReader.ReadModel(nonrigidArgs.TargetModel.EditPath);

                            List<Vector<float>> sourceVertexBuffer = sourceModel.GetVertices();
                            List<Vector<float>> targetVertexBuffer = targetModel.GetVertices();

                            RigidRegistration rigidRegistration = new RigidRegistration(new KdTreeMapping(), nonrigidArgs.NumberOfIterations);
                            NonrigidRegistration nonrigidRegistration = new NonrigidRegistration(rigidRegistration);
                            sourceVertexBuffer = nonrigidRegistration.ComputeRegistration(sourceVertexBuffer, targetVertexBuffer);

                            sourceModel.SetVertices(sourceVertexBuffer);
                            ObjFileWriter fileWriter = new ObjFileWriter(nonrigidArgs.SourceModel.EditPath);
                            fileWriter.WriteModel(sourceModel.Model);
                            fileWriter.Close();

                            nonrigidArgs.SourceModel.Changed = true;
                            nonrigidArgs.SourceModel.Exported = false;
                        };

                        worker.RunWorkerAsync(new NonrigidArgs(nonrigidSettingsForm.SourceModels[i], nonrigidSettingsForm.TargetModels[j], nonrigidSettingsForm.MaxIterations));
                    }
                }
            }
        }

        /// <summary>
        /// Cleans and reports when a task is completed.
        /// </summary>
        /// <param name="sender">The sender of an event.</param>
        /// <param name="e">Event arguments.</param>
        private void ReportTaskCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.statusLabel.Text = string.Empty;
                this.statusProgressBar.Visible = false;
                MessageBox.Show(this, "An error occurred while the task was running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                List<Model3DFile> fileList = this.listModel3DPanel.GetCheckedItems<Model3DFile>();
                for (int i = 0; i < fileList.Count; i++)
                {
                    if (fileList[i].Changed)
                    {
                        this.helixViewer.Remove(fileList[i].EditPath);
                        this.helixViewer.AddModel(fileList[i].EditPath);
                        fileList[i].Changed = false;
                    }
                }

                this.listModel3DPanel.Refresh();
                this.statusProgressBar.Visible = false;
                this.statusLabel.Text = "Task is completed";
                MessageBox.Show(this, "Task is completed!", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
