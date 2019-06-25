namespace IntentoMT.Plugin.PropertiesForm
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
            this.components = new System.ComponentModel.Container();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.apiKey_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.groupBoxProviderSettings = new System.Windows.Forms.GroupBox();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.buttonWizard = new System.Windows.Forms.Button();
            this.textBoxCredentials = new System.Windows.Forms.TextBox();
            this.groupBoxAuthCredentialId = new System.Windows.Forms.GroupBox();
            this.comboBoxCredentialId = new System.Windows.Forms.ComboBox();
            this.labelStoredCredential1 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxUseCustomModel = new System.Windows.Forms.CheckBox();
            this.groupBoxGlossary = new System.Windows.Forms.GroupBox();
            this.textBoxGlossary = new System.Windows.Forms.TextBox();
            this.comboBoxGlossaries = new System.Windows.Forms.ComboBox();
            this.groupBoxModel = new System.Windows.Forms.GroupBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.comboBoxModels = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProviders = new System.Windows.Forms.ComboBox();
            this.checkBoxUseOwnCred = new System.Windows.Forms.CheckBox();
            this.toolTipFormat = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxTrace = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxShowHidden = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxSmartRouting = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveApiKeyInRegistry = new System.Windows.Forms.CheckBox();
            this.textBoxLabel1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBoxProviderSettings.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.groupBoxAuthCredentialId.SuspendLayout();
            this.groupBoxGlossary.SuspendLayout();
            this.groupBoxModel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCheck
            // 
            this.buttonCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCheck.Location = new System.Drawing.Point(446, 54);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(81, 23);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // apiKey_tb
            // 
            this.apiKey_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apiKey_tb.Location = new System.Drawing.Point(55, 56);
            this.apiKey_tb.Name = "apiKey_tb";
            this.apiKey_tb.Size = new System.Drawing.Size(369, 20);
            this.apiKey_tb.TabIndex = 1;
            this.apiKey_tb.UseSystemPasswordChar = true;
            this.apiKey_tb.WordWrap = false;
            this.apiKey_tb.TextChanged += new System.EventHandler(this.apiKey_tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "API Key";
            // 
            // buttonContinue
            // 
            this.buttonContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonContinue.Enabled = false;
            this.buttonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinue.Location = new System.Drawing.Point(7, 495);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 14;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // groupBoxProviderSettings
            // 
            this.groupBoxProviderSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProviderSettings.Controls.Add(this.groupBoxAuth);
            this.groupBoxProviderSettings.Controls.Add(this.checkBoxUseCustomModel);
            this.groupBoxProviderSettings.Controls.Add(this.groupBoxGlossary);
            this.groupBoxProviderSettings.Controls.Add(this.groupBoxModel);
            this.groupBoxProviderSettings.Controls.Add(this.label1);
            this.groupBoxProviderSettings.Controls.Add(this.comboBoxProviders);
            this.groupBoxProviderSettings.Controls.Add(this.checkBoxUseOwnCred);
            this.groupBoxProviderSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxProviderSettings.Location = new System.Drawing.Point(7, 118);
            this.groupBoxProviderSettings.Name = "groupBoxProviderSettings";
            this.groupBoxProviderSettings.Size = new System.Drawing.Size(531, 356);
            this.groupBoxProviderSettings.TabIndex = 5;
            this.groupBoxProviderSettings.TabStop = false;
            this.groupBoxProviderSettings.Text = "MT Provider settings";
            this.groupBoxProviderSettings.Visible = false;
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.buttonWizard);
            this.groupBoxAuth.Controls.Add(this.groupBoxAuthCredentialId);
            this.groupBoxAuth.Controls.Add(this.textBoxCredentials);
            this.groupBoxAuth.Enabled = false;
            this.groupBoxAuth.Location = new System.Drawing.Point(10, 84);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Size = new System.Drawing.Size(511, 89);
            this.groupBoxAuth.TabIndex = 7;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Authorization parameters";
            // 
            // buttonWizard
            // 
            this.buttonWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWizard.Location = new System.Drawing.Point(414, 31);
            this.buttonWizard.Name = "buttonWizard";
            this.buttonWizard.Size = new System.Drawing.Size(81, 23);
            this.buttonWizard.TabIndex = 8;
            this.buttonWizard.Text = "Fill or edit";
            this.buttonWizard.UseVisualStyleBackColor = true;
            this.buttonWizard.Click += new System.EventHandler(this.buttonWizard_Click);
            // 
            // textBoxCredentials
            // 
            this.textBoxCredentials.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCredentials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCredentials.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxCredentials.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxCredentials.Location = new System.Drawing.Point(21, 31);
            this.textBoxCredentials.Name = "textBoxCredentials";
            this.textBoxCredentials.ReadOnly = true;
            this.textBoxCredentials.Size = new System.Drawing.Size(387, 20);
            this.textBoxCredentials.TabIndex = 7;
            this.textBoxCredentials.UseSystemPasswordChar = true;
            this.textBoxCredentials.Click += new System.EventHandler(this.textBoxCredentials_Enter);
            this.textBoxCredentials.Enter += new System.EventHandler(this.textBoxCredentials_Enter);
            // 
            // groupBoxAuthCredentialId
            // 
            this.groupBoxAuthCredentialId.Controls.Add(this.comboBoxCredentialId);
            this.groupBoxAuthCredentialId.Controls.Add(this.labelStoredCredential1);
            this.groupBoxAuthCredentialId.Controls.Add(this.linkLabel3);
            this.groupBoxAuthCredentialId.Controls.Add(this.label8);
            this.groupBoxAuthCredentialId.Location = new System.Drawing.Point(79, 42);
            this.groupBoxAuthCredentialId.Name = "groupBoxAuthCredentialId";
            this.groupBoxAuthCredentialId.Size = new System.Drawing.Size(512, 86);
            this.groupBoxAuthCredentialId.TabIndex = 9;
            this.groupBoxAuthCredentialId.TabStop = false;
            this.groupBoxAuthCredentialId.Text = "Stored Credentials";
            // 
            // comboBoxCredentialId
            // 
            this.comboBoxCredentialId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCredentialId.Enabled = false;
            this.comboBoxCredentialId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCredentialId.FormattingEnabled = true;
            this.comboBoxCredentialId.Location = new System.Drawing.Point(42, 21);
            this.comboBoxCredentialId.Name = "comboBoxCredentialId";
            this.comboBoxCredentialId.Size = new System.Drawing.Size(387, 21);
            this.comboBoxCredentialId.TabIndex = 9;
            this.comboBoxCredentialId.SelectedIndexChanged += new System.EventHandler(this.comboBoxCredentialId_SelectedIndexChanged);
            // 
            // labelStoredCredential1
            // 
            this.labelStoredCredential1.AutoSize = true;
            this.labelStoredCredential1.Location = new System.Drawing.Point(40, 59);
            this.labelStoredCredential1.Name = "labelStoredCredential1";
            this.labelStoredCredential1.Size = new System.Drawing.Size(216, 13);
            this.labelStoredCredential1.TabIndex = 33;
            this.labelStoredCredential1.Text = "More information on Stored Credentials here:";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(255, 59);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(174, 13);
            this.linkLabel3.TabIndex = 32;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "https://console.inten.to/credentials";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 27;
            // 
            // checkBoxUseCustomModel
            // 
            this.checkBoxUseCustomModel.AutoSize = true;
            this.checkBoxUseCustomModel.Enabled = false;
            this.checkBoxUseCustomModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUseCustomModel.Location = new System.Drawing.Point(15, 183);
            this.checkBoxUseCustomModel.Name = "checkBoxUseCustomModel";
            this.checkBoxUseCustomModel.Size = new System.Drawing.Size(133, 17);
            this.checkBoxUseCustomModel.TabIndex = 10;
            this.checkBoxUseCustomModel.Text = "Use own custom model";
            this.checkBoxUseCustomModel.UseVisualStyleBackColor = true;
            this.checkBoxUseCustomModel.Visible = false;
            this.checkBoxUseCustomModel.CheckedChanged += new System.EventHandler(this.checkBoxUseCustomModel_CheckedChanged);
            // 
            // groupBoxGlossary
            // 
            this.groupBoxGlossary.Controls.Add(this.textBoxGlossary);
            this.groupBoxGlossary.Controls.Add(this.comboBoxGlossaries);
            this.groupBoxGlossary.Location = new System.Drawing.Point(9, 280);
            this.groupBoxGlossary.Name = "groupBoxGlossary";
            this.groupBoxGlossary.Size = new System.Drawing.Size(511, 59);
            this.groupBoxGlossary.TabIndex = 13;
            this.groupBoxGlossary.TabStop = false;
            this.groupBoxGlossary.Text = "Glossary";
            this.groupBoxGlossary.Visible = false;
            // 
            // textBoxGlossary
            // 
            this.textBoxGlossary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGlossary.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxGlossary.Location = new System.Drawing.Point(11, 19);
            this.textBoxGlossary.Name = "textBoxGlossary";
            this.textBoxGlossary.Size = new System.Drawing.Size(485, 20);
            this.textBoxGlossary.TabIndex = 13;
            // 
            // comboBoxGlossaries
            // 
            this.comboBoxGlossaries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGlossaries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxGlossaries.FormattingEnabled = true;
            this.comboBoxGlossaries.Location = new System.Drawing.Point(11, 19);
            this.comboBoxGlossaries.Name = "comboBoxGlossaries";
            this.comboBoxGlossaries.Size = new System.Drawing.Size(485, 21);
            this.comboBoxGlossaries.TabIndex = 37;
            // 
            // groupBoxModel
            // 
            this.groupBoxModel.Controls.Add(this.textBoxModel);
            this.groupBoxModel.Controls.Add(this.comboBoxModels);
            this.groupBoxModel.Location = new System.Drawing.Point(9, 208);
            this.groupBoxModel.Name = "groupBoxModel";
            this.groupBoxModel.Size = new System.Drawing.Size(512, 66);
            this.groupBoxModel.TabIndex = 11;
            this.groupBoxModel.TabStop = false;
            this.groupBoxModel.Text = "Model";
            this.groupBoxModel.Visible = false;
            // 
            // textBoxModel
            // 
            this.textBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxModel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxModel.Location = new System.Drawing.Point(11, 30);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(485, 20);
            this.textBoxModel.TabIndex = 12;
            this.textBoxModel.TextChanged += new System.EventHandler(this.textBoxModel_TextChanged);
            // 
            // comboBoxModels
            // 
            this.comboBoxModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxModels.FormattingEnabled = true;
            this.comboBoxModels.Location = new System.Drawing.Point(11, 19);
            this.comboBoxModels.Name = "comboBoxModels";
            this.comboBoxModels.Size = new System.Drawing.Size(485, 21);
            this.comboBoxModels.TabIndex = 11;
            this.comboBoxModels.Visible = false;
            this.comboBoxModels.SelectedIndexChanged += new System.EventHandler(this.comboBoxModels_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Provider";
            // 
            // comboBoxProviders
            // 
            this.comboBoxProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProviders.Enabled = false;
            this.comboBoxProviders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxProviders.FormattingEnabled = true;
            this.comboBoxProviders.Location = new System.Drawing.Point(8, 33);
            this.comboBoxProviders.Name = "comboBoxProviders";
            this.comboBoxProviders.Size = new System.Drawing.Size(512, 21);
            this.comboBoxProviders.TabIndex = 5;
            this.comboBoxProviders.SelectedIndexChanged += new System.EventHandler(this.comboBoxProviders_SelectedIndexChanged);
            // 
            // checkBoxUseOwnCred
            // 
            this.checkBoxUseOwnCred.AutoSize = true;
            this.checkBoxUseOwnCred.Enabled = false;
            this.checkBoxUseOwnCred.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUseOwnCred.Location = new System.Drawing.Point(15, 61);
            this.checkBoxUseOwnCred.Name = "checkBoxUseOwnCred";
            this.checkBoxUseOwnCred.Size = new System.Drawing.Size(142, 17);
            this.checkBoxUseOwnCred.TabIndex = 6;
            this.checkBoxUseOwnCred.Text = "Use your own credentials";
            this.checkBoxUseOwnCred.UseVisualStyleBackColor = true;
            this.checkBoxUseOwnCred.CheckedChanged += new System.EventHandler(this.checkBoxUseOwnCred_CheckedChanged);
            // 
            // toolTipFormat
            // 
            this.toolTipFormat.ToolTipTitle = "Format";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "If you don\'t have an Intento API key, register here: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "More info: ";
            // 
            // checkBoxTrace
            // 
            this.checkBoxTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTrace.AutoSize = true;
            this.checkBoxTrace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTrace.Location = new System.Drawing.Point(383, 498);
            this.checkBoxTrace.Name = "checkBoxTrace";
            this.checkBoxTrace.Size = new System.Drawing.Size(155, 17);
            this.checkBoxTrace.TabIndex = 17;
            this.checkBoxTrace.Text = "Log payloads for 30 minutes";
            this.checkBoxTrace.UseVisualStyleBackColor = true;
            this.checkBoxTrace.CheckedChanged += new System.EventHandler(this.checkBoxTrace_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(546, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(476, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel2.Text = "x.x.x/20xx";
            // 
            // checkBoxShowHidden
            // 
            this.checkBoxShowHidden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowHidden.AutoSize = true;
            this.checkBoxShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowHidden.Location = new System.Drawing.Point(266, 498);
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.checkBoxShowHidden.Size = new System.Drawing.Size(105, 17);
            this.checkBoxShowHidden.TabIndex = 16;
            this.checkBoxShowHidden.Text = "Show hidden text";
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxSmartRouting
            // 
            this.checkBoxSmartRouting.AutoSize = true;
            this.checkBoxSmartRouting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxSmartRouting.Location = new System.Drawing.Point(55, 82);
            this.checkBoxSmartRouting.Name = "checkBoxSmartRouting";
            this.checkBoxSmartRouting.Size = new System.Drawing.Size(85, 17);
            this.checkBoxSmartRouting.TabIndex = 3;
            this.checkBoxSmartRouting.Text = "Smart routing";
            this.checkBoxSmartRouting.UseVisualStyleBackColor = true;
            this.checkBoxSmartRouting.CheckedChanged += new System.EventHandler(this.checkBoxSmartRouting_CheckedChanged);
            // 
            // checkBoxSaveApiKeyInRegistry
            // 
            this.checkBoxSaveApiKeyInRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSaveApiKeyInRegistry.AutoSize = true;
            this.checkBoxSaveApiKeyInRegistry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxSaveApiKeyInRegistry.Location = new System.Drawing.Point(125, 498);
            this.checkBoxSaveApiKeyInRegistry.Name = "checkBoxSaveApiKeyInRegistry";
            this.checkBoxSaveApiKeyInRegistry.Size = new System.Drawing.Size(121, 17);
            this.checkBoxSaveApiKeyInRegistry.TabIndex = 15;
            this.checkBoxSaveApiKeyInRegistry.Text = "Save API Key locally";
            this.checkBoxSaveApiKeyInRegistry.UseVisualStyleBackColor = true;
            this.checkBoxSaveApiKeyInRegistry.CheckedChanged += new System.EventHandler(this.checkBoxSaveApiKeyInRegistry_CheckedChanged);
            // 
            // textBoxLabel1
            // 
            this.textBoxLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLabel1.ForeColor = System.Drawing.Color.Blue;
            this.textBoxLabel1.Location = new System.Drawing.Point(261, 13);
            this.textBoxLabel1.Name = "textBoxLabel1";
            this.textBoxLabel1.Size = new System.Drawing.Size(118, 13);
            this.textBoxLabel1.TabIndex = 18;
            this.textBoxLabel1.Text = "https://console.inten.to";
            this.textBoxLabel1.Enter += new System.EventHandler(this.textBoxLabel_Enter);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(64, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 13);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "https://inten.to";
            this.textBox1.Enter += new System.EventHandler(this.textBoxLabel_Enter);
            // 
            // IntentoTranslationProviderOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(546, 548);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBoxLabel1);
            this.Controls.Add(this.checkBoxSaveApiKeyInRegistry);
            this.Controls.Add(this.checkBoxSmartRouting);
            this.Controls.Add(this.checkBoxShowHidden);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.checkBoxTrace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxProviderSettings);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.apiKey_tb);
            this.Controls.Add(this.buttonCheck);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoTranslationProviderOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Intento plugin settings";
            this.Shown += new System.EventHandler(this.IntentoTranslationProviderOptionsForm_Shown);
            this.groupBoxProviderSettings.ResumeLayout(false);
            this.groupBoxProviderSettings.PerformLayout();
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.groupBoxAuthCredentialId.ResumeLayout(false);
            this.groupBoxAuthCredentialId.PerformLayout();
            this.groupBoxGlossary.ResumeLayout(false);
            this.groupBoxGlossary.PerformLayout();
            this.groupBoxModel.ResumeLayout(false);
            this.groupBoxModel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.TextBox apiKey_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.GroupBox groupBoxProviderSettings;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBoxProviders;
        public System.Windows.Forms.CheckBox checkBoxUseOwnCred;
        private System.Windows.Forms.ToolTip toolTipFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxTrace;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public System.Windows.Forms.CheckBox checkBoxShowHidden;
        public System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.Button buttonWizard;
        private System.Windows.Forms.TextBox textBoxCredentials;
        public System.Windows.Forms.CheckBox checkBoxUseCustomModel;
        public System.Windows.Forms.GroupBox groupBoxModel;
        public System.Windows.Forms.ComboBox comboBoxModels;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.CheckBox checkBoxSmartRouting;
        public System.Windows.Forms.TextBox textBoxModel;
        public System.Windows.Forms.GroupBox groupBoxAuthCredentialId;
        private System.Windows.Forms.ComboBox comboBoxCredentialId;
        private System.Windows.Forms.Label labelStoredCredential1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBoxGlossary;
        private System.Windows.Forms.TextBox textBoxGlossary;
        private System.Windows.Forms.ComboBox comboBoxGlossaries;
        public System.Windows.Forms.CheckBox checkBoxSaveApiKeyInRegistry;
        private System.Windows.Forms.TextBox textBoxLabel1;
        private System.Windows.Forms.TextBox textBox1;
    }
}