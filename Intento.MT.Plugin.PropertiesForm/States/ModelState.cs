using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.DI;
using Intento.SDK.Translate;
using Intento.SDK.Translate.DTO;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public class ModelState : BaseState
    {
        private readonly ProviderState providerState;
        private readonly AuthState authState;
        private ITranslateService TranslateService => Form.Locator?.Resolve<ITranslateService>();

        private StateModeEnum modelMode = StateModeEnum.Unknown;
        private bool isList;

        private bool firstTimeDraw = true;

        // Indicator of change of control status from the inside
        // in this case no change event call is required
        public static bool InternalControlChange { get; set; }

        // List of custom models obtained from provider
        private Dictionary<string, Model> models;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authState"></param>
        /// <param name="options"></param>
        public ModelState(AuthState authState, IntentoMTFormOptions options) : base(authState, options)
        {
            this.authState = authState;
            providerState = authState.ProviderState;
        }

        /// <summary>
        /// Update state
        /// </summary>
        /// <param name="form"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string Draw(IntentoTranslationProviderOptionsForm form, ModelState state)
        {
            if (state != null)
            {
                return state.Draw();
            }

            ModelGroupEnabled(form.FormMt, false);
            return null;
        }

        private string Draw()
        {
            FillModelControls();
            firstTimeDraw = false;
            ModelControlBackColorState(false);
            if (modelMode == StateModeEnum.Prohibited)
                return null;

            string errorMessage = null;

            ModelGroupEnabled(Form.FormMt, true);

            // set state of checkBoxUseCustomModel
            if (modelMode == StateModeEnum.Required && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.ModelRequiredMessage;
            else if (UseModel && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.CustomModelRequiredMessage;

            // set back color 
            ModelControlBackColorState(!string.IsNullOrEmpty(errorMessage));
            if (SelectedModelFrom != null && providerState.FromLanguages != null)
            {
                var lang = GetLanguage(providerState.FromLanguages, SelectedModelFrom);
                if (!string.IsNullOrEmpty(lang))
                {
                    FormMt.comboBoxFrom.SelectedIndex = FormMt.comboBoxFrom.Items.IndexOf(lang);
                }
            }

            if (SelectedModelTo != null && providerState.ToLanguages != null)
            {
                var lang = GetLanguage(providerState.ToLanguages, SelectedModelTo);
                if (!string.IsNullOrEmpty(lang))
                {
                    FormMt.comboBoxTo.SelectedIndex = FormMt.comboBoxTo.Items.IndexOf(lang);
                }
            }

            return errorMessage;
        }

        private static string GetLanguage(IReadOnlyDictionary<string, string> dict, string lang)
        {
            if (dict.TryGetValue(lang, out var val))
            {
                return val;
            }

            switch (lang)
            {
                case "zh-TW":
                    return dict["zh-hant"];
                default:
                    var index = lang.IndexOf("-", StringComparison.OrdinalIgnoreCase);
                    if (index < 0)
                    {
                        return null;
                    }

                    var baseLang = lang.Substring(0, index);
                    return dict.TryGetValue(baseLang, out var bLang) ? bLang : null;
            }
        }

        #region Properties

        private string ModelName
        {
            get
            {
                if (!UseModel)
                {
                    return null;
                }

                if (!isList)
                {
                    // Use model name form text box
                    return string.IsNullOrEmpty(FormMt.textBoxModel.Text) ? null : FormMt.textBoxModel.Text;
                }

                // Use model name from combo box
                if (!models.TryGetValue(FormMt.comboBoxModels.Text, out _))
                {
                    return null;
                }

                var modelName = models[FormMt.comboBoxModels.Text].Id;
                return string.IsNullOrEmpty(modelName) ? null : modelName;
            }
        }

        private string To
        {
            get
            {
                return ModelName != null && FormMt.comboBoxTo.SelectedIndex != -1
                    ? authState.ProviderState.ToLanguages.First(x => x.Value == FormMt.comboBoxTo.Text).Key
                    : "es";
            }
        }

        private string From
        {
            get
            {
                return ModelName != null && FormMt.comboBoxFrom.SelectedIndex != -1
                    ? authState.ProviderState.FromLanguages.First(x => x.Value == FormMt.comboBoxFrom.Text).Key
                    : "en";
            }
        }

        public string SelectedModelTo
        {
            get
            {
                if (!UseModel || !isList || ModelName == null)
                {
                    return null;
                }

                var model = models[FormMt.comboBoxModels.Text];
                return model.To;
            }
        }

        public string SelectedModelFrom
        {
            get
            {
                if (!UseModel || !isList || ModelName == null)
                    return null;
                var model = models[FormMt.comboBoxModels.Text];
                return model.From;
            }
        }


        public bool UseModel => FormMt.checkBoxUseCustomModel.Checked;

        #endregion Properties

        private void FillModelControls()
        {
            // Fill combo or text box depending on provider features
            if (!authState.IsOk)
            {
                return;
            }

            if (authState.ProviderState.SmartRoutingState.SmartRouting)
                modelMode = StateModeEnum.Prohibited;
            else if (!authState.ProviderState.StockModel && authState.ProviderState.CustomModel)
                modelMode = StateModeEnum.Required;
            else if (!authState.ProviderState.CustomModel)
                modelMode = StateModeEnum.Prohibited;
            else
                modelMode = StateModeEnum.Optional;

            if (modelMode == StateModeEnum.Prohibited)
            {
                ModelGroupEnabled(FormMt, false);
                Clear();
                return;
            }

            ReadModels();

            ModelGroupEnabled(FormMt, true);

            FormMt.checkBoxUseCustomModel.Enabled = true;

            if (isList)
            {
                Clear();
                if (modelMode == StateModeEnum.Required)
                {
                    Internal_Change_checkBoxUseCustomModel_Checked(FormMt, true);
                    FormMt.checkBoxUseCustomModel.Enabled = false;
                }
                else
                {
                    Internal_Change_checkBoxUseCustomModel_Checked(FormMt, Options.UseCustomModel);
                }

                FormMt.comboBoxModels.Visible = UseModel;
                FormMt.textBoxModel.Visible = false;

                if (models.Count > 1 || modelMode == StateModeEnum.Optional)
                {
                    FormMt.comboBoxModels.Items.Add("");
                }

                // Fill comboBoxModels and choose SelectedIndex
                foreach (var x in models.Select(x => x.Key).OrderBy(x => x))
                {
                    var n = FormMt.comboBoxModels.Items.Add(x);
                    if (models[x].Id == Options.CustomModel)
                    {
                        FormMt.comboBoxModels.SelectedIndex = n;
                    }
                }

                if (FormMt.comboBoxModels.Items.Count == 1)
                {
                    FormMt.comboBoxModels.SelectedIndex = 0;
                }
            }
            else
            {
                // Text box to enter model
                FormMt.textBoxModel.Visible = UseModel;
                FormMt.comboBoxModels.Visible = false;

                if (modelMode == StateModeEnum.Optional)
                {
                    FormMt.checkBoxUseCustomModel.Enabled = true;
                    Internal_Change_checkBoxUseCustomModel_Checked(FormMt, Options.UseCustomModel);
                }
                else
                {
                    FormMt.checkBoxUseCustomModel.Enabled = false;
                    Internal_Change_checkBoxUseCustomModel_Checked(FormMt, true);
                }

                if (Options.UseCustomModel)
                    FormMt.textBoxModel.Text = Options.CustomModel;
                else
                    Clear(false);
            }
        }

        public static void FillOptions(ModelState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.UseCustomModel = false;
                options.CustomModel = null;
                options.GlossaryMode = StateModeEnum.Unknown;
                options.CustomModelName = null;
                options.FromLanguage = null;
                options.ToLanguage = null;
            }
            else
            {
                options.UseCustomModel = state.UseModel;
                options.CustomModel = state.ModelName;
                options.CustomModelMode = state.modelMode;
                var mData = state.models?.Select(x => x.Value)
                    .FirstOrDefault(y => y.Id == state.ModelName);

                options.CustomModelName = mData != null ? mData.Name : state.ModelName;
                options.FromLanguage = options.CustomModelName == null || !options.UseCustomModel ? null : state.From;
                options.ToLanguage = options.CustomModelName == null || !options.UseCustomModel ? null : state.To;
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomModel = false;
            options.CustomModel = null;
            ModelGroupEnabled(FormMt, false);

            Clear();
        }

        private bool readDone;

        private void ReadModels()
        {
            if (readDone)
            {
                return;
            }

            readDone = true;

            models = new Dictionary<string, Model>();
            try
            {
                var providerModelsRec = TranslateService.Models(
                    authState.ProviderState.CurrentProviderId,
                    authState.UseCustomAuth ? authState.ProviderDataAuthDict : null);
                if (providerModelsRec != null)
                {
                    models = ProcessModels(providerState, providerModelsRec);
                }

                isList = true;

                // Temporary! Empty list means that manual entry is possible
                if (models.Count == 0)
                {
                    isList = false;
                }
            }
            catch
            {
                isList = false;
            }
        }

        #region Events

        public void checkBoxUseCustomModel_CheckedChanged()
        {
            FormMt.textBoxModel.Visible = UseModel;
            FormMt.comboBoxModels.Visible = UseModel;
            OptionalGroupEnabled(UseModel);
            if (InternalControlChange)
            {
                return;
            }

            Options.UseCustomModel = UseModel;
            EnableDisable();
        }

        public void modelControls_ValueChanged()
        {
            if (!firstTimeDraw && !InternalControlChange)
            {
                FillOptions(this, Options);
            }

            EnableDisable();
        }

        private static Dictionary<string, Model> ProcessModels(ProviderState providerState, IEnumerable<Model> raw)
        {
            // check for name duplicates and provide special naming for duplicates
            var data = new Dictionary<string, List<Model>>();
            foreach (var item in raw)
            {
                var name = item.Name;
                if (data.ContainsKey(name))
                    data[name].Add(item);
                else
                    data[name] = new List<Model> { item };
            }

            var res = new Dictionary<string, Model>();
            foreach (var pair in data)
            {
                if (pair.Value.Count == 1)
                    res[MakeModelName(pair.Value[0])] = pair.Value[0];
                else
                {
                    foreach (var item in pair.Value)
                    {
                        var name = MakeModelName(item, true, providerState.CurrentProviderId.Contains("google"));
                        res[name] = item;
                    }
                }
            }

            return res;
        }

        private static string MakeModelName(Model model, bool longName = false, bool isGoogle = false)
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

        private static void Internal_Change_checkBoxUseCustomModel_Checked(IntentoFormOptionsMT formMt, bool value)
        {
            InternalControlChange = formMt.checkBoxUseCustomModel.Checked != value;
            formMt.checkBoxUseCustomModel.Checked = value;
        }

        #endregion Events

        #region methods for managing a group of controls

        private void Clear(bool clearTextBox = true, bool clearComboBox = true)
        {
            InternalControlChange = true;
            if (clearComboBox)
                FormMt.comboBoxModels.Items.Clear();
            if (clearTextBox)
                FormMt.textBoxModel.Text = string.Empty;
            InternalControlChange = false;
        }

        private static void ModelGroupEnabled(IntentoFormOptionsMT formMt, bool value)
        {
            if (value)
            {
                formMt.groupBoxModel.Enabled = true;
            }
            else
            {
                formMt.groupBoxModel.Enabled = false;
                formMt.comboBoxModels.Visible = false;
                formMt.textBoxModel.Visible = false;
                Internal_Change_checkBoxUseCustomModel_Checked(formMt, false);
                formMt.textBoxModel.Text = "";
                formMt.comboBoxModels.Items.Clear();
            }
        }

        private void ModelControlBackColorState(bool hasErrors)
        {
            FormMt.comboBoxModels.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
            FormMt.textBoxModel.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
        }

        private void OptionalGroupEnabled(bool value)
        {
            if (value)
            {
                // temporary! formMT.groupBoxOptional.Enabled = value;
                FormMt.groupBoxOptional.Enabled = true;
            }
            else
            {
                FormMt.groupBoxOptional.Enabled = UseModel || Options.Glossary != null;
            }
        }

        #endregion methods for managing a group of controls
    }
}