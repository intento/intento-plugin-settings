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
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.labelError = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxAuth = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelAddress = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
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
            this.checkBoxSaveLocally = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBoxProxy.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxProxy
            // 
            this.groupBoxProxy.Controls.Add(this.groupBoxAuth);
            this.groupBoxProxy.Controls.Add(this.panel2);
            this.groupBoxProxy.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxProxy.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxProxy.Enabled = false;
            this.groupBoxProxy.Location = new System.Drawing.Point(3, 50);
            this.groupBoxProxy.Name = "groupBoxProxy";
            this.groupBoxProxy.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.groupBoxProxy.Size = new System.Drawing.Size(655, 191);
            this.groupBoxProxy.TabIndex = 12;
            this.groupBoxProxy.TabStop = false;
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.tableLayoutPanel4);
            this.groupBoxAuth.Controls.Add(this.labelError);
            this.groupBoxAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAuth.Enabled = false;
            this.groupBoxAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxAuth.Location = new System.Drawing.Point(8, 79);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Size = new System.Drawing.Size(639, 104);
            this.groupBoxAuth.TabIndex = 16;
            this.groupBoxAuth.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel4.Controls.Add(this.labelUserName, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPassword, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelPassword, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.textBoxUserName, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(633, 53);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // labelUserName
            // 
            this.labelUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUserName.Location = new System.Drawing.Point(3, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(88, 28);
            this.labelUserName.TabIndex = 4;
            this.labelUserName.Text = "User name:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxPassword.Location = new System.Drawing.Point(97, 31);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(533, 22);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxAuth_TextChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPassword.Location = new System.Drawing.Point(3, 28);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(88, 42);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxUserName.Location = new System.Drawing.Point(97, 3);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(533, 22);
            this.textBoxUserName.TabIndex = 5;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxAuth_TextChanged);
            // 
            // labelError
            // 
            this.labelError.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(3, 71);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(633, 30);
            this.labelError.TabIndex = 34;
            this.labelError.Text = "Proxy connection error";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelError.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxAuth);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(8, 51);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.panel2.Size = new System.Drawing.Size(639, 28);
            this.panel2.TabIndex = 19;
            // 
            // checkBoxAuth
            // 
            this.checkBoxAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAuth.Location = new System.Drawing.Point(3, 3);
            this.checkBoxAuth.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxAuth.Name = "checkBoxAuth";
            this.checkBoxAuth.Size = new System.Drawing.Size(633, 22);
            this.checkBoxAuth.TabIndex = 17;
            this.checkBoxAuth.Text = "Authorization required";
            this.checkBoxAuth.UseVisualStyleBackColor = true;
            this.checkBoxAuth.CheckedChanged += new System.EventHandler(this.checkBoxAuth_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.labelAddress, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxAddress, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelPort, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxPort, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(639, 28);
            this.tableLayoutPanel2.TabIndex = 18;
            // 
            // labelAddress
            // 
            this.labelAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAddress.Location = new System.Drawing.Point(0, 0);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(0);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(217, 28);
            this.labelAddress.TabIndex = 12;
            this.labelAddress.Text = "IP address or host name: http(s)://";
            this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAddress.Location = new System.Drawing.Point(220, 5);
            this.textBoxAddress.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(249, 22);
            this.textBoxAddress.TabIndex = 13;
            // 
            // labelPort
            // 
            this.labelPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPort.Location = new System.Drawing.Point(475, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(32, 28);
            this.labelPort.TabIndex = 14;
            this.labelPort.Text = "Port:";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxPort
            // 
            this.textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPort.Location = new System.Drawing.Point(513, 5);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(123, 22);
            this.textBoxPort.TabIndex = 15;
            this.textBoxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPort_KeyPress);
            // 
            // checkBoxProxy
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.checkBoxProxy, 2);
            this.checkBoxProxy.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxProxy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxProxy.Location = new System.Drawing.Point(3, 26);
            this.checkBoxProxy.Name = "checkBoxProxy";
            this.checkBoxProxy.Size = new System.Drawing.Size(649, 18);
            this.checkBoxProxy.TabIndex = 13;
            this.checkBoxProxy.Text = "checkBoxProxy";
            this.checkBoxProxy.UseVisualStyleBackColor = true;
            this.checkBoxProxy.CheckedChanged += new System.EventHandler(this.checkBoxProxy_CheckedChanged);
            // 
            // checkBoxSaveApiKeyInRegistry
            // 
            this.checkBoxSaveApiKeyInRegistry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxSaveApiKeyInRegistry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxSaveApiKeyInRegistry.Location = new System.Drawing.Point(3, 77);
            this.checkBoxSaveApiKeyInRegistry.Name = "checkBoxSaveApiKeyInRegistry";
            this.checkBoxSaveApiKeyInRegistry.Size = new System.Drawing.Size(633, 18);
            this.checkBoxSaveApiKeyInRegistry.TabIndex = 15;
            this.checkBoxSaveApiKeyInRegistry.Text = "checkBoxSaveApiKeyInRegistry";
            this.checkBoxSaveApiKeyInRegistry.UseVisualStyleBackColor = true;
            this.checkBoxSaveApiKeyInRegistry.Visible = false;
            // 
            // checkBoxTrace
            // 
            this.checkBoxTrace.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxTrace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTrace.Location = new System.Drawing.Point(3, 3);
            this.checkBoxTrace.Name = "checkBoxTrace";
            this.checkBoxTrace.Size = new System.Drawing.Size(321, 17);
            this.checkBoxTrace.TabIndex = 14;
            this.checkBoxTrace.Text = "checkBoxTrace";
            this.checkBoxTrace.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(559, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(93, 28);
            this.buttonCancel.TabIndex = 33;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(461, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(92, 28);
            this.buttonSave.TabIndex = 32;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(655, 22);
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
            this.textBoxCustomSettingsName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCustomSettingsName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCustomSettingsName.Location = new System.Drawing.Point(3, 49);
            this.textBoxCustomSettingsName.Name = "textBoxCustomSettingsName";
            this.textBoxCustomSettingsName.Size = new System.Drawing.Size(633, 22);
            this.textBoxCustomSettingsName.TabIndex = 36;
            // 
            // labelCustomSettingsName
            // 
            this.labelCustomSettingsName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCustomSettingsName.Location = new System.Drawing.Point(3, 23);
            this.labelCustomSettingsName.Name = "labelCustomSettingsName";
            this.labelCustomSettingsName.Size = new System.Drawing.Size(633, 23);
            this.labelCustomSettingsName.TabIndex = 37;
            this.labelCustomSettingsName.Text = "Custom name for plugin settings (leave blank to use default name)";
            // 
            // checkBoxCustomTagParser
            // 
            this.checkBoxCustomTagParser.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxCustomTagParser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxCustomTagParser.Location = new System.Drawing.Point(11, 11);
            this.checkBoxCustomTagParser.Name = "checkBoxCustomTagParser";
            this.checkBoxCustomTagParser.Size = new System.Drawing.Size(633, 24);
            this.checkBoxCustomTagParser.TabIndex = 38;
            this.checkBoxCustomTagParser.Text = "CustomTagParser";
            this.checkBoxCustomTagParser.UseVisualStyleBackColor = true;
            this.checkBoxCustomTagParser.Visible = false;
            // 
            // checkBoxCutTags
            // 
            this.checkBoxCutTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxCutTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxCutTags.Location = new System.Drawing.Point(3, 3);
            this.checkBoxCutTags.Name = "checkBoxCutTags";
            this.checkBoxCutTags.Size = new System.Drawing.Size(633, 17);
            this.checkBoxCutTags.TabIndex = 39;
            this.checkBoxCutTags.Text = "checkBoxCutTags";
            this.checkBoxCutTags.UseVisualStyleBackColor = true;
            this.checkBoxCutTags.Visible = false;
            // 
            // checkBoxSaveLocally
            // 
            this.checkBoxSaveLocally.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxSaveLocally.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxSaveLocally.Location = new System.Drawing.Point(330, 3);
            this.checkBoxSaveLocally.Name = "checkBoxSaveLocally";
            this.checkBoxSaveLocally.Size = new System.Drawing.Size(322, 17);
            this.checkBoxSaveLocally.TabIndex = 40;
            this.checkBoxSaveLocally.Text = "checkBoxSaveLocally";
            this.checkBoxSaveLocally.UseVisualStyleBackColor = true;
            this.checkBoxSaveLocally.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.checkBoxTrace, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxSaveLocally, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxProxy, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(655, 47);
            this.tableLayoutPanel1.TabIndex = 41;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 491);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 34);
            this.panel1.TabIndex = 42;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Controls.Add(this.buttonSave, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonCancel, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(655, 34);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.checkBoxCutTags, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelCustomSettingsName, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.textBoxCustomSettingsName, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.checkBoxSaveApiKeyInRegistry, 0, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(639, 98);
            this.tableLayoutPanel5.TabIndex = 43;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.checkBoxCustomTagParser);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 241);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(11, 11, 11, 0);
            this.panel3.Size = new System.Drawing.Size(655, 35);
            this.panel3.TabIndex = 44;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 276);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panel4.Size = new System.Drawing.Size(655, 114);
            this.panel4.TabIndex = 45;
            // 
            // IntentoFormAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(661, 550);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxProxy);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(408, 319);
            this.Name = "IntentoFormAdvanced";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IntentoFormAdvanced";
            this.Shown += new System.EventHandler(this.IntentoFormAdvanced_Shown);
            this.groupBoxProxy.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}