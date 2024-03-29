﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.Translate;
using Intento.SDK.Translate.DTO;

namespace Intento.MT.Plugin.PropertiesForm.States
{
    public class ProvideAgnosticGlossaryState : BaseState
    {
        private readonly AuthState authState;
        private static bool _internalControlChange;

        private IList<AgnosticGlossaryType> types;

        private IList<GlossaryDetailed> allGlossaries;

        private ITranslateService TranslateService => Form.Locator.Resolve<ITranslateService>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="authState"></param>
        /// <param name="options"></param>
        public ProvideAgnosticGlossaryState(AuthState authState, IntentoMTFormOptions options) : base(authState,
            options)
        {
            this.authState = authState;
        }

        public static string Draw(IntentoTranslationProviderOptionsForm form, ProvideAgnosticGlossaryState state)
        {
            if (state != null)
            {
                return state.Draw();
            }

            HideVisibility(form.FormMt);
            return null;
        }

        private string Draw()
        {
            FillGlossaryControls();
            if (!authState.ProviderState.IntentoGlossary)
            {
                FormMt.providerAgnosticGlossariesGroup.Visible = false;
                FormMt.listOfIntentoGlossaries.Visible = false;
            }
            else
            {
                FormMt.listOfIntentoGlossaries.Visible = true;
                FormMt.providerAgnosticGlossariesGroup.Visible = true;
            }

            return null;
        }

        public void glossaryControls_ValueChanged()
        {
            if (!_internalControlChange)
            {
                FillOptions(this, Options);
            }

            EnableDisable();
        }

        private int[] GetGlossaries()
        {
            ReadGlossaries();
            if (allGlossaries == null)
            {
                return new int[0];
            }

            var checkedElements = FormMt.listOfIntentoGlossaries.CheckedIndices.Cast<int>().ToList();

            return checkedElements.Select(index => Convert.ToInt32(allGlossaries[index].Id)).ToArray();
        }

        public IEnumerable<GlossaryDetailed> GetGlossariesDetailed(int[] ids = null)
        {
            var selected = ids ?? GetGlossaries();
            return allGlossaries.Where(g => selected.Contains(g.Id)).ToList();
        }

        public static void FillOptions(ProvideAgnosticGlossaryState state, IntentoMTFormOptions options)
        {
            options.IntentoGlossaries = state?.GetGlossaries();
        }

        public void ClearOptions(IntentoMTFormOptions options)
        {
            options.IntentoGlossaries = null;
            HideVisibility(FormMt);
        }

        private void Clear(bool clearComboBox = true)
        {
            _internalControlChange = true;
            if (clearComboBox)
            {
                FormMt.listOfIntentoGlossaries.Items.Clear();
            }

            _internalControlChange = false;
        }

        private void FillGlossaryControls()
        {
            if (!authState.IsOk)
                return;

            if (!authState.ProviderState.IntentoGlossary)
            {
                // AllGlossaries are not supported by provider
                FormMt.providerAgnosticGlossariesGroup.Visible = false;
                Clear();
                return;
            }

            FormMt.providerAgnosticGlossariesGroup.Visible = true;

            ReadGlossaries();
            ReadTypes();

            Clear();

            if (allGlossaries == null) return;
            // Fill Glossary and choose SelectedIndex
            foreach (var x in allGlossaries)
            {
                var type = types?.FirstOrDefault(t => t.Id == x.CsType.ToString());
                var name = x.Name + (type != null ? $" ({type.Name})" : "");
                var n = FormMt.listOfIntentoGlossaries.Items.Add(name);
                var currentValue = Convert.ToInt32(allGlossaries[n].Id);
                if (Options.IntentoGlossaries != null && Options.IntentoGlossaries.Contains(currentValue))
                {
                    FormMt.listOfIntentoGlossaries.SetItemChecked(n, true);
                }
            }
        }

        private bool readDone;

        private void ReadGlossaries()
        {
            if (readDone)
            {
                return;
            }

            try
            {
                allGlossaries = TranslateService.AgnosticGlossaries();
                readDone = true;
            }
            catch
            {
                readDone = false;
            }
        }

        private bool readTypesDone;

        private void ReadTypes()
        {
            if (readTypesDone)
            {
                return;
            }

            try
            {
                types = TranslateService.AgnosticGlossariesTypes();
                readTypesDone = true;
            }
            catch
            {
                readTypesDone = false;
            }
        }

        #region methods for managing a group of controls

        private static void HideVisibility(IntentoFormOptionsMT formMt)
        {
            formMt.providerAgnosticGlossariesGroup.Visible = false;
            formMt.listOfIntentoGlossaries.Visible = false;
            formMt.listOfIntentoGlossaries.Items.Clear();
        }

        #endregion methods for managing a group of controls
    }
}