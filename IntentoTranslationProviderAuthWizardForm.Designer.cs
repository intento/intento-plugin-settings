namespace IntentoMT.Plugin.PropertiesForm
{
    partial class IntentoTranslationProviderAuthWizardForm
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
            this.buttonContinue = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxShowHidden = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonContinue
            // 
            this.buttonContinue.Enabled = false;
            this.buttonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinue.Location = new System.Drawing.Point(289, 45);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 8;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Custom credentials (required)";
            this.label1.UseWaitCursor = true;
            // 
            // checkBoxShowHidden
            // 
            this.checkBoxShowHidden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowHidden.AutoSize = true;
            this.checkBoxShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowHidden.Location = new System.Drawing.Point(12, 65);
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.checkBoxShowHidden.Size = new System.Drawing.Size(105, 17);
            this.checkBoxShowHidden.TabIndex = 17;
            this.checkBoxShowHidden.Text = "Show hidden text";
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
            // 
            // IntentoTranslationProviderAuthWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 86);
            this.Controls.Add(this.checkBoxShowHidden);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonContinue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoTranslationProviderAuthWizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Own credentials wizard";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxShowHidden;
    }
}