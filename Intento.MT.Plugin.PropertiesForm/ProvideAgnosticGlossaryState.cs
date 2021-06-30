using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Intento.MT.Plugin.PropertiesForm.IntentoMTFormOptions;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class ProvideAgnosticGlossaryState: BaseState
    {
        AuthState authState;
        public StateModeEnum mode = StateModeEnum.unknown;
        public static bool internalControlChange = false;

        private IList<dynamic> glossaries;
        private IList<dynamic> types;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_options"></param>
        /// <param name="fromForm">Call from event, change of form check box may result recourcing</param>
        public ProvideAgnosticGlossaryState(AuthState authState, IntentoMTFormOptions _options, bool fromForm = false) : base(authState, _options)
        {
            this.authState = authState;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, ProvideAgnosticGlossaryState state)
        {
            if (state == null)
            {
                HideVisibility(form.formMT);
                return null;
            }
            return state.Draw();
        }

        public string Draw()
        {
            FillGlossaryControls();

            string errorMessage = null;
            if (!authState.providerState.intento_glossary)
            {
                formMT.providerAgnosticGlossariesGroup.Visible = false;
                formMT.listOfIntentoGlossaries.Visible = false;
            }
            else
            {
                formMT.listOfIntentoGlossaries.Visible = true;
                formMT.providerAgnosticGlossariesGroup.Visible = true;
            }           
            return errorMessage;
        }

        public void glossaryControls_ValueChanged()
        {
            if (!internalControlChange)
            {
                FillOptions(this, options);
            }
            EnableDisable();
        }

        public int[] GetGlossaries()
        {
            ReadGlossaries();
            if (glossaries == null)
            {
                return new int[0];
            }
            var checkedElements = formMT.listOfIntentoGlossaries.CheckedIndices.Cast<int>().ToList();

            var result = new List<int>();
            foreach (var index in checkedElements)
            {
                result.Add(Convert.ToInt32(glossaries[index].id.Value));
            }
            return result.ToArray();          
        }

        public static void FillOptions(ProvideAgnosticGlossaryState state, IntentoMTFormOptions options)
        {
            if (state == null)
            {
                options.IntentoGlossaries = null;
            }
            else
            {
                options.IntentoGlossaries = state.GetGlossaries();                              
            }
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.IntentoGlossaries = null;
            HideVisibility(formMT);
        }

        private void Clear(bool clearTextBox = true, bool clearComboBox = true)
        {
            internalControlChange = true;
            if (clearComboBox)
            {
                formMT.listOfIntentoGlossaries.Items.Clear();
            }
            internalControlChange = false;
        }

        private void FillGlossaryControls()
        {
            if (!authState.IsOK)
                return;

            if (!authState.providerState.intento_glossary)
            {   // glossaries are not supported by provider
                formMT.providerAgnosticGlossariesGroup.Visible = false;
                Clear();
                return;
            }
            formMT.providerAgnosticGlossariesGroup.Visible = true;

            ReadGlossaries();
            ReadTypes();

            Clear();

            if (glossaries != null)
            {
                // Fill Glossary and choose SelectedIndex
                foreach (var x in glossaries)
                {
                    var type = types?.FirstOrDefault(t => t.id == x.cs_type);
                    var name = x.name.Value + (type != null ? $" ({type.name})" : "");
                    int n = (int)formMT.listOfIntentoGlossaries.Items.Add(name);
                    var currentValue = (int)Convert.ToInt32(glossaries[n].id.Value);
                    if (options.IntentoGlossaries != null && options.IntentoGlossaries.Contains(currentValue))
                    {
                        formMT.listOfIntentoGlossaries.SetItemChecked(n, true);
                    }
                }
                formMT.comboBoxGlossaries.Visible = true;
            }
        }

        private bool readDone = false;
        private void ReadGlossaries()
        {
            if (readDone)
            {
                return;
            }
            try
            {
                glossaries = form._translate.AgnosticGlossaries();
                readDone = true;
            }
            catch
            {
                readDone = false;
            }
        }

        private bool readTypesDone = false;

        private void ReadTypes()
        {
            if (readTypesDone)
            {
                return;
            }
            try
            {
                types = form._translate.AgnosticGlossariesTypes();
                readTypesDone = true;
            }
            catch
            {
                readTypesDone = false;
            }
        }

        #region methods for managing a group of controls

        private static void HideVisibility(IntentoFormOptionsMT formMT)
        {
            formMT.providerAgnosticGlossariesGroup.Visible = false;
            formMT.listOfIntentoGlossaries.Visible = false;          
            formMT.listOfIntentoGlossaries.Items.Clear();
        }

        #endregion methods for managing a group of controls
    }
}
