namespace Intento.MT.Plugin.PropertiesForm
{
    partial class IntentoFormOptionsAPI
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
            this.label5 = new System.Windows.Forms.Label();
            this.apiKey_tb = new System.Windows.Forms.TextBox();
            this.checkBoxShowHidden = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Intento API Key:";
            // 
            // apiKey_tb
            // 
            this.apiKey_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apiKey_tb.Location = new System.Drawing.Point(102, 28);
            this.apiKey_tb.Name = "apiKey_tb";
            this.apiKey_tb.Size = new System.Drawing.Size(326, 20);
            this.apiKey_tb.TabIndex = 20;
            this.apiKey_tb.UseSystemPasswordChar = true;
            this.apiKey_tb.WordWrap = false;
            // 
            // checkBoxShowHidden
            // 
            this.checkBoxShowHidden.AutoSize = true;
            this.checkBoxShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowHidden.Location = new System.Drawing.Point(450, 30);
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.checkBoxShowHidden.Size = new System.Drawing.Size(62, 17);
            this.checkBoxShowHidden.TabIndex = 22;
            this.checkBoxShowHidden.Text = "Unmask";
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "EMPTY";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(307, 69);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 30);
            this.buttonSave.TabIndex = 24;
            this.buttonSave.Text = "Test and save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(427, 69);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(81, 30);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // IntentoFormOptionsAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(531, 111);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxShowHidden);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.apiKey_tb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoFormOptionsAPI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ApiForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox apiKey_tb;
        public System.Windows.Forms.CheckBox checkBoxShowHidden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}