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

        public enum EnumGlossariesMode
        {
            // Starting mode, glossaries not checked yet
            unknown = 0,
            prohibited,
            optional
        }

        public EnumGlossariesMode mode = EnumGlossariesMode.unknown;
        bool isList;

        // List of custom models obtained from provider
        Dictionary<string, dynamic> glossaries = null;

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

            Clear();
        }

        void Clear()
        {
            form.Glossary_ComboBox_Clear();
            form.Glossary_TextBox_Text = string.Empty;
        }

        bool controls_ok = false;
        private void FillGlossaryControls()
        {
            if (!authState.IsOK || controls_ok)
                return;

            controls_ok = true;
            Clear();

            if (!providerState.custom_glossary)
            {   // glossaries are not supported by provider
                mode = EnumGlossariesMode.prohibited;
                form.Glossary_Group_Visible = false;
                return;
            }

            mode = EnumGlossariesMode.optional;
            form.Glossary_Group_Visible = true;

            ReadGlossaries();

            if (isList)
            {
                // Fill Glossary and choose SelectedIndex
                form.Glossary_ComboBox_Insert(0, "");
                foreach (string x in glossaries.Select(x => (string)x.Key).OrderBy(x => x))
                {
                    int n = form.Glossary_ComboBoxAdd(x);
                    if ((string)glossaries[x].id == options.Glossary)
                        form.Glossary_ComboBox_SelectedIndex = n;
                }
            }
            else
            {
                form.Glossary_TextBox_Text = options.Glossary;
            }
        }

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
            FillGlossaryControls();

            string errorMessage = null;

            switch(mode)
            {
                case EnumGlossariesMode.prohibited:
                    form.Glossary_TextBox_Visible = false;
                    form.Glossary_ComboBox_Visible = false;
                    form.Glossary_Group_Visible = false;
                    break;

                case EnumGlossariesMode.optional:
                    if (isList)
                    {
                        form.Glossary_Group_Visible = true;
                        if (glossaries.Count == 0)
                        {
                            form.Glossary_TextBox_Visible = true;
                            form.Glossary_ComboBox_Visible = false;
                            form.Glossary_TextBox_Enabled = false;
                            form.Glossary_TextBox_Text = "No glossaries available";
                        }
                        else
                        {
                            form.Glossary_TextBox_Visible = false;
                            form.Glossary_ComboBox_Visible = true;
                            form.Glossary_ComboBox_Enabled = true;
                        }
                    }
                    else
                    {
                        form.Glossary_Group_Visible = true;
                        form.Glossary_TextBox_Visible = true;
                        form.Glossary_ComboBox_Visible = false;
                        form.Glossary_TextBox_Enabled = true;
                    }

                    break;

                default:
                    throw new Exception(string.Format("Invalid mode {0}", mode));
            }
            return errorMessage;
        }

        private string Glossary
        {
            get
            {
                ReadGlossaries();

                switch (mode)
                {
                    case EnumGlossariesMode.prohibited:
                        return null;

                    case EnumGlossariesMode.optional:
                        if (isList)
                        {
                            if (string.IsNullOrEmpty(form.Glossary_ComboBox_Text))
                                return null;
                            return (string)glossaries[form.Glossary_ComboBox_Text].id;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(form.Glossary_TextBox_Text))
                                return null;
                            return form.Glossary_TextBox_Text;
                        }

                    default:
                        throw new Exception(string.Format("Invalid mode {0}", mode));
                }

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
            Clear();
        }

        bool readDone = false;
        private void ReadGlossaries()
        {
            if (readDone)
                return;
            readDone = true;

            try
            {
                IList<dynamic> providerGlossariesRec = form.Glossaries(
                    providerState.currentProviderId, 
                    authState.UseCustomAuth ? authState.providerDataAuthDict : null);
                glossaries = new Dictionary<string, dynamic>();
                if (providerGlossariesRec != null && providerGlossariesRec.Any())
                    glossaries = providerGlossariesRec.ToDictionary(s => (string)s.name, q => q);
                isList = true;

                // Temporary! Empty list means that manual entry is possbile
                if (glossaries.Count == 0)
                    isList = false;
            }
            catch
            {
                isList = false;
            }
        }

    }
}
