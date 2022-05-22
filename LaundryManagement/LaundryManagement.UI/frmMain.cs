using LaundryManagement.BLL;
using LaundryManagement.Services;

namespace LaundryManagement.UI
{
    public partial class frmMain : Form
    {
        private LoginBLL loginBLL;
        public frmMain()
        {
            loginBLL = new LoginBLL();
            
            InitializeComponent();
            ApplySetup();
            ValidateForm();

        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;
        }

        public void ValidateForm()
        {
            ShowMenus();

            if(!loginBLL.IsLogged())
                ShowLogin();

        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            loginBLL.Logout();
            ShowMenus();
            ShowLogin();
        }

        private void ShowMenus()
        {
            //Mostrar según los permisos del usuario
            foreach (ToolStripItem item in this.menuStrip1.Items)
            {
                item.Visible = loginBLL.IsLogged();
            }
            this.menuStrip1.Enabled = loginBLL.IsLogged();
        }

        private void ShowLogin()
        {
            var frmLogin = new frmLogin();
            frmLogin.MdiParent = this;
            frmLogin.Show();
        }
    }
}
