﻿using System.Drawing;

namespace Intento.MT.Plugin.PropertiesForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntentoFormOptionsMT));
            this.groupBoxProvider = new System.Windows.Forms.GroupBox();
            this.comboBoxProviders = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBoxBillingAccount = new System.Windows.Forms.GroupBox();
            this.panelConnectAccount = new System.Windows.Forms.Panel();
            this.textBoxLabelConnectAccount = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.labelHelpBillingAccount = new System.Windows.Forms.Label();
            this.comboBoxCredentialId = new System.Windows.Forms.ComboBox();
            this.groupBoxModel = new System.Windows.Forms.GroupBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.labelHelpModel = new System.Windows.Forms.Label();
            this.checkBoxUseCustomModel = new System.Windows.Forms.CheckBox();
            this.comboBoxModels = new System.Windows.Forms.ComboBox();
            this.groupBoxGlossary = new System.Windows.Forms.GroupBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.textBoxGlossary = new System.Windows.Forms.TextBox();
            this.labelHelpGlossary = new System.Windows.Forms.Label();
            this.comboBoxGlossaries = new System.Windows.Forms.ComboBox();
            this.groupBoxOptional = new System.Windows.Forms.GroupBox();
            this.labelHelpOptional = new System.Windows.Forms.Label();
            this.labelDirectionChar = new System.Windows.Forms.Label();
            this.comboBoxTo = new System.Windows.Forms.ComboBox();
            this.comboBoxFrom = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.labelTMP = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxLabelURL = new System.Windows.Forms.TextBox();
            this.labelSmartRouting = new System.Windows.Forms.Label();
            this.comboBoxRouting = new System.Windows.Forms.ComboBox();
            this.groupBoxProvider.SuspendLayout();
            this.groupBoxBillingAccount.SuspendLayout();
            this.panelConnectAccount.SuspendLayout();
            this.groupBoxModel.SuspendLayout();
            this.groupBoxGlossary.SuspendLayout();
            this.groupBoxOptional.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxProvider
            // 
            this.groupBoxProvider.Controls.Add(this.comboBoxProviders);
            this.groupBoxProvider.Location = new System.Drawing.Point(15, 111);
            this.groupBoxProvider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxProvider.Name = "groupBoxProvider";
            this.groupBoxProvider.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxProvider.Size = new System.Drawing.Size(584, 71);
            this.groupBoxProvider.TabIndex = 25;
            this.groupBoxProvider.TabStop = false;
            this.groupBoxProvider.Text = "Provider";
            // 
            // comboBoxProviders
            // 
            this.comboBoxProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProviders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxProviders.FormattingEnabled = true;
            this.comboBoxProviders.Location = new System.Drawing.Point(14, 30);
            this.comboBoxProviders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxProviders.Name = "comboBoxProviders";
            this.comboBoxProviders.Size = new System.Drawing.Size(554, 27);
            this.comboBoxProviders.TabIndex = 6;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel1.Location = new System.Drawing.Point(54, 0);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(15, 19);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "model";
            this.linkLabel1.Text = "?";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_Clicked);
            // 
            // groupBoxBillingAccount
            // 
            this.groupBoxBillingAccount.Controls.Add(this.panelConnectAccount);
            this.groupBoxBillingAccount.Controls.Add(this.buttonRefresh);
            this.groupBoxBillingAccount.Controls.Add(this.linkLabel3);
            this.groupBoxBillingAccount.Controls.Add(this.labelHelpBillingAccount);
            this.groupBoxBillingAccount.Controls.Add(this.comboBoxCredentialId);
            this.groupBoxBillingAccount.Location = new System.Drawing.Point(15, 186);
            this.groupBoxBillingAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxBillingAccount.Name = "groupBoxBillingAccount";
            this.groupBoxBillingAccount.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxBillingAccount.Size = new System.Drawing.Size(582, 131);
            this.groupBoxBillingAccount.TabIndex = 26;
            this.groupBoxBillingAccount.TabStop = false;
            this.groupBoxBillingAccount.Text = "Billing account";
            // 
            // panelConnectAccount
            // 
            this.panelConnectAccount.Controls.Add(this.textBoxLabelConnectAccount);
            this.panelConnectAccount.Location = new System.Drawing.Point(16, 104);
            this.panelConnectAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelConnectAccount.Name = "panelConnectAccount";
            this.panelConnectAccount.Size = new System.Drawing.Size(325, 24);
            this.panelConnectAccount.TabIndex = 39;
            this.panelConnectAccount.Tag = "accountControl";
            this.panelConnectAccount.Visible = false;
            // 
            // textBoxLabelConnectAccount
            // 
            this.textBoxLabelConnectAccount.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLabelConnectAccount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLabelConnectAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxLabelConnectAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLabelConnectAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.textBoxLabelConnectAccount.Location = new System.Drawing.Point(0, 4);
            this.textBoxLabelConnectAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLabelConnectAccount.Name = "textBoxLabelConnectAccount";
            this.textBoxLabelConnectAccount.Size = new System.Drawing.Size(292, 16);
            this.textBoxLabelConnectAccount.TabIndex = 37;
            this.textBoxLabelConnectAccount.Tag = "https://console.inten.to/accounts";
            this.textBoxLabelConnectAccount.Text = "Connect your own account for this provider";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(540, 72);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(29, 29);
            this.buttonRefresh.TabIndex = 37;
            this.buttonRefresh.Tag = "accountControl";
            this.toolTipHelp.SetToolTip(this.buttonRefresh, "refresh list your accounts for this provider");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Visible = false;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel3.Location = new System.Drawing.Point(110, 0);
            this.linkLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(15, 19);
            this.linkLabel3.TabIndex = 33;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Tag = "account";
            this.linkLabel3.Text = "?";
            this.linkLabel3.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_Clicked);
            // 
            // labelHelpBillingAccount
            // 
            this.labelHelpBillingAccount.BackColor = System.Drawing.SystemColors.Control;
            this.labelHelpBillingAccount.Location = new System.Drawing.Point(10, 26);
            this.labelHelpBillingAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHelpBillingAccount.Name = "labelHelpBillingAccount";
            this.labelHelpBillingAccount.Size = new System.Drawing.Size(534, 40);
            this.labelHelpBillingAccount.TabIndex = 12;
            this.labelHelpBillingAccount.Text = "Most of the MT Providers are available via Intento contracts, but you may also u" +
    "se your own account at MT Provider connected via Intento Console.";
            this.labelHelpBillingAccount.Visible = false;
            // 
            // comboBoxCredentialId
            // 
            this.comboBoxCredentialId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCredentialId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCredentialId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCredentialId.FormattingEnabled = true;
            this.comboBoxCredentialId.Location = new System.Drawing.Point(14, 74);
            this.comboBoxCredentialId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxCredentialId.Name = "comboBoxCredentialId";
            this.comboBoxCredentialId.Size = new System.Drawing.Size(518, 27);
            this.comboBoxCredentialId.TabIndex = 10;
            this.comboBoxCredentialId.Tag = "accountControl";
            this.comboBoxCredentialId.Visible = false;
            this.comboBoxCredentialId.VisibleChanged += new System.EventHandler(this.comboBoxCredentialId_VisibleChanged);
            // 
            // groupBoxModel
            // 
            this.groupBoxModel.Controls.Add(this.linkLabel1);
            this.groupBoxModel.Controls.Add(this.textBoxModel);
            this.groupBoxModel.Controls.Add(this.labelHelpModel);
            this.groupBoxModel.Controls.Add(this.checkBoxUseCustomModel);
            this.groupBoxModel.Controls.Add(this.comboBoxModels);
            this.groupBoxModel.Enabled = false;
            this.groupBoxModel.Location = new System.Drawing.Point(15, 329);
            this.groupBoxModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxModel.Name = "groupBoxModel";
            this.groupBoxModel.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxModel.Size = new System.Drawing.Size(582, 140);
            this.groupBoxModel.TabIndex = 27;
            this.groupBoxModel.TabStop = false;
            this.groupBoxModel.Text = "Model";
            // 
            // textBoxModel
            // 
            this.textBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxModel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxModel.Location = new System.Drawing.Point(14, 115);
            this.textBoxModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(547, 26);
            this.textBoxModel.TabIndex = 14;
            this.textBoxModel.Tag = "modelControl";
            this.textBoxModel.Visible = false;
            // 
            // labelHelpModel
            // 
            this.labelHelpModel.Location = new System.Drawing.Point(10, 26);
            this.labelHelpModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHelpModel.Name = "labelHelpModel";
            this.labelHelpModel.Size = new System.Drawing.Size(534, 40);
            this.labelHelpModel.TabIndex = 13;
            this.labelHelpModel.Text = "By default, the pre-trained general-purpose MT model is used. If it\'s not availab" +
    "le or you want to use your custom-trained model, please specify it below.";
            this.labelHelpModel.Visible = false;
            // 
            // checkBoxUseCustomModel
            // 
            this.checkBoxUseCustomModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseCustomModel.AutoSize = true;
            this.checkBoxUseCustomModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUseCustomModel.Location = new System.Drawing.Point(14, 70);
            this.checkBoxUseCustomModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxUseCustomModel.Name = "checkBoxUseCustomModel";
            this.checkBoxUseCustomModel.Size = new System.Drawing.Size(165, 23);
            this.checkBoxUseCustomModel.TabIndex = 11;
            this.checkBoxUseCustomModel.Tag = "modelControl";
            this.checkBoxUseCustomModel.Text = "Use custom MT model";
            this.checkBoxUseCustomModel.UseVisualStyleBackColor = true;
            // 
            // comboBoxModels
            // 
            this.comboBoxModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxModels.FormattingEnabled = true;
            this.comboBoxModels.ItemHeight = 19;
            this.comboBoxModels.Location = new System.Drawing.Point(14, 99);
            this.comboBoxModels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxModels.Name = "comboBoxModels";
            this.comboBoxModels.Size = new System.Drawing.Size(546, 27);
            this.comboBoxModels.TabIndex = 10;
            this.comboBoxModels.Tag = "modelControl";
            this.comboBoxModels.Visible = false;
            // 
            // groupBoxGlossary
            // 
            this.groupBoxGlossary.Controls.Add(this.linkLabel2);
            this.groupBoxGlossary.Controls.Add(this.textBoxGlossary);
            this.groupBoxGlossary.Controls.Add(this.labelHelpGlossary);
            this.groupBoxGlossary.Controls.Add(this.comboBoxGlossaries);
            this.groupBoxGlossary.Location = new System.Drawing.Point(15, 481);
            this.groupBoxGlossary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxGlossary.Name = "groupBoxGlossary";
            this.groupBoxGlossary.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxGlossary.Size = new System.Drawing.Size(582, 118);
            this.groupBoxGlossary.TabIndex = 28;
            this.groupBoxGlossary.TabStop = false;
            this.groupBoxGlossary.Text = "Glossary";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel2.Location = new System.Drawing.Point(68, 0);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(15, 19);
            this.linkLabel2.TabIndex = 33;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "glossary";
            this.linkLabel2.Text = "?";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_Clicked);
            // 
            // textBoxGlossary
            // 
            this.textBoxGlossary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGlossary.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxGlossary.Location = new System.Drawing.Point(14, 92);
            this.textBoxGlossary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxGlossary.Name = "textBoxGlossary";
            this.textBoxGlossary.Size = new System.Drawing.Size(547, 26);
            this.textBoxGlossary.TabIndex = 14;
            this.textBoxGlossary.Tag = "glossaryControl";
            this.textBoxGlossary.Visible = false;
            // 
            // labelHelpGlossary
            // 
            this.labelHelpGlossary.Location = new System.Drawing.Point(10, 26);
            this.labelHelpGlossary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHelpGlossary.Name = "labelHelpGlossary";
            this.labelHelpGlossary.Size = new System.Drawing.Size(534, 40);
            this.labelHelpGlossary.TabIndex = 13;
            this.labelHelpGlossary.Text = "Some of MT Providers enable using your custom terminology or glossary. If you hab" +
    "e one, please specify it below.";
            this.labelHelpGlossary.Visible = false;
            // 
            // comboBoxGlossaries
            // 
            this.comboBoxGlossaries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxGlossaries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGlossaries.Enabled = false;
            this.comboBoxGlossaries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxGlossaries.FormattingEnabled = true;
            this.comboBoxGlossaries.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxGlossaries.Location = new System.Drawing.Point(14, 76);
            this.comboBoxGlossaries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxGlossaries.Name = "comboBoxGlossaries";
            this.comboBoxGlossaries.Size = new System.Drawing.Size(546, 27);
            this.comboBoxGlossaries.TabIndex = 10;
            this.comboBoxGlossaries.Tag = "glossaryControl";
            this.comboBoxGlossaries.Visible = false;
            // 
            // groupBoxOptional
            // 
            this.groupBoxOptional.Controls.Add(this.labelHelpOptional);
            this.groupBoxOptional.Controls.Add(this.labelDirectionChar);
            this.groupBoxOptional.Controls.Add(this.comboBoxTo);
            this.groupBoxOptional.Controls.Add(this.comboBoxFrom);
            this.groupBoxOptional.Location = new System.Drawing.Point(15, 615);
            this.groupBoxOptional.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxOptional.Name = "groupBoxOptional";
            this.groupBoxOptional.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxOptional.Size = new System.Drawing.Size(582, 129);
            this.groupBoxOptional.TabIndex = 29;
            this.groupBoxOptional.TabStop = false;
            this.groupBoxOptional.Text = "OPTIONAL";
            // 
            // labelHelpOptional
            // 
            this.labelHelpOptional.BackColor = System.Drawing.SystemColors.Control;
            this.labelHelpOptional.Location = new System.Drawing.Point(10, 26);
            this.labelHelpOptional.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHelpOptional.Name = "labelHelpOptional";
            this.labelHelpOptional.Size = new System.Drawing.Size(534, 40);
            this.labelHelpOptional.TabIndex = 33;
            this.labelHelpOptional.Text = "We will send one-character translation request to test this configuration.\r\nIf yo" +
    "u have selected custom model or glossary, please adjust the language pair:";
            // 
            // labelDirectionChar
            // 
            this.labelDirectionChar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDirectionChar.AutoSize = true;
            this.labelDirectionChar.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDirectionChar.Location = new System.Drawing.Point(172, 74);
            this.labelDirectionChar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDirectionChar.Name = "labelDirectionChar";
            this.labelDirectionChar.Size = new System.Drawing.Size(44, 41);
            this.labelDirectionChar.TabIndex = 14;
            this.labelDirectionChar.Tag = "fix_high_dpi";
            this.labelDirectionChar.Text = "→";
            // 
            // comboBoxTo
            // 
            this.comboBoxTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxTo.FormattingEnabled = true;
            this.comboBoxTo.Location = new System.Drawing.Point(225, 85);
            this.comboBoxTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxTo.Name = "comboBoxTo";
            this.comboBoxTo.Size = new System.Drawing.Size(148, 27);
            this.comboBoxTo.TabIndex = 13;
            this.comboBoxTo.Tag = "fix_high_dpi";
            // 
            // comboBoxFrom
            // 
            this.comboBoxFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxFrom.FormattingEnabled = true;
            this.comboBoxFrom.Location = new System.Drawing.Point(14, 85);
            this.comboBoxFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxFrom.Name = "comboBoxFrom";
            this.comboBoxFrom.Size = new System.Drawing.Size(148, 27);
            this.comboBoxFrom.TabIndex = 10;
            this.comboBoxFrom.Tag = "fix_high_dpi";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(498, 759);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(101, 28);
            this.buttonCancel.TabIndex = 31;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(348, 759);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(142, 28);
            this.buttonSave.TabIndex = 30;
            this.buttonSave.Text = "Test and save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // toolTipHelp
            // 
            this.toolTipHelp.IsBalloon = true;
            // 
            // labelTMP
            // 
            this.labelTMP.AutoSize = true;
            this.labelTMP.Location = new System.Drawing.Point(29, 759);
            this.labelTMP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTMP.Name = "labelTMP";
            this.labelTMP.Size = new System.Drawing.Size(65, 19);
            this.labelTMP.TabIndex = 32;
            this.labelTMP.Text = "labelTMP";
            this.labelTMP.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLabelURL);
            this.groupBox1.Controls.Add(this.labelSmartRouting);
            this.groupBox1.Controls.Add(this.comboBoxRouting);
            this.groupBox1.Location = new System.Drawing.Point(15, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(582, 88);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automatic provider selection";
            // 
            // textBoxLabelURL
            // 
            this.textBoxLabelURL.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLabelURL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLabelURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxLabelURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLabelURL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.textBoxLabelURL.Location = new System.Drawing.Point(291, 24);
            this.textBoxLabelURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxLabelURL.Name = "textBoxLabelURL";
            this.textBoxLabelURL.Size = new System.Drawing.Size(148, 16);
            this.textBoxLabelURL.TabIndex = 35;
            this.textBoxLabelURL.Tag = "https://try.inten.to/mt_report_2020";
            this.textBoxLabelURL.Text = "the Intento Benchmarks.";
            // 
            // labelSmartRouting
            // 
            this.labelSmartRouting.AutoSize = true;
            this.labelSmartRouting.Location = new System.Drawing.Point(14, 24);
            this.labelSmartRouting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSmartRouting.Name = "labelSmartRouting";
            this.labelSmartRouting.Size = new System.Drawing.Size(272, 19);
            this.labelSmartRouting.TabIndex = 37;
            this.labelSmartRouting.Text = "Select MT provider automatically based on ";
            // 
            // comboBoxRouting
            // 
            this.comboBoxRouting.DisplayMember = "(none)";
            this.comboBoxRouting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRouting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxRouting.FormattingEnabled = true;
            this.comboBoxRouting.Items.AddRange(new object[] {
            "Disabled",
            "General routing based on Intento benchmarks",
            "Industry-specific routing based on Intento benchmarks",
            "Industry-specific routing based on Intento benchmarks 1",
            "Industry-specific routing based on Intento benchmarks 2"});
            this.comboBoxRouting.Location = new System.Drawing.Point(18, 50);
            this.comboBoxRouting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxRouting.Name = "comboBoxRouting";
            this.comboBoxRouting.Size = new System.Drawing.Size(550, 27);
            this.comboBoxRouting.TabIndex = 36;
            // 
            // IntentoFormOptionsMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(614, 805);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelTMP);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxOptional);
            this.Controls.Add(this.groupBoxGlossary);
            this.Controls.Add(this.groupBoxModel);
            this.Controls.Add(this.groupBoxBillingAccount);
            this.Controls.Add(this.groupBoxProvider);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoFormOptionsMT";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IntentoFormOptionsMT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IntentoFormOptionsMT_FormClosing);
            this.groupBoxProvider.ResumeLayout(false);
            this.groupBoxBillingAccount.ResumeLayout(false);
            this.groupBoxBillingAccount.PerformLayout();
            this.panelConnectAccount.ResumeLayout(false);
            this.panelConnectAccount.PerformLayout();
            this.groupBoxModel.ResumeLayout(false);
            this.groupBoxModel.PerformLayout();
            this.groupBoxGlossary.ResumeLayout(false);
            this.groupBoxGlossary.PerformLayout();
            this.groupBoxOptional.ResumeLayout(false);
            this.groupBoxOptional.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox comboBoxProviders;
        public System.Windows.Forms.ComboBox comboBoxCredentialId;
        public System.Windows.Forms.CheckBox checkBoxUseCustomModel;
        public System.Windows.Forms.ComboBox comboBoxModels;
        public System.Windows.Forms.ComboBox comboBoxGlossaries;
        public System.Windows.Forms.ComboBox comboBoxFrom;
        private System.Windows.Forms.Label labelDirectionChar;
        public System.Windows.Forms.ComboBox comboBoxTo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelHelpBillingAccount;
        private System.Windows.Forms.Label labelHelpModel;
        private System.Windows.Forms.Label labelHelpGlossary;
        private System.Windows.Forms.Label labelHelpOptional;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip toolTipHelp;
        public System.Windows.Forms.GroupBox groupBoxBillingAccount;
        public System.Windows.Forms.GroupBox groupBoxModel;
        public System.Windows.Forms.TextBox textBoxModel;
        public System.Windows.Forms.GroupBox groupBoxGlossary;
        public System.Windows.Forms.TextBox textBoxGlossary;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Label labelTMP;
        private System.Windows.Forms.LinkLabel linkLabel2;
        public System.Windows.Forms.GroupBox groupBoxProvider;
        public System.Windows.Forms.GroupBox groupBoxOptional;
        private System.Windows.Forms.LinkLabel linkLabel3;
		public System.Windows.Forms.TextBox textBoxLabelConnectAccount;
		public System.Windows.Forms.Panel panelConnectAccount;
		public System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxLabelURL;
		public System.Windows.Forms.ComboBox comboBoxRouting;
		private System.Windows.Forms.Label labelSmartRouting;
	}
}