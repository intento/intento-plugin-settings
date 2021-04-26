
namespace Intento.MT.Plugin.PropertiesForm.WinForms.API
{
    partial class LocalClientAPISettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileSelector = new System.Windows.Forms.OpenFileDialog();
            this.tableNotSelectedFile = new System.Windows.Forms.TableLayoutPanel();
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.tableNotSelectedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableNotSelectedFile
            // 
            this.tableNotSelectedFile.ColumnCount = 2;
            this.tableNotSelectedFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableNotSelectedFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableNotSelectedFile.Controls.Add(this.filePathBox, 0, 0);
            this.tableNotSelectedFile.Controls.Add(this.selectFileButton, 1, 0);
            this.tableNotSelectedFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableNotSelectedFile.Location = new System.Drawing.Point(0, 0);
            this.tableNotSelectedFile.Name = "tableNotSelectedFile";
            this.tableNotSelectedFile.RowCount = 1;
            this.tableNotSelectedFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableNotSelectedFile.Size = new System.Drawing.Size(876, 52);
            this.tableNotSelectedFile.TabIndex = 1;
            // 
            // filePathBox
            // 
            this.filePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathBox.Location = new System.Drawing.Point(0, 9);
            this.filePathBox.Margin = new System.Windows.Forms.Padding(0, 9, 10, 9);
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.Size = new System.Drawing.Size(719, 31);
            this.filePathBox.TabIndex = 0;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectFileButton.Location = new System.Drawing.Point(733, 5);
            this.selectFileButton.Margin = new System.Windows.Forms.Padding(4, 5, 0, 5);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(143, 42);
            this.selectFileButton.TabIndex = 1;
            this.selectFileButton.Text = "Open...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // LocalClientAPISettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableNotSelectedFile);
            this.Name = "LocalClientAPISettingsControl";
            this.Size = new System.Drawing.Size(876, 52);
            this.tableNotSelectedFile.ResumeLayout(false);
            this.tableNotSelectedFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileSelector;
        private System.Windows.Forms.TableLayoutPanel tableNotSelectedFile;
        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Button selectFileButton;
    }
}
