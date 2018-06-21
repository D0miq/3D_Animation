namespace Registration_v2.UI
{
    partial class RigidSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RigidSettingsForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mappingLabel = new System.Windows.Forms.Label();
            this.algorihmComboBox = new System.Windows.Forms.ComboBox();
            this.iterationsLabel = new System.Windows.Forms.Label();
            this.iterationsTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.sourceGroupBox = new System.Windows.Forms.GroupBox();
            this.sourceListBox = new System.Windows.Forms.ListBox();
            this.referGroupBox = new System.Windows.Forms.GroupBox();
            this.targetListBox = new System.Windows.Forms.ListBox();
            this.registrationDirectionLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.sourceGroupBox.SuspendLayout();
            this.referGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.mappingLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.algorihmComboBox, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.iterationsLabel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.iterationsTextBox, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.cancelButton, 4, 3);
            this.tableLayoutPanel.Controls.Add(this.sourceGroupBox, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.referGroupBox, 4, 2);
            this.tableLayoutPanel.Controls.Add(this.registrationDirectionLabel, 3, 2);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // mappingLabel
            // 
            resources.ApplyResources(this.mappingLabel, "mappingLabel");
            this.tableLayoutPanel.SetColumnSpan(this.mappingLabel, 3);
            this.mappingLabel.Name = "mappingLabel";
            // 
            // algorihmComboBox
            // 
            resources.ApplyResources(this.algorihmComboBox, "algorihmComboBox");
            this.tableLayoutPanel.SetColumnSpan(this.algorihmComboBox, 4);
            this.algorihmComboBox.FormattingEnabled = true;
            this.algorihmComboBox.Items.AddRange(new object[] {
            resources.GetString("algorihmComboBox.Items"),
            resources.GetString("algorihmComboBox.Items1")});
            this.algorihmComboBox.Name = "algorihmComboBox";
            // 
            // iterationsLabel
            // 
            resources.ApplyResources(this.iterationsLabel, "iterationsLabel");
            this.tableLayoutPanel.SetColumnSpan(this.iterationsLabel, 3);
            this.iterationsLabel.Name = "iterationsLabel";
            // 
            // iterationsTextBox
            // 
            resources.ApplyResources(this.iterationsTextBox, "iterationsTextBox");
            this.tableLayoutPanel.SetColumnSpan(this.iterationsTextBox, 4);
            this.iterationsTextBox.Name = "iterationsTextBox";
            // 
            // okButton
            // 
            this.tableLayoutPanel.SetColumnSpan(this.okButton, 2);
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.tableLayoutPanel.SetColumnSpan(this.cancelButton, 2);
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // sourceGroupBox
            // 
            this.tableLayoutPanel.SetColumnSpan(this.sourceGroupBox, 3);
            this.sourceGroupBox.Controls.Add(this.sourceListBox);
            resources.ApplyResources(this.sourceGroupBox, "sourceGroupBox");
            this.sourceGroupBox.Name = "sourceGroupBox";
            this.sourceGroupBox.TabStop = false;
            // 
            // sourceListBox
            // 
            resources.ApplyResources(this.sourceListBox, "sourceListBox");
            this.sourceListBox.FormattingEnabled = true;
            this.sourceListBox.Name = "sourceListBox";
            // 
            // referGroupBox
            // 
            this.tableLayoutPanel.SetColumnSpan(this.referGroupBox, 3);
            this.referGroupBox.Controls.Add(this.targetListBox);
            resources.ApplyResources(this.referGroupBox, "referGroupBox");
            this.referGroupBox.Name = "referGroupBox";
            this.referGroupBox.TabStop = false;
            // 
            // targetListBox
            // 
            resources.ApplyResources(this.targetListBox, "targetListBox");
            this.targetListBox.FormattingEnabled = true;
            this.targetListBox.Name = "targetListBox";
            // 
            // registrationDirectionLabel
            // 
            resources.ApplyResources(this.registrationDirectionLabel, "registrationDirectionLabel");
            this.registrationDirectionLabel.Name = "registrationDirectionLabel";
            // 
            // RigidSettingsForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.tableLayoutPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RigidSettingsForm";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RigidSettingsForm_FormClosing);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.sourceGroupBox.ResumeLayout(false);
            this.referGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox sourceGroupBox;
        private System.Windows.Forms.GroupBox referGroupBox;
        private System.Windows.Forms.ListBox sourceListBox;
        private System.Windows.Forms.Label registrationDirectionLabel;
        private System.Windows.Forms.Label iterationsLabel;
        private System.Windows.Forms.TextBox iterationsTextBox;
        private System.Windows.Forms.Label mappingLabel;
        private System.Windows.Forms.ComboBox algorihmComboBox;
        private System.Windows.Forms.ListBox targetListBox;
    }
}