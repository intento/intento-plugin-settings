namespace Intento.MT.Plugin.PropertiesForm
{
    partial class IntentoTranslationProviderOptionsForm
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
			this.groupBoxMTConnect = new System.Windows.Forms.GroupBox();
			this.textBoxLabel1 = new System.Windows.Forms.TextBox();
			this.labelRegister1 = new System.Windows.Forms.Label();
			this.buttonSetApi = new System.Windows.Forms.Button();
			this.labelRegister2 = new System.Windows.Forms.Label();
			this.groupBoxMTSettings = new System.Windows.Forms.GroupBox();
			this.labelApiKeyIsChanged = new System.Windows.Forms.Label();
			this.groupBoxGlossary = new System.Windows.Forms.GroupBox();
			this.textBoxGlossary = new System.Windows.Forms.TextBox();
			this.groupBoxModel = new System.Windows.Forms.GroupBox();
			this.textBoxModel = new System.Windows.Forms.TextBox();
			this.groupBoxBillingAccount = new System.Windows.Forms.GroupBox();
			this.textBoxAccount = new System.Windows.Forms.TextBox();
			this.buttonMTSetting = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxProviderName = new System.Windows.Forms.TextBox();
			this.buttonContinue = new System.Windows.Forms.Button();
			this.groupBoxMTConnect2 = new System.Windows.Forms.GroupBox();
			this.labelIAK = new System.Windows.Forms.Label();
			this.apiKey_tb = new System.Windows.Forms.TextBox();
			this.buttonCheck = new System.Windows.Forms.Button();
			this.buttonAdvanced = new System.Windows.Forms.Button();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBoxMTConnectWL = new System.Windows.Forms.GroupBox();
			this.labelWL = new System.Windows.Forms.Label();
			this.buttonWL = new System.Windows.Forms.Button();
			this.labelSBKey = new System.Windows.Forms.Label();
			this.groupBoxMTConnect.SuspendLayout();
			this.groupBoxMTSettings.SuspendLayout();
			this.groupBoxGlossary.SuspendLayout();
			this.groupBoxModel.SuspendLayout();
			this.groupBoxBillingAccount.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBoxMTConnect2.SuspendLayout();
			this.groupBoxMTConnectWL.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxMTConnect
			// 
			this.groupBoxMTConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxMTConnect.Controls.Add(this.textBoxLabel1);
			this.groupBoxMTConnect.Controls.Add(this.labelRegister1);
			this.groupBoxMTConnect.Controls.Add(this.buttonSetApi);
			this.groupBoxMTConnect.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupBoxMTConnect.Location = new System.Drawing.Point(12, 12);
			this.groupBoxMTConnect.Name = "groupBoxMTConnect";
			this.groupBoxMTConnect.Size = new System.Drawing.Size(623, 85);
			this.groupBoxMTConnect.TabIndex = 0;
			this.groupBoxMTConnect.TabStop = false;
			this.groupBoxMTConnect.Text = "Connect to Intento MT HUB";
			// 
			// textBoxLabel1
			// 
			this.textBoxLabel1.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.textBoxLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxLabel1.ForeColor = System.Drawing.Color.Blue;
			this.textBoxLabel1.Location = new System.Drawing.Point(249, 40);
			this.textBoxLabel1.Name = "textBoxLabel1";
			this.textBoxLabel1.Size = new System.Drawing.Size(118, 13);
			this.textBoxLabel1.TabIndex = 20;
			this.textBoxLabel1.Tag = "https://console.inten.to";
			this.textBoxLabel1.Text = "https://console.inten.to";
			this.textBoxLabel1.Click += new System.EventHandler(this.linkLabel_LinkClicked);
			// 
			// labelRegister1
			// 
			this.labelRegister1.AutoSize = true;
			this.labelRegister1.Location = new System.Drawing.Point(183, 40);
			this.labelRegister1.Name = "labelRegister1";
			this.labelRegister1.Size = new System.Drawing.Size(65, 13);
			this.labelRegister1.TabIndex = 19;
			this.labelRegister1.Text = "or register at";
			// 
			// buttonSetApi
			// 
			this.buttonSetApi.Location = new System.Drawing.Point(17, 31);
			this.buttonSetApi.Name = "buttonSetApi";
			this.buttonSetApi.Size = new System.Drawing.Size(150, 30);
			this.buttonSetApi.TabIndex = 0;
			this.buttonSetApi.Text = "Enter your Intento API key";
			this.buttonSetApi.UseVisualStyleBackColor = true;
			this.buttonSetApi.Click += new System.EventHandler(this.buttonSetApi_Click);
			// 
			// labelRegister2
			// 
			this.labelRegister2.AutoSize = true;
			this.labelRegister2.Location = new System.Drawing.Point(364, 40);
			this.labelRegister2.Name = "labelRegister2";
			this.labelRegister2.Size = new System.Drawing.Size(58, 13);
			this.labelRegister2.TabIndex = 21;
			this.labelRegister2.Text = "to get one.";
			// 
			// groupBoxMTSettings
			// 
			this.groupBoxMTSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxMTSettings.Controls.Add(this.labelApiKeyIsChanged);
			this.groupBoxMTSettings.Controls.Add(this.groupBoxGlossary);
			this.groupBoxMTSettings.Controls.Add(this.groupBoxModel);
			this.groupBoxMTSettings.Controls.Add(this.groupBoxBillingAccount);
			this.groupBoxMTSettings.Controls.Add(this.buttonMTSetting);
			this.groupBoxMTSettings.Controls.Add(this.groupBox1);
			this.groupBoxMTSettings.Location = new System.Drawing.Point(12, 112);
			this.groupBoxMTSettings.Name = "groupBoxMTSettings";
			this.groupBoxMTSettings.Size = new System.Drawing.Size(623, 286);
			this.groupBoxMTSettings.TabIndex = 1;
			this.groupBoxMTSettings.TabStop = false;
			this.groupBoxMTSettings.Text = "Machine Translation Settings";
			// 
			// labelApiKeyIsChanged
			// 
			this.labelApiKeyIsChanged.AutoSize = true;
			this.labelApiKeyIsChanged.ForeColor = System.Drawing.Color.Red;
			this.labelApiKeyIsChanged.Location = new System.Drawing.Point(334, 11);
			this.labelApiKeyIsChanged.Name = "labelApiKeyIsChanged";
			this.labelApiKeyIsChanged.Size = new System.Drawing.Size(283, 13);
			this.labelApiKeyIsChanged.TabIndex = 29;
			this.labelApiKeyIsChanged.Text = "You changed ApiKey. We recommend to test MT Settings.";
			this.labelApiKeyIsChanged.Visible = false;
			// 
			// groupBoxGlossary
			// 
			this.groupBoxGlossary.Controls.Add(this.textBoxGlossary);
			this.groupBoxGlossary.Location = new System.Drawing.Point(18, 211);
			this.groupBoxGlossary.Name = "groupBoxGlossary";
			this.groupBoxGlossary.Size = new System.Drawing.Size(589, 46);
			this.groupBoxGlossary.TabIndex = 4;
			this.groupBoxGlossary.TabStop = false;
			this.groupBoxGlossary.Text = "Glossary";
			// 
			// textBoxGlossary
			// 
			this.textBoxGlossary.BackColor = System.Drawing.SystemColors.Info;
			this.textBoxGlossary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxGlossary.Enabled = false;
			this.textBoxGlossary.Location = new System.Drawing.Point(13, 19);
			this.textBoxGlossary.Name = "textBoxGlossary";
			this.textBoxGlossary.ReadOnly = true;
			this.textBoxGlossary.Size = new System.Drawing.Size(564, 20);
			this.textBoxGlossary.TabIndex = 0;
			this.textBoxGlossary.Text = "N/A";
			// 
			// groupBoxModel
			// 
			this.groupBoxModel.Controls.Add(this.textBoxModel);
			this.groupBoxModel.Location = new System.Drawing.Point(18, 148);
			this.groupBoxModel.Name = "groupBoxModel";
			this.groupBoxModel.Size = new System.Drawing.Size(589, 46);
			this.groupBoxModel.TabIndex = 3;
			this.groupBoxModel.TabStop = false;
			this.groupBoxModel.Text = "Model";
			// 
			// textBoxModel
			// 
			this.textBoxModel.BackColor = System.Drawing.SystemColors.Info;
			this.textBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxModel.Enabled = false;
			this.textBoxModel.Location = new System.Drawing.Point(13, 19);
			this.textBoxModel.Name = "textBoxModel";
			this.textBoxModel.ReadOnly = true;
			this.textBoxModel.Size = new System.Drawing.Size(564, 20);
			this.textBoxModel.TabIndex = 0;
			this.textBoxModel.Text = "N/A";
			// 
			// groupBoxBillingAccount
			// 
			this.groupBoxBillingAccount.Controls.Add(this.textBoxAccount);
			this.groupBoxBillingAccount.Location = new System.Drawing.Point(18, 87);
			this.groupBoxBillingAccount.Name = "groupBoxBillingAccount";
			this.groupBoxBillingAccount.Size = new System.Drawing.Size(589, 46);
			this.groupBoxBillingAccount.TabIndex = 2;
			this.groupBoxBillingAccount.TabStop = false;
			this.groupBoxBillingAccount.Text = "Billing Account";
			// 
			// textBoxAccount
			// 
			this.textBoxAccount.BackColor = System.Drawing.SystemColors.Info;
			this.textBoxAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxAccount.Enabled = false;
			this.textBoxAccount.Location = new System.Drawing.Point(13, 19);
			this.textBoxAccount.Name = "textBoxAccount";
			this.textBoxAccount.ReadOnly = true;
			this.textBoxAccount.Size = new System.Drawing.Size(564, 20);
			this.textBoxAccount.TabIndex = 0;
			this.textBoxAccount.Text = "N/A";
			// 
			// buttonMTSetting
			// 
			this.buttonMTSetting.Location = new System.Drawing.Point(440, 33);
			this.buttonMTSetting.Name = "buttonMTSetting";
			this.buttonMTSetting.Size = new System.Drawing.Size(150, 40);
			this.buttonMTSetting.TabIndex = 1;
			this.buttonMTSetting.Text = "Change MT Settings";
			this.buttonMTSetting.UseVisualStyleBackColor = true;
			this.buttonMTSetting.Click += new System.EventHandler(this.buttonMTSetting_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxProviderName);
			this.groupBox1.Location = new System.Drawing.Point(18, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(404, 46);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Provider";
			// 
			// textBoxProviderName
			// 
			this.textBoxProviderName.BackColor = System.Drawing.SystemColors.Info;
			this.textBoxProviderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxProviderName.Enabled = false;
			this.textBoxProviderName.Location = new System.Drawing.Point(13, 19);
			this.textBoxProviderName.Name = "textBoxProviderName";
			this.textBoxProviderName.ReadOnly = true;
			this.textBoxProviderName.Size = new System.Drawing.Size(379, 20);
			this.textBoxProviderName.TabIndex = 0;
			this.textBoxProviderName.Text = "N/A";
			// 
			// buttonContinue
			// 
			this.buttonContinue.Enabled = false;
			this.buttonContinue.Location = new System.Drawing.Point(485, 413);
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.Size = new System.Drawing.Size(150, 30);
			this.buttonContinue.TabIndex = 19;
			this.buttonContinue.Text = "Close";
			this.buttonContinue.UseVisualStyleBackColor = true;
			this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
			// 
			// groupBoxMTConnect2
			// 
			this.groupBoxMTConnect2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxMTConnect2.Controls.Add(this.labelIAK);
			this.groupBoxMTConnect2.Controls.Add(this.apiKey_tb);
			this.groupBoxMTConnect2.Controls.Add(this.buttonCheck);
			this.groupBoxMTConnect2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupBoxMTConnect2.Location = new System.Drawing.Point(198, 6);
			this.groupBoxMTConnect2.Name = "groupBoxMTConnect2";
			this.groupBoxMTConnect2.Size = new System.Drawing.Size(623, 85);
			this.groupBoxMTConnect2.TabIndex = 21;
			this.groupBoxMTConnect2.TabStop = false;
			this.groupBoxMTConnect2.Text = "Connect to Intento MT HUB";
			// 
			// labelIAK
			// 
			this.labelIAK.AutoSize = true;
			this.labelIAK.Location = new System.Drawing.Point(18, 40);
			this.labelIAK.Name = "labelIAK";
			this.labelIAK.Size = new System.Drawing.Size(45, 13);
			this.labelIAK.TabIndex = 28;
			this.labelIAK.Text = "API Key";
			// 
			// apiKey_tb
			// 
			this.apiKey_tb.BackColor = System.Drawing.SystemColors.Info;
			this.apiKey_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.apiKey_tb.Location = new System.Drawing.Point(118, 37);
			this.apiKey_tb.Name = "apiKey_tb";
			this.apiKey_tb.PasswordChar = '*';
			this.apiKey_tb.ReadOnly = true;
			this.apiKey_tb.Size = new System.Drawing.Size(369, 20);
			this.apiKey_tb.TabIndex = 26;
			this.apiKey_tb.UseSystemPasswordChar = true;
			this.apiKey_tb.WordWrap = false;
			// 
			// buttonCheck
			// 
			this.buttonCheck.Location = new System.Drawing.Point(510, 35);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(81, 23);
			this.buttonCheck.TabIndex = 27;
			this.buttonCheck.Text = "Check";
			this.buttonCheck.UseVisualStyleBackColor = true;
			this.buttonCheck.Click += new System.EventHandler(this.buttonSetApi_Click);
			// 
			// buttonAdvanced
			// 
			this.buttonAdvanced.Location = new System.Drawing.Point(29, 413);
			this.buttonAdvanced.Name = "buttonAdvanced";
			this.buttonAdvanced.Size = new System.Drawing.Size(150, 30);
			this.buttonAdvanced.TabIndex = 22;
			this.buttonAdvanced.Text = "1Adnanced";
			this.buttonAdvanced.UseVisualStyleBackColor = true;
			this.buttonAdvanced.Click += new System.EventHandler(this.buttonAdvanced_Click);
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(198, 413);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(101, 30);
			this.buttonHelp.TabIndex = 23;
			this.buttonHelp.Text = "Help";
			this.buttonHelp.UseVisualStyleBackColor = true;
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 430);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(10, 13);
			this.label1.TabIndex = 24;
			this.label1.Text = ".";
			// 
			// groupBoxMTConnectWL
			// 
			this.groupBoxMTConnectWL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxMTConnectWL.Controls.Add(this.labelSBKey);
			this.groupBoxMTConnectWL.Controls.Add(this.labelWL);
			this.groupBoxMTConnectWL.Controls.Add(this.buttonWL);
			this.groupBoxMTConnectWL.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupBoxMTConnectWL.Location = new System.Drawing.Point(12, 79);
			this.groupBoxMTConnectWL.Name = "groupBoxMTConnectWL";
			this.groupBoxMTConnectWL.Size = new System.Drawing.Size(623, 85);
			this.groupBoxMTConnectWL.TabIndex = 25;
			this.groupBoxMTConnectWL.TabStop = false;
			this.groupBoxMTConnectWL.Text = "Connect to Intento MT HUB";
			// 
			// labelWL
			// 
			this.labelWL.AutoSize = true;
			this.labelWL.Location = new System.Drawing.Point(223, 40);
			this.labelWL.Name = "labelWL";
			this.labelWL.Size = new System.Drawing.Size(133, 13);
			this.labelWL.TabIndex = 19;
			this.labelWL.Text = "You are currently logged in";
			this.labelWL.Visible = false;
			// 
			// buttonWL
			// 
			this.buttonWL.Location = new System.Drawing.Point(17, 31);
			this.buttonWL.Name = "buttonWL";
			this.buttonWL.Size = new System.Drawing.Size(170, 30);
			this.buttonWL.TabIndex = 0;
			this.buttonWL.Text = "Login to Intento ";
			this.buttonWL.UseVisualStyleBackColor = true;
			this.buttonWL.Click += new System.EventHandler(this.buttonWL_Click);
			// 
			// labelSBKey
			// 
			this.labelSBKey.AutoSize = true;
			this.labelSBKey.Location = new System.Drawing.Point(490, 60);
			this.labelSBKey.Name = "labelSBKey";
			this.labelSBKey.Size = new System.Drawing.Size(116, 13);
			this.labelSBKey.TabIndex = 20;
			this.labelSBKey.Text = "Received sandbox key";
			this.labelSBKey.Visible = false;
			// 
			// IntentoTranslationProviderOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(647, 456);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBoxMTConnectWL);
			this.Controls.Add(this.groupBoxMTConnect2);
			this.Controls.Add(this.buttonHelp);
			this.Controls.Add(this.buttonAdvanced);
			this.Controls.Add(this.buttonContinue);
			this.Controls.Add(this.groupBoxMTSettings);
			this.Controls.Add(this.groupBoxMTConnect);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "IntentoTranslationProviderOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.groupBoxMTConnect.ResumeLayout(false);
			this.groupBoxMTConnect.PerformLayout();
			this.groupBoxMTSettings.ResumeLayout(false);
			this.groupBoxMTSettings.PerformLayout();
			this.groupBoxGlossary.ResumeLayout(false);
			this.groupBoxGlossary.PerformLayout();
			this.groupBoxModel.ResumeLayout(false);
			this.groupBoxModel.PerformLayout();
			this.groupBoxBillingAccount.ResumeLayout(false);
			this.groupBoxBillingAccount.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBoxMTConnect2.ResumeLayout(false);
			this.groupBoxMTConnect2.PerformLayout();
			this.groupBoxMTConnectWL.ResumeLayout(false);
			this.groupBoxMTConnectWL.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxMTSettings;
        private System.Windows.Forms.Button buttonSetApi;
        private System.Windows.Forms.Label labelRegister2;
        private System.Windows.Forms.TextBox textBoxLabel1;
        private System.Windows.Forms.Label labelRegister1;
        private System.Windows.Forms.GroupBox groupBoxGlossary;
        private System.Windows.Forms.GroupBox groupBoxModel;
        private System.Windows.Forms.GroupBox groupBoxBillingAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelIAK;
        public System.Windows.Forms.TextBox apiKey_tb;
        public System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonAdvanced;
        private System.Windows.Forms.Label labelApiKeyIsChanged;
        private System.Windows.Forms.Button buttonHelp;
        public System.Windows.Forms.Button buttonContinue;
        public System.Windows.Forms.GroupBox groupBoxMTConnect;
        public System.Windows.Forms.GroupBox groupBoxMTConnect2;
        public System.Windows.Forms.Button buttonMTSetting;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxGlossary;
        public System.Windows.Forms.TextBox textBoxModel;
        public System.Windows.Forms.TextBox textBoxAccount;
        public System.Windows.Forms.TextBox textBoxProviderName;
		public System.Windows.Forms.GroupBox groupBoxMTConnectWL;
		private System.Windows.Forms.Label labelWL;
		public System.Windows.Forms.Button buttonWL;
		private System.Windows.Forms.Label labelSBKey;
	}
}