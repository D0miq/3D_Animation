namespace Registration
{
    partial class RegistrationForm
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
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nextRegistration = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nonrigidRadioButton = new System.Windows.Forms.RadioButton();
            this.rigidRadioButton = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.resetButton1 = new System.Windows.Forms.Button();
            this.nextRigid = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.kabschRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kdTreeRadioButton = new System.Windows.Forms.RadioButton();
            this.bruteForceRadioButton = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.resetButton2 = new System.Windows.Forms.Button();
            this.nextNonrigid = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.resetButton3 = new System.Windows.Forms.Button();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.sourceButton = new System.Windows.Forms.Button();
            this.referenceButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.registrationWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.iterationsRadioButton = new System.Windows.Forms.RadioButton();
            this.distanceRadioButton = new System.Windows.Forms.RadioButton();
            this.stopConditionText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(737, 274);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nextRegistration);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(729, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Registration";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nextRegistration
            // 
            this.nextRegistration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextRegistration.Location = new System.Drawing.Point(603, 219);
            this.nextRegistration.Name = "nextRegistration";
            this.nextRegistration.Size = new System.Drawing.Size(120, 23);
            this.nextRegistration.TabIndex = 9;
            this.nextRegistration.Text = "Next";
            this.nextRegistration.UseVisualStyleBackColor = true;
            this.nextRegistration.Click += new System.EventHandler(this.NextRegistration_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nonrigidRadioButton);
            this.groupBox1.Controls.Add(this.rigidRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 76);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration";
            // 
            // nonrigidRadioButton
            // 
            this.nonrigidRadioButton.AutoSize = true;
            this.nonrigidRadioButton.Location = new System.Drawing.Point(7, 43);
            this.nonrigidRadioButton.Name = "nonrigidRadioButton";
            this.nonrigidRadioButton.Size = new System.Drawing.Size(64, 17);
            this.nonrigidRadioButton.TabIndex = 1;
            this.nonrigidRadioButton.TabStop = true;
            this.nonrigidRadioButton.Text = "Nonrigid";
            this.nonrigidRadioButton.UseVisualStyleBackColor = true;
            // 
            // rigidRadioButton
            // 
            this.rigidRadioButton.AutoSize = true;
            this.rigidRadioButton.Location = new System.Drawing.Point(6, 19);
            this.rigidRadioButton.Name = "rigidRadioButton";
            this.rigidRadioButton.Size = new System.Drawing.Size(49, 17);
            this.rigidRadioButton.TabIndex = 0;
            this.rigidRadioButton.TabStop = true;
            this.rigidRadioButton.Text = "Rigid";
            this.rigidRadioButton.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.resetButton1);
            this.tabPage2.Controls.Add(this.nextRigid);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(729, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rigid settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // resetButton1
            // 
            this.resetButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton1.Location = new System.Drawing.Point(477, 219);
            this.resetButton1.Name = "resetButton1";
            this.resetButton1.Size = new System.Drawing.Size(120, 23);
            this.resetButton1.TabIndex = 14;
            this.resetButton1.Text = "Reset";
            this.resetButton1.UseVisualStyleBackColor = true;
            this.resetButton1.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // nextRigid
            // 
            this.nextRigid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextRigid.Location = new System.Drawing.Point(603, 219);
            this.nextRigid.Name = "nextRigid";
            this.nextRigid.Size = new System.Drawing.Size(120, 23);
            this.nextRigid.TabIndex = 13;
            this.nextRigid.Text = "Next";
            this.nextRigid.UseVisualStyleBackColor = true;
            this.nextRigid.Click += new System.EventHandler(this.NextRigid_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.kabschRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(6, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 66);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rotation";
            // 
            // kabschRadioButton
            // 
            this.kabschRadioButton.AutoSize = true;
            this.kabschRadioButton.Location = new System.Drawing.Point(7, 19);
            this.kabschRadioButton.Name = "kabschRadioButton";
            this.kabschRadioButton.Size = new System.Drawing.Size(61, 17);
            this.kabschRadioButton.TabIndex = 0;
            this.kabschRadioButton.TabStop = true;
            this.kabschRadioButton.Text = "Kabsch";
            this.kabschRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.kdTreeRadioButton);
            this.groupBox2.Controls.Add(this.bruteForceRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 71);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mapping";
            // 
            // kdTreeRadioButton
            // 
            this.kdTreeRadioButton.AutoSize = true;
            this.kdTreeRadioButton.Location = new System.Drawing.Point(7, 43);
            this.kdTreeRadioButton.Name = "kdTreeRadioButton";
            this.kdTreeRadioButton.Size = new System.Drawing.Size(61, 17);
            this.kdTreeRadioButton.TabIndex = 1;
            this.kdTreeRadioButton.TabStop = true;
            this.kdTreeRadioButton.Text = "KD-tree";
            this.kdTreeRadioButton.UseVisualStyleBackColor = true;
            // 
            // bruteForceRadioButton
            // 
            this.bruteForceRadioButton.AutoSize = true;
            this.bruteForceRadioButton.Location = new System.Drawing.Point(7, 19);
            this.bruteForceRadioButton.Name = "bruteForceRadioButton";
            this.bruteForceRadioButton.Size = new System.Drawing.Size(77, 17);
            this.bruteForceRadioButton.TabIndex = 0;
            this.bruteForceRadioButton.TabStop = true;
            this.bruteForceRadioButton.Text = "Brute force";
            this.bruteForceRadioButton.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.resetButton2);
            this.tabPage3.Controls.Add(this.nextNonrigid);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(729, 248);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Nonrigid settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // resetButton2
            // 
            this.resetButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton2.Location = new System.Drawing.Point(477, 219);
            this.resetButton2.Name = "resetButton2";
            this.resetButton2.Size = new System.Drawing.Size(120, 23);
            this.resetButton2.TabIndex = 11;
            this.resetButton2.Text = "Reset";
            this.resetButton2.UseVisualStyleBackColor = true;
            this.resetButton2.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // nextNonrigid
            // 
            this.nextNonrigid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextNonrigid.Location = new System.Drawing.Point(603, 219);
            this.nextNonrigid.Name = "nextNonrigid";
            this.nextNonrigid.Size = new System.Drawing.Size(120, 23);
            this.nextNonrigid.TabIndex = 10;
            this.nextNonrigid.Text = "Next";
            this.nextNonrigid.UseVisualStyleBackColor = true;
            this.nextNonrigid.Click += new System.EventHandler(this.NextNonrigid_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.resetButton3);
            this.tabPage4.Controls.Add(this.checkedListBox);
            this.tabPage4.Controls.Add(this.sourceButton);
            this.tabPage4.Controls.Add(this.referenceButton);
            this.tabPage4.Controls.Add(this.registerButton);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(729, 248);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Finish";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // resetButton3
            // 
            this.resetButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.resetButton3.Location = new System.Drawing.Point(493, 219);
            this.resetButton3.Name = "resetButton3";
            this.resetButton3.Size = new System.Drawing.Size(120, 23);
            this.resetButton3.TabIndex = 20;
            this.resetButton3.Text = "Reset";
            this.resetButton3.UseVisualStyleBackColor = true;
            this.resetButton3.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // checkedListBox
            // 
            this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(6, 6);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(717, 199);
            this.checkedListBox.TabIndex = 19;
            // 
            // sourceButton
            // 
            this.sourceButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sourceButton.Location = new System.Drawing.Point(241, 219);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(120, 23);
            this.sourceButton.TabIndex = 18;
            this.sourceButton.Text = "Add source objects";
            this.sourceButton.UseVisualStyleBackColor = true;
            this.sourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // referenceButton
            // 
            this.referenceButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.referenceButton.Location = new System.Drawing.Point(115, 219);
            this.referenceButton.Name = "referenceButton";
            this.referenceButton.Size = new System.Drawing.Size(120, 23);
            this.referenceButton.TabIndex = 17;
            this.referenceButton.Text = "Set referential object";
            this.referenceButton.UseVisualStyleBackColor = true;
            this.referenceButton.Click += new System.EventHandler(this.ReferenceButton_Click);
            // 
            // registerButton
            // 
            this.registerButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.registerButton.Location = new System.Drawing.Point(367, 219);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(120, 23);
            this.registerButton.TabIndex = 16;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // registrationWorker
            // 
            this.registrationWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.RegistrationWorker_DoWork);
            this.registrationWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.RegistrationWorker_ProgressChanged);
            this.registrationWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.RegistrationWorker_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 289);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(761, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.statusLabel.Size = new System.Drawing.Size(644, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.stopConditionText);
            this.groupBox4.Controls.Add(this.distanceRadioButton);
            this.groupBox4.Controls.Add(this.iterationsRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(212, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 143);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Stop condition";
            // 
            // iterationsRadioButton
            // 
            this.iterationsRadioButton.AutoSize = true;
            this.iterationsRadioButton.Location = new System.Drawing.Point(7, 20);
            this.iterationsRadioButton.Name = "iterationsRadioButton";
            this.iterationsRadioButton.Size = new System.Drawing.Size(119, 17);
            this.iterationsRadioButton.TabIndex = 0;
            this.iterationsRadioButton.TabStop = true;
            this.iterationsRadioButton.Text = "Number of iterations";
            this.toolTip.SetToolTip(this.iterationsRadioButton, "Number of iterations has to be greater than 1.");
            this.iterationsRadioButton.UseVisualStyleBackColor = true;
            // 
            // distanceRadioButton
            // 
            this.distanceRadioButton.AutoSize = true;
            this.distanceRadioButton.Location = new System.Drawing.Point(7, 43);
            this.distanceRadioButton.Name = "distanceRadioButton";
            this.distanceRadioButton.Size = new System.Drawing.Size(88, 17);
            this.distanceRadioButton.TabIndex = 1;
            this.distanceRadioButton.TabStop = true;
            this.distanceRadioButton.Text = "Max distance";
            this.toolTip.SetToolTip(this.distanceRadioButton, "Max distance has to be greater than 0.");
            this.distanceRadioButton.UseVisualStyleBackColor = true;
            // 
            // stopConditionText
            // 
            this.stopConditionText.Location = new System.Drawing.Point(49, 117);
            this.stopConditionText.Name = "stopConditionText";
            this.stopConditionText.Size = new System.Drawing.Size(145, 20);
            this.stopConditionText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Value:";
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 311);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Name = "RegistrationForm";
            this.Text = "Mesh registration";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button nextRegistration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton nonrigidRadioButton;
        private System.Windows.Forms.RadioButton rigidRadioButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button nextRigid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton kabschRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton kdTreeRadioButton;
        private System.Windows.Forms.RadioButton bruteForceRadioButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button nextNonrigid;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button sourceButton;
        private System.Windows.Forms.Button referenceButton;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.Button resetButton3;
        private System.Windows.Forms.Button resetButton1;
        private System.Windows.Forms.Button resetButton2;
        private System.ComponentModel.BackgroundWorker registrationWorker;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox stopConditionText;
        private System.Windows.Forms.RadioButton distanceRadioButton;
        private System.Windows.Forms.RadioButton iterationsRadioButton;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

