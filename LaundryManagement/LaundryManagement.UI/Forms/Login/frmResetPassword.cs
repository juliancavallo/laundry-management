using LaundryManagement.BLL;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmResetPassword : Form
    {
        private UserBLL userBLL;
        private SecurityService securityService;
        public frmResetPassword()
        {
            userBLL = new UserBLL();
            securityService = new SecurityService();
            InitializeComponent();
            this.ApplySetup();
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

                FormValidation.ShowMessage("The password was successfully reset", ValidationType.Info);

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
    }
}
