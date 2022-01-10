using System.Drawing;

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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelTMP = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.providerAgnosticGlossariesGroup = new System.Windows.Forms.GroupBox();
            this.listOfIntentoGlossaries = new System.Windows.Forms.CheckedListBox();
            this.controlPanelForGlossaries = new System.Windows.Forms.FlowLayoutPanel();
            this.checkAllGlossaries = new System.Windows.Forms.LinkLabel();
            this.uncheckAllGlossaries = new System.Windows.Forms.LinkLabel();
            this.groupBoxOptional = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.labelHelpOptional = new System.Windows.Forms.Label();
            this.comboBoxTo = new System.Windows.Forms.ComboBox();
            this.labelDirectionChar = new System.Windows.Forms.Label();
            this.comboBoxFrom = new System.Windows.Forms.ComboBox();
            this.groupBoxGlossary = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxGlossaries = new System.Windows.Forms.ComboBox();
            this.textBoxGlossary = new System.Windows.Forms.TextBox();
            this.labelHelpGlossary = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.groupBoxModel = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.comboBoxModels = new System.Windows.Forms.ComboBox();
            this.labelHelpModel = new System.Windows.Forms.Label();
            this.checkBoxUseCustomModel = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBoxBillingAccount = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelHelpBillingAccount = new System.Windows.Forms.Label();
            this.panelConnectAccount = new System.Windows.Forms.Panel();
            this.textBoxLabelConnectAccount = new System.Windows.Forms.TextBox();
            this.comboBoxCredentialId = new System.Windows.Forms.ComboBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.groupBoxProvider = new System.Windows.Forms.GroupBox();
            this.comboBoxProviders = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxRouting = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxLabelURL = new System.Windows.Forms.LinkLabel();
            this.labelSmartRouting = new System.Windows.Forms.Label();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.providerAgnosticGlossariesGroup.SuspendLayout();
            this.controlPanelForGlossaries.SuspendLayout();
            this.groupBoxOptional.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBoxGlossary.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBoxModel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxBillingAccount.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelConnectAccount.SuspendLayout();
            this.groupBoxProvider.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(429, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 28);
            this.buttonCancel.TabIndex = 31;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(323, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 28);
            this.buttonSave.TabIndex = 30;
            this.buttonSave.Text = "Test and save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // toolTipHelp
            // 
            this.toolTipHelp.IsBalloon = true;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(476, 39);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(22, 23);
            this.buttonRefresh.TabIndex = 37;
            this.buttonRefresh.Tag = "accountControl";
            this.toolTipHelp.SetToolTip(this.buttonRefresh, "refresh list your accounts for this provider");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Visible = false;
            // 
            // labelTMP
            // 
            this.labelTMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTMP.Location = new System.Drawing.Point(3, 0);
            this.labelTMP.Name = "labelTMP";
            this.labelTMP.Size = new System.Drawing.Size(314, 34);
            this.labelTMP.TabIndex = 32;
            this.labelTMP.Text = "labelTMP";
            this.labelTMP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTMP.Visible = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.labelTMP, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonCancel, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonSave, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(534, 34);
            this.tableLayoutPanel5.TabIndex = 35;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(16, 666);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(534, 34);
            this.panel3.TabIndex = 36;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.providerAgnosticGlossariesGroup);
            this.panel4.Controls.Add(this.groupBoxOptional);
            this.panel4.Controls.Add(this.groupBoxGlossary);
            this.panel4.Controls.Add(this.groupBoxModel);
            this.panel4.Controls.Add(this.groupBoxBillingAccount);
            this.panel4.Controls.Add(this.groupBoxProvider);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(16, 16);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(534, 650);
            this.panel4.TabIndex = 30;
            // 
            // providerAgnosticGlossariesGroup
            // 
            this.providerAgnosticGlossariesGroup.AutoSize = true;
            this.providerAgnosticGlossariesGroup.Controls.Add(this.listOfIntentoGlossaries);
            this.providerAgnosticGlossariesGroup.Controls.Add(this.controlPanelForGlossaries);
            this.providerAgnosticGlossariesGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.providerAgnosticGlossariesGroup.Location = new System.Drawing.Point(0, 591);
            this.providerAgnosticGlossariesGroup.Name = "providerAgnosticGlossariesGroup";
            this.providerAgnosticGlossariesGroup.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.providerAgnosticGlossariesGroup.Size = new System.Drawing.Size(517, 155);
            this.providerAgnosticGlossariesGroup.TabIndex = 41;
            this.providerAgnosticGlossariesGroup.TabStop = false;
            this.providerAgnosticGlossariesGroup.Text = "Provider-agnostic glossaries";
            // 
            // listOfIntentoGlossaries
            // 
            this.listOfIntentoGlossaries.CheckOnClick = true;
            this.listOfIntentoGlossaries.Dock = System.Windows.Forms.DockStyle.Top;
            this.listOfIntentoGlossaries.FormattingEnabled = true;
            this.listOfIntentoGlossaries.Location = new System.Drawing.Point(10, 39);
            this.listOfIntentoGlossaries.Name = "listOfIntentoGlossaries";
            this.listOfIntentoGlossaries.Size = new System.Drawing.Size(497, 106);
            this.listOfIntentoGlossaries.TabIndex = 1;
            // 
            // controlPanelForGlossaries
            // 
            this.controlPanelForGlossaries.Controls.Add(this.checkAllGlossaries);
            this.controlPanelForGlossaries.Controls.Add(this.uncheckAllGlossaries);
            this.controlPanelForGlossaries.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanelForGlossaries.Location = new System.Drawing.Point(10, 20);
            this.controlPanelForGlossaries.Name = "controlPanelForGlossaries";
            this.controlPanelForGlossaries.Size = new System.Drawing.Size(497, 19);
            this.controlPanelForGlossaries.TabIndex = 2;
            // 
            // checkAllGlossaries
            // 
            this.checkAllGlossaries.ActiveLinkColor = System.Drawing.Color.Black;
            this.checkAllGlossaries.AutoSize = true;
            this.checkAllGlossaries.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkAllGlossaries.LinkColor = System.Drawing.Color.Black;
            this.checkAllGlossaries.Location = new System.Drawing.Point(0, 0);
            this.checkAllGlossaries.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.checkAllGlossaries.Name = "checkAllGlossaries";
            this.checkAllGlossaries.Size = new System.Drawing.Size(53, 13);
            this.checkAllGlossaries.TabIndex = 0;
            this.checkAllGlossaries.TabStop = true;
            this.checkAllGlossaries.Text = "Check all";
            this.checkAllGlossaries.VisitedLinkColor = System.Drawing.Color.Black;
            this.checkAllGlossaries.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkAllGlossaries_LinkClicked);
            // 
            // uncheckAllGlossaries
            // 
            this.uncheckAllGlossaries.ActiveLinkColor = System.Drawing.Color.Black;
            this.uncheckAllGlossaries.AutoSize = true;
            this.uncheckAllGlossaries.LinkColor = System.Drawing.Color.Black;
            this.uncheckAllGlossaries.Location = new System.Drawing.Point(59, 0);
            this.uncheckAllGlossaries.Name = "uncheckAllGlossaries";
            this.uncheckAllGlossaries.Size = new System.Drawing.Size(66, 13);
            this.uncheckAllGlossaries.TabIndex = 1;
            this.uncheckAllGlossaries.TabStop = true;
            this.uncheckAllGlossaries.Text = "Uncheck all";
            this.uncheckAllGlossaries.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uncheckAllGlossaries_LinkClicked);
            // 
            // groupBoxOptional
            // 
            this.groupBoxOptional.AutoSize = true;
            this.groupBoxOptional.Controls.Add(this.tableLayoutPanel6);
            this.groupBoxOptional.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOptional.Location = new System.Drawing.Point(0, 497);
            this.groupBoxOptional.Name = "groupBoxOptional";
            this.groupBoxOptional.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxOptional.Size = new System.Drawing.Size(517, 94);
            this.groupBoxOptional.TabIndex = 39;
            this.groupBoxOptional.TabStop = false;
            this.groupBoxOptional.Text = "OPTIONAL";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.labelHelpOptional, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.comboBoxTo, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.labelDirectionChar, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.comboBoxFrom, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(501, 63);
            this.tableLayoutPanel6.TabIndex = 34;
            // 
            // labelHelpOptional
            // 
            this.labelHelpOptional.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel6.SetColumnSpan(this.labelHelpOptional, 4);
            this.labelHelpOptional.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHelpOptional.Location = new System.Drawing.Point(2, 2);
            this.labelHelpOptional.Margin = new System.Windows.Forms.Padding(2);
            this.labelHelpOptional.Name = "labelHelpOptional";
            this.labelHelpOptional.Size = new System.Drawing.Size(497, 34);
            this.labelHelpOptional.TabIndex = 33;
            this.labelHelpOptional.Text = "We will send one-character translation request to test this configuration.\r\nIf yo" +
    "u have selected custom model or glossary, please adjust the language pair:";
            // 
            // comboBoxTo
            // 
            this.comboBoxTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTo.FormattingEnabled = true;
            this.comboBoxTo.Location = new System.Drawing.Point(145, 40);
            this.comboBoxTo.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxTo.Name = "comboBoxTo";
            this.comboBoxTo.Size = new System.Drawing.Size(115, 21);
            this.comboBoxTo.TabIndex = 13;
            this.comboBoxTo.Tag = "fix_high_dpi";
            // 
            // labelDirectionChar
            // 
            this.labelDirectionChar.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDirectionChar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelDirectionChar.Location = new System.Drawing.Point(119, 38);
            this.labelDirectionChar.Margin = new System.Windows.Forms.Padding(0);
            this.labelDirectionChar.Name = "labelDirectionChar";
            this.labelDirectionChar.Size = new System.Drawing.Size(24, 22);
            this.labelDirectionChar.TabIndex = 14;
            this.labelDirectionChar.Tag = "fix_high_dpi";
            this.labelDirectionChar.Text = "→";
            this.labelDirectionChar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxFrom
            // 
            this.comboBoxFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrom.FormattingEnabled = true;
            this.comboBoxFrom.Location = new System.Drawing.Point(2, 40);
            this.comboBoxFrom.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxFrom.Name = "comboBoxFrom";
            this.comboBoxFrom.Size = new System.Drawing.Size(115, 21);
            this.comboBoxFrom.TabIndex = 10;
            this.comboBoxFrom.Tag = "fix_high_dpi";
            // 
            // groupBoxGlossary
            // 
            this.groupBoxGlossary.AutoSize = true;
            this.groupBoxGlossary.Controls.Add(this.tableLayoutPanel4);
            this.groupBoxGlossary.Controls.Add(this.linkLabel2);
            this.groupBoxGlossary.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxGlossary.Location = new System.Drawing.Point(0, 385);
            this.groupBoxGlossary.Name = "groupBoxGlossary";
            this.groupBoxGlossary.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxGlossary.Size = new System.Drawing.Size(517, 112);
            this.groupBoxGlossary.TabIndex = 38;
            this.groupBoxGlossary.TabStop = false;
            this.groupBoxGlossary.Text = "Glossary";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelHelpGlossary, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(501, 81);
            this.tableLayoutPanel4.TabIndex = 34;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.comboBoxGlossaries);
            this.panel2.Controls.Add(this.textBoxGlossary);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 36);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(497, 43);
            this.panel2.TabIndex = 35;
            // 
            // comboBoxGlossaries
            // 
            this.comboBoxGlossaries.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxGlossaries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGlossaries.Enabled = false;
            this.comboBoxGlossaries.FormattingEnabled = true;
            this.comboBoxGlossaries.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxGlossaries.Location = new System.Drawing.Point(0, 22);
            this.comboBoxGlossaries.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxGlossaries.Name = "comboBoxGlossaries";
            this.comboBoxGlossaries.Size = new System.Drawing.Size(497, 21);
            this.comboBoxGlossaries.TabIndex = 10;
            this.comboBoxGlossaries.Tag = "glossaryControl";
            this.comboBoxGlossaries.Visible = false;
            // 
            // textBoxGlossary
            // 
            this.textBoxGlossary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGlossary.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxGlossary.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxGlossary.Location = new System.Drawing.Point(0, 0);
            this.textBoxGlossary.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxGlossary.Name = "textBoxGlossary";
            this.textBoxGlossary.Size = new System.Drawing.Size(497, 22);
            this.textBoxGlossary.TabIndex = 14;
            this.textBoxGlossary.Tag = "glossaryControl";
            this.textBoxGlossary.Visible = false;
            // 
            // labelHelpGlossary
            // 
            this.labelHelpGlossary.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHelpGlossary.Location = new System.Drawing.Point(0, 0);
            this.labelHelpGlossary.Margin = new System.Windows.Forms.Padding(0);
            this.labelHelpGlossary.Name = "labelHelpGlossary";
            this.labelHelpGlossary.Size = new System.Drawing.Size(501, 34);
            this.labelHelpGlossary.TabIndex = 13;
            this.labelHelpGlossary.Text = "Some of MT Providers enable using your custom terminology or glossary. If you hab" +
    "e one, please specify it below.";
            this.labelHelpGlossary.Visible = false;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel2.Location = new System.Drawing.Point(54, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(12, 13);
            this.linkLabel2.TabIndex = 33;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "glossary";
            this.linkLabel2.Text = "?";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_Clicked);
            // 
            // groupBoxModel
            // 
            this.groupBoxModel.AutoSize = true;
            this.groupBoxModel.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxModel.Controls.Add(this.linkLabel1);
            this.groupBoxModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxModel.Enabled = false;
            this.groupBoxModel.Location = new System.Drawing.Point(0, 246);
            this.groupBoxModel.Name = "groupBoxModel";
            this.groupBoxModel.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxModel.Size = new System.Drawing.Size(517, 139);
            this.groupBoxModel.TabIndex = 37;
            this.groupBoxModel.TabStop = false;
            this.groupBoxModel.Text = "Model";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelHelpModel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxUseCustomModel, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(501, 108);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.textBoxModel);
            this.panel1.Controls.Add(this.comboBoxModels);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(497, 43);
            this.panel1.TabIndex = 15;
            // 
            // textBoxModel
            // 
            this.textBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxModel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxModel.Location = new System.Drawing.Point(0, 21);
            this.textBoxModel.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(497, 22);
            this.textBoxModel.TabIndex = 14;
            this.textBoxModel.Tag = "modelControl";
            this.textBoxModel.Visible = false;
            // 
            // comboBoxModels
            // 
            this.comboBoxModels.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModels.FormattingEnabled = true;
            this.comboBoxModels.ItemHeight = 13;
            this.comboBoxModels.Location = new System.Drawing.Point(0, 0);
            this.comboBoxModels.Name = "comboBoxModels";
            this.comboBoxModels.Size = new System.Drawing.Size(497, 21);
            this.comboBoxModels.TabIndex = 10;
            this.comboBoxModels.Tag = "modelControl";
            this.comboBoxModels.Visible = false;
            // 
            // labelHelpModel
            // 
            this.labelHelpModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHelpModel.Location = new System.Drawing.Point(2, 2);
            this.labelHelpModel.Margin = new System.Windows.Forms.Padding(2);
            this.labelHelpModel.Name = "labelHelpModel";
            this.labelHelpModel.Size = new System.Drawing.Size(497, 34);
            this.labelHelpModel.TabIndex = 13;
            this.labelHelpModel.Text = "By default, the pre-trained general-purpose MT model is used. If it\'s not availab" +
    "le or you want to use your custom-trained model, please specify it below.";
            this.labelHelpModel.Visible = false;
            // 
            // checkBoxUseCustomModel
            // 
            this.checkBoxUseCustomModel.AutoSize = true;
            this.checkBoxUseCustomModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxUseCustomModel.Location = new System.Drawing.Point(3, 41);
            this.checkBoxUseCustomModel.Name = "checkBoxUseCustomModel";
            this.checkBoxUseCustomModel.Size = new System.Drawing.Size(136, 17);
            this.checkBoxUseCustomModel.TabIndex = 11;
            this.checkBoxUseCustomModel.Tag = "modelControl";
            this.checkBoxUseCustomModel.Text = "Use custom MT model";
            this.checkBoxUseCustomModel.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel1.Location = new System.Drawing.Point(44, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(12, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "model";
            this.linkLabel1.Text = "?";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_Clicked);
            // 
            // groupBoxBillingAccount
            // 
            this.groupBoxBillingAccount.AutoSize = true;
            this.groupBoxBillingAccount.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxBillingAccount.Controls.Add(this.linkLabel3);
            this.groupBoxBillingAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxBillingAccount.Location = new System.Drawing.Point(0, 127);
            this.groupBoxBillingAccount.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxBillingAccount.Name = "groupBoxBillingAccount";
            this.groupBoxBillingAccount.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxBillingAccount.Size = new System.Drawing.Size(517, 119);
            this.groupBoxBillingAccount.TabIndex = 36;
            this.groupBoxBillingAccount.TabStop = false;
            this.groupBoxBillingAccount.Text = "Billing account";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.labelHelpBillingAccount, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelConnectAccount, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxCredentialId, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonRefresh, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(501, 88);
            this.tableLayoutPanel2.TabIndex = 40;
            // 
            // labelHelpBillingAccount
            // 
            this.labelHelpBillingAccount.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.SetColumnSpan(this.labelHelpBillingAccount, 2);
            this.labelHelpBillingAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHelpBillingAccount.Location = new System.Drawing.Point(2, 2);
            this.labelHelpBillingAccount.Margin = new System.Windows.Forms.Padding(2);
            this.labelHelpBillingAccount.Name = "labelHelpBillingAccount";
            this.labelHelpBillingAccount.Size = new System.Drawing.Size(497, 34);
            this.labelHelpBillingAccount.TabIndex = 12;
            this.labelHelpBillingAccount.Text = "Most of the MT Providers are available via Intento contracts, but you may also us" +
    "e your own account at MT Provider connected via Intento Console.";
            this.labelHelpBillingAccount.Visible = false;
            // 
            // panelConnectAccount
            // 
            this.panelConnectAccount.Controls.Add(this.textBoxLabelConnectAccount);
            this.panelConnectAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConnectAccount.Location = new System.Drawing.Point(3, 68);
            this.panelConnectAccount.Name = "panelConnectAccount";
            this.panelConnectAccount.Size = new System.Drawing.Size(467, 17);
            this.panelConnectAccount.TabIndex = 39;
            this.panelConnectAccount.Tag = "accountControl";
            this.panelConnectAccount.Visible = false;
            // 
            // textBoxLabelConnectAccount
            // 
            this.textBoxLabelConnectAccount.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLabelConnectAccount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLabelConnectAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxLabelConnectAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxLabelConnectAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLabelConnectAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.textBoxLabelConnectAccount.Location = new System.Drawing.Point(0, 0);
            this.textBoxLabelConnectAccount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLabelConnectAccount.Name = "textBoxLabelConnectAccount";
            this.textBoxLabelConnectAccount.Size = new System.Drawing.Size(467, 13);
            this.textBoxLabelConnectAccount.TabIndex = 37;
            this.textBoxLabelConnectAccount.Tag = "https://console.inten.to/accounts";
            this.textBoxLabelConnectAccount.Text = "Connect your own account for this provider";
            // 
            // comboBoxCredentialId
            // 
            this.comboBoxCredentialId.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxCredentialId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCredentialId.FormattingEnabled = true;
            this.comboBoxCredentialId.Location = new System.Drawing.Point(3, 41);
            this.comboBoxCredentialId.Name = "comboBoxCredentialId";
            this.comboBoxCredentialId.Size = new System.Drawing.Size(467, 21);
            this.comboBoxCredentialId.TabIndex = 10;
            this.comboBoxCredentialId.Tag = "accountControl";
            this.comboBoxCredentialId.Visible = false;
            this.comboBoxCredentialId.VisibleChanged += new System.EventHandler(this.comboBoxCredentialId_VisibleChanged);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel3.Location = new System.Drawing.Point(88, 0);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(12, 13);
            this.linkLabel3.TabIndex = 33;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Tag = "account";
            this.linkLabel3.Text = "?";
            this.linkLabel3.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_Clicked);
            // 
            // groupBoxProvider
            // 
            this.groupBoxProvider.AutoSize = true;
            this.groupBoxProvider.Controls.Add(this.comboBoxProviders);
            this.groupBoxProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxProvider.Location = new System.Drawing.Point(0, 71);
            this.groupBoxProvider.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxProvider.Name = "groupBoxProvider";
            this.groupBoxProvider.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxProvider.Size = new System.Drawing.Size(517, 56);
            this.groupBoxProvider.TabIndex = 35;
            this.groupBoxProvider.TabStop = false;
            this.groupBoxProvider.Text = "Provider";
            // 
            // comboBoxProviders
            // 
            this.comboBoxProviders.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProviders.FormattingEnabled = true;
            this.comboBoxProviders.Location = new System.Drawing.Point(10, 25);
            this.comboBoxProviders.Name = "comboBoxProviders";
            this.comboBoxProviders.Size = new System.Drawing.Size(497, 21);
            this.comboBoxProviders.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBoxRouting);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(517, 71);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automatic provider selection";
            // 
            // comboBoxRouting
            // 
            this.comboBoxRouting.DisplayMember = "(none)";
            this.comboBoxRouting.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxRouting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRouting.FormattingEnabled = true;
            this.comboBoxRouting.Items.AddRange(new object[] {
            "Disabled",
            "General routing based on Intento benchmarks",
            "Industry-specific routing based on Intento benchmarks",
            "Industry-specific routing based on Intento benchmarks 1",
            "Industry-specific routing based on Intento benchmarks 2"});
            this.comboBoxRouting.Location = new System.Drawing.Point(8, 42);
            this.comboBoxRouting.Name = "comboBoxRouting";
            this.comboBoxRouting.Size = new System.Drawing.Size(501, 21);
            this.comboBoxRouting.TabIndex = 36;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.textBoxLabelURL, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelSmartRouting, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(501, 19);
            this.tableLayoutPanel1.TabIndex = 38;
            // 
            // textBoxLabelURL
            // 
            this.textBoxLabelURL.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.textBoxLabelURL.AutoSize = true;
            this.textBoxLabelURL.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLabelURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxLabelURL.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxLabelURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLabelURL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.textBoxLabelURL.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.textBoxLabelURL.Location = new System.Drawing.Point(227, 3);
            this.textBoxLabelURL.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textBoxLabelURL.Name = "textBoxLabelURL";
            this.textBoxLabelURL.Size = new System.Drawing.Size(271, 13);
            this.textBoxLabelURL.TabIndex = 37;
            this.textBoxLabelURL.TabStop = true;
            this.textBoxLabelURL.Tag = "https://try.inten.to/machine-translation-report-2021";
            this.textBoxLabelURL.Text = "the Intento Benchmarks.";
            this.textBoxLabelURL.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            // 
            // labelSmartRouting
            // 
            this.labelSmartRouting.AutoSize = true;
            this.labelSmartRouting.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSmartRouting.Location = new System.Drawing.Point(0, 3);
            this.labelSmartRouting.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelSmartRouting.Name = "labelSmartRouting";
            this.labelSmartRouting.Size = new System.Drawing.Size(227, 13);
            this.labelSmartRouting.TabIndex = 37;
            this.labelSmartRouting.Text = "Select MT provider automatically based on ";
            this.labelSmartRouting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IntentoFormOptionsMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(566, 716);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(408, 319);
            this.Name = "IntentoFormOptionsMT";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IntentoFormOptionsMT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IntentoFormOptionsMT_FormClosing);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.providerAgnosticGlossariesGroup.ResumeLayout(false);
            this.controlPanelForGlossaries.ResumeLayout(false);
            this.controlPanelForGlossaries.PerformLayout();
            this.groupBoxOptional.ResumeLayout(false);
            this.groupBoxOptional.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBoxGlossary.ResumeLayout(false);
            this.groupBoxGlossary.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBoxModel.ResumeLayout(false);
            this.groupBoxModel.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxBillingAccount.ResumeLayout(false);
            this.groupBoxBillingAccount.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelConnectAccount.ResumeLayout(false);
            this.panelConnectAccount.PerformLayout();
            this.groupBoxProvider.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolTip toolTipHelp;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Label labelTMP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.GroupBox groupBoxOptional;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label labelHelpOptional;
        public System.Windows.Forms.ComboBox comboBoxTo;
        private System.Windows.Forms.Label labelDirectionChar;
        public System.Windows.Forms.ComboBox comboBoxFrom;
        public System.Windows.Forms.GroupBox groupBoxGlossary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox comboBoxGlossaries;
        public System.Windows.Forms.TextBox textBoxGlossary;
        private System.Windows.Forms.Label labelHelpGlossary;
        private System.Windows.Forms.LinkLabel linkLabel2;
        public System.Windows.Forms.GroupBox groupBoxModel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox comboBoxModels;
        public System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.Label labelHelpModel;
        public System.Windows.Forms.CheckBox checkBoxUseCustomModel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.GroupBox groupBoxBillingAccount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelHelpBillingAccount;
        public System.Windows.Forms.Panel panelConnectAccount;
        public System.Windows.Forms.TextBox textBoxLabelConnectAccount;
        public System.Windows.Forms.ComboBox comboBoxCredentialId;
        public System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.LinkLabel linkLabel3;
        public System.Windows.Forms.GroupBox groupBoxProvider;
        public System.Windows.Forms.ComboBox comboBoxProviders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelSmartRouting;
        public System.Windows.Forms.ComboBox comboBoxRouting;
        private System.Windows.Forms.LinkLabel textBoxLabelURL;
        public System.Windows.Forms.GroupBox providerAgnosticGlossariesGroup;
        public System.Windows.Forms.CheckedListBox listOfIntentoGlossaries;
        private System.Windows.Forms.FlowLayoutPanel controlPanelForGlossaries;
        private System.Windows.Forms.LinkLabel checkAllGlossaries;
        private System.Windows.Forms.LinkLabel uncheckAllGlossaries;
    }
}