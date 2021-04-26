
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
            this.tableSelectedFile = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.tableNotSelectedFile.SuspendLayout();
            this.tableSelectedFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableNotSelectedFile
            // 
            this.tableNotSelectedFile.ColumnCount = 2;
            this.tableNotSelectedFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableNotSelectedFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableNotSelectedFile.Controls.Add(this.filePathBox, 0, 0);
            this.tableNotSelectedFile.Controls.Add(this.selectFileButton, 1, 0);
            this.tableNotSelectedFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableNotSelectedFile.Location = new System.Drawing.Point(0, 0);
            this.tableNotSelectedFile.Margin = new System.Windows.Forms.Padding(2);
            this.tableNotSelectedFile.Name = "tableNotSelectedFile";
            this.tableNotSelectedFile.RowCount = 1;
            this.tableNotSelectedFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableNotSelectedFile.Size = new System.Drawing.Size(584, 36);
            this.tableNotSelectedFile.TabIndex = 1;
            // 
            // filePathBox
            // 
            this.filePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathBox.Location = new System.Drawing.Point(7, 6);
            this.filePathBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.Size = new System.Drawing.Size(472, 22);
            this.filePathBox.TabIndex = 0;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectFileButton.Location = new System.Drawing.Point(489, 3);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(92, 30);
            this.selectFileButton.TabIndex = 1;
            this.selectFileButton.Text = "Open...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // tableSelectedFile
            // 
            this.tableSelectedFile.ColumnCount = 2;
            this.tableSelectedFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableSelectedFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableSelectedFile.Controls.Add(this.button1, 1, 0);
            this.tableSelectedFile.Controls.Add(this.label, 0, 0);
            this.tableSelectedFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableSelectedFile.Location = new System.Drawing.Point(0, 36);
            this.tableSelectedFile.Margin = new System.Windows.Forms.Padding(2);
            this.tableSelectedFile.Name = "tableSelectedFile";
            this.tableSelectedFile.RowCount = 1;
            this.tableSelectedFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSelectedFile.Size = new System.Drawing.Size(584, 36);
            this.tableSelectedFile.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(489, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point(3, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(5);
            this.label.Size = new System.Drawing.Size(480, 36);
            this.label.TabIndex = 2;
            this.label.Text = "File for traslate is selected";
            // 
            // LocalClientAPISettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableSelectedFile);
            this.Controls.Add(this.tableNotSelectedFile);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LocalClientAPISettingsControl";
            this.Size = new System.Drawing.Size(584, 84);
            this.tableNotSelectedFile.ResumeLayout(false);
            this.tableNotSelectedFile.PerformLayout();
            this.tableSelectedFile.ResumeLayout(false);
            this.tableSelectedFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileSelector;
        private System.Windows.Forms.TableLayoutPanel tableNotSelectedFile;
        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.TableLayoutPanel tableSelectedFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label;
    }
}
