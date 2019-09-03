namespace Intento.MT.Plugin.PropertiesForm
{
    partial class SmartRoutingPluginFormatted
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
            this.textBoxSrc = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSrc
            // 
            this.textBoxSrc.Location = new System.Drawing.Point(12, 12);
            this.textBoxSrc.Multiline = true;
            this.textBoxSrc.Name = "textBoxSrc";
            this.textBoxSrc.Size = new System.Drawing.Size(343, 426);
            this.textBoxSrc.TabIndex = 0;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(445, 12);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(343, 426);
            this.textBoxResult.TabIndex = 1;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(361, 12);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(78, 41);
            this.buttonGenerate.TabIndex = 2;
            this.buttonGenerate.Text = "->";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(361, 397);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(78, 41);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // SmartRoutingPluginFormatted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.textBoxSrc);
            this.Name = "SmartRoutingPluginFormatted";
            this.Text = "SmartRoutingPluginFormatted";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSrc;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonSave;
    }
}