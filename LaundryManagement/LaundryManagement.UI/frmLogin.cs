using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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
    public partial class frmLogin : Form
    {
        LoginBLL loginBLL;
        public frmLogin()
        {
            InitializeComponent();
            ApplySetup();

            loginBLL = new LoginBLL();
        }
        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = false;
            this.txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = this.txtEmail.Text.Trim();
            var password = this.txtPassword.Text.Trim();
            
            var loginDTO = new LoginDTO(email, password);
            var response = loginBLL.Login(loginDTO);

            if (response.Success)
            {
                Session.Login(response.User);
                this.Close();
            }
            else
                MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
