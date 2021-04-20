namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    partial class IntentoFormSplash
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntentoFormSplash));
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBoxSplash = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSplash)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(109, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(225, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Initializing and checking current options...";
			this.label1.UseWaitCursor = true;
			// 
			// pictureBoxSplash
			// 
			this.pictureBoxSplash.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSplash.Image")));
			this.pictureBoxSplash.Location = new System.Drawing.Point(31, 13);
			this.pictureBoxSplash.Name = "pictureBoxSplash";
			this.pictureBoxSplash.Size = new System.Drawing.Size(50, 50);
			this.pictureBoxSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxSplash.TabIndex = 1;
			this.pictureBoxSplash.TabStop = false;
			this.pictureBoxSplash.UseWaitCursor = true;
			// 
			// IntentoFormSplash
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(359, 77);
			this.Controls.Add(this.pictureBoxSplash);
			this.Controls.Add(this.label1);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "IntentoFormSplash";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "IntentoFormSplash";
			this.TopMost = true;
			this.UseWaitCursor = true;
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSplash)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxSplash;
    }
}