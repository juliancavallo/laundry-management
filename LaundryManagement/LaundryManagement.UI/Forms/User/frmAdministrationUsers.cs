using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
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

namespace LaundryManagement.UI
{
    public partial class frmAdministrationUsers : Form
    {
        UserBLL userBLL;
        public frmAdministrationUsers()
        {
            InitializeComponent();
            ApplySetup();

            userBLL = new UserBLL();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridUsers.AllowUserToAddRows = false;
            this.gridUsers.AllowUserToDeleteRows = false;
            this.gridUsers.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridUsers.MultiSelect = false;
            this.gridUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void ReloadGridEvent(object sender, EventArgs e)
        {
            this.gridUsers.DataSource = null;
            this.gridUsers.DataSource = userBLL.GetAllForView();
            this.gridUsers.Columns["Id"].Visible = false;
        }

        private void frmAdministrationUsers_Load(object sender, EventArgs e)
        {
            this.ReloadGridEvent(sender, e);
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            var frmNewUser = new frmNewUser(null);
            frmNewUser.FormClosed += ReloadGridEvent;
            frmNewUser.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridUsers);

                var selectedId = ((UserViewDTO)this.gridUsers.CurrentRow.DataBoundItem).Id;
                if(selectedId == Session.Instance.User.Id)
                    throw new ValidationException("Cannot edit the user with which you are logged in", ValidationType.Warning);

                var dto = userBLL.GetById(selectedId);

                var frmNewUser = new frmNewUser(dto);
                frmNewUser.FormClosed += ReloadGridEvent;
                frmNewUser.ShowDialog();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridUsers);

                var selectedId = ((UserViewDTO)this.gridUsers.CurrentRow.DataBoundItem).Id;
                var dto = userBLL.GetById(selectedId);
                userBLL.Delete(dto);
                ReloadGridEvent(sender, e);
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

        private void btnViewRoles_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridUsers);

                var selectedId = ((UserViewDTO)this.gridUsers.CurrentRow.DataBoundItem).Id;

                var dto = userBLL.GetById(selectedId);

                var frmUserRoles = new frmUserRoles(dto, false);
                frmUserRoles.ShowDialog();
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
