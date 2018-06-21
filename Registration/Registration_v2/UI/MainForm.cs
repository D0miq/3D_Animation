namespace Registration_v2.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using MathNet.Numerics.LinearAlgebra;
    using Registration_v2.Data;
    using Registration_v2.IO;
    using Registration_v2.Tools.Registration.Nonrigid;
    using Registration_v2.Tools.Registration.Rigid;

    public partial class MainForm : System.Windows.Forms.Form, CheckListPanel.ICheckListPanelListener
    {
        private CheckListPanel listModel3DPanel;

        private Model3DBean model3DBean;

        public MainForm(Model3DBean model3DBean)
        {
            this.InitializeComponent();
            this.model3DBean = model3DBean;
            this.listModel3DPanel = new CheckListPanel(this, this.viewModelsMenuItem.Text);
        }

        public void OnSaveClicked(object sender, EventArgs e)
        {
            Model3DFile file = this.listModel3DPanel.GetSelectedItem<Model3DFile>();
        }

        public void OnDeleteClicked(object sender, EventArgs e)
        {
            Model3DFile file = this.listModel3DPanel.GetSelectedItem<Model3DFile>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void fileSaveMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void toolsRegistrationRigidMenuItem_Click(object sender, EventArgs e)
        {
            RigidSettingsForm rigidSettingsForm = new RigidSettingsForm(this.model3DBean.ModelListBean);
            rigidSettingsForm.ShowDialog(this);

            if (rigidSettingsForm.DialogResult == DialogResult.OK)
            {
                for (int i = 0; i < rigidSettingsForm.SourceModels.Count; i++)
                {
                    for (int j = 0; j < rigidSettingsForm.TargetModels.Count; j++)
                    {
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.WorkerReportsProgress = true;
                        worker.ProgressChanged += this.ReportProgress;
                        worker.RunWorkerCompleted += this.ReportTaskCompleted;
                        worker.DoWork += (obj, args) =>
                        {
                            ((BackgroundWorker)obj).ReportProgress(1);
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

        private void toolsRegistrationNonrigidMenuItem_Click(object sender, EventArgs e)
        {
            NonrigidSettingsForm nonrigidSettingsForm = new NonrigidSettingsForm(this.model3DBean.ModelListBean);
            nonrigidSettingsForm.ShowDialog(this);

            if (nonrigidSettingsForm.DialogResult == DialogResult.OK)
            {
                for (int i = 0; i < nonrigidSettingsForm.SourceModels.Count; i++)
                {
                    for (int j = 0; j < nonrigidSettingsForm.TargetModels.Count; j++)
                    {
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.WorkerReportsProgress = true;
                        worker.ProgressChanged += this.ReportProgress;
                        worker.RunWorkerCompleted += this.ReportTaskCompleted;
                        worker.DoWork += (obj, args) =>
                        {
                            ((BackgroundWorker)obj).ReportProgress(1);
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

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            this.statusProgressBar.Increment(e.ProgressPercentage);
            this.statusLabel.Text = "Finished " + this.statusProgressBar.Value;
        }

        private void ReportTaskCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            this.statusLabel.Text = "Task is completed";
            MessageBox.Show(this, "Task is completed!", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
