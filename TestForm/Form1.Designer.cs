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
            this.buttonTestReadData = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(103, 258);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(80, 30);
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
            this.label6.Location = new System.Drawing.Point(48, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Clossary";
            // 
            // textBoxGlossary
            // 
            this.textBoxGlossary.Location = new System.Drawing.Point(103, 218);
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
            this.buttonSaveData.Location = new System.Drawing.Point(245, 310);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(75, 30);
            this.buttonSaveData.TabIndex = 17;
            this.buttonSaveData.Text = "Save Data";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // comboBoxTestName
            // 
            this.comboBoxTestName.FormattingEnabled = true;
            this.comboBoxTestName.Location = new System.Drawing.Point(32, 310);
            this.comboBoxTestName.Name = "comboBoxTestName";
            this.comboBoxTestName.Size = new System.Drawing.Size(192, 21);
            this.comboBoxTestName.TabIndex = 18;
            this.comboBoxTestName.SelectedIndexChanged += new System.EventHandler(this.comboBoxTestName_SelectedIndexChanged);
            // 
            // buttonTestReadData
            // 
            this.buttonTestReadData.Location = new System.Drawing.Point(333, 310);
            this.buttonTestReadData.Name = "buttonTestReadData";
            this.buttonTestReadData.Size = new System.Drawing.Size(75, 30);
            this.buttonTestReadData.TabIndex = 19;
            this.buttonTestReadData.Text = "Read Data";
            this.buttonTestReadData.UseVisualStyleBackColor = true;
            this.buttonTestReadData.Click += new System.EventHandler(this.buttonTestReadData_Click);
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
            this.buttonTranslatePlain.Location = new System.Drawing.Point(222, 355);
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
            this.checkBoxStage.Location = new System.Drawing.Point(354, 354);
            this.checkBoxStage.Name = "checkBoxStage";
            this.checkBoxStage.Size = new System.Drawing.Size(54, 17);
            this.checkBoxStage.TabIndex = 22;
            this.checkBoxStage.Text = "Stage";
            this.checkBoxStage.UseVisualStyleBackColor = true;
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(32, 355);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(34, 20);
            this.textBoxFrom.TabIndex = 23;
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(72, 355);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(34, 20);
            this.textBoxTo.TabIndex = 24;
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(32, 391);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(376, 20);
            this.textBoxText.TabIndex = 25;
            // 
            // checkBoxFormatted
            // 
            this.checkBoxFormatted.AutoSize = true;
            this.checkBoxFormatted.Location = new System.Drawing.Point(112, 355);
            this.checkBoxFormatted.Name = "checkBoxFormatted";
            this.checkBoxFormatted.Size = new System.Drawing.Size(73, 17);
            this.checkBoxFormatted.TabIndex = 26;
            this.checkBoxFormatted.Text = "Formatted";
            this.checkBoxFormatted.UseVisualStyleBackColor = true;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Enabled = false;
            this.textBoxResult.Location = new System.Drawing.Point(32, 443);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(376, 20);
            this.textBoxResult.TabIndex = 27;
            this.textBoxResult.TextChanged += new System.EventHandler(this.textBoxResult_TextChanged);
            // 
            // textBoxExpected
            // 
            this.textBoxExpected.Location = new System.Drawing.Point(32, 469);
            this.textBoxExpected.Name = "textBoxExpected";
            this.textBoxExpected.Size = new System.Drawing.Size(376, 20);
            this.textBoxExpected.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 519);
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
            this.Controls.Add(this.buttonSaveData);
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
        private System.Windows.Forms.Button buttonTestReadData;
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
    }
}

