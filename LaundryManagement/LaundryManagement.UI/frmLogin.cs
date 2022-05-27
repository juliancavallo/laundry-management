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
        private LoginBLL loginBLL;
        public frmLogin()
        {
            loginBLL = new LoginBLL();
            
            InitializeComponent();
            ApplySetup();
        }
        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.txtPassword.PasswordChar = '*';

            this.txtEmail.TabIndex = 0;
            this.txtPassword.TabIndex = 1;
            this.btnLogin.TabIndex = 2;

            this.txtEmail.Text = "jcavallo11@gmail.com";
            this.txtPassword.Text = "1234";

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

                var response = loginBLL.Login(loginDTO);

                if (response.Success)
                    this.CloseForm();
                else
                    FormValidation.ShowError(response.Message);
            }
            catch(Exception ex)
            {
                FormValidation.ShowError(ex.Message);
            }

        }

        private void CloseForm()
        {
            frmMain frmParent = (frmMain)this.MdiParent;
            frmParent.ValidateForm();
            this.Close();
        }
    }
}
