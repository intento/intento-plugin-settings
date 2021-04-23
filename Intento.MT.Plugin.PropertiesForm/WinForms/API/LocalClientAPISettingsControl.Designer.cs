
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.Controls.Add(this.filePathBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.selectFileButton, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(876, 52);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // filePathBox
            // 
            this.filePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathBox.Location = new System.Drawing.Point(10, 10);
            this.filePathBox.Margin = new System.Windows.Forms.Padding(10);
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.Size = new System.Drawing.Size(709, 31);
            this.filePathBox.TabIndex = 0;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectFileButton.Location = new System.Drawing.Point(734, 5);
            this.selectFileButton.Margin = new System.Windows.Forms.Padding(5);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(137, 42);
            this.selectFileButton.TabIndex = 1;
            this.selectFileButton.Text = "Open...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            // 
            // LocalClientAPISettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "LocalClientAPISettingsControl";
            this.Size = new System.Drawing.Size(876, 52);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileSelector;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Button selectFileButton;
    }
}
