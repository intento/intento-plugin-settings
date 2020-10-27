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
			this.components = new System.ComponentModel.Container();
			this.labelAPI = new System.Windows.Forms.Label();
			this.apiKey_tb = new System.Windows.Forms.TextBox();
			this.checkBoxShowHidden = new System.Windows.Forms.CheckBox();
			this.labelError = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// labelAPI
			// 
			this.labelAPI.AutoSize = true;
			this.labelAPI.Location = new System.Drawing.Point(12, 30);
			this.labelAPI.Name = "labelAPI";
			this.labelAPI.Size = new System.Drawing.Size(87, 13);
			this.labelAPI.TabIndex = 21;
			this.labelAPI.Text = "Intento API Key:";
			// 
			// apiKey_tb
			// 
			this.apiKey_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.apiKey_tb.Location = new System.Drawing.Point(102, 28);
			this.apiKey_tb.Name = "apiKey_tb";
			this.apiKey_tb.Size = new System.Drawing.Size(326, 22);
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
			this.checkBoxShowHidden.Size = new System.Drawing.Size(64, 17);
			this.checkBoxShowHidden.TabIndex = 22;
			this.checkBoxShowHidden.Text = "Unmask";
			this.checkBoxShowHidden.UseVisualStyleBackColor = true;
			this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
			// 
			// labelError
			// 
			this.labelError.AutoSize = true;
			this.labelError.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelError.ForeColor = System.Drawing.Color.Red;
			this.labelError.Location = new System.Drawing.Point(12, 78);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(39, 13);
			this.labelError.TabIndex = 23;
			this.labelError.Text = "EMPTY";
			this.toolTip1.SetToolTip(this.labelError, "1Click to copy error to clipboard");
			this.labelError.Visible = false;
			this.labelError.Click += new System.EventHandler(this.labelError_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(307, 69);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(114, 30);
			this.buttonSave.TabIndex = 24;
			this.buttonSave.Text = "Test and save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// IntentoFormOptionsAPI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(531, 111);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.labelError);
			this.Controls.Add(this.checkBoxShowHidden);
			this.Controls.Add(this.labelAPI);
			this.Controls.Add(this.apiKey_tb);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "IntentoFormOptionsAPI";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ApiForm";
			this.Shown += new System.EventHandler(this.IntentoFormOptionsAPI_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAPI;
        public System.Windows.Forms.TextBox apiKey_tb;
        public System.Windows.Forms.CheckBox checkBoxShowHidden;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}