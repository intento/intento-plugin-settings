using Intento.MT.Plugin.PropertiesForm;
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
    public class GlossaryState : BaseState
    {
        ProviderState providerState;
        AuthState authState;
        bool firstTime = true;

        // List of custom models obtained from provider
        Dictionary<string, dynamic> providerGlossaries = new Dictionary<string, dynamic>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public GlossaryState(AuthState authState, IntentoMTFormOptions _options, bool fromForm = false) : base(authState, _options)
        {
            this.authState = authState;
            providerState = authState.providerState;
            providerState = authState.providerState;

            form.Glossary_TextBox_Text = string.Empty;
            form.Glossary_ComboBox_Clear();

            FillProviderGlossaries();
        }

        private void FillProviderGlossaries()
        {
            form.Glossary_ComboBox_Clear();
            form.Glossary_TextBox_Text = string.Empty;
            try
            {
                providerGlossaries = providerState.GetGlossaries(authState.providerDataAuthDict);
                if (providerGlossaries != null && providerGlossaries.Any())
                {
                    // Fill Glossary and choose SelectedIndex
                    form.Glossary_ComboBox_Insert(0, "");
                    foreach (string x in providerGlossaries.Select(x => (string)x.Key).OrderBy(x => x))
                    {
                        int n = form.Glossary_ComboBoxAdd(x);
                        if ((string)providerGlossaries[x].id == options.Glossary)
                            form.Glossary_ComboBox_SelectedIndex = n;
                    }
                    form.Glossary_TextBox_Text = null;
                }
                else
                    providerGlossaries = null;
            }
            catch
            {
                providerGlossaries = null;
            }

            if (providerGlossaries == null)
                form.Glossary_TextBox_Text = options.Glossary;
        }

        /// <summary>
        /// Do we need to use text box to enter/get gloosary name or from combo box
        /// </summary>
        public bool UseEspecialGlossary
        { get { return providerGlossaries == null; } }

        public static string Draw(IForm form, GlossaryState state)
        {
            if (state == null)
            {
                form.Glossary_Group_Visible = false;
                // form.textBoxGlossary.Visible = false;
                // form.comboBoxGlossaries.Visible = false;
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            string errorMessage = null;

            if (firstTime)
            {
                if (providerGlossaries != null)
                {
                    // Fill Glossary and choose SelectedIndex
                    form.Glossary_ComboBox_Insert(0, "");
                    foreach (string x in providerGlossaries.Select(x => (string)x.Key).OrderBy(x => x))
                    {
                        int n = form.Glossary_ComboBoxAdd(x);
                        if ((string)providerGlossaries[x].id == options.Glossary)
                            form.Glossary_ComboBox_SelectedIndex = n;
                    }
                    form.Glossary_TextBox_Text = null;
                }
                firstTime = false;
            }

            // set state of glossary selection control
            if (form.Glossary_Group_Visible = providerState.custom_glossary && authState.IsOK)
            {
                var glossaries = providerState.GetGlossaries(authState.providerDataAuthDict);
                if (glossaries != null && glossaries.Count != 0)
                {
                    form.Glossary_TextBox_Visible = false;
                    form.Glossary_ComboBox_Visible = true;
                }
                else
                {
                    form.Glossary_TextBox_Visible = true;
                    form.Glossary_ComboBox_Visible = false;
                }

            }
            return errorMessage;
        }

        private string Glossary
        {
            get
            {
                if (!form.Glossary_Group_Visible)
                    return null;

                if (UseEspecialGlossary)
                {
                    if (string.IsNullOrEmpty(form.Glossary_TextBox_Text))
                        return null;
                    return form.Glossary_TextBox_Text;
                }

                if (!string.IsNullOrEmpty(form.Glossary_ComboBox_Text) && providerGlossaries != null && providerGlossaries.Count != 0)
                    return (string)providerGlossaries[form.Glossary_ComboBox_Text].id;

                return null;
            }
        }

        public static void FillOptions(GlossaryState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.Glossary = null;
            }
            else
            {
                options.Glossary = state.Glossary;
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.Glossary = null;
            form.Glossary_ComboBox_Clear();
        }

    }
}
