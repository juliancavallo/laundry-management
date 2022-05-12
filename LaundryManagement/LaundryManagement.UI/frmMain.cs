using LaundryManagement.Services;

namespace LaundryManagement.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            ApplySetup();
            ValidateForm();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;
        }

        private void ValidateForm()
        {
            if(Session.Instance == null)
            {
                var frmLogin = new frmLogin();
                frmLogin.MdiParent = this;
                frmLogin.Show();
            }
        }
    }
}
