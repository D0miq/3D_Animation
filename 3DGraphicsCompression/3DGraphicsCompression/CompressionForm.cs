namespace _3DGraphicsCompression
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;
    using _3DGraphicsCompression.ObjParser;
    using log4net;
    using Microsoft.VisualBasic;

    /// <summary>
    /// An instance of the <see cref="FileFormatterForm"/> class creates a windows of the application and processes events from controls.
    /// </summary>
    public partial class CompressionForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionForm"/> class.
        /// </summary>
        public CompressionForm()
        {
            this.InitializeComponent();
            this.backgroundWorker.DoWork += this.BackgroundWorker_DoWork;
            this.backgroundWorker.ProgressChanged += this.BackgroundWorker_ProgressChanged;
            this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_Completed;
        }

        /// <summary>
        /// Enables and disables controls during execution.
        /// </summary>
        /// <param name="state">True if controls should be enabled, else otherwise.</param>
        private void Enabled(bool state)
        {
            this.convertBT.Enabled = state;
            this.listFiles.Enabled = state;
            this.addFilesBT.Enabled = state;
        }

        /// <summary>
        /// Starts a worker on the background. It compresses given files and creates a new compressed .3gbf file from them.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Log.Info("Background worker starts.");
            Compression compression = new Compression();

            for (int i = 0; i < this.listFiles.CheckedItems.Count; i++)
            {
                string path = (string)this.listFiles.CheckedItems[i];
                Log.Debug("Path of a file: " + path);

                try
                {
                    IFileReader fileReader = new ObjFileReader(path);
                    compression.AddFile(fileReader);
                }
                catch (IOException ex)
                {
                    Log.Error("Unable to read " + path + ". " + ex.Message);
                    CompressionForm.statusBar.Text = "Unable to read " + path + ".";
                }
                finally
                {
                    this.backgroundWorker.ReportProgress(i + 1);
                }
            }

            Log.Debug("Background worker argument: " + (int)e.Argument);
            compression.CompressVertices((int)e.Argument);

            try
            {
                FileWriter writer = new FileWriter(string.Format(@"3Danimation{0}.3gbf", DateTime.Now.Ticks));
                writer.WriteTrajectory(compression.AverageTrajectory);
                writer.WriteEigenVectors(compression.SubEigenVectors);
                writer.WriteControlTrajectories(compression.ControlTrajectories);
                writer.WriteFaces(compression.CompressedMesh.Faces);
                writer.WriteTextures(compression.CompressedMesh.TextureCoords);
                writer.WriteTextures(compression.CompressedMesh.Normals);
                writer.Close();
            } catch (Exception ex)
            {
                Log.Error("Cannot write to a file. " + ex.Message);
                statusBar.Text = "Error during writing to the file. Compression failed.";
            }
        }

        /// <summary>
        /// Reports progress from worker thread.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = (int)((double)e.ProgressPercentage / (double)this.listFiles.CheckedItems.Count * 100);
            Log.Debug("Progress percentage: " + percentage);
            this.progressBar.Value = percentage;
        }

        /// <summary>
        /// Cleans resources when background worker completes its task.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void BackgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Info("Background worker finishes.");
            this.progressBar.Value = 0;
            this.Enabled(true);
        }

        /// <summary>
        /// Opens a new file dialog and adds new files to the <see cref="listFiles"/>.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void AddFilesBT_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on add files button.");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Obj Files|*.obj";
            openFileDialog.Title = "Select a 3D files";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    this.listFiles.Items.Add(fileName, true);
                }
            }
        }

        /// <summary>
        /// Starts a background worker.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Arguments of the event.</param>
        /// <seealso cref="BackgroundWorker_DoWork(object, DoWorkEventArgs)"/>
        /// <seealso cref="BackgroundWorker_ProgressChanged(object, ProgressChangedEventArgs)"/>
        /// <seealso cref="BackgroundWorker_Completed(object, RunWorkerCompletedEventArgs)"/>
        private void ConvertBT_Click(object sender, EventArgs e)
        {
            Log.Info("Clicked on convert button.");
            this.Enabled(false);
            int controlTrajectoriesCount;

            repeatInput:
            try
            {
                string input = Interaction.InputBox("Count of control trajectories:", string.Empty, "10");
                Log.Debug("Control trajectories count: " + input);
                if (input == string.Empty)
                {
                    return;
                }

                controlTrajectoriesCount = int.Parse(input);
                if (controlTrajectoriesCount < 1)
                {
                    throw new FormatException();
                }
            } catch (FormatException ex)
            {
                Log.Error("Input format exception. " + ex.Message);
                MessageBox.Show("The given value has to be positive integer.", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto repeatInput;
            }

            this.backgroundWorker.RunWorkerAsync(controlTrajectoriesCount);
        }
    }
}
