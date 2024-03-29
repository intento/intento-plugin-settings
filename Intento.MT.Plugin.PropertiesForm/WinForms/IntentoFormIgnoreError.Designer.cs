﻿namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    partial class IntentoFormIgnoreError
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
            this.buttonContinueEdit = new System.Windows.Forms.Button();
            this.buttonIgnoreAndSave = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxResponse = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonContinueEdit
            // 
            this.buttonContinueEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonContinueEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonContinueEdit.Location = new System.Drawing.Point(3, 246);
            this.buttonContinueEdit.Name = "buttonContinueEdit";
            this.buttonContinueEdit.Size = new System.Drawing.Size(187, 25);
            this.buttonContinueEdit.TabIndex = 33;
            this.buttonContinueEdit.Text = "Continue edit";
            this.buttonContinueEdit.UseVisualStyleBackColor = true;
            // 
            // buttonIgnoreAndSave
            // 
            this.buttonIgnoreAndSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonIgnoreAndSave.Location = new System.Drawing.Point(196, 246);
            this.buttonIgnoreAndSave.Name = "buttonIgnoreAndSave";
            this.buttonIgnoreAndSave.Size = new System.Drawing.Size(187, 25);
            this.buttonIgnoreAndSave.TabIndex = 32;
            this.buttonIgnoreAndSave.Text = "Ignore error and save";
            this.buttonIgnoreAndSave.UseVisualStyleBackColor = true;
            // 
            // labelError
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.labelError, 2);
            this.labelError.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(3, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(380, 30);
            this.labelError.TabIndex = 34;
            this.labelError.Text = "EMPTY";
            this.labelError.Click += new System.EventHandler(this.labelError_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            this.toolTip1.AutomaticDelay = 300;
            this.toolTip1.AutoPopDelay = 7000;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.ReshowDelay = 60;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelError, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonContinueEdit, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonIgnoreAndSave, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxResponse, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(386, 274);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // textBoxResponse
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxResponse, 2);
            this.textBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResponse.Location = new System.Drawing.Point(3, 33);
            this.textBoxResponse.Multiline = true;
            this.textBoxResponse.Name = "textBoxResponse";
            this.textBoxResponse.ReadOnly = true;
            this.textBoxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResponse.Size = new System.Drawing.Size(380, 207);
            this.textBoxResponse.TabIndex = 35;
            // 
            // IntentoFormIgnoreError
            // 
            this.AcceptButton = this.buttonIgnoreAndSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonContinueEdit;
            this.ClientSize = new System.Drawing.Size(418, 306);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntentoFormIgnoreError";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test translation error";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button buttonContinueEdit;
        public System.Windows.Forms.Button buttonIgnoreAndSave;
        public System.Windows.Forms.Label labelError;
		private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxResponse;
    }
}