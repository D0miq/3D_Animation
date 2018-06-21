namespace _3DGraphicsCompression
{
    partial class CompressionForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listFiles = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            statusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.convertBT = new System.Windows.Forms.Button();
            this.addFilesBT = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listFiles, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listFiles
            // 
            this.listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFiles.FormattingEnabled = true;
            this.listFiles.Location = new System.Drawing.Point(3, 3);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(664, 419);
            this.listFiles.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusStrip);
            this.panel1.Controls.Add(this.convertBT);
            this.panel1.Controls.Add(this.addFilesBT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 428);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 69);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            statusBar,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 45);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(664, 24);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusBar
            // 
            statusBar.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            statusBar.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            statusBar.Name = "statusBar";
            statusBar.Size = new System.Drawing.Size(516, 19);
            statusBar.Spring = true;
            statusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // convertBT
            // 
            this.convertBT.Location = new System.Drawing.Point(90, 3);
            this.convertBT.Name = "convertBT";
            this.convertBT.Size = new System.Drawing.Size(75, 23);
            this.convertBT.TabIndex = 1;
            this.convertBT.Text = "Compress";
            this.convertBT.UseVisualStyleBackColor = true;
            this.convertBT.Click += new System.EventHandler(this.ConvertBT_Click);
            // 
            // addFilesBT
            // 
            this.addFilesBT.Location = new System.Drawing.Point(9, 3);
            this.addFilesBT.Name = "addFilesBT";
            this.addFilesBT.Size = new System.Drawing.Size(75, 23);
            this.addFilesBT.TabIndex = 0;
            this.addFilesBT.Text = "Add files";
            this.addFilesBT.UseVisualStyleBackColor = true;
            this.addFilesBT.Click += new System.EventHandler(this.AddFilesBT_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            // 
            // CompressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CompressionForm";
            this.Text = "Compress 3D graphics files";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox listFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button convertBT;
        private System.Windows.Forms.Button addFilesBT;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        public static System.Windows.Forms.ToolStripStatusLabel statusBar;
    }
}

