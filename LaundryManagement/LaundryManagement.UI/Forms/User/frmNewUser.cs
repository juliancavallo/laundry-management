using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmNewUser : Form, ILanguageObserver
    {
        public UserDTO userDTO;
        private UserBLL userBLL;
        private TranslatorBLL translatorBLL;
        private LocationBLL locationBLL;
        private LogBLL logBLL;

        private IList<Control> controls;
        private SecurityService securityService;
        public frmNewUser(UserDTO paramDTO)
        {
            userBLL = new UserBLL();
            translatorBLL = new TranslatorBLL();
            logBLL = new LogBLL();
            locationBLL = new LocationBLL();
            userDTO = paramDTO;
            securityService = new SecurityService();

            InitializeComponent();
            PopulateComboLanguages();
            PopulateComboLocations();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSave, this.lblConfirmPassword, this.lblEmail, this.lblFirstName, this.lblLastName, this.lblLanguage, this.lblPassword, this.lblUserName, this.lblLocation };
            Translate();
        }

        private void ApplySetup()
        {
            this.txtEmail.Text = userDTO?.Email;
            this.txtLastName.Text = userDTO?.LastName;
            this.txtFirstName.Text = userDTO?.Name;
            this.txtUserName.Text = userDTO?.UserName;
            if(userDTO != null)
            {
                this.comboLanguage.SelectedValue = userDTO.Language.Id;
                this.comboLocation.SelectedValue = userDTO.Location.Id;
            }

            this.txtPassword.PasswordChar = '*';
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtPassword.PlaceholderText = userDTO?.Id == null ? "" : Session.Translations[Tags.PasswordPlaceholder];
            this.txtConfirmPassword.Enabled = false;
            this.comboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboLocation.DropDownStyle = ComboBoxStyle.DropDownList;

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
            this.lblLocation.Tag = "Location";
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
                bool validatePasswords = this.txtPassword.Text.Length > 0 || this.userDTO == null;
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

                    if (!securityService.CheckPasswordSecurity(this.txtPassword.Text))
                    {
                        FormValidation.ShowPasswordUnsecureMessage();
                        return;
                    }
                }

                var password = validatePasswords ? Encryptor.HashToString(this.txtPassword.Text.Trim()) : this.userDTO.Password;

                var userDTO = new UserDTO()
                {
                    Id = this.userDTO?.Id ?? 0,
                    Email = this.txtEmail.Text.Trim(),
                    LastName = this.txtLastName.Text.Trim(),
                    Name = this.txtFirstName.Text.Trim(),
                    Password = password,
                    UserName = this.txtUserName.Text.Trim(),
                    Language = comboLanguage.SelectedItem as ILanguage,
                    Location = comboLocation.SelectedItem as ILocationDTO
                };

                userBLL.Save(userDTO);

                this.Close();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                switch (ex.ValidationType)
                {
                    case ValidationType.Warning:
                        logBLL.LogWarning(MovementTypeEnum.User, ex.Message);
                        break;

                    case ValidationType.Error:
                        logBLL.LogError(MovementTypeEnum.User, ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error); 
                logBLL.LogError(MovementTypeEnum.User, ex.Message);
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

        private void PopulateComboLocations()
        {
            try
            {
                this.comboLocation.DataSource = null;
                this.comboLocation.DataSource = locationBLL.GetAll();
                this.comboLocation.DisplayMember = "CompleteName";
                this.comboLocation.ValueMember = "Id";
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
