using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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
        private UserBLL userBLL;
        public UserDTO _userDTO;
        public frmNewUser(UserDTO paramDTO)
        {
            userBLL = new UserBLL();
            _userDTO = paramDTO;

            InitializeComponent();
            ApplySetup();
        }

        private void ApplySetup()
        {
            this.txtEmail.Text = _userDTO?.Email;
            this.txtPassword.Text = _userDTO?.Password;
            this.txtLastName.Text = _userDTO?.LastName;
            this.txtName.Text = _userDTO?.Name;
            this.txtUserName.Text = _userDTO?.UserName;

            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Enabled = _userDTO == null;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>
                {
                    this.txtEmail, this.txtPassword, this.txtLastName, this.txtName, this.txtUserName
                });

                var password = _userDTO?.Id == null ? Encryptor.Hash(this.txtPassword.Text.Trim()) : this.txtPassword.Text.Trim();

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
    }
}
