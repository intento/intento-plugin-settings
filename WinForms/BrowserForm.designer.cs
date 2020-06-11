namespace Intento.MT.Plugin.PropertiesForm
{
	partial class BrowserForm
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
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonFillCode = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonToIntento = new System.Windows.Forms.Button();
			this.labelIEVersion = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.panelTest = new System.Windows.Forms.Panel();
			this.panelTest.SuspendLayout();
			this.SuspendLayout();
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(0, 0);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.ScrollBarsEnabled = false;
			this.webBrowser1.Size = new System.Drawing.Size(562, 498);
			this.webBrowser1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(506, 7);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(40, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Clear";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(13, 62);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(533, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "https://console-dt.inten.to/logout/memoq";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.buttonToIntento_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(336, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(10, 121);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Browser window size:";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(270, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "API KEY:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(144, 121);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "sad";
			// 
			// buttonFillCode
			// 
			this.buttonFillCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonFillCode.Location = new System.Drawing.Point(13, 7);
			this.buttonFillCode.Name = "buttonFillCode";
			this.buttonFillCode.Size = new System.Drawing.Size(236, 23);
			this.buttonFillCode.TabIndex = 3;
			this.buttonFillCode.Text = "Fill page fro demo";
			this.buttonFillCode.UseVisualStyleBackColor = true;
			this.buttonFillCode.Click += new System.EventHandler(this.buttonFillCode_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(382, 121);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(127, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Emulation IE version:";
			// 
			// buttonToIntento
			// 
			this.buttonToIntento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonToIntento.Location = new System.Drawing.Point(13, 33);
			this.buttonToIntento.Name = "buttonToIntento";
			this.buttonToIntento.Size = new System.Drawing.Size(533, 23);
			this.buttonToIntento.TabIndex = 2;
			this.buttonToIntento.Text = "https://console-dt.inten.to/login/memoq";
			this.buttonToIntento.UseVisualStyleBackColor = true;
			this.buttonToIntento.Click += new System.EventHandler(this.buttonToIntento_Click);
			// 
			// labelIEVersion
			// 
			this.labelIEVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelIEVersion.AutoSize = true;
			this.labelIEVersion.Location = new System.Drawing.Point(515, 121);
			this.labelIEVersion.Name = "labelIEVersion";
			this.labelIEVersion.Size = new System.Drawing.Size(16, 13);
			this.labelIEVersion.TabIndex = 13;
			this.labelIEVersion.Text = "-1";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(13, 91);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(533, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "https://console.inten.to/login/excel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.buttonToIntento_Click);
			// 
			// panelTest
			// 
			this.panelTest.Controls.Add(this.button2);
			this.panelTest.Controls.Add(this.labelIEVersion);
			this.panelTest.Controls.Add(this.buttonToIntento);
			this.panelTest.Controls.Add(this.label5);
			this.panelTest.Controls.Add(this.buttonFillCode);
			this.panelTest.Controls.Add(this.label4);
			this.panelTest.Controls.Add(this.label1);
			this.panelTest.Controls.Add(this.label3);
			this.panelTest.Controls.Add(this.label2);
			this.panelTest.Controls.Add(this.button3);
			this.panelTest.Controls.Add(this.button1);
			this.panelTest.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelTest.Location = new System.Drawing.Point(0, 358);
			this.panelTest.Name = "panelTest";
			this.panelTest.Size = new System.Drawing.Size(562, 140);
			this.panelTest.TabIndex = 14;
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 498);
			this.Controls.Add(this.panelTest);
			this.Controls.Add(this.webBrowser1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BrowserForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "WebLogin";
			this.ResizeEnd += new System.EventHandler(this.BrowserForm_Resize);
			this.panelTest.ResumeLayout(false);
			this.panelTest.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonFillCode;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonToIntento;
		private System.Windows.Forms.Label labelIEVersion;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panelTest;
	}
}