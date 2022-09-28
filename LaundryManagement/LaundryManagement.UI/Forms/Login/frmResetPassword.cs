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
        private LogBLL logBLL;
        private IList<Control> controls;
        private SecurityService securityService;
        public frmResetPassword(ILanguage language)
        {
            userBLL = new UserBLL();
            translatorBLL = new TranslatorBLL();
            logBLL = new LogBLL();
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

                if (!securityService.CheckPasswordSecurity(this.txtNewPassword.Text))
                {
                    FormValidation.ShowPasswordUnsecureMessage();
                    return;
                }

                userBLL.ResetPassword(this.txtEmail.Text, this.txtNewPassword.Text);

                FormValidation.ShowMessage(Session.Translations[Tags.PasswordReset], ValidationType.Info);

                this.Close();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);

                switch (ex.ValidationType)
                {
                    case ValidationType.Warning:
                        logBLL.LogWarning(MovementTypeEnum.ResetPassword, ex.Message);
                        break;

                    case ValidationType.Error:
                        logBLL.LogError(MovementTypeEnum.ResetPassword, ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                logBLL.LogError(MovementTypeEnum.ResetPassword, ex.Message);
            }
        }

        public void UpdateLanguage(ILanguage language) => Translate(language);

        private void Translate(ILanguage language = null)
        {
            try
            {
                var translations = translatorBLL.GetTranslations((Language)language);

                FormValidation.Translate(translations, controls);
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

        private void frmResetPassword_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmResetPassword_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);
    }
}
