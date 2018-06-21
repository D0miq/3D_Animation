namespace Registration_v2.UI
{
    partial class CheckListPanel
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

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckListPanel));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.checkListBox = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Controls.Add(this.checkListBox);
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            // 
            // checkListBox
            // 
            resources.ApplyResources(this.checkListBox, "checkListBox");
            this.checkListBox.ContextMenuStrip = this.contextMenuStrip;
            this.checkListBox.FormattingEnabled = true;
            this.checkListBox.Name = "checkListBox";
            this.checkListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListBox_ItemCheck);
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // CheckListPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.DoubleBuffered = true;
            this.Name = "CheckListPanel";
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.CheckedListBox checkListBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}
