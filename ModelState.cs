﻿using Intento.MT.Plugin.PropertiesForm;
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
            form.Model_ComboBox_Clear();
            form.Model_TextBox_Text = string.Empty;
            form.Model_CheckBox_Checked = false;
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
                form.Model_Group_Enabled = false;
                //form.Model_CheckBox_Visible = false;
                return;
            }

            ReadModels();

            form.Model_Group_Enabled = true;
            //form.Model_CheckBox_Visible = true;

            if (isList)
            {
                form.Model_ComboBox_Visible = true;
                form.Model_TextBox_Visible = false;

                if (models.Count > 1 || modelMode == StateModeEnum.optional)
                    form.Model_ComboBox_Add("");

                // Fill comboBoxModels and choose SelectedIndex
                foreach (string x in models.Select(x => (string)x.Key).OrderBy(x => x))
                {
                    int n = form.Model_ComboBox_Add(x);
                    if (models[x].id == options.CustomModel)
                        form.Model_ComboBox_SelectedIndex = n;
                }
                form.Model_ComboBox_Visible = !(form.Model_TextBox_Visible = false);
                if (form.Model_ComboBox_Count == 1)
                {
                    form.Model_ComboBox_SelectedIndex = 0;
                }

                if (modelMode == StateModeEnum.required)
                {
                    form.Model_CheckBox_Checked = true;
                    form.Model_CheckBox_Enabled = false;
                }
                else
                {
                    form.Model_CheckBox_Checked = options.UseCustomModel;
                    form.Model_CheckBox_Enabled = true;
                }
            }
            else
            {   // Text box to enter model
                form.Model_TextBox_Visible = true;
                form.Model_ComboBox_Visible = false;

                if (modelMode == StateModeEnum.optional)
                    form.Model_CheckBox_Enabled = true;
                else
                {
                    form.Model_CheckBox_Enabled = false;
                    form.Model_CheckBox_Checked = true;
                }
            }
        }

        public static string Draw(IForm form, ModelState state)
        {
            if (state == null)
            {
                // form.Model_TextBox_Visible = false;
                // form.Model_ComboBox_Visible = false;
                //form.Model_CheckBox_Visible = false;
                form.Model_Group_Enabled = false;
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            FillModelControls();

            if (modelMode == StateModeEnum.prohibited)
                return null;

            string errorMessage = null;

            form.Model_Group_Enabled = true;

            // set state of checkBoxUseCustomModel
            if (modelMode == StateModeEnum.required && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.ModelRequiredMessage;
            else if (form.Model_CheckBox_Checked && string.IsNullOrEmpty(ModelName))
                errorMessage = Resource.CustomModelRequiredMessage;

            // set back color 
            form.Model_Control_BackColor_State(!string.IsNullOrEmpty(errorMessage));

            return errorMessage;
        }

        public void checkBoxUseCustomModel_CheckedChanged()
        {
            options.UseCustomModel = form.Model_CheckBox_Checked;
            form.Model_CheckBox_Checked = form.Model_CheckBox_Checked;
            form.Optional_Group_Enabled = form.Model_CheckBox_Checked;
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
                    if (string.IsNullOrEmpty(form.Model_TextBox_Text))
                        return null;
                    return form.Model_TextBox_Text;
                }

                // Use model name from combo box
                dynamic d;
                if (models.TryGetValue(form.Model_ComboBox_Text, out d))
                {
                    string modelName = (string)models[form.Model_ComboBox_Text].id;
                    if (string.IsNullOrEmpty(modelName))
                        return null;
                    return modelName;
                }

                return null;
            }
        }

        public bool UseModel
        { get { return form.Model_CheckBox_Checked; } }

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
                IList<dynamic> providerModelsRec = form.Models(
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


    }
}
