using IntentoMT.Plugin.PropertiesForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IntentoMT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{
    public interface IForm
    {
        ApiKeyState ApiKeyState { get; }

        // ApiKey textBox (apiKey_tb)
        string ApiKey_TextBox_Text { get; set; }
        bool ApiKey_TextBox_Enabled { set; }
        Color ApiKey_TextBox_BackColor { set; }

        // ApiKey Check Button ()
        bool ApiKeyCheck_Button_Enabled { get; set; }

        // Continue Button (buttonContinue)
        bool Continue_Button_Enabled { get; set; }

        // SmartRouting_CheckBox (checkBoxSmartRouting)
        bool SmartRouting_CheckBox_Checked { get; set; }
        bool SmartRouting_CheckBox_Visible { set; }
        bool SmartRouting_CheckBox_Enabled { set; }

        // Providers_ComboBox (comboBoxProviders)
        void Providers_ComboBox_Clear();
        void Providers_ComboBox_AddRange(object[] items);
        string Providers_ComboBox_SelectedItem { set; }
        string Providers_ComboBox_Text { get; }
        Color Providers_ComboBox_BackColor { set; }

        // Providers_GroupBox (groupBoxProviderSettings)
        bool Providers_Group_Visible { set; }
        bool Providers_Group_Enabled { set; }

        // Auth_CheckBox (checkBoxUseOwnCred)
        bool Auth_CheckBox_Visible { set; }
        bool Auth_CheckBox_Enabled { set; }
        bool Auth_CheckBox_Checked { get; set; }

        // AuthText_Group (groupBoxAuth)
        bool AuthText_Group_Visible { set; }

        // AuthText_TextBox (textBoxCredentials)
        Color AuthText_TextBox_BackColor { set; }
        string AuthText_TextBox_Text { set; }

        // AuthCombo_Group (groupBoxAuthCredentialId)
        bool AuthCombo_Group_Visible { set; }

        // AuthCombo_ComboBox (comboBoxCredentialId)
        void AuthCombo_ComboBox_Clear();
        void AuthCombo_ComboBox_AddRange(object[] items);
        void AuthCombo_ComboBox_Insert(int n, string text);
        bool AuthCombo_ComboBox_Contains(string text);
        object AuthCombo_ComboBox_SelectedItem { set; get; }
        bool AuthCombo_ComboBox_Enabled { set; }
        int AuthCombo_ComboBox_Count { get; }
        int AuthCombo_ComboBox_SelectedIndex { set; }
        Color AuthCombo_ComboBox_BackColor { set; }
        string AuthCombo_ComboBox_Text { get; }

        // Model_CheckBox (checkBoxUseCustomModel)
        bool Model_CheckBox_Checked { get; set; }
        bool Model_CheckBox_Visible { set; }
        bool Model_CheckBox_Enabled { set; }

        // Model_Group (groupBoxModel)
        bool Model_Group_Visible { set; }
        bool Model_Group_Enabled { set; }

        // Model_ComboBox (comboBoxModels)
        void Model_ComboBox_Clear();
        int Model_ComboBox_Add(string text);
        int Model_ComboBox_SelectedIndex { set; }
        int Model_ComboBox_Count { get; }
        bool Model_ComboBox_Visible { set; }
        Color Model_ComboBox_BackColor { set; }
        string Model_ComboBox_Text { get; set; }

        // Model_TextBox (textBoxModel)
        bool Model_TextBox_Visible { set; }
        string Model_TextBox_Text { get; set; }
        Color Model_TextBox_BackColor { set; }

        // Glossary_Group (groupBoxGlossary)
        bool Glossary_Group_Visible { get; set; }

        // Glossary_TextBox (textBoxGlossary)
        string Glossary_TextBox_Text { get; set; }
        bool Glossary_TextBox_Visible { set; }

        // Glossary_ComboBox (comboBoxGlossaries)
        void Glossary_ComboBox_Clear();
        int Glossary_ComboBoxAdd(string text);
        void Glossary_ComboBox_Insert(int n, string text);
        int Glossary_ComboBox_SelectedIndex { set; }
        bool Glossary_ComboBox_Visible { set; }
        string Glossary_ComboBox_Text { get; }

        // ErrorMessage TextBox (toolStripStatusLabel1)
        string ErrorMessage_TextBox_Text { get; set; }
        Color ErrorMessage_TextBox_BackColor { set; }

        // SaveApiKeyInRegistry CheckBox ()
        bool SaveApiKeyInRegistry_CheckBox_Checked { get; set; }

        // ShowHidden_CheckBox (checkBoxShowHidden)
        bool ShowHidden_CheckBox_Checked { get; }

        // Intento API
        IEnumerable<dynamic> Providers(Dictionary<string, string> filter);
        dynamic Provider(string provider, string additionaParams);
        IList<dynamic> DelegatedCredentials();
        IList<dynamic> Models(string provider, Dictionary<string, string> credential_id);
        IList<dynamic> Glossaries(string provider, Dictionary<string, string> credential_id);

        // Other
        int CursorCount { get; set; }
        Cursor Cursor { set; }
        void SuspendLayout();
        void ResumeLayout();
        List<string> Errors { get; set; }
        LangPair[] LanguagePairs { get; }
    } 
}
