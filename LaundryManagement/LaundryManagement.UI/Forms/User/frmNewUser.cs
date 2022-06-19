using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmNewUser : Form, ILanguageObserver
    {
        public UserDTO _userDTO;
        private UserBLL userBLL;
        private TranslatorBLL translatorBLL;

        private IList<Control> controls;
        private SecurityService securityService;
        public frmNewUser(UserDTO paramDTO)
        {
            userBLL = new UserBLL();
            translatorBLL = new TranslatorBLL();
            _userDTO = paramDTO;
            securityService = new SecurityService();

            InitializeComponent();
            PopulateComboLanguages();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSave, this.lblConfirmPassword, this.lblEmail, this.lblFirstName, this.lblLastName, this.lblLanguage, this.lblPassword, this.lblUserName };
            Translate();
        }

        private void ApplySetup()
        {
            this.txtEmail.Text = _userDTO?.Email;
            this.txtLastName.Text = _userDTO?.LastName;
            this.txtFirstName.Text = _userDTO?.Name;
            this.txtUserName.Text = _userDTO?.UserName;
            if(_userDTO != null)
                this.comboLanguage.SelectedValue = _userDTO.Language.Id;

            this.txtPassword.PasswordChar = '*';
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtPassword.PlaceholderText = _userDTO?.Id == null ? "" : Session.Translations[Tags.PasswordPlaceholder].Text;
            this.txtConfirmPassword.Enabled = false;
            this.comboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;

            this.txtFirstName.TabIndex = 0;
            this.txtLastName.TabIndex = 1;
            this.txtUserName.TabIndex = 2;
            this.txtEmail.TabIndex = 3;
            this.txtPassword.TabIndex = 4;
            this.txtConfirmPassword.TabIndex = 5;
            this.btnSave.TabIndex = 6;

            this.lblFirstName.Tag = "Name";
            this.lblLastName.Tag = "LastName";
            this.lblUserName.Tag = "UserName";
            this.lblEmail.Tag = "Email";
            this.lblPassword.Tag = "Password";
            this.lblConfirmPassword.Tag = "RepeatPassword";
            this.lblLanguage.Tag = "Language";
            this.btnSave.Tag = "Save";
            this.Tag = "User";

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
                    this.txtEmail, this.txtLastName, this.txtFirstName, this.txtUserName
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
                    Name = this.txtFirstName.Text.Trim(),
                    Password = password,
                    UserName = this.txtUserName.Text.Trim(),
                    Language = comboLanguage.SelectedItem as ILanguage
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

        private void txtPassword_TextChanged(object sender, EventArgs e) => this.txtConfirmPassword.Enabled = this.txtPassword.Text.Length > 0;

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmNewUser_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmNewUser_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void PopulateComboLanguages()
        {
            try 
            {
                this.comboLanguage.DataSource = null;
                this.comboLanguage.DataSource = translatorBLL.GetAllLanguages();
                this.comboLanguage.DisplayMember = "Name";
                this.comboLanguage.ValueMember = "Id";

                this.comboLanguage.SelectedValue = translatorBLL.GetDefaultLanguage().Id;
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
