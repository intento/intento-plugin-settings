using Intento.MT.Plugin.PropertiesForm;
using IntentoSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class GlossaryState : BaseState
    {
        ProviderState providerState;
        AuthState authState;

        public StateModeEnum mode = StateModeEnum.unknown;
        bool isList;

        // List of custom models obtained from provider
        Dictionary<string, dynamic> glossaries = null;

        public static bool internalControlChange = false;

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

        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, GlossaryState state)
        {
            if (state == null)
            {
                Glossary_GroupBox_Disable(form.formMT);
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            FillGlossaryControls();

            string errorMessage = null;

            switch (mode)
            {
                case StateModeEnum.prohibited:
                    formMT.textBoxGlossary.Visible = false;
                    formMT.comboBoxGlossaries.Visible = false;
                    formMT.groupBoxGlossary.Visible = false;
                    break;

                case StateModeEnum.optional:
                    formMT.groupBoxGlossary.Visible = true;
                    if (isList)
                    {
                        if (glossaries.Count == 0)
                        {
                            formMT.textBoxGlossary.Visible = true;
                            formMT.comboBoxGlossaries.Visible = false;
                            formMT.textBoxGlossary.Enabled = false;
                            formMT.textBoxGlossary.Text = "No glossaries available";
                        }
                        else
                        {
                            formMT.textBoxGlossary.Visible = false;
                            formMT.comboBoxGlossaries.Visible = true;
                            formMT.comboBoxGlossaries.Enabled = true;
                        }
                    }
                    else
                    {
                        formMT.groupBoxGlossary.Enabled = true;
                        formMT.textBoxGlossary.Visible = true;
                        formMT.comboBoxGlossaries.Visible = false;
                        formMT.textBoxGlossary.Enabled = true;
                    }

                    break;

                default:
                    throw new Exception(string.Format("Invalid mode {0}", mode));
            }
            return errorMessage;
        }

        #region Properties

        private string Glossary
        {
            get
            {
                ReadGlossaries();

                switch (mode)
                {
                    case StateModeEnum.prohibited:
                        return null;

                    case StateModeEnum.optional:
                        if (isList)
                        {
                            if (string.IsNullOrEmpty(formMT.comboBoxGlossaries.Text))
                                return null;
                            return (string)glossaries[formMT.comboBoxGlossaries.Text].id;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(formMT.textBoxGlossary.Text))
                                return null;
                            return formMT.textBoxGlossary.Text;
                        }

                    default:
						return null;
						throw new Exception(string.Format("Invalid mode {0}", mode));
                }

            }
        }

        public string currentGlossary
        {
            get
            {
                string ret = null;
                if (mode == StateModeEnum.optional)
                {
                    if (isList)
                    {
                        if (!string.IsNullOrEmpty(formMT.comboBoxGlossaries.Text))
                            ret =  (string)glossaries[formMT.comboBoxGlossaries.Text].id;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(formMT.textBoxGlossary.Text))
                            ret = formMT.textBoxGlossary.Text;
                    }
                }
                return ret;
            }
        }

        #endregion Properties

        void Clear()
        {
            internalControlChange = true;
            formMT.comboBoxGlossaries.Items.Clear();
            formMT.textBoxGlossary.Text = string.Empty;
            internalControlChange = false;
        }

        private void FillGlossaryControls()
        {
            if (!authState.IsOK)
                return;

            Clear();

            if (!providerState.custom_glossary)
            {   // glossaries are not supported by provider
                mode = StateModeEnum.prohibited;
                formMT.groupBoxGlossary.Enabled = false;
                return;
            }

            mode = StateModeEnum.optional;
            formMT.groupBoxGlossary.Enabled = true;

            ReadGlossaries();

            if (isList)
            {
                // Fill Glossary and choose SelectedIndex
                formMT.comboBoxGlossaries.Items.Insert(0, "");
                foreach (string x in glossaries.Select(x => (string)x.Key).OrderBy(x => x))
                {
                    int n = formMT.comboBoxGlossaries.Items.Add(x);
                    if ((string)glossaries[x].id == options.Glossary)
                        formMT.comboBoxGlossaries.SelectedIndex = n;
                }
                formMT.comboBoxGlossaries.Visible = true;
            }
            else
            {
                formMT.textBoxGlossary.Text = options.Glossary;
                formMT.textBoxGlossary.Visible = true;
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
                options.GlossaryMode = state.mode;
                dynamic mData = null;
                if (state.glossaries != null)
                    mData = state.glossaries.Select(x => (dynamic)x.Value).Where(y => (string)y.id == state.Glossary).FirstOrDefault();
                options.GlossaryName = mData != null ? mData.name : state.Glossary;

            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.Glossary = null;
            Glossary_GroupBox_Disable(formMT);
        }

        bool readDone = false;
        private void ReadGlossaries()
        {
            if (readDone)
                return;
            readDone = true;

            try
            {
                IList<dynamic> providerGlossariesRec = form.testGlossaryData != null ? form.testGlossaryData :
                 form._translate.Glossaries(
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

        #region Events

        public void glossaryControls_ValueChanged()
        {
            formMT.groupBoxOptional.Enabled = options.UseCustomModel || currentGlossary != null;
            if (!internalControlChange)
                GlossaryState.FillOptions(this, options);
        }

        #endregion Events

        #region methods for managing a group of controls

        static void Glossary_GroupBox_Disable(IntentoFormOptionsMT formMT)
        {
            formMT.groupBoxGlossary.Enabled = false;
            formMT.comboBoxGlossaries.Visible = false;
            formMT.textBoxGlossary.Visible = false;
            //Internal_Change_checkBoxUseGlossary_Checked(formMT, false);
            formMT.textBoxGlossary.Text = "";
            formMT.comboBoxGlossaries.Items.Clear();
        }

        #endregion methods for managing a group of controls

    }
}
