using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm.WinForms
{
    public partial class IntentoFormProviderAuthWizard : Form
    {
        private readonly Dictionary<string, string> authParam;

        private readonly TextBox[] tbs;
        private readonly Label[] lbs;

        public IntentoFormProviderAuthWizard(
            Dictionary<string, string> authDict, bool showHidden, bool hideHiddenTextButton)
        {
            InitializeComponent();
            LocalizeContent();

            authParam = authDict;

            if (hideHiddenTextButton)
                checkBoxShowHidden.Visible = false;

            byte i = 0;
            var paramsCount = authParam.Count;
            tlpCustomCredentials.RowCount = paramsCount;
            for (var j = 0; j < paramsCount-1; j++)
                tlpCustomCredentials.RowStyles.Add(new RowStyle());
            lbs = new Label[paramsCount];
            tbs = new TextBox[paramsCount];
            foreach (var authField in authParam)
            {
                var key = authField.Key;
                lbs[i] = new Label();
                lbs[i].Parent = this;
                lbs[i].Location = new Point(0,0);
                lbs[i].Name = "lbl" + key;
                lbs[i].Text = key;
                lbs[i].Visible = true;
                lbs[i].AutoSize = true;
                lbs[i].Margin = new Padding(6);
                tlpCustomCredentials.Controls.Add(lbs[i], 0, i);

                tbs[i] = new TextBox();
                tbs[i].Parent = this;
                tbs[i].BorderStyle = BorderStyle.FixedSingle;
                tbs[i].Location = new Point(0,0);

                tbs[i].Name = key;
                tbs[i].Size = new Size(240, 20);
                tbs[i].Text = authField.Value;
                tbs[i].TabIndex = i;
                tbs[i].Visible = true;
                tbs[i].TextChanged += textBoxes_TextChanged;
                tbs[i].UseSystemPasswordChar = !checkBoxShowHidden.Checked;
                tlpCustomCredentials.Controls.Add(tbs[i], 1, i);

                i++;
            }
            textBoxes_TextChanged(this, EventArgs.Empty);
            checkBoxShowHidden.Checked = showHidden;
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            var checkedForm = true;
            foreach (var tb in tbs)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.BackColor = Color.LightPink;
                    checkedForm = false;
                }
                else
                    tb.BackColor = SystemColors.Window;
            }
            buttonOK.Enabled = checkedForm;
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            for (byte i = 0; i < tbs.Length; i++)
            {
                authParam[tbs[i].Name] = tbs[i].Text;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var tb in tbs)
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