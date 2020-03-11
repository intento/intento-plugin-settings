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
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxProviders = new System.Windows.Forms.TextBox();
            this.comboBox_formats = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBoxSrc
            // 
            this.textBoxSrc.Location = new System.Drawing.Point(12, 46);
            this.textBoxSrc.MaxLength = 500000;
            this.textBoxSrc.Multiline = true;
            this.textBoxSrc.Name = "textBoxSrc";
            this.textBoxSrc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSrc.Size = new System.Drawing.Size(343, 284);
            this.textBoxSrc.TabIndex = 0;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(445, 46);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResult.Size = new System.Drawing.Size(343, 284);
            this.textBoxResult.TabIndex = 1;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(361, 46);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(78, 41);
            this.buttonGenerate.TabIndex = 2;
            this.buttonGenerate.Text = "->";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(361, 93);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(78, 41);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(59, 11);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(393, 20);
            this.textBoxApiKey.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ApiKey";
            // 
            // textBoxProviders
            // 
            this.textBoxProviders.Location = new System.Drawing.Point(445, 337);
            this.textBoxProviders.Multiline = true;
            this.textBoxProviders.Name = "textBoxProviders";
            this.textBoxProviders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProviders.Size = new System.Drawing.Size(343, 157);
            this.textBoxProviders.TabIndex = 6;
            // 
            // comboBox_formats
            // 
            this.comboBox_formats.FormattingEnabled = true;
            this.comboBox_formats.Items.AddRange(new object[] {
            "tagged",
            "html",
            "xml"});
            this.comboBox_formats.Location = new System.Drawing.Point(12, 350);
            this.comboBox_formats.Name = "comboBox_formats";
            this.comboBox_formats.Size = new System.Drawing.Size(121, 21);
            this.comboBox_formats.TabIndex = 7;
            // 
            // SmartRoutingPluginFormatted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.comboBox_formats);
            this.Controls.Add(this.textBoxProviders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxApiKey);
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
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProviders;
        private System.Windows.Forms.ComboBox comboBox_formats;
    }
}