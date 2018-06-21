namespace Registration_v2.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.stripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.viewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.helixViewer = new Registration_v2.UI.HelixViewer();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileImportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewModelsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsRegistrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsRegistrationRigidMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsRegistrationNonrigidMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.stripContainer.ContentPanel.SuspendLayout();
            this.stripContainer.TopToolStripPanel.SuspendLayout();
            this.stripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewSplitContainer)).BeginInit();
            this.viewSplitContainer.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripContainer
            // 
            resources.ApplyResources(this.stripContainer, "stripContainer");
            // 
            // stripContainer.BottomToolStripPanel
            // 
            resources.ApplyResources(this.stripContainer.BottomToolStripPanel, "stripContainer.BottomToolStripPanel");
            // 
            // stripContainer.ContentPanel
            // 
            resources.ApplyResources(this.stripContainer.ContentPanel, "stripContainer.ContentPanel");
            this.stripContainer.ContentPanel.Controls.Add(this.mainSplitContainer);
            // 
            // stripContainer.LeftToolStripPanel
            // 
            resources.ApplyResources(this.stripContainer.LeftToolStripPanel, "stripContainer.LeftToolStripPanel");
            this.stripContainer.LeftToolStripPanelVisible = false;
            this.stripContainer.Name = "stripContainer";
            // 
            // stripContainer.RightToolStripPanel
            // 
            resources.ApplyResources(this.stripContainer.RightToolStripPanel, "stripContainer.RightToolStripPanel");
            this.stripContainer.RightToolStripPanelVisible = false;
            // 
            // stripContainer.TopToolStripPanel
            // 
            resources.ApplyResources(this.stripContainer.TopToolStripPanel, "stripContainer.TopToolStripPanel");
            this.stripContainer.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            // 
            // mainSplitContainer
            // 
            resources.ApplyResources(this.mainSplitContainer, "mainSplitContainer");
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            resources.ApplyResources(this.mainSplitContainer.Panel1, "mainSplitContainer.Panel1");
            this.mainSplitContainer.Panel1.Controls.Add(this.viewSplitContainer);
            this.mainSplitContainer.Panel1Collapsed = true;
            // 
            // mainSplitContainer.Panel2
            // 
            resources.ApplyResources(this.mainSplitContainer.Panel2, "mainSplitContainer.Panel2");
            this.mainSplitContainer.Panel2.Controls.Add(this.elementHost);
            // 
            // viewSplitContainer
            // 
            resources.ApplyResources(this.viewSplitContainer, "viewSplitContainer");
            this.viewSplitContainer.Name = "viewSplitContainer";
            // 
            // viewSplitContainer.Panel1
            // 
            resources.ApplyResources(this.viewSplitContainer.Panel1, "viewSplitContainer.Panel1");
            // 
            // viewSplitContainer.Panel2
            // 
            resources.ApplyResources(this.viewSplitContainer.Panel2, "viewSplitContainer.Panel2");
            this.viewSplitContainer.Panel2Collapsed = true;
            // 
            // elementHost
            // 
            resources.ApplyResources(this.elementHost, "elementHost");
            this.elementHost.BackColor = System.Drawing.Color.Black;
            this.elementHost.Name = "elementHost";
            this.elementHost.Child = this.helixViewer;
            // 
            // mainMenuStrip
            // 
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.viewMenuItem,
            this.toolsMenuItem});
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // fileMenuItem
            // 
            resources.ApplyResources(this.fileMenuItem, "fileMenuItem");
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileImportMenuItem,
            this.fileSaveMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            // 
            // fileImportMenuItem
            // 
            resources.ApplyResources(this.fileImportMenuItem, "fileImportMenuItem");
            this.fileImportMenuItem.Name = "fileImportMenuItem";
            this.fileImportMenuItem.Click += new System.EventHandler(this.fileImportMenuItem_Click);
            // 
            // fileSaveMenuItem
            // 
            resources.ApplyResources(this.fileSaveMenuItem, "fileSaveMenuItem");
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.Click += new System.EventHandler(this.fileSaveMenuItem_Click);
            // 
            // viewMenuItem
            // 
            resources.ApplyResources(this.viewMenuItem, "viewMenuItem");
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewModelsMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            // 
            // viewModelsMenuItem
            // 
            resources.ApplyResources(this.viewModelsMenuItem, "viewModelsMenuItem");
            this.viewModelsMenuItem.Name = "viewModelsMenuItem";
            this.viewModelsMenuItem.Click += new System.EventHandler(this.viewModelsMenuItem_Click);
            // 
            // toolsMenuItem
            // 
            resources.ApplyResources(this.toolsMenuItem, "toolsMenuItem");
            this.toolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsRegistrationMenuItem});
            this.toolsMenuItem.Name = "toolsMenuItem";
            // 
            // toolsRegistrationMenuItem
            // 
            resources.ApplyResources(this.toolsRegistrationMenuItem, "toolsRegistrationMenuItem");
            this.toolsRegistrationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsRegistrationRigidMenuItem,
            this.toolsRegistrationNonrigidMenuItem});
            this.toolsRegistrationMenuItem.Name = "toolsRegistrationMenuItem";
            // 
            // toolsRegistrationRigidMenuItem
            // 
            resources.ApplyResources(this.toolsRegistrationRigidMenuItem, "toolsRegistrationRigidMenuItem");
            this.toolsRegistrationRigidMenuItem.Name = "toolsRegistrationRigidMenuItem";
            this.toolsRegistrationRigidMenuItem.Click += new System.EventHandler(this.toolsRegistrationRigidMenuItem_Click);
            // 
            // toolsRegistrationNonrigidMenuItem
            // 
            resources.ApplyResources(this.toolsRegistrationNonrigidMenuItem, "toolsRegistrationNonrigidMenuItem");
            this.toolsRegistrationNonrigidMenuItem.Name = "toolsRegistrationNonrigidMenuItem";
            this.toolsRegistrationNonrigidMenuItem.Click += new System.EventHandler(this.toolsRegistrationNonrigidMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "obj";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            this.openFileDialog.Multiselect = true;
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgressBar});
            this.statusStrip.Name = "statusStrip";
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Spring = true;
            // 
            // statusProgressBar
            // 
            resources.ApplyResources(this.statusProgressBar, "statusProgressBar");
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Step = 1;
            this.statusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "obj";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.stripContainer);
            this.Name = "MainForm";
            this.stripContainer.ContentPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.ResumeLayout(false);
            this.stripContainer.TopToolStripPanel.PerformLayout();
            this.stripContainer.ResumeLayout(false);
            this.stripContainer.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewSplitContainer)).EndInit();
            this.viewSplitContainer.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer stripContainer;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileImportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsRegistrationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsRegistrationRigidMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsRegistrationNonrigidMenuItem;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private HelixViewer helixViewer;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem viewModelsMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.SplitContainer viewSplitContainer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

