namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    partial class IntentoFormAdvanced
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
			this.groupBoxProxy = new System.Windows.Forms.GroupBox();
			this.checkBoxAuth = new System.Windows.Forms.CheckBox();
			this.groupBoxAuth = new System.Windows.Forms.GroupBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.labelPassword = new System.Windows.Forms.Label();
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.labelUserName = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.labelPort = new System.Windows.Forms.Label();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.checkBoxProxy = new System.Windows.Forms.CheckBox();
			this.checkBoxSaveApiKeyInRegistry = new System.Windows.Forms.CheckBox();
			this.checkBoxTrace = new System.Windows.Forms.CheckBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.textBoxCustomSettingsName = new System.Windows.Forms.TextBox();
			this.labelCustomSettingsName = new System.Windows.Forms.Label();
			this.checkBoxCustomTagParser = new System.Windows.Forms.CheckBox();
			this.checkBoxCutTags = new System.Windows.Forms.CheckBox();
			this.labelError = new System.Windows.Forms.Label();
			this.checkBoxSaveLocally = new System.Windows.Forms.CheckBox();
			this.groupBoxProxy.SuspendLayout();
			this.groupBoxAuth.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxProxy
			// 
			this.groupBoxProxy.Controls.Add(this.checkBoxAuth);
			this.groupBoxProxy.Controls.Add(this.groupBoxAuth);
			this.groupBoxProxy.Controls.Add(this.textBoxPort);
			this.groupBoxProxy.Controls.Add(this.labelPort);
			this.groupBoxProxy.Controls.Add(this.textBoxAddress);
			this.groupBoxProxy.Controls.Add(this.labelAddress);
			this.groupBoxProxy.Enabled = false;
			this.groupBoxProxy.Location = new System.Drawing.Point(16, 69);
			this.groupBoxProxy.Name = "groupBoxProxy";
			this.groupBoxProxy.Size = new System.Drawing.Size(449, 189);
			this.groupBoxProxy.TabIndex = 12;
			this.groupBoxProxy.TabStop = false;
			// 
			// checkBoxAuth
			// 
			this.checkBoxAuth.AutoSize = true;
			this.checkBoxAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxAuth.Location = new System.Drawing.Point(16, 52);
			this.checkBoxAuth.Name = "checkBoxAuth";
			this.checkBoxAuth.Size = new System.Drawing.Size(141, 17);
			this.checkBoxAuth.TabIndex = 17;
			this.checkBoxAuth.Text = "Authorization required";
			this.checkBoxAuth.UseVisualStyleBackColor = true;
			this.checkBoxAuth.CheckedChanged += new System.EventHandler(this.checkBoxAuth_CheckedChanged);
			// 
			// groupBoxAuth
			// 
			this.groupBoxAuth.Controls.Add(this.textBoxPassword);
			this.groupBoxAuth.Controls.Add(this.labelPassword);
			this.groupBoxAuth.Controls.Add(this.textBoxUserName);
			this.groupBoxAuth.Controls.Add(this.labelUserName);
			this.groupBoxAuth.Enabled = false;
			this.groupBoxAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBoxAuth.Location = new System.Drawing.Point(16, 67);
			this.groupBoxAuth.Name = "groupBoxAuth";
			this.groupBoxAuth.Size = new System.Drawing.Size(416, 100);
			this.groupBoxAuth.TabIndex = 16;
			this.groupBoxAuth.TabStop = false;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPassword.Location = new System.Drawing.Point(78, 62);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(321, 22);
			this.textBoxPassword.TabIndex = 7;
			this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxAuth_TextChanged);
			// 
			// labelPassword
			// 
			this.labelPassword.AutoSize = true;
			this.labelPassword.Location = new System.Drawing.Point(11, 65);
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.Size = new System.Drawing.Size(59, 13);
			this.labelPassword.TabIndex = 6;
			this.labelPassword.Text = "Password:";
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxUserName.Location = new System.Drawing.Point(78, 23);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(321, 22);
			this.textBoxUserName.TabIndex = 5;
			this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxAuth_TextChanged);
			// 
			// labelUserName
			// 
			this.labelUserName.AutoSize = true;
			this.labelUserName.Location = new System.Drawing.Point(11, 26);
			this.labelUserName.Name = "labelUserName";
			this.labelUserName.Size = new System.Drawing.Size(64, 13);
			this.labelUserName.TabIndex = 4;
			this.labelUserName.Text = "User name:";
			// 
			// textBoxPort
			// 
			this.textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPort.Location = new System.Drawing.Point(383, 25);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(48, 22);
			this.textBoxPort.TabIndex = 15;
			this.textBoxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPort_KeyPress);
			// 
			// labelPort
			// 
			this.labelPort.AutoSize = true;
			this.labelPort.Location = new System.Drawing.Point(353, 28);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(31, 13);
			this.labelPort.TabIndex = 14;
			this.labelPort.Text = "Port:";
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxAddress.Location = new System.Drawing.Point(191, 25);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(160, 22);
			this.textBoxAddress.TabIndex = 13;
			// 
			// labelAddress
			// 
			this.labelAddress.AutoSize = true;
			this.labelAddress.Location = new System.Drawing.Point(14, 28);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(180, 13);
			this.labelAddress.TabIndex = 12;
			this.labelAddress.Text = "IP address or host name: http(s)://";
			// 
			// checkBoxProxy
			// 
			this.checkBoxProxy.AutoSize = true;
			this.checkBoxProxy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxProxy.Location = new System.Drawing.Point(16, 49);
			this.checkBoxProxy.Name = "checkBoxProxy";
			this.checkBoxProxy.Size = new System.Drawing.Size(98, 17);
			this.checkBoxProxy.TabIndex = 13;
			this.checkBoxProxy.Text = "checkBoxProxy";
			this.checkBoxProxy.UseVisualStyleBackColor = true;
			this.checkBoxProxy.CheckedChanged += new System.EventHandler(this.checkBoxProxy_CheckedChanged);
			// 
			// checkBoxSaveApiKeyInRegistry
			// 
			this.checkBoxSaveApiKeyInRegistry.AutoSize = true;
			this.checkBoxSaveApiKeyInRegistry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxSaveApiKeyInRegistry.Location = new System.Drawing.Point(240, 373);
			this.checkBoxSaveApiKeyInRegistry.Name = "checkBoxSaveApiKeyInRegistry";
			this.checkBoxSaveApiKeyInRegistry.Size = new System.Drawing.Size(179, 17);
			this.checkBoxSaveApiKeyInRegistry.TabIndex = 15;
			this.checkBoxSaveApiKeyInRegistry.Text = "checkBoxSaveApiKeyInRegistry";
			this.checkBoxSaveApiKeyInRegistry.UseVisualStyleBackColor = true;
			this.checkBoxSaveApiKeyInRegistry.Visible = false;
			// 
			// checkBoxTrace
			// 
			this.checkBoxTrace.AutoSize = true;
			this.checkBoxTrace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxTrace.Location = new System.Drawing.Point(16, 15);
			this.checkBoxTrace.Name = "checkBoxTrace";
			this.checkBoxTrace.Size = new System.Drawing.Size(97, 17);
			this.checkBoxTrace.TabIndex = 14;
			this.checkBoxTrace.Text = "checkBoxTrace";
			this.checkBoxTrace.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(384, 352);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(81, 22);
			this.buttonCancel.TabIndex = 33;
			this.buttonCancel.Text = "1Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(293, 352);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(81, 22);
			this.buttonSave.TabIndex = 32;
			this.buttonSave.Text = "1Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 388);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.statusStrip1.Size = new System.Drawing.Size(481, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 35;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
			this.toolStripStatusLabel1.Text = "0.0.0.0";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxCustomSettingsName
			// 
			this.textBoxCustomSettingsName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCustomSettingsName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxCustomSettingsName.Location = new System.Drawing.Point(16, 313);
			this.textBoxCustomSettingsName.Name = "textBoxCustomSettingsName";
			this.textBoxCustomSettingsName.Size = new System.Drawing.Size(449, 22);
			this.textBoxCustomSettingsName.TabIndex = 36;
			// 
			// labelCustomSettingsName
			// 
			this.labelCustomSettingsName.AutoSize = true;
			this.labelCustomSettingsName.Location = new System.Drawing.Point(13, 293);
			this.labelCustomSettingsName.Name = "labelCustomSettingsName";
			this.labelCustomSettingsName.Size = new System.Drawing.Size(349, 13);
			this.labelCustomSettingsName.TabIndex = 37;
			this.labelCustomSettingsName.Text = "Custom name for plugin settings (leave blank to use default name)";
			// 
			// checkBoxCustomTagParser
			// 
			this.checkBoxCustomTagParser.AutoSize = true;
			this.checkBoxCustomTagParser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxCustomTagParser.Location = new System.Drawing.Point(337, 293);
			this.checkBoxCustomTagParser.Name = "checkBoxCustomTagParser";
			this.checkBoxCustomTagParser.Size = new System.Drawing.Size(111, 17);
			this.checkBoxCustomTagParser.TabIndex = 38;
			this.checkBoxCustomTagParser.Text = "CustomTagParser";
			this.checkBoxCustomTagParser.UseVisualStyleBackColor = true;
			this.checkBoxCustomTagParser.Visible = false;
			// 
			// checkBoxCutTags
			// 
			this.checkBoxCutTags.AutoSize = true;
			this.checkBoxCutTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxCutTags.Location = new System.Drawing.Point(16, 270);
			this.checkBoxCutTags.Name = "checkBoxCutTags";
			this.checkBoxCutTags.Size = new System.Drawing.Size(112, 17);
			this.checkBoxCutTags.TabIndex = 39;
			this.checkBoxCutTags.Text = "checkBoxCutTags";
			this.checkBoxCutTags.UseVisualStyleBackColor = true;
			this.checkBoxCutTags.Visible = false;
			// 
			// labelError
			// 
			this.labelError.AutoSize = true;
			this.labelError.ForeColor = System.Drawing.Color.Red;
			this.labelError.Location = new System.Drawing.Point(13, 363);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(123, 13);
			this.labelError.TabIndex = 34;
			this.labelError.Text = "Proxy connection error";
			this.labelError.Visible = false;
			// 
			// checkBoxSaveLocally
			// 
			this.checkBoxSaveLocally.AutoSize = true;
			this.checkBoxSaveLocally.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxSaveLocally.Location = new System.Drawing.Point(227, 15);
			this.checkBoxSaveLocally.Name = "checkBoxSaveLocally";
			this.checkBoxSaveLocally.Size = new System.Drawing.Size(128, 17);
			this.checkBoxSaveLocally.TabIndex = 40;
			this.checkBoxSaveLocally.Text = "checkBoxSaveLocally";
			this.checkBoxSaveLocally.UseVisualStyleBackColor = true;
			this.checkBoxSaveLocally.Visible = false;
			// 
			// IntentoFormAdvanced
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(481, 410);
			this.Controls.Add(this.checkBoxSaveLocally);
			this.Controls.Add(this.checkBoxCutTags);
			this.Controls.Add(this.checkBoxCustomTagParser);
			this.Controls.Add(this.labelCustomSettingsName);
			this.Controls.Add(this.textBoxCustomSettingsName);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.labelError);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.checkBoxSaveApiKeyInRegistry);
			this.Controls.Add(this.checkBoxTrace);
			this.Controls.Add(this.checkBoxProxy);
			this.Controls.Add(this.groupBoxProxy);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "IntentoFormAdvanced";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "IntentoFormAdvanced";
			this.Shown += new System.EventHandler(this.IntentoFormAdvanced_Shown);
			this.groupBoxProxy.ResumeLayout(false);
			this.groupBoxProxy.PerformLayout();
			this.groupBoxAuth.ResumeLayout(false);
			this.groupBoxAuth.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxProxy;
        private System.Windows.Forms.CheckBox checkBoxAuth;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelAddress;
        public System.Windows.Forms.CheckBox checkBoxProxy;
        public System.Windows.Forms.CheckBox checkBoxSaveApiKeyInRegistry;
        public System.Windows.Forms.CheckBox checkBoxTrace;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox textBoxCustomSettingsName;
        private System.Windows.Forms.Label labelCustomSettingsName;
		public System.Windows.Forms.CheckBox checkBoxCustomTagParser;
		public System.Windows.Forms.CheckBox checkBoxCutTags;
		private System.Windows.Forms.Label labelError;
		public System.Windows.Forms.CheckBox checkBoxSaveLocally;
	}
}