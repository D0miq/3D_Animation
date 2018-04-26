namespace Registration
{
    partial class RigidUserControl
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stopConditionText = new System.Windows.Forms.TextBox();
            this.distanceRadioButton = new System.Windows.Forms.RadioButton();
            this.iterationsRadioButton = new System.Windows.Forms.RadioButton();
            this.resetButton1 = new System.Windows.Forms.Button();
            this.nextRigid = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.kabschRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maxMappingText = new System.Windows.Forms.TextBox();
            this.kdTreeRadioButton = new System.Windows.Forms.RadioButton();
            this.bruteForceRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.stopConditionText);
            this.groupBox4.Controls.Add(this.distanceRadioButton);
            this.groupBox4.Controls.Add(this.iterationsRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(259, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 116);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Stop condition";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Value:";
            // 
            // stopConditionText
            // 
            this.stopConditionText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopConditionText.Location = new System.Drawing.Point(49, 90);
            this.stopConditionText.Name = "stopConditionText";
            this.stopConditionText.Size = new System.Drawing.Size(195, 20);
            this.stopConditionText.TabIndex = 2;
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
            this.distanceRadioButton.UseVisualStyleBackColor = true;
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
            this.iterationsRadioButton.UseVisualStyleBackColor = true;
            // 
            // resetButton1
            // 
            this.resetButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton1.Location = new System.Drawing.Point(477, 346);
            this.resetButton1.Name = "resetButton1";
            this.resetButton1.Size = new System.Drawing.Size(120, 23);
            this.resetButton1.TabIndex = 19;
            this.resetButton1.Text = "Reset";
            this.resetButton1.UseVisualStyleBackColor = true;
            // 
            // nextRigid
            // 
            this.nextRigid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextRigid.Location = new System.Drawing.Point(603, 346);
            this.nextRigid.Name = "nextRigid";
            this.nextRigid.Size = new System.Drawing.Size(120, 23);
            this.nextRigid.TabIndex = 18;
            this.nextRigid.Text = "Next";
            this.nextRigid.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.kabschRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(3, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 114);
            this.groupBox3.TabIndex = 17;
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
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.maxMappingText);
            this.groupBox2.Controls.Add(this.kdTreeRadioButton);
            this.groupBox2.Controls.Add(this.bruteForceRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 116);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mapping";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max distance:";
            // 
            // maxMappingText
            // 
            this.maxMappingText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxMappingText.Location = new System.Drawing.Point(86, 90);
            this.maxMappingText.Name = "maxMappingText";
            this.maxMappingText.Size = new System.Drawing.Size(158, 20);
            this.maxMappingText.TabIndex = 2;
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
            // RigidUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.resetButton1);
            this.Controls.Add(this.nextRigid);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "RigidUserControl";
            this.Size = new System.Drawing.Size(726, 372);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox stopConditionText;
        private System.Windows.Forms.RadioButton distanceRadioButton;
        private System.Windows.Forms.RadioButton iterationsRadioButton;
        private System.Windows.Forms.Button resetButton1;
        private System.Windows.Forms.Button nextRigid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton kabschRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxMappingText;
        private System.Windows.Forms.RadioButton kdTreeRadioButton;
        private System.Windows.Forms.RadioButton bruteForceRadioButton;
    }
}
