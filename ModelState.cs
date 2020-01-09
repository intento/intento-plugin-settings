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
            Clear();
        }

        void Clear()
        {
            formMT.comboBoxModels.Items.Clear();
            formMT.textBoxModel.Text = string.Empty;
            formMT.checkBoxUseCustomModel.Checked = false;
        }

        bool controls_ok = false;
        private void FillModelControls()
        {   // Fill combo or text box depending on provider features
            if (!authState.IsOK || controls_ok)
                return;

            controls_ok = true;
            Clear();

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
                return;
            }

            ReadModels();

            Model_Group_Enabled(formMT, false);

            if (isList)
            {
                formMT.comboBoxModels.Visible = true;
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
                formMT.comboBoxModels.Visible = !(formMT.textBoxModel.Visible = false);

                if (formMT.comboBoxModels.Items.Count == 1)
                {
                    formMT.comboBoxModels.SelectedIndex = 0;
                }
                if (modelMode == StateModeEnum.required)
                {
                    formMT.checkBoxUseCustomModel.Checked = true;
                    formMT.checkBoxUseCustomModel.Enabled = false;
                }
                else
                {
                    formMT.checkBoxUseCustomModel.Checked = options.UseCustomModel;
                    formMT.checkBoxUseCustomModel.Enabled = true;
                }
            }
            else
            {   // Text box to enter model
                formMT.textBoxModel.Visible = true;
                formMT.comboBoxModels.Visible = false;

                if (modelMode == StateModeEnum.optional)
                {
                    formMT.checkBoxUseCustomModel.Enabled = true;
                }
                else
                {
                    formMT.checkBoxUseCustomModel.Enabled = false;
                    formMT.checkBoxUseCustomModel.Checked = true;
                }
            }
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
            Model_Control_BackColor_State(false);
            if (modelMode == StateModeEnum.prohibited)
                return null;

            string errorMessage = null;

            Model_Group_Enabled(form.formMT, true);

            // set state of checkBoxUseCustomModel
            if (modelMode == StateModeEnum.required && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.ModelRequiredMessage;
            else if (formMT.checkBoxUseCustomModel.Checked && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.CustomModelRequiredMessage;

            // set back color 
            Model_Control_BackColor_State(!string.IsNullOrEmpty(errorMessage));

            return errorMessage;
        }

        public void checkBoxUseCustomModel_CheckedChanged()
        {
            options.UseCustomModel = formMT.checkBoxUseCustomModel.Checked;
            Optional_Group_Enabled(formMT.checkBoxUseCustomModel.Checked);
            EnableDisable();
        }

        public void comboBoxModels_SelectedIndexChanged()
        {

        }

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

        public bool UseModel
        { get { return formMT.checkBoxUseCustomModel.Checked; } }

        public static void FillOptions(ModelState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.UseCustomModel = false;
                options.CustomModel = null;
                options.GlossaryMode = StateModeEnum.unknown;
                options.CustomModelName = null;
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

            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.UseCustomModel = false;
            options.CustomModel = null;
            Model_GroupBox_Disable();

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
                IList<dynamic> providerModelsRec = form._translate.Models(
                    authState.providerState.currentProviderId, 
                    authState.UseCustomAuth ? authState.providerDataAuthDict : null);
                if (providerModelsRec != null)
                    models = providerModelsRec.ToDictionary(s => (string)s.name, q => q);
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

        #region methods for managing a group of controls

        static void Model_Group_Enabled(IntentoFormOptionsMT formMT, bool value)
        {
                //if (!value)
                //    ((IForm)this).Model_Control_BackColor_State(false);
                formMT.groupBoxModel.Enabled = value;
                formMT.comboBoxModels.Enabled = formMT.checkBoxUseCustomModel.Checked;
                formMT.textBoxModel.Enabled = formMT.checkBoxUseCustomModel.Checked;
        }

        void Model_Control_BackColor_State(bool hasErrors)
        {

            if (hasErrors)
            {
                formMT.comboBoxModels.BackColor = Color.LightPink;
                formMT.textBoxModel.BackColor = Color.LightPink;
            }
            else
            {
                formMT.comboBoxModels.BackColor = formMT.comboBoxModels.Enabled ? Color.White : SystemColors.Window;
                formMT.textBoxModel.BackColor = formMT.comboBoxModels.Enabled ? Color.White : SystemColors.Window;
            }
        }

        void Optional_Group_Enabled(bool value)
        {
            if (value)
                formMT.groupBoxOptional.Enabled = value;
            else
            {
                formMT.groupBoxOptional.Enabled = options.UseCustomModel
                    || !string.IsNullOrWhiteSpace(authState?.GetGlossaryState()?.currentGlossary);
            }
        }

        void Model_GroupBox_Disable()
        {
            formMT.groupBoxModel.Enabled = false;
            formMT.comboBoxModels.Visible = false;
            formMT.textBoxModel.Visible = true;
            formMT.checkBoxUseCustomModel.Checked = false;
            formMT.textBoxModel.Text = "";
            formMT.comboBoxModels.Items.Clear();
        }

        #endregion methods for managing a group of controls

    }
}
