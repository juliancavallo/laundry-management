using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.User
{
    public partial class frmUserHistory : Form, ILanguageObserver
    {
        private UserHistoryBLL userHistoryBLL;

        private IList<Control> controls;
        private int _userId;

        public frmUserHistory(int userId)
        {
            userHistoryBLL = new UserHistoryBLL();

            _userId = userId;

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnView };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridHistory.AllowUserToAddRows = false;
            this.gridHistory.AllowUserToDeleteRows = false;
            this.gridHistory.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridHistory.MultiSelect = false;
            this.gridHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "UserHistory";
            this.btnView.Tag = "View";
        }

        private void ReloadGridEvent(object sender, EventArgs e)
        {
            this.gridHistory.DataSource = null;
            this.gridHistory.DataSource = userHistoryBLL.GetHistoryForView(_userId);
            this.gridHistory.Columns["Id"].Visible = false;
        }

        private void frmUserHistory_Load(object sender, EventArgs e)
        {
            try
            {
                this.ReloadGridEvent(sender, e);
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

        private void frmAdministrationUsers_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridHistory);

                var selectedId = ((UserHistoryViewDTO)this.gridHistory.CurrentRow.DataBoundItem).Id;

                var frmUserHistory = new frmUserHistoryDetail(userHistoryBLL.GetHistoryById(selectedId));

                frmUserHistory.Show();
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
