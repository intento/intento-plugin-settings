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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAPI
            // 
            this.labelAPI.AutoSize = true;
            this.labelAPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAPI.Location = new System.Drawing.Point(6, 0);
            this.labelAPI.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelAPI.Name = "labelAPI";
            this.labelAPI.Size = new System.Drawing.Size(170, 56);
            this.labelAPI.TabIndex = 21;
            this.labelAPI.Text = "Intento API Key:";
            this.labelAPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // apiKey_tb
            // 
            this.apiKey_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.apiKey_tb, 3);
            this.apiKey_tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apiKey_tb.Location = new System.Drawing.Point(188, 6);
            this.apiKey_tb.Margin = new System.Windows.Forms.Padding(6);
            this.apiKey_tb.Name = "apiKey_tb";
            this.apiKey_tb.Size = new System.Drawing.Size(625, 37);
            this.apiKey_tb.TabIndex = 20;
            this.apiKey_tb.UseSystemPasswordChar = true;
            this.apiKey_tb.WordWrap = false;
            // 
            // checkBoxShowHidden
            // 
            this.checkBoxShowHidden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowHidden.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxShowHidden.Location = new System.Drawing.Point(825, 6);
            this.checkBoxShowHidden.Margin = new System.Windows.Forms.Padding(6);
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.checkBoxShowHidden.Size = new System.Drawing.Size(127, 44);
            this.checkBoxShowHidden.TabIndex = 22;
            this.checkBoxShowHidden.Text = "Unmask";
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
            // 
            // labelError
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.labelError, 2);
            this.labelError.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(6, 100);
            this.labelError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(306, 62);
            this.labelError.TabIndex = 23;
            this.labelError.Text = "EMPTY";
            this.toolTip1.SetToolTip(this.labelError, "1Click to copy error to clipboard");
            this.labelError.Visible = false;
            this.labelError.Click += new System.EventHandler(this.labelError_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(324, 106);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(307, 50);
            this.buttonSave.TabIndex = 24;
            this.buttonSave.Text = "Test and save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCancel, 2);
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(643, 106);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(309, 50);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Controls.Add(this.labelAPI, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelError, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.apiKey_tb, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxShowHidden, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 32);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(958, 162);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // IntentoFormOptionsAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1022, 226);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoFormOptionsAPI";
            this.Padding = new System.Windows.Forms.Padding(32);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ApiForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IntentoFormOptionsAPI_FormClosed);
            this.Shown += new System.EventHandler(this.IntentoFormOptionsAPI_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}