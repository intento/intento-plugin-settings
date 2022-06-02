namespace Intento.MT.Plugin.PropertiesForm.WinForms
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAPI
            // 
            this.labelAPI.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelAPI.Location = new System.Drawing.Point(0, 8);
            this.labelAPI.Name = "labelAPI";
            this.labelAPI.Size = new System.Drawing.Size(93, 23);
            this.labelAPI.TabIndex = 21;
            this.labelAPI.Text = "Intento API Key:";
            this.labelAPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // apiKey_tb
            // 
            this.apiKey_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apiKey_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiKey_tb.Location = new System.Drawing.Point(93, 8);
            this.apiKey_tb.Name = "apiKey_tb";
            this.apiKey_tb.Size = new System.Drawing.Size(326, 22);
            this.apiKey_tb.TabIndex = 20;
            this.apiKey_tb.UseSystemPasswordChar = true;
            this.apiKey_tb.WordWrap = false;
            // 
            // checkBoxShowHidden
            // 
            this.checkBoxShowHidden.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBoxShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowHidden.Location = new System.Drawing.Point(419, 8);
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.checkBoxShowHidden.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.checkBoxShowHidden.Size = new System.Drawing.Size(73, 23);
            this.checkBoxShowHidden.TabIndex = 22;
            this.checkBoxShowHidden.Text = "Unmask";
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
            // 
            // labelError
            // 
            this.labelError.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(0, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(302, 26);
            this.labelError.TabIndex = 23;
            this.labelError.Text = "EMPTY";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.labelError, "1Click to copy error to clipboard");
            this.labelError.Visible = false;
            this.labelError.Click += new System.EventHandler(this.labelError_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.Location = new System.Drawing.Point(302, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(90, 26);
            this.buttonSave.TabIndex = 24;
            this.buttonSave.Text = "Test and save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.Location = new System.Drawing.Point(402, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 26);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.apiKey_tb);
            this.panel1.Controls.Add(this.checkBoxShowHidden);
            this.panel1.Controls.Add(this.labelAPI);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(16, 16);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.panel1.Size = new System.Drawing.Size(492, 39);
            this.panel1.TabIndex = 27;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelError);
            this.panel3.Controls.Add(this.buttonSave);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.buttonCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(16, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(492, 26);
            this.panel3.TabIndex = 28;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(392, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 26);
            this.panel4.TabIndex = 26;
            // 
            // IntentoFormOptionsAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(524, 113);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 152);
            this.Name = "IntentoFormOptionsAPI";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ApiForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IntentoFormOptionsAPI_FormClosed);
            this.Shown += new System.EventHandler(this.IntentoFormOptionsAPI_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAPI;
        public System.Windows.Forms.TextBox apiKey_tb;
        public System.Windows.Forms.CheckBox checkBoxShowHidden;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}