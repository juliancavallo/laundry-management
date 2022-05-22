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
            this.lblSelectedUser.Text = "";
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            var frmNewUser = new frmNewUser(null);
            frmNewUser.ShowDialog();
        }

        private void frmAdministrationUsers_Load(object sender, EventArgs e)
        {
            this.gridUsers.DataSource = null;
            this.gridUsers.DataSource = userBLL.GetAll();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = (UserDTO)this.gridUsers.CurrentRow.DataBoundItem;
            var frmNewUser = new frmNewUser(selected);
            frmNewUser.ShowDialog();
        }

        private void gridUsers_SelectionChanged(object sender, EventArgs e)
        {
            var selected = (UserDTO)this.gridUsers.CurrentRow.DataBoundItem;
            this.lblSelectedUser.Text = selected.UserName;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = (UserDTO)this.gridUsers.CurrentRow.DataBoundItem;
            userBLL.Delete(selected);
        }
    }
}
