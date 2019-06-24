namespace Intento.MT.Plugin.PropertiesForm
{
    partial class IntentoTranslationProviderProxySettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.checkBoxAuth = new System.Windows.Forms.CheckBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP address or host name: http(s)://";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAddress.Location = new System.Drawing.Point(180, 29);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(166, 20);
            this.textBoxAddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(352, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPort.Location = new System.Drawing.Point(382, 29);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(48, 20);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPort_KeyPress);
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAuth.Controls.Add(this.textBoxPassword);
            this.groupBoxAuth.Controls.Add(this.label3);
            this.groupBoxAuth.Controls.Add(this.textBoxUserName);
            this.groupBoxAuth.Controls.Add(this.label4);
            this.groupBoxAuth.Enabled = false;
            this.groupBoxAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxAuth.Location = new System.Drawing.Point(16, 71);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Size = new System.Drawing.Size(416, 100);
            this.groupBoxAuth.TabIndex = 4;
            this.groupBoxAuth.TabStop = false;
            // 
            // checkBoxAuth
            // 
            this.checkBoxAuth.AutoSize = true;
            this.checkBoxAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAuth.Location = new System.Drawing.Point(16, 56);
            this.checkBoxAuth.Name = "checkBoxAuth";
            this.checkBoxAuth.Size = new System.Drawing.Size(125, 17);
            this.checkBoxAuth.TabIndex = 5;
            this.checkBoxAuth.Text = "Authorization required";
            this.checkBoxAuth.UseVisualStyleBackColor = true;
            this.checkBoxAuth.CheckedChanged += new System.EventHandler(this.checkBoxAuth_CheckedChanged);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Location = new System.Drawing.Point(78, 62);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(321, 20);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxAuth_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserName.Location = new System.Drawing.Point(78, 23);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(321, 20);
            this.textBoxUserName.TabIndex = 5;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxAuth_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "User name:";
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDone.Location = new System.Drawing.Point(357, 186);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 5;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // IntentoTranslationProviderProxySettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 221);
            this.Controls.Add(this.checkBoxAuth);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.label1);
            this.Name = "IntentoTranslationProviderProxySettingsForm";
            this.Text = "Proxy Settings";
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.CheckBox checkBoxAuth;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDone;
    }
}