namespace Registration_v2.UI
{
    partial class NonrigidSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NonrigidSettingsForm));
            this.registrationDirectionLabel = new System.Windows.Forms.Label();
            this.referGroupBox = new System.Windows.Forms.GroupBox();
            this.targetListBox = new System.Windows.Forms.ListBox();
            this.sourceGroupBox = new System.Windows.Forms.GroupBox();
            this.sourceListBox = new System.Windows.Forms.ListBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.iterationsTextBox = new System.Windows.Forms.TextBox();
            this.iterationsLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.referGroupBox.SuspendLayout();
            this.sourceGroupBox.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // registrationDirectionLabel
            // 
            resources.ApplyResources(this.registrationDirectionLabel, "registrationDirectionLabel");
            this.registrationDirectionLabel.Name = "registrationDirectionLabel";
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
            // cancelButton
            // 
            this.tableLayoutPanel.SetColumnSpan(this.cancelButton, 2);
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.tableLayoutPanel.SetColumnSpan(this.okButton, 2);
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // iterationsTextBox
            // 
            resources.ApplyResources(this.iterationsTextBox, "iterationsTextBox");
            this.tableLayoutPanel.SetColumnSpan(this.iterationsTextBox, 4);
            this.iterationsTextBox.Name = "iterationsTextBox";
            // 
            // iterationsLabel
            // 
            resources.ApplyResources(this.iterationsLabel, "iterationsLabel");
            this.tableLayoutPanel.SetColumnSpan(this.iterationsLabel, 3);
            this.iterationsLabel.Name = "iterationsLabel";
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.iterationsLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.iterationsTextBox, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.cancelButton, 4, 2);
            this.tableLayoutPanel.Controls.Add(this.sourceGroupBox, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.referGroupBox, 4, 1);
            this.tableLayoutPanel.Controls.Add(this.registrationDirectionLabel, 3, 1);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // NonrigidSettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "NonrigidSettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NonrigidSettingsForm_FormClosing);
            this.referGroupBox.ResumeLayout(false);
            this.sourceGroupBox.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label registrationDirectionLabel;
        private System.Windows.Forms.GroupBox referGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label iterationsLabel;
        private System.Windows.Forms.TextBox iterationsTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox sourceGroupBox;
        private System.Windows.Forms.ListBox sourceListBox;
        private System.Windows.Forms.ListBox targetListBox;
    }
}