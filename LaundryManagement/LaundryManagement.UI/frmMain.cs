using LaundryManagement.BLL;
using LaundryManagement.Domain;

namespace LaundryManagement.UI
{
    public partial class frmMain : Form
    {
        private LoginBLL loginBLL;
        private Configuration configuration;
        public frmMain()
        {
            loginBLL = new LoginBLL();
            configuration = new Configuration();

            InitializeComponent();
            ApplySetup();
            ValidateForm();

        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;

            this.BackgroundImage = Image.FromFile(Path.GetFullPath(@"..\..\..\") + configuration.GetValue("imagePath"));
            this.BackgroundImageLayout = ImageLayout.Stretch;
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

        private void menuAdministrationUsers_Click(object sender, EventArgs e)
        {
            var frmAdmUsers = new frmAdministrationUsers();
            frmAdmUsers.MdiParent = this;
            frmAdmUsers.Show();
        }
    }
}
