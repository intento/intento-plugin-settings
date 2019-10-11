namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    partial class IntentoFormOptionsMT
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
            this.checkBoxSmartRouting = new System.Windows.Forms.CheckBox();
            this.textBoxLabel1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxProviders = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseOwnCred = new System.Windows.Forms.CheckBox();
            this.comboBoxCredentialId = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseCustomModel = new System.Windows.Forms.CheckBox();
            this.comboBoxModels = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxUse = new System.Windows.Forms.CheckBox();
            this.comboBoxGlossary = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxSmartRouting
            // 
            this.checkBoxSmartRouting.AutoSize = true;
            this.checkBoxSmartRouting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxSmartRouting.Location = new System.Drawing.Point(12, 12);
            this.checkBoxSmartRouting.Name = "checkBoxSmartRouting";
            this.checkBoxSmartRouting.Size = new System.Drawing.Size(228, 17);
            this.checkBoxSmartRouting.TabIndex = 23;
            this.checkBoxSmartRouting.Text = "Select MT Provider automatically based on ";
            this.checkBoxSmartRouting.UseVisualStyleBackColor = true;
            // 
            // textBoxLabel1
            // 
            this.textBoxLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLabel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLabel1.ForeColor = System.Drawing.Color.Blue;
            this.textBoxLabel1.Location = new System.Drawing.Point(234, 14);
            this.textBoxLabel1.Name = "textBoxLabel1";
            this.textBoxLabel1.Size = new System.Drawing.Size(118, 13);
            this.textBoxLabel1.TabIndex = 24;
            this.textBoxLabel1.Text = "https://console.inten.to";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxProviders);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 57);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Provider";
            // 
            // comboBoxProviders
            // 
            this.comboBoxProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProviders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxProviders.FormattingEnabled = true;
            this.comboBoxProviders.Location = new System.Drawing.Point(16, 24);
            this.comboBoxProviders.Name = "comboBoxProviders";
            this.comboBoxProviders.Size = new System.Drawing.Size(434, 21);
            this.comboBoxProviders.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxUseOwnCred);
            this.groupBox2.Controls.Add(this.comboBoxCredentialId);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 80);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Provider";
            // 
            // checkBoxUseOwnCred
            // 
            this.checkBoxUseOwnCred.AutoSize = true;
            this.checkBoxUseOwnCred.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUseOwnCred.Location = new System.Drawing.Point(11, 18);
            this.checkBoxUseOwnCred.Name = "checkBoxUseOwnCred";
            this.checkBoxUseOwnCred.Size = new System.Drawing.Size(191, 17);
            this.checkBoxUseOwnCred.TabIndex = 11;
            this.checkBoxUseOwnCred.Text = "Use your own MT Provider account";
            this.checkBoxUseOwnCred.UseVisualStyleBackColor = true;
            // 
            // comboBoxCredentialId
            // 
            this.comboBoxCredentialId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCredentialId.Enabled = false;
            this.comboBoxCredentialId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCredentialId.FormattingEnabled = true;
            this.comboBoxCredentialId.Location = new System.Drawing.Point(11, 47);
            this.comboBoxCredentialId.Name = "comboBoxCredentialId";
            this.comboBoxCredentialId.Size = new System.Drawing.Size(439, 21);
            this.comboBoxCredentialId.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxUseCustomModel);
            this.groupBox3.Controls.Add(this.comboBoxModels);
            this.groupBox3.Location = new System.Drawing.Point(12, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(467, 80);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Model";
            // 
            // checkBoxUseCustomModel
            // 
            this.checkBoxUseCustomModel.AutoSize = true;
            this.checkBoxUseCustomModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUseCustomModel.Location = new System.Drawing.Point(11, 18);
            this.checkBoxUseCustomModel.Name = "checkBoxUseCustomModel";
            this.checkBoxUseCustomModel.Size = new System.Drawing.Size(129, 17);
            this.checkBoxUseCustomModel.TabIndex = 11;
            this.checkBoxUseCustomModel.Text = "Use custom MT model";
            this.checkBoxUseCustomModel.UseVisualStyleBackColor = true;
            // 
            // comboBoxModels
            // 
            this.comboBoxModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModels.Enabled = false;
            this.comboBoxModels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxModels.FormattingEnabled = true;
            this.comboBoxModels.Location = new System.Drawing.Point(11, 47);
            this.comboBoxModels.Name = "comboBoxModels";
            this.comboBoxModels.Size = new System.Drawing.Size(439, 21);
            this.comboBoxModels.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxUse);
            this.groupBox4.Controls.Add(this.comboBoxGlossary);
            this.groupBox4.Location = new System.Drawing.Point(12, 303);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(467, 80);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Glossary";
            // 
            // checkBoxUse
            // 
            this.checkBoxUse.AutoSize = true;
            this.checkBoxUse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUse.Location = new System.Drawing.Point(11, 18);
            this.checkBoxUse.Name = "checkBoxUse";
            this.checkBoxUse.Size = new System.Drawing.Size(120, 17);
            this.checkBoxUse.TabIndex = 11;
            this.checkBoxUse.Text = "Use custom glossary";
            this.checkBoxUse.UseVisualStyleBackColor = true;
            // 
            // comboBoxGlossary
            // 
            this.comboBoxGlossary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGlossary.Enabled = false;
            this.comboBoxGlossary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxGlossary.FormattingEnabled = true;
            this.comboBoxGlossary.Location = new System.Drawing.Point(11, 47);
            this.comboBoxGlossary.Name = "comboBoxGlossary";
            this.comboBoxGlossary.Size = new System.Drawing.Size(439, 21);
            this.comboBoxGlossary.TabIndex = 10;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.comboBox2);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Location = new System.Drawing.Point(12, 396);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(467, 104);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "OPTIONAL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(138, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "→";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Enabled = false;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(180, 68);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(120, 21);
            this.comboBox2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "If you have selected custom model or glossary, please adjust the language pair:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "We will send one-character translation request to test this configuration.";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(398, 526);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(81, 30);
            this.buttonCancel.TabIndex = 31;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(278, 526);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 30);
            this.buttonSave.TabIndex = 30;
            this.buttonSave.Text = "Test and save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // IntentoFormOptionsMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(491, 568);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxLabel1);
            this.Controls.Add(this.checkBoxSmartRouting);
            this.Name = "IntentoFormOptionsMT";
            this.Text = "IntentoFormOptionsMT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkBoxSmartRouting;
        private System.Windows.Forms.TextBox textBoxLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox comboBoxProviders;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox comboBoxCredentialId;
        public System.Windows.Forms.CheckBox checkBoxUseOwnCred;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.CheckBox checkBoxUseCustomModel;
        public System.Windows.Forms.ComboBox comboBoxModels;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.CheckBox checkBoxUse;
        public System.Windows.Forms.ComboBox comboBoxGlossary;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}