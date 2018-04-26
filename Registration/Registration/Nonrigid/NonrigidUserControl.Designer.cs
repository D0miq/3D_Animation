namespace Registration
{
    partial class NonrigidUserControl
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
            this.resetButton2 = new System.Windows.Forms.Button();
            this.nextNonrigid = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resetButton2
            // 
            this.resetButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton2.Location = new System.Drawing.Point(478, 347);
            this.resetButton2.Name = "resetButton2";
            this.resetButton2.Size = new System.Drawing.Size(120, 23);
            this.resetButton2.TabIndex = 13;
            this.resetButton2.Text = "Reset";
            this.resetButton2.UseVisualStyleBackColor = true;
            // 
            // nextNonrigid
            // 
            this.nextNonrigid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextNonrigid.Location = new System.Drawing.Point(604, 347);
            this.nextNonrigid.Name = "nextNonrigid";
            this.nextNonrigid.Size = new System.Drawing.Size(120, 23);
            this.nextNonrigid.TabIndex = 12;
            this.nextNonrigid.Text = "Next";
            this.nextNonrigid.UseVisualStyleBackColor = true;
            // 
            // NonrigidUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resetButton2);
            this.Controls.Add(this.nextNonrigid);
            this.Name = "NonrigidUserControl";
            this.Size = new System.Drawing.Size(727, 373);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button resetButton2;
        private System.Windows.Forms.Button nextNonrigid;
    }
}
