using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public partial class IntentoTranslationProviderAuthWizardForm : Form
    {
        public Dictionary<string, string> authParam;

        private TextBox[] tbs;
        private Label[] lbs;

        public IntentoTranslationProviderAuthWizardForm(
            Dictionary<string, string> authDict, bool showHidden, bool HideHiddenTextButton)
        {
            InitializeComponent();
            LocalizeContent();

            authParam = authDict;

            if (HideHiddenTextButton)
                checkBoxShowHidden.Visible = false;

            byte i = 0;
            int paramsCount = authParam.Count;
            tlpCustomCredentials.RowCount = paramsCount;
            for (int j = 0; j < paramsCount-1; j++)
                this.tlpCustomCredentials.RowStyles.Add(new System.Windows.Forms.RowStyle());
            lbs = new Label[paramsCount];
            tbs = new TextBox[paramsCount];
            foreach (KeyValuePair<string, string> authField in authParam)
            {
                string key = authField.Key;
                lbs[i] = new System.Windows.Forms.Label();
                lbs[i].Parent = this;
                lbs[i].Location = new System.Drawing.Point(0,0);
                lbs[i].Name = "lbl" + key;
                lbs[i].Text = key;
                lbs[i].Visible = true;
                lbs[i].AutoSize = true;
                lbs[i].Margin = new Padding(6);
                tlpCustomCredentials.Controls.Add(lbs[i], 0, i);

                tbs[i] = new System.Windows.Forms.TextBox();
                tbs[i].Parent = this;
                tbs[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                tbs[i].Location = new System.Drawing.Point(0,0);

                tbs[i].Name = key;
                tbs[i].Size = new System.Drawing.Size(240, 20);
                tbs[i].Text = authField.Value;
                tbs[i].TabIndex = i;
                tbs[i].Visible = true;
                tbs[i].TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
                tbs[i].UseSystemPasswordChar = !checkBoxShowHidden.Checked;
                tlpCustomCredentials.Controls.Add(tbs[i], 1, i);

                i++;
            }
            textBoxes_TextChanged(this, EventArgs.Empty);
            checkBoxShowHidden.Checked = showHidden;
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            bool checkedForm = true;
            foreach (TextBox tb in tbs)
            {
                if (String.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.BackColor = Color.LightPink;
                    checkedForm = false;
                }
                else
                    tb.BackColor = SystemColors.Window;
            }
            this.buttonOK.Enabled = checkedForm;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            for (byte i = 0; i < tbs.Count(); i++)
            {
                authParam[tbs[i].Name] = tbs[i].Text;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TextBox tb in tbs)
                tb.UseSystemPasswordChar = !checkBoxShowHidden.Checked;
        }

        private void LocalizeContent()
        {
            Text = Resource.AuthWizardFormCaption;
            label1.Text = Resource.CustomCredentials;
            buttonOK.Text = Resource.OKLabel;
            checkBoxShowHidden.Text = Resource.ShowHiddenTextLabel;
        }
    }
}