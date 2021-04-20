using Intento.MT.Plugin.PropertiesForm;
using Intento.MT.Plugin.PropertiesForm.WinForms;
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
    public class ModelState : BaseState
    {
        ProviderState providerState;
        AuthState authState;

        public StateModeEnum modelMode = StateModeEnum.unknown;
        bool isList;
        bool firstTimeDraw = true;
        // Indicator of change of control status from the inside
        // in this case no change event call is required
        public static bool internalControlChange = false;

        // List of custom models obtained from provider
        private Dictionary<string, dynamic> models;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public ModelState(AuthState authState, IntentoMTFormOptions _options) : base(authState, _options)
        {
            this.authState = authState;
            providerState = authState.providerState;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, ModelState state)
        {
            if (state == null)
            {
                Model_Group_Enabled(form.formMT, false);
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            FillModelControls();
            firstTimeDraw = false;
            Model_Control_BackColor_State(false);
            if (modelMode == StateModeEnum.prohibited)
                return null;

            string errorMessage = null;

            Model_Group_Enabled(form.formMT, true);

            // set state of checkBoxUseCustomModel
            if (modelMode == StateModeEnum.required && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.ModelRequiredMessage;
            else if (UseModel && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.CustomModelRequiredMessage;

            // set back color 
            Model_Control_BackColor_State(!string.IsNullOrEmpty(errorMessage));
			if (SelectedModelFrom != null && providerState.fromLanguages != null)
				formMT.comboBoxFrom.SelectedItem = providerState.fromLanguages[SelectedModelFrom];
			if (SelectedModelTo != null && providerState.toLanguages != null)
				formMT.comboBoxTo.SelectedItem = providerState.toLanguages[SelectedModelTo];

			return errorMessage;
        }

        #region Properties

        private string ModelName
        {
            get
            {
                if (!UseModel)
                    return null;

                if (!isList)
                {   // Use model name form text box
                    if (string.IsNullOrEmpty(formMT.textBoxModel.Text))
                        return null;
                    return formMT.textBoxModel.Text;
                }

                // Use model name from combo box
                dynamic d;
                if (models.TryGetValue(formMT.comboBoxModels.Text, out d))
                {
                    string modelName = (string)models[formMT.comboBoxModels.Text].id;
                    if (string.IsNullOrEmpty(modelName))
                        return null;
                    return modelName;
                }

                return null;
            }
        }

        public string To
        {
            get
            {
                return ModelName != null && formMT.comboBoxTo.SelectedIndex != -1 ?
                authState.providerState.toLanguages.Where(x => x.Value == formMT.comboBoxTo.Text).First().Key : "es";
            }
        }
        public string From
        {
            get
            {
                return ModelName != null && formMT.comboBoxFrom.SelectedIndex != -1 ?
                authState.providerState.fromLanguages.Where(x => x.Value == formMT.comboBoxFrom.Text).First().Key : "en";
            }
        }

		public string SelectedModelTo
		{
			get
			{
				if (!UseModel || !isList || ModelName == null)
					return null;
				dynamic model =models[formMT.comboBoxModels.Text];
				return (string)model.to;
			}
		}
		public string SelectedModelFrom
		{
			get
			{
				if (!UseModel || !isList || ModelName == null)
					return null;
				dynamic model = models[formMT.comboBoxModels.Text];
				return (string)model.from;
			}
		}



		public bool UseModel
        { get { return formMT.checkBoxUseCustomModel.Checked; } }

        #endregion Properties

        private void FillModelControls()
        {   
            // Fill combo or text box depending on provider features
            if (!authState.IsOK)
                return;

            if (authState.providerState.smartRoutingState.SmartRouting)
                modelMode = StateModeEnum.prohibited;
            else if (!authState.providerState.stock_model && authState.providerState.custom_model)
                modelMode = StateModeEnum.required;
            else if (!authState.providerState.custom_model)
                modelMode = StateModeEnum.prohibited;
            else
                modelMode = StateModeEnum.optional;

            if (modelMode == StateModeEnum.prohibited)
            {
                Model_Group_Enabled(formMT, false);
				Clear();
				return;
            }

            ReadModels();

            Model_Group_Enabled(formMT, true);

            formMT.checkBoxUseCustomModel.Enabled = true;

            if (isList)
            {
				Clear();
				if (modelMode == StateModeEnum.required)
                {
                    Internal_Change_checkBoxUseCustomModel_Checked(formMT, true);
                    formMT.checkBoxUseCustomModel.Enabled = false;
                }
                else
                {
                    Internal_Change_checkBoxUseCustomModel_Checked(formMT, options.UseCustomModel);
                }
                formMT.comboBoxModels.Visible = UseModel;
                formMT.textBoxModel.Visible = false;

                if (models.Count > 1 || modelMode == StateModeEnum.optional)
                    formMT.comboBoxModels.Items.Add("");

                // Fill comboBoxModels and choose SelectedIndex
                foreach (string x in models.Select(x => (string)x.Key).OrderBy(x => x))
                {
                    int n = formMT.comboBoxModels.Items.Add(x);
                    if (models[x].id == options.CustomModel)
                        formMT.comboBoxModels.SelectedIndex = n;
                }

                if (formMT.comboBoxModels.Items.Count == 1)
                {
                    formMT.comboBoxModels.SelectedIndex = 0;
                }

            }
            else
            {   // Text box to enter model
                formMT.textBoxModel.Visible = UseModel;
                formMT.comboBoxModels.Visible = false;

                if (modelMode == StateModeEnum.optional)
                {
                    formMT.checkBoxUseCustomModel.Enabled = true;
                    Internal_Change_checkBoxUseCustomModel_Checked(formMT, options.UseCustomModel);
                }
                else
                {
                    formMT.checkBoxUseCustomModel.Enabled = false;
                    Internal_Change_checkBoxUseCustomModel_Checked(formMT, true);
                }
				if (options.UseCustomModel)
					formMT.textBoxModel.Text = options.CustomModel;
				else
					Clear(clearTextBox: false);

			}
        }

        public static void FillOptions(ModelState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.UseCustomModel = false;
                options.CustomModel = null;
                options.GlossaryMode = StateModeEnum.unknown;
                options.CustomModelName = null;
                options.FromLanguage = null;
                options.ToLanguage = null;
            }
            else
            {
                options.UseCustomModel = state.UseModel;
                options.CustomModel = state.ModelName;
                options.CustomModelMode = state.modelMode;
                dynamic mData = null;
                if (state.models != null)
                    mData = state.models.Select(x => (dynamic)x.Value).Where(y => (string)y.id == state.ModelName).FirstOrDefault();
                options.CustomModelName = mData != null ? mData.name : state.ModelName;
                options.FromLanguage = options.CustomModelName == null || !options.UseCustomModel ? null : state.From;
                options.ToLanguage = options.CustomModelName == null || !options.UseCustomModel ? null : state.To;

            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomModel = false;
            options.CustomModel = null;
            Model_Group_Enabled(formMT, false);

            Clear();
        }

        bool readDone = false;
        public void ReadModels()
        {
            if (readDone)
                return;
            readDone = true;

            models = new Dictionary<string, dynamic>();
            try
            {
                IList<dynamic> providerModelsRec = form.testModelData != null ? form.testModelData :
                    form._translate.Models(
                    authState.providerState.currentProviderId, 
                    authState.UseCustomAuth ? authState.providerDataAuthDict : null);
                if (providerModelsRec != null)
                    models = ProcessModels(providerState, providerModelsRec);
                isList = true;

                // Temporary! Empty list means that manual entry is possbile
                if (models.Count == 0)
                    isList = false;
            }
            catch {
                isList = false;
            }

            return;
        }

        #region Events

        public void checkBoxUseCustomModel_CheckedChanged()
        {
            formMT.textBoxModel.Visible = UseModel;
            formMT.comboBoxModels.Visible = UseModel;
            Optional_Group_Enabled(UseModel);
            if (internalControlChange) return;
            options.UseCustomModel = UseModel;
            EnableDisable();
        }

        public void modelControls_ValueChanged()
        {
            if (!firstTimeDraw && !internalControlChange)
			if (!firstTimeDraw && !internalControlChange)
                ModelState.FillOptions(this, options);
            EnableDisable();
        }

        static void Internal_Change_checkBoxUseCustomModel_Checked(IntentoFormOptionsMT formMT, bool value)
        {
            internalControlChange = formMT.checkBoxUseCustomModel.Checked != value;
            formMT.checkBoxUseCustomModel.Checked = value;
        }

        #endregion Events

        #region methods for managing a group of controls

        void Clear(bool clearTextBox = true, bool clearComboBox = true)
        {
            internalControlChange = true;
			if (clearComboBox)
				formMT.comboBoxModels.Items.Clear();
			if (clearTextBox)
				formMT.textBoxModel.Text = string.Empty;
            internalControlChange = false;
        }

        static void Model_Group_Enabled(IntentoFormOptionsMT formMT, bool value)
        {
            if (value)
            {
                formMT.groupBoxModel.Enabled = value;
            }
            else
            {
                formMT.groupBoxModel.Enabled = false;
                formMT.comboBoxModels.Visible = false;
                formMT.textBoxModel.Visible = false;
                Internal_Change_checkBoxUseCustomModel_Checked(formMT, false);
                formMT.textBoxModel.Text = "";
                formMT.comboBoxModels.Items.Clear();
            }
        }

        void Model_Control_BackColor_State(bool hasErrors)
        {

            formMT.comboBoxModels.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
            formMT.textBoxModel.BackColor = hasErrors ? Color.LightPink : SystemColors.Window;
            //if (hasErrors)
            //{
            //    formMT.comboBoxModels.BackColor = Color.LightPink;
            //    formMT.textBoxModel.BackColor = Color.LightPink;
            //}
            //else
            //{
            //    formMT.comboBoxModels.BackColor = formMT.comboBoxModels.Enabled ? SystemColors.Window : SystemColors.Window;
            //    formMT.textBoxModel.BackColor = formMT.comboBoxModels.Enabled ? SystemColors.Window : SystemColors.Window;
            //}
        }

        void Optional_Group_Enabled(bool value)
        {
            if (value)
            {
                // temporary! formMT.groupBoxOptional.Enabled = value;
                formMT.groupBoxOptional.Enabled = true;
            }
            else
            {
                //formMT.groupBoxOptional.Enabled = options.UseCustomModel
                //    || !string.IsNullOrWhiteSpace(authState?.GetGlossaryState()?.currentGlossary);

                formMT.groupBoxOptional.Enabled = UseModel || options.Glossary != null;
            }
        }

        #endregion methods for managing a group of controls

    }
}
