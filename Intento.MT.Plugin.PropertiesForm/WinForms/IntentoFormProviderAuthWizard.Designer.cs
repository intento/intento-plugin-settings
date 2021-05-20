namespace Intento.MT.Plugin.PropertiesForm
{
    partial class IntentoFormProviderAuthWizard
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxShowHidden = new System.Windows.Forms.CheckBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpCustomCredentials = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(706, 129);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(163, 52);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 19, 6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(688, 27);
            this.label1.TabIndex = 9;
            this.label1.Text = "Custom credentials (required)";
            this.label1.UseWaitCursor = true;
            // 
            // checkBoxShowHidden
            // 
            this.checkBoxShowHidden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowHidden.Location = new System.Drawing.Point(6, 129);
            this.checkBoxShowHidden.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.checkBoxShowHidden.Size = new System.Drawing.Size(688, 52);
            this.checkBoxShowHidden.TabIndex = 17;
            this.checkBoxShowHidden.Text = "Show hidden text";
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Controls.Add(this.checkBoxShowHidden, 0, 2);
            this.tlpMain.Controls.Add(this.buttonOK, 1, 2);
            this.tlpMain.Controls.Add(this.tlpCustomCredentials, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(16, 16);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpMain.Size = new System.Drawing.Size(875, 187);
            this.tlpMain.TabIndex = 18;
            // 
            // tlpCustomCredentials
            // 
            this.tlpCustomCredentials.AutoSize = true;
            this.tlpCustomCredentials.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpCustomCredentials.ColumnCount = 2;
            this.tlpMain.SetColumnSpan(this.tlpCustomCredentials, 2);
            this.tlpCustomCredentials.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpCustomCredentials.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpCustomCredentials.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCustomCredentials.Location = new System.Drawing.Point(6, 71);
            this.tlpCustomCredentials.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tlpCustomCredentials.Name = "tlpCustomCredentials";
            this.tlpCustomCredentials.RowCount = 1;
            this.tlpCustomCredentials.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCustomCredentials.Size = new System.Drawing.Size(863, 0);
            this.tlpCustomCredentials.TabIndex = 18;
            // 
            // IntentoFormProviderAuthWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(907, 219);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoFormProviderAuthWizard";
            this.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter credentials";
            this.TopMost = true;
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxShowHidden;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpCustomCredentials;
    }
}