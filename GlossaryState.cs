using IntentoMT.Plugin.PropertiesForm;
using IntentoSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class GlossaryState
    {
        IntentoTranslationProviderOptionsForm form;
        ProviderState providerState;
        AuthState authState;
        IntentoMTFormOptions options;

        // List of custom models obtained from provider
        Dictionary<string, dynamic> providerGlossaries = new Dictionary<string, dynamic>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public GlossaryState(AuthState authState, IntentoMTFormOptions _options, bool fromForm = false)
        {
            this.authState = authState;
            providerState = authState.providerState;
            form = providerState.form;
            options = _options;
            providerState = form.providerState;

            form.textBoxGlossary.Text = string.Empty;
            form.comboBoxGlossaries.Items.Clear();
        }

        private void FillProviderGlossaries()
        {
            if (!providerState.authState.IsOK)
                return;
            form.comboBoxGlossaries.Items.Clear();
            form.textBoxGlossary.Text = string.Empty;
            try
            {
                providerGlossaries = providerState.GetGlossaries(authState.providerDataAuthDict);
                if (providerGlossaries.Any())
                {
                    // Fill Glossary and choose SelectedIndex
                    form.comboBoxGlossaries.Items.Insert(0, "");
                    foreach (string x in providerGlossaries.Select(x => (string)x.Key).OrderBy(x => x))
                    {
                        int n = form.comboBoxGlossaries.Items.Add(x);
                        if ((string)providerGlossaries[x].id == options.Glossary)
                            form.comboBoxGlossaries.SelectedIndex = n;
                    }
                    form.textBoxGlossary.Text = null;
                }
                else
                    providerGlossaries = null;
            }
            catch
            {
                providerGlossaries = null;
            }

            if (providerGlossaries == null)
                form.textBoxGlossary.Text = options.Glossary;

            form.EnableDisable();
        }

        /// <summary>
        /// Do we need to use text box to enter/get gloosary name or from combo box
        /// </summary>
        public bool UseEspecialGlossary
        { get { return providerGlossaries == null; } }

        public static string Draw(IntentoTranslationProviderOptionsForm form, GlossaryState state)
        {
            if (state == null)
            {
                form.textBoxGlossary.Visible = false;
                form.comboBoxGlossaries.Visible = false;
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            string errorMessage = null;

            // If smart routing or provider is not initialized or no custom auth - no model selection
            if (form.groupBoxProviderSettings.Enabled)
            {
                if (form.apiKeyState.IsOK)
                {
                    if (providerState.IsOK)
                    {
                        // set state of glossary selection control
                        form.groupBoxGlossary.Visible = providerState.custom_glossary && authState.IsOK;
                        if (form.groupBoxGlossary.Visible)
                        {
                            if (providerState.GetGlossaries(authState.providerDataAuthDict) != null)
                            {
                                form.textBoxGlossary.Visible = false;
                                form.comboBoxGlossaries.Visible = true;
                                form.comboBoxGlossaries.Enabled = true;
                            }
                            else
                            {
                                form.textBoxGlossary.Visible = true;
                                form.textBoxGlossary.Enabled = true;
                                form.comboBoxGlossaries.Visible = false;
                            }

                        }
                    }
                }
            }
            return errorMessage;
        }

        private string Glossary()
        {
            if (!UseEspecialGlossary)
            {
                if (string.IsNullOrEmpty(form.textBoxGlossary.Text))
                    return null;
                return form.textBoxGlossary.Text;
            }

            if (!string.IsNullOrEmpty(form.comboBoxGlossaries.Text) && providerGlossaries != null && providerGlossaries.Count != 0)
                return (string)providerGlossaries[form.comboBoxGlossaries.Text].id;

            return null;
        }

        public void FillOptions(IntentoMTFormOptions options)
        {
            if (options.SmartRouting)
            {
                options.Glossary = null;
            }
            else
            {
                options.Glossary = form.textBoxGlossary.Visible ? form.textBoxGlossary.Text :
                        string.IsNullOrEmpty(form.comboBoxGlossaries.Text) ? null : (string)providerState.GetGlossaries(providerState.authState.providerDataAuthDict)[form.comboBoxGlossaries.Text].id;
            }
        }

    }
}
