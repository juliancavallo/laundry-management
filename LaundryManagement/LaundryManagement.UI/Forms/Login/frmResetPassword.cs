using LaundryManagement.BLL;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmResetPassword : Form, ILanguageObserver
    {
        private UserBLL userBLL;
        private TranslatorBLL translatorBLL;
        private IList<Control> controls;
        private SecurityService securityService;
        public frmResetPassword(ILanguage language)
        {
            userBLL = new UserBLL();
            translatorBLL = new TranslatorBLL();
            securityService = new SecurityService();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this.lblEmail, this.lblPassword, this.lblRepeatPassword, this, this.btnAccept};
            Translate(language);
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPasswordRepeated.PasswordChar = '*';

            this.txtEmail.TabIndex = 0;
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPasswordRepeated.TabIndex = 2;
            this.lblEmail.Tag = "Email";
            this.lblPassword.Tag = "NewPassword";
            this.lblRepeatPassword.Tag = "RepeatPassword";
            this.btnAccept.Tag = "Accept";
            this.Tag = "ResetPassword";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>
                {
                    this.txtEmail, this.txtNewPassword, this.txtNewPasswordRepeated
                });
                FormValidation.ValidatePasswordMatch(this.txtNewPassword.Text, this.txtNewPasswordRepeated.Text);

                var policies = new PasswordPolicies();
                if (!securityService.CheckPasswordSecurity(this.txtNewPassword.Text, policies))
                {
                    FormValidation.ShowPasswordUnsecureMessage(policies);
                    return;
                }

                userBLL.ResetPassword(this.txtEmail.Text, this.txtNewPassword.Text);

                FormValidation.ShowMessage(Session.Translations[Tags.PasswordReset].Text, ValidationType.Info);

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

        public void UpdateLanguage(ILanguage language) => Translate(language);

        private void Translate(ILanguage language = null)
        {
            var translations = translatorBLL.GetTranslations((Language)language);

            FormValidation.Translate(translations, controls);
        }

        private void frmResetPassword_Load(object sender, EventArgs e) => Session.SubsribeObserver(this);

        private void frmResetPassword_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubsribeObserver(this);
    }
}
