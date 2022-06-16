using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmLogin : Form, ILanguageObserver
    {
        private LoginBLL loginBLL;
        private TranslatorBLL translatorBLL;
        private IList<Control> controls;
        public frmLogin()
        {
            loginBLL = new LoginBLL();
            translatorBLL = new TranslatorBLL();

            InitializeComponent();
            ApplySetup();

            loginBLL.SeedData();

            controls = new List<Control>() { this, this.lblEmail, this.lblPassword, this.lblResetPassword, this.btnLogin};

            Translate();
            PopulateComboLanguages();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.txtPassword.PasswordChar = '*';
            this.comboLanguages.DropDownStyle = ComboBoxStyle.DropDownList;

            this.txtEmail.TabIndex = 0;
            this.txtPassword.TabIndex = 1;
            this.btnLogin.TabIndex = 2;

            this.txtEmail.Text = "jcavallo11@gmail.com";
            this.txtPassword.Text = "1234";

            this.Tag = "Login";
            this.lblEmail.Tag = "Email";
            this.lblPassword.Tag = "Password";
            this.lblResetPassword.Tag = "ResetPassword";
            this.btnLogin.Tag = "Login";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>
                {
                    this.txtEmail, this.txtPassword
                });

                var loginDTO = new LoginDTO(
                    this.txtEmail.Text.Trim(), 
                    this.txtPassword.Text.Trim()
                    );

                loginBLL.Login(loginDTO);

                Session.ChangeLanguage(Session.Instance.User.Language);

                this.CloseForm();
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

        private void CloseForm()
        {
            frmMain frmParent = (frmMain)this.MdiParent;
            frmParent.ValidateForm();
            this.Close();
        }

        private void lblResetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmResetPassword frm = new frmResetPassword(this.comboLanguages.SelectedItem as ILanguage);
            frm.FormClosed += (sernder, e) => 
            { 
                this.txtEmail.Clear();
                this.txtPassword.Clear();
            };
            frm.ShowDialog();
        }

        #region Language
        public void UpdateLanguage(ILanguage language)
        {
            Translate(language);
        }

        private void Translate(ILanguage language = null)
        {
            var translations = translatorBLL.GetTranslations((Language)language);

            FormValidation.Translate(translations, controls);
        }

        private void PopulateComboLanguages()
        {
            this.comboLanguages.DataSource = null;
            this.comboLanguages.DataSource = translatorBLL.GetAllLanguages();
            this.comboLanguages.DisplayMember = "Name";
            this.comboLanguages.ValueMember = "Id";

            this.comboLanguages.SelectedValue = translatorBLL.GetDefaultLanguage().Id;
        }

        private void comboLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as ILanguage;
            Translate(translatorBLL.GetById(selectedItem.Id));
        }
        #endregion

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Session.SubsribeObserver(this);
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.UnsubsribeObserver(this);
        }
    }
}
