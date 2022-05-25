using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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

            this.lblSelectedUser.Text = "";

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
            var selectedId = ((UserViewDTO)this.gridUsers.CurrentRow.DataBoundItem).Id;
            var dto = userBLL.GetById(selectedId);

            var frmNewUser = new frmNewUser(dto);
            frmNewUser.FormClosed += ReloadGridEvent;
            frmNewUser.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedId = ((UserViewDTO)this.gridUsers.CurrentRow.DataBoundItem).Id;
            var dto = userBLL.GetById(selectedId);
            userBLL.Delete(dto);
        }

        private void gridUsers_SelectionChanged(object sender, EventArgs e)
        {
            if(this.gridUsers.CurrentRow != null)
            {
                var selected = (UserViewDTO)this.gridUsers.CurrentRow.DataBoundItem;
                this.lblSelectedUser.Text = selected.UserName;
            }
        }

    }
}
