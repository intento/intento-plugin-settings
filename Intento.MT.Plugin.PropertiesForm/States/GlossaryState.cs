using System;
using System.Collections.Generic;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.DI;
using Intento.SDK.Translate;
using Intento.SDK.Translate.DTO;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public class GlossaryState : BaseState
    {
        private readonly ProviderState providerState;
        private readonly AuthState authState;

        private StateModeEnum mode = StateModeEnum.Unknown;
        private bool isList;

        // List of custom models obtained from provider
        private Dictionary<string, NativeGlossary> glossaries;

        private static bool _internalControlChange;

        private ITranslateService TranslateService => Locator.Resolve<ITranslateService>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authState"></param>
        /// <param name="options"></param>
        public GlossaryState(AuthState authState, IntentoMTFormOptions options) : base(authState, options)
        {
            this.authState = authState;
            providerState = authState.ProviderState;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, GlossaryState state)
        {
            if (state != null)
            {
                return state.Draw();
            }

            GlossaryGroupBoxDisable(form.FormMt);
            return null;
        }

        private string Draw()
        {
            FillGlossaryControls();

            switch (mode)
            {
                case StateModeEnum.Prohibited:
                    FormMt.textBoxGlossary.Visible = false;
                    FormMt.comboBoxGlossaries.Visible = false;
                    FormMt.groupBoxGlossary.Visible = false;
                    break;

                case StateModeEnum.Optional:
                    FormMt.groupBoxGlossary.Visible = true;
                    if (isList)
                    {
                        if (glossaries.Count == 0)
                        {
                            FormMt.textBoxGlossary.Visible = true;
                            FormMt.comboBoxGlossaries.Visible = false;
                            FormMt.textBoxGlossary.Enabled = false;
                            FormMt.textBoxGlossary.Text = "No glossaries available";
                        }
                        else
                        {
                            FormMt.textBoxGlossary.Visible = false;
                            FormMt.comboBoxGlossaries.Visible = true;
                            FormMt.comboBoxGlossaries.Enabled = true;
                        }
                    }
                    else
                    {
                        FormMt.groupBoxGlossary.Enabled = true;
                        FormMt.textBoxGlossary.Visible = true;
                        FormMt.comboBoxGlossaries.Visible = false;
                        FormMt.textBoxGlossary.Enabled = true;
                    }

                    break;

                case StateModeEnum.Unknown:
                case StateModeEnum.Required:
                default:
                    throw new Exception($"Invalid mode {mode}");
            }

            return null;
        }

        #region Properties

        private string Glossary
        {
            get
            {
                ReadGlossaries();

                switch (mode)
                {
                    case StateModeEnum.Prohibited:
                        return null;

                    case StateModeEnum.Optional:
                        if (isList)
                        {
                            if (string.IsNullOrEmpty(FormMt.comboBoxGlossaries.Text))
                                return null;
                            return (string)glossaries[FormMt.comboBoxGlossaries.Text].Id;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(FormMt.textBoxGlossary.Text))
                                return null;
                            return FormMt.textBoxGlossary.Text;
                        }

                    case StateModeEnum.Unknown:
                    case StateModeEnum.Required:
                    default:
                        throw new Exception($"Invalid mode {mode}");
                }
            }
        }

        public string CurrentGlossary
        {
            get
            {
                string ret = null;
                if (mode != StateModeEnum.Optional)
                {
                    return null;
                }

                if (isList)
                {
                    if (!string.IsNullOrEmpty(FormMt.comboBoxGlossaries.Text))
                    {
                        ret = glossaries[FormMt.comboBoxGlossaries.Text].Id;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(FormMt.textBoxGlossary.Text))
                    {
                        ret = FormMt.textBoxGlossary.Text;
                    }
                }

                return ret;
            }
        }

        public string SelectedGlossaryTo
        {
            get
            {
                if (!isList || CurrentGlossary == null)
                    return null;
                var glossary = glossaries[FormMt.comboBoxGlossaries.Text];
                return glossary.To;
            }
        }

        public string SelectedGlossaryFrom
        {
            get
            {
                if (!isList || CurrentGlossary == null)
                    return null;
                var glossary = glossaries[FormMt.comboBoxGlossaries.Text];
                return glossary.From;
            }
        }

        #endregion Properties

        private void Clear(bool clearTextBox = true, bool clearComboBox = true)
        {
            _internalControlChange = true;
            if (clearComboBox) FormMt.comboBoxGlossaries.Items.Clear();
            if (clearTextBox) FormMt.textBoxGlossary.Text = string.Empty;
            _internalControlChange = false;
        }


        private void FillGlossaryControls()
        {
            if (!authState.IsOk)
                return;


            if (!providerState.CustomGlossary)
            {
                // glossaries are not supported by provider
                mode = StateModeEnum.Prohibited;
                FormMt.groupBoxGlossary.Enabled = false;
                Clear();
                return;
            }

            mode = StateModeEnum.Optional;
            FormMt.groupBoxGlossary.Enabled = true;

            ReadGlossaries();

            if (isList)
            {
                Clear();
                // Fill Glossary and choose SelectedIndex
                FormMt.comboBoxGlossaries.Items.Insert(0, "");
                foreach (var x in glossaries.Select(x => x.Key).OrderBy(x => x))
                {
                    var n = FormMt.comboBoxGlossaries.Items.Add(x);
                    if (glossaries[x].Id == Options.Glossary)
                    {
                        FormMt.comboBoxGlossaries.SelectedIndex = n;
                    }
                }

                FormMt.comboBoxGlossaries.Visible = true;
            }
            else
            {
                Clear(false);
                FormMt.textBoxGlossary.Text = Options.Glossary;
                FormMt.textBoxGlossary.Visible = true;
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
                var mData = (state.glossaries?.Select(x => x.Value) ?? new NativeGlossary[0])
                    .FirstOrDefault(y => y.Id == state.Glossary);
                options.GlossaryName = mData != null ? mData.Name : state.Glossary;
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.Glossary = null;
            GlossaryGroupBoxDisable(FormMt);
        }

        private bool readDone;

        private void ReadGlossaries()
        {
            if (readDone)
                return;
            readDone = true;

            try
            {
                var providerGlossariesRec = TranslateService.Glossaries(
                    providerState.CurrentProviderId,
                    authState.UseCustomAuth ? authState.ProviderDataAuthDict : null);
                glossaries = new Dictionary<string, NativeGlossary>();
                if (providerGlossariesRec != null && providerGlossariesRec.Any())
                {
                    glossaries = ProcessNativeGlossaries(providerState, providerGlossariesRec);
                }

                isList = true;

                // Temporary! Empty list means that manual entry is possible
                if (glossaries.Count == 0)
                {
                    isList = false;
                }
            }
            catch
            {
                isList = false;
            }
        }

        private static Dictionary<string, NativeGlossary> ProcessNativeGlossaries(ProviderState providerState,
            IEnumerable<NativeGlossary> raw)
        {
            // check for name duplicates and provide special naming for duplicates
            var data = new Dictionary<string, List<NativeGlossary>>();
            foreach (var item in raw)
            {
                var name = item.Name;
                if (data.ContainsKey(name))
                    data[name].Add(item);
                else
                    data[name] = new List<NativeGlossary> { item };
            }

            var res = new Dictionary<string, NativeGlossary>();
            foreach (var pair in data)
            {
                if (pair.Value.Count == 1)
                    res[MakeNativeGlossaryName(pair.Value[0])] = pair.Value[0];
                else
                {
                    foreach (var item in pair.Value)
                    {
                        var name = MakeNativeGlossaryName(item, true,
                            providerState.CurrentProviderId.Contains("google"));
                        res[name] = item;
                    }
                }
            }

            return res;
        }

        private static string MakeNativeGlossaryName(NativeGlossary model, bool longName = false, bool isGoogle = false)
        {
            var name = model.Name;
            var id = model.Id;
            var fromLang = model.From;
            var toLang = model.To;

            if (longName)
            {
                if (isGoogle)
                {
                    id = id.Substring(id.LastIndexOf('/') + 1);
                }

                if (fromLang == null || toLang == null)
                {
                    return $"{name} ({id})";
                }

                return $"{name} [{fromLang}/{toLang}] ({id})";
            }

            if (fromLang == null || toLang == null)
            {
                return $"{name}";
            }

            return $"{name} [{fromLang}/{toLang}]";
        }

        #region Events

        public void glossaryControls_ValueChanged()
        {
            FormMt.groupBoxOptional.Enabled = Options.UseCustomModel || CurrentGlossary != null;
            if (!_internalControlChange)
            {
                FillOptions(this, Options);
            }

            EnableDisable();
        }

        #endregion Events

        #region methods for managing a group of controls

        private static void GlossaryGroupBoxDisable(IntentoFormOptionsMT formMt)
        {
            formMt.groupBoxGlossary.Enabled = false;
            formMt.comboBoxGlossaries.Visible = false;
            formMt.textBoxGlossary.Visible = false;
            formMt.textBoxGlossary.Text = "";
            formMt.comboBoxGlossaries.Items.Clear();
        }

        #endregion methods for managing a group of controls
    }
}