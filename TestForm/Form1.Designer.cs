namespace TestForm
{
    partial class Form1
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
			this.buttonShow = new System.Windows.Forms.Button();
			this.textBoxApiKey = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxProviderId = new System.Windows.Forms.TextBox();
			this.textBoxModel = new System.Windows.Forms.TextBox();
			this.textBoxAuth = new System.Windows.Forms.TextBox();
			this.checkBoxAuth = new System.Windows.Forms.CheckBox();
			this.checkBoxModel = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxGlossary = new System.Windows.Forms.TextBox();
			this.checkBoxSmartRouting = new System.Windows.Forms.CheckBox();
			this.buttonSaveData = new System.Windows.Forms.Button();
			this.comboBoxTestName = new System.Windows.Forms.ComboBox();
			this.textBoxFormat = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonTranslatePlain = new System.Windows.Forms.Button();
			this.checkBoxStage = new System.Windows.Forms.CheckBox();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.textBoxText = new System.Windows.Forms.TextBox();
			this.checkBoxFormatted = new System.Windows.Forms.CheckBox();
			this.textBoxResult = new System.Windows.Forms.TextBox();
			this.textBoxExpected = new System.Windows.Forms.TextBox();
			this.buttonTestReadData = new System.Windows.Forms.Button();
			this.checkBoxForbidSaveApikey = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxProxy = new System.Windows.Forms.CheckBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkBoxHideHiddenTextButton = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.checkBoxCustomTagParser = new System.Windows.Forms.CheckBox();
			this.checkBoxTradosApp = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.comboBoxIEVersion = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonShow
			// 
			this.buttonShow.Location = new System.Drawing.Point(32, 447);
			this.buttonShow.Name = "buttonShow";
			this.buttonShow.Size = new System.Drawing.Size(102, 41);
			this.buttonShow.TabIndex = 0;
			this.buttonShow.Text = "Open Form";
			this.buttonShow.UseVisualStyleBackColor = true;
			this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
			// 
			// textBoxApiKey
			// 
			this.textBoxApiKey.Location = new System.Drawing.Point(103, 18);
			this.textBoxApiKey.Name = "textBoxApiKey";
			this.textBoxApiKey.Size = new System.Drawing.Size(305, 20);
			this.textBoxApiKey.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(45, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "ApiKey";
			// 
			// textBoxProviderId
			// 
			this.textBoxProviderId.Location = new System.Drawing.Point(103, 66);
			this.textBoxProviderId.Name = "textBoxProviderId";
			this.textBoxProviderId.Size = new System.Drawing.Size(305, 20);
			this.textBoxProviderId.TabIndex = 3;
			// 
			// textBoxModel
			// 
			this.textBoxModel.Location = new System.Drawing.Point(103, 192);
			this.textBoxModel.Name = "textBoxModel";
			this.textBoxModel.Size = new System.Drawing.Size(305, 20);
			this.textBoxModel.TabIndex = 4;
			// 
			// textBoxAuth
			// 
			this.textBoxAuth.Location = new System.Drawing.Point(103, 142);
			this.textBoxAuth.Name = "textBoxAuth";
			this.textBoxAuth.Size = new System.Drawing.Size(305, 20);
			this.textBoxAuth.TabIndex = 5;
			// 
			// checkBoxAuth
			// 
			this.checkBoxAuth.AutoSize = true;
			this.checkBoxAuth.Location = new System.Drawing.Point(103, 119);
			this.checkBoxAuth.Name = "checkBoxAuth";
			this.checkBoxAuth.Size = new System.Drawing.Size(48, 17);
			this.checkBoxAuth.TabIndex = 7;
			this.checkBoxAuth.Text = "Auth";
			this.checkBoxAuth.UseVisualStyleBackColor = true;
			// 
			// checkBoxModel
			// 
			this.checkBoxModel.AutoSize = true;
			this.checkBoxModel.Location = new System.Drawing.Point(103, 169);
			this.checkBoxModel.Name = "checkBoxModel";
			this.checkBoxModel.Size = new System.Drawing.Size(55, 17);
			this.checkBoxModel.TabIndex = 8;
			this.checkBoxModel.Text = "Model";
			this.checkBoxModel.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(45, 66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Provider Id";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(48, 148);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Auth";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(48, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Model";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(48, 245);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Clossary";
			// 
			// textBoxGlossary
			// 
			this.textBoxGlossary.Location = new System.Drawing.Point(103, 245);
			this.textBoxGlossary.Name = "textBoxGlossary";
			this.textBoxGlossary.Size = new System.Drawing.Size(305, 20);
			this.textBoxGlossary.TabIndex = 14;
			// 
			// checkBoxSmartRouting
			// 
			this.checkBoxSmartRouting.AutoSize = true;
			this.checkBoxSmartRouting.Location = new System.Drawing.Point(103, 43);
			this.checkBoxSmartRouting.Name = "checkBoxSmartRouting";
			this.checkBoxSmartRouting.Size = new System.Drawing.Size(88, 17);
			this.checkBoxSmartRouting.TabIndex = 16;
			this.checkBoxSmartRouting.Text = "Smart routing";
			this.checkBoxSmartRouting.UseVisualStyleBackColor = true;
			// 
			// buttonSaveData
			// 
			this.buttonSaveData.Location = new System.Drawing.Point(235, 495);
			this.buttonSaveData.Name = "buttonSaveData";
			this.buttonSaveData.Size = new System.Drawing.Size(75, 23);
			this.buttonSaveData.TabIndex = 17;
			this.buttonSaveData.Text = "Save Data";
			this.buttonSaveData.UseVisualStyleBackColor = true;
			this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
			// 
			// comboBoxTestName
			// 
			this.comboBoxTestName.FormattingEnabled = true;
			this.comboBoxTestName.Location = new System.Drawing.Point(32, 497);
			this.comboBoxTestName.Name = "comboBoxTestName";
			this.comboBoxTestName.Size = new System.Drawing.Size(192, 21);
			this.comboBoxTestName.TabIndex = 18;
			this.comboBoxTestName.SelectedIndexChanged += new System.EventHandler(this.comboBoxTestName_SelectedIndexChanged);
			// 
			// textBoxFormat
			// 
			this.textBoxFormat.Location = new System.Drawing.Point(103, 92);
			this.textBoxFormat.Name = "textBoxFormat";
			this.textBoxFormat.Size = new System.Drawing.Size(305, 20);
			this.textBoxFormat.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(45, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Format";
			// 
			// buttonTranslatePlain
			// 
			this.buttonTranslatePlain.Location = new System.Drawing.Point(111, 524);
			this.buttonTranslatePlain.Name = "buttonTranslatePlain";
			this.buttonTranslatePlain.Size = new System.Drawing.Size(126, 30);
			this.buttonTranslatePlain.TabIndex = 20;
			this.buttonTranslatePlain.Text = "Translate";
			this.buttonTranslatePlain.UseVisualStyleBackColor = true;
			this.buttonTranslatePlain.Click += new System.EventHandler(this.buttonTranslatePlain_Click);
			// 
			// checkBoxStage
			// 
			this.checkBoxStage.AutoSize = true;
			this.checkBoxStage.Location = new System.Drawing.Point(256, 537);
			this.checkBoxStage.Name = "checkBoxStage";
			this.checkBoxStage.Size = new System.Drawing.Size(54, 17);
			this.checkBoxStage.TabIndex = 22;
			this.checkBoxStage.Text = "Stage";
			this.checkBoxStage.UseVisualStyleBackColor = true;
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.Location = new System.Drawing.Point(142, 218);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(34, 20);
			this.textBoxFrom.TabIndex = 23;
			// 
			// textBoxTo
			// 
			this.textBoxTo.Location = new System.Drawing.Point(244, 218);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(34, 20);
			this.textBoxTo.TabIndex = 24;
			// 
			// textBoxText
			// 
			this.textBoxText.Location = new System.Drawing.Point(32, 560);
			this.textBoxText.Name = "textBoxText";
			this.textBoxText.Size = new System.Drawing.Size(257, 20);
			this.textBoxText.TabIndex = 25;
			// 
			// checkBoxFormatted
			// 
			this.checkBoxFormatted.AutoSize = true;
			this.checkBoxFormatted.Location = new System.Drawing.Point(32, 537);
			this.checkBoxFormatted.Name = "checkBoxFormatted";
			this.checkBoxFormatted.Size = new System.Drawing.Size(73, 17);
			this.checkBoxFormatted.TabIndex = 26;
			this.checkBoxFormatted.Text = "Formatted";
			this.checkBoxFormatted.UseVisualStyleBackColor = true;
			// 
			// textBoxResult
			// 
			this.textBoxResult.Enabled = false;
			this.textBoxResult.Location = new System.Drawing.Point(32, 588);
			this.textBoxResult.Name = "textBoxResult";
			this.textBoxResult.Size = new System.Drawing.Size(257, 20);
			this.textBoxResult.TabIndex = 27;
			this.textBoxResult.TextChanged += new System.EventHandler(this.textBoxResult_TextChanged);
			// 
			// textBoxExpected
			// 
			this.textBoxExpected.Location = new System.Drawing.Point(32, 614);
			this.textBoxExpected.Name = "textBoxExpected";
			this.textBoxExpected.Size = new System.Drawing.Size(257, 20);
			this.textBoxExpected.TabIndex = 28;
			// 
			// buttonTestReadData
			// 
			this.buttonTestReadData.Location = new System.Drawing.Point(316, 495);
			this.buttonTestReadData.Name = "buttonTestReadData";
			this.buttonTestReadData.Size = new System.Drawing.Size(75, 23);
			this.buttonTestReadData.TabIndex = 19;
			this.buttonTestReadData.Text = "Read Data";
			this.buttonTestReadData.UseVisualStyleBackColor = true;
			this.buttonTestReadData.Visible = false;
			this.buttonTestReadData.Click += new System.EventHandler(this.buttonTestReadData_Click);
			// 
			// checkBoxForbidSaveApikey
			// 
			this.checkBoxForbidSaveApikey.AutoSize = true;
			this.checkBoxForbidSaveApikey.Checked = true;
			this.checkBoxForbidSaveApikey.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxForbidSaveApikey.Location = new System.Drawing.Point(48, 271);
			this.checkBoxForbidSaveApikey.Name = "checkBoxForbidSaveApikey";
			this.checkBoxForbidSaveApikey.Size = new System.Drawing.Size(112, 17);
			this.checkBoxForbidSaveApikey.TabIndex = 29;
			this.checkBoxForbidSaveApikey.Text = "ForbidSaveApikey";
			this.checkBoxForbidSaveApikey.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBoxProxy);
			this.groupBox1.Controls.Add(this.textBoxPassword);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textBoxUserName);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textBoxPort);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textBoxAddress);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(48, 295);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 146);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Proxy";
			// 
			// checkBoxProxy
			// 
			this.checkBoxProxy.AutoSize = true;
			this.checkBoxProxy.Enabled = false;
			this.checkBoxProxy.Location = new System.Drawing.Point(253, 19);
			this.checkBoxProxy.Name = "checkBoxProxy";
			this.checkBoxProxy.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.checkBoxProxy.Size = new System.Drawing.Size(94, 17);
			this.checkBoxProxy.TabIndex = 32;
			this.checkBoxProxy.Text = "Proxy Enabled";
			this.checkBoxProxy.UseVisualStyleBackColor = true;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPassword.Location = new System.Drawing.Point(94, 103);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(239, 20);
			this.textBoxPassword.TabIndex = 13;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(27, 106);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Password:";
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxUserName.Location = new System.Drawing.Point(94, 71);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(239, 20);
			this.textBoxUserName.TabIndex = 11;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(27, 74);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(61, 13);
			this.label8.TabIndex = 10;
			this.label8.Text = "User name:";
			// 
			// textBoxPort
			// 
			this.textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPort.Location = new System.Drawing.Point(286, 42);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(61, 20);
			this.textBoxPort.TabIndex = 9;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(256, 45);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(29, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "Port:";
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxAddress.Location = new System.Drawing.Point(141, 42);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(100, 20);
			this.textBoxAddress.TabIndex = 7;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(14, 45);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(124, 13);
			this.label10.TabIndex = 6;
			this.label10.Text = "IP address or host name:";
			// 
			// checkBoxHideHiddenTextButton
			// 
			this.checkBoxHideHiddenTextButton.AutoSize = true;
			this.checkBoxHideHiddenTextButton.Location = new System.Drawing.Point(155, 271);
			this.checkBoxHideHiddenTextButton.Name = "checkBoxHideHiddenTextButton";
			this.checkBoxHideHiddenTextButton.Size = new System.Drawing.Size(134, 17);
			this.checkBoxHideHiddenTextButton.TabIndex = 31;
			this.checkBoxHideHiddenTextButton.Text = "HideHiddenTextButton";
			this.checkBoxHideHiddenTextButton.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(109, 221);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(27, 13);
			this.label11.TabIndex = 34;
			this.label11.Text = "from";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(222, 221);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(16, 13);
			this.label12.TabIndex = 36;
			this.label12.Text = "to";
			// 
			// checkBoxCustomTagParser
			// 
			this.checkBoxCustomTagParser.AutoSize = true;
			this.checkBoxCustomTagParser.Location = new System.Drawing.Point(289, 271);
			this.checkBoxCustomTagParser.Name = "checkBoxCustomTagParser";
			this.checkBoxCustomTagParser.Size = new System.Drawing.Size(111, 17);
			this.checkBoxCustomTagParser.TabIndex = 37;
			this.checkBoxCustomTagParser.Text = "Custom tag parser";
			this.checkBoxCustomTagParser.UseVisualStyleBackColor = true;
			// 
			// checkBoxTradosApp
			// 
			this.checkBoxTradosApp.AutoSize = true;
			this.checkBoxTradosApp.Location = new System.Drawing.Point(155, 447);
			this.checkBoxTradosApp.Name = "checkBoxTradosApp";
			this.checkBoxTradosApp.Size = new System.Drawing.Size(224, 17);
			this.checkBoxTradosApp.TabIndex = 38;
			this.checkBoxTradosApp.Text = "Set AppName to \"SdlTradosStudioPlugin\"";
			this.checkBoxTradosApp.UseVisualStyleBackColor = true;
			this.checkBoxTradosApp.CheckedChanged += new System.EventHandler(this.checkBoxTradosApp_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(32, 648);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(101, 30);
			this.button1.TabIndex = 39;
			this.button1.Text = "Web Login";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label13
			// 
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.Location = new System.Drawing.Point(172, 662);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(103, 13);
			this.label13.TabIndex = 41;
			this.label13.Text = "Emulation IE version";
			// 
			// comboBoxIEVersion
			// 
			this.comboBoxIEVersion.FormattingEnabled = true;
			this.comboBoxIEVersion.Location = new System.Drawing.Point(281, 654);
			this.comboBoxIEVersion.Name = "comboBoxIEVersion";
			this.comboBoxIEVersion.Size = new System.Drawing.Size(121, 21);
			this.comboBoxIEVersion.TabIndex = 42;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 690);
			this.Controls.Add(this.comboBoxIEVersion);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.checkBoxTradosApp);
			this.Controls.Add(this.checkBoxCustomTagParser);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.checkBoxHideHiddenTextButton);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBoxForbidSaveApikey);
			this.Controls.Add(this.buttonSaveData);
			this.Controls.Add(this.textBoxExpected);
			this.Controls.Add(this.textBoxResult);
			this.Controls.Add(this.checkBoxFormatted);
			this.Controls.Add(this.textBoxText);
			this.Controls.Add(this.textBoxTo);
			this.Controls.Add(this.textBoxFrom);
			this.Controls.Add(this.checkBoxStage);
			this.Controls.Add(this.buttonTranslatePlain);
			this.Controls.Add(this.buttonTestReadData);
			this.Controls.Add(this.comboBoxTestName);
			this.Controls.Add(this.checkBoxSmartRouting);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBoxGlossary);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBoxModel);
			this.Controls.Add(this.checkBoxAuth);
			this.Controls.Add(this.textBoxFormat);
			this.Controls.Add(this.textBoxAuth);
			this.Controls.Add(this.textBoxModel);
			this.Controls.Add(this.textBoxProviderId);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxApiKey);
			this.Controls.Add(this.buttonShow);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProviderId;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.TextBox textBoxAuth;
        private System.Windows.Forms.CheckBox checkBoxAuth;
        private System.Windows.Forms.CheckBox checkBoxModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxGlossary;
        private System.Windows.Forms.CheckBox checkBoxSmartRouting;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.ComboBox comboBoxTestName;
        private System.Windows.Forms.TextBox textBoxFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTranslatePlain;
        private System.Windows.Forms.CheckBox checkBoxStage;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.CheckBox checkBoxFormatted;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.TextBox textBoxExpected;
        private System.Windows.Forms.Button buttonTestReadData;
        private System.Windows.Forms.CheckBox checkBoxForbidSaveApikey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxProxy;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxHideHiddenTextButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox checkBoxCustomTagParser;
		private System.Windows.Forms.CheckBox checkBoxTradosApp;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ComboBox comboBoxIEVersion;
	}
}

