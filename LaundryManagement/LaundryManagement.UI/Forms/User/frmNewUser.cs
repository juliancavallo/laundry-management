using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmNewUser : Form
    {
        public UserDTO _userDTO;
        private UserBLL userBLL;
        private SecurityService securityService;
        public frmNewUser(UserDTO paramDTO)
        {
            userBLL = new UserBLL();
            _userDTO = paramDTO;
            securityService = new SecurityService();

            InitializeComponent();
            ApplySetup();
        }

        private void ApplySetup()
        {
            this.txtEmail.Text = _userDTO?.Email;
            this.txtLastName.Text = _userDTO?.LastName;
            this.txtName.Text = _userDTO?.Name;
            this.txtUserName.Text = _userDTO?.UserName;

            this.txtPassword.PasswordChar = '*';
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtPassword.PlaceholderText = _userDTO?.Id == null ? "" : "Type here to change the password";
            this.txtConfirmPassword.Enabled = false;

            this.txtName.TabIndex = 0;
            this.txtLastName.TabIndex = 1;
            this.txtUserName.TabIndex = 2;
            this.txtEmail.TabIndex = 3;
            this.txtPassword.TabIndex = 4;
            this.txtConfirmPassword.TabIndex = 5;
            this.btnSave.TabIndex = 6;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool validatePasswords = this.txtPassword.Text.Length > 0 || _userDTO == null;
                var textboxes = new List<TextBox>
                {
                    this.txtEmail, this.txtLastName, this.txtName, this.txtUserName
                };

                if(validatePasswords)
                {
                    textboxes.Add(this.txtPassword);
                    textboxes.Add(this.txtConfirmPassword);
                }

                FormValidation.ValidateTextBoxCompleted(textboxes);
                FormValidation.ValidateEmailFormat(this.txtEmail.Text);

                if (validatePasswords)
                {
                    FormValidation.ValidatePasswordMatch(this.txtConfirmPassword.Text, this.txtPassword.Text);

                    var securityParams = new PasswordPolicies();
                    if (!securityService.CheckPasswordSecurity(this.txtPassword.Text, securityParams))
                    {
                        FormValidation.ShowPasswordUnsecureMessage(securityParams);
                        return;
                    }
                }

                var password = validatePasswords ? Encryptor.Hash(this.txtPassword.Text.Trim()) : _userDTO.Password;

                var userDTO = new UserDTO()
                {
                    Id = _userDTO?.Id ?? 0,
                    Email = this.txtEmail.Text.Trim(),
                    LastName = this.txtLastName.Text.Trim(),
                    Name = this.txtName.Text.Trim(),
                    Password = password,
                    UserName = this.txtUserName.Text.Trim(),
                };

                userBLL.Save(userDTO);

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

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.txtConfirmPassword.Enabled = this.txtPassword.Text.Length > 0;
            
        }
    }
}
