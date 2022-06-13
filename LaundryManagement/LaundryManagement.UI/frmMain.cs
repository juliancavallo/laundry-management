using LaundryManagement.BLL;
using LaundryManagement.Domain;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmMain : Form
    {
        private LoginBLL loginBLL;
        private TranslatorBLL translatorBLL;
        private Configuration configuration;
        private UserBLL userBLL;
        public frmMain()
        {
            loginBLL = new LoginBLL();
            translatorBLL = new TranslatorBLL();
            userBLL = new UserBLL();
            configuration = new Configuration();

            InitializeComponent();
            ApplySetup();
            ValidateForm();

        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;

            this.BackgroundImage = Image.FromFile(Path.GetFullPath(@"..\..\..\") + configuration.GetValue<string>("imagePath"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.menuLogout.Alignment = ToolStripItemAlignment.Right;
            this.menuLanguage.Alignment = ToolStripItemAlignment.Right;
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
            PopulateLanguageMenu();
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

        private void CheckLanguage(ILanguage language)
        {
            foreach (var item in this.menuLanguage.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = language.Id.Equals(((ILanguage)((ToolStripMenuItem)item).Tag).Id);
            }
        }

        private void languageItem_Click(object sender, EventArgs e)
        {
            Session.ChangeLanguage((ILanguage)((ToolStripMenuItem)sender).Tag);
            userBLL.Save((UserDTO)Session.Instance.User);
            CheckLanguage(Session.Instance.User.Language);
        }

        private void PopulateLanguageMenu()
        {
            this.menuLanguage.DropDownItems.Clear();

            foreach (var language in translatorBLL.GetAllLanguages())
            {
                var item = new ToolStripMenuItem();
                item.Text = language.Name;
                item.Tag = language;
                item.Click += languageItem_Click;
                this.menuLanguage.DropDownItems.Add(item);
            }
            if (loginBLL.IsLogged())
                CheckLanguage(Session.Instance.User.Language);
        }
    }
}
