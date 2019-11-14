namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    partial class IntentoFormIgnoreError
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
            this.buttonContinueEdit = new System.Windows.Forms.Button();
            this.buttonIgnoreAndSave = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonContinueEdit
            // 
            this.buttonContinueEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonContinueEdit.Location = new System.Drawing.Point(237, 66);
            this.buttonContinueEdit.Name = "buttonContinueEdit";
            this.buttonContinueEdit.Size = new System.Drawing.Size(155, 30);
            this.buttonContinueEdit.TabIndex = 33;
            this.buttonContinueEdit.Text = "1Continue edit";
            this.buttonContinueEdit.UseVisualStyleBackColor = true;
            // 
            // buttonIgnoreAndSave
            // 
            this.buttonIgnoreAndSave.Location = new System.Drawing.Point(30, 66);
            this.buttonIgnoreAndSave.Name = "buttonIgnoreAndSave";
            this.buttonIgnoreAndSave.Size = new System.Drawing.Size(188, 30);
            this.buttonIgnoreAndSave.TabIndex = 32;
            this.buttonIgnoreAndSave.Text = "1Ignore error and save";
            this.buttonIgnoreAndSave.UseVisualStyleBackColor = true;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(12, 28);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(44, 13);
            this.labelError.TabIndex = 34;
            this.labelError.Text = "EMPTY";
            // 
            // IntentoFormIgnoreError
            // 
            this.AcceptButton = this.buttonIgnoreAndSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonContinueEdit;
            this.ClientSize = new System.Drawing.Size(422, 107);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.buttonContinueEdit);
            this.Controls.Add(this.buttonIgnoreAndSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoFormIgnoreError";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test translation error";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonContinueEdit;
        public System.Windows.Forms.Button buttonIgnoreAndSave;
        public System.Windows.Forms.Label labelError;
    }
}