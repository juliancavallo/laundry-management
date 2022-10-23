using LaundryManagement.BLL;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Integrity
{
    public partial class frmIntegrity : Form, ILanguageObserver
    {
        private CheckDigitBLL _checkDigitBLL;
        private TranslatorBLL _translatorBLL;
        private bool _integrityRestored = false;

        private IEnumerable<ICheckDigitEntity> _horizontalCorruptedEntities;
        private IEnumerable<Type> _verticalCorruptedEntities;
        private IList<Control> controls;

        public frmIntegrity(IEnumerable<ICheckDigitEntity> horizontalCorruptedEntities, IEnumerable<Type> verticalCorruptedEntities)
        {
            InitializeComponent();

            _checkDigitBLL = new CheckDigitBLL();
            _translatorBLL = new TranslatorBLL();
            _horizontalCorruptedEntities = horizontalCorruptedEntities;
            _verticalCorruptedEntities = verticalCorruptedEntities; 

            controls = new List<Control>() { this, this.txtHeader, this.lblUser, this.lblPassword, 
                this.btnValidateCredentials, this.btnViewEntities, this.btnAccept, this.radioRecalculate, 
                this.radioLastBackup };

            ApplySetup();
            Translate();
            PopulateComboLanguages();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.btnViewEntities.Enabled = false;

            this.txtPassword.PasswordChar = '*';
            this.comboLanguages.DropDownStyle = ComboBoxStyle.DropDownList;
            this.txtHeader.Multiline = true;
            this.txtHeader.Size = new System.Drawing.Size(357, 58);
            this.txtHeader.Enabled = false;
            this.radioLastBackup.Enabled = false;
            this.radioRecalculate.Enabled = false;
            this.btnAccept.Enabled = false;

            this.Tag = "Integrity";
            this.lblUser.Tag = "User";
            this.lblPassword.Tag = "Password";
            this.btnValidateCredentials.Tag = "ValidateCredentials";
            this.btnViewEntities.Tag = "ViewCorruptedEntities";
            this.btnAccept.Tag = "Accept";
            this.radioRecalculate.Tag = "IntegrityRecalculate";
            this.radioLastBackup.Tag = "IntegrityLastBackup";

            if (_horizontalCorruptedEntities.Count() > 0 || _verticalCorruptedEntities.Count() > 0)
                this.txtHeader.Tag = "IntegrityAdminCredentials";

        }

        private void PopulateComboLanguages()
        {
            this.comboLanguages.DataSource = null;
            this.comboLanguages.DataSource = _translatorBLL.GetAllLanguages();
            this.comboLanguages.DisplayMember = "Name";
            this.comboLanguages.ValueMember = "Id";

            this.comboLanguages.SelectedValue = _translatorBLL.GetDefaultLanguage().Id;
        }

        private void frmIntegrity_Load(object sender, EventArgs e)
        {
            try
            {
                Session.SetTranslations(_translatorBLL.GetTranslations());
                Session.SubscribeObserver(this);

            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmIntegrity_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.UnsubscribeObserver(this);

            if (!_integrityRestored)
                Application.Exit();
        }

        private void comboLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = ((ComboBox)sender).SelectedItem as ILanguage;

                Session.SetTranslations(_translatorBLL.GetTranslations((Language)selectedItem));

                Translate();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedAction = this.controls
                    .OfType<RadioButton>()
                    .FirstOrDefault(x => x.Checked)?
                    .Tag
                    .ToString();
                if (selectedAction == null)
                    throw new ValidationException("Must select an option", ValidationType.Warning);

                _checkDigitBLL.RestoreIntegrity(selectedAction);

                _integrityRestored = true;
                this.Close();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void btnViewEntities_Click(object sender, EventArgs e)
        {
            var frm = new frmCorruptedEntities(_horizontalCorruptedEntities, _verticalCorruptedEntities);
            frm.ShowDialog();
        }

        private void btnValidateCredentials_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>
                {
                    this.txtUser, this.txtPassword
                });

                _checkDigitBLL.ValidateAdminCredentials(this.txtUser.Text, this.txtPassword.Text);

                this.btnViewEntities.Enabled = true;
                this.txtPassword.Enabled = false;
                this.txtUser.Enabled = false;
                this.btnValidateCredentials.Enabled = false;
                this.radioLastBackup.Enabled = true;
                this.radioRecalculate.Enabled = true;
                this.btnAccept.Enabled = true;

            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }
    }
}
