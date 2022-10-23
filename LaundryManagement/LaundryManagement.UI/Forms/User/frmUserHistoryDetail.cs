using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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

namespace LaundryManagement.UI.Forms.User
{
    public partial class frmUserHistoryDetail : Form, ILanguageObserver
    {
        public UserHistoryDTO _historyDTO;
        private UserHistoryBLL userHistoryBLL;

        private IList<Control> controls;
        public frmUserHistoryDetail(UserHistoryDTO historyDTO)
        {
            userHistoryBLL = new UserHistoryBLL();
            _historyDTO = historyDTO;

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnApplyHistory, this.lblEmail, this.lblFirstName, this.lblLastName, this.lblLanguage, this.lblPassword, this.lblUserName, this.lblLocation };
            Translate();
        }

        private void ApplySetup()
        {
            this.txtEmail.Text = _historyDTO.Email;
            this.txtLastName.Text = _historyDTO.LastName;
            this.txtFirstName.Text = _historyDTO.Name;
            this.txtUserName.Text = _historyDTO.UserName;
            this.txtLanguage.Text = _historyDTO.Language.Name;
            this.txtLocation.Text = _historyDTO.Location.CompleteName;
            this.txtPassword.Text = _historyDTO.Password;

            this.lblFirstName.Tag = "Name";
            this.lblLastName.Tag = "LastName";
            this.lblUserName.Tag = "UserName";
            this.lblEmail.Tag = "Email";
            this.lblPassword.Tag = "Password";
            this.lblLanguage.Tag = "Language";
            this.lblLocation.Tag = "Location";
            this.btnApplyHistory.Tag = "Apply";
            this.Tag = "UserHistory";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.StartPosition = FormStartPosition.CenterParent;

            var textboxes = new List<TextBox>() { this.txtEmail, this.txtLastName, this.txtFirstName, this.txtUserName, this.txtLanguage, this.txtLocation, this.txtPassword };
            textboxes.ForEach(x => x.Enabled = false);
        }

        private void btnApplyHistory_Click(object sender, EventArgs e)
        {
            try
            {
                userHistoryBLL.ApplyHistory(_historyDTO);
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

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmNewUser_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmNewUser_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);
    }
}
