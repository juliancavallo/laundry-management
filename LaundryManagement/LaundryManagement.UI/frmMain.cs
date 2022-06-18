using LaundryManagement.BLL;
using LaundryManagement.Domain;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using LaundryManagement.UI.Forms.Translations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmMain : Form, ILanguageObserver
    {
        private LoginBLL loginBLL;
        private TranslatorBLL translatorBLL;
        private UserBLL userBLL;

        private Configuration configuration;
        private IList<Control> controls;
        private IList<ToolStripItem> toolStripItems;
        public frmMain()
        {
            loginBLL = new LoginBLL();
            translatorBLL = new TranslatorBLL();
            userBLL = new UserBLL();
            configuration = new Configuration();

            InitializeComponent();
            ApplySetup();
            ValidateForm();

            controls = new List<Control>() { this };
            toolStripItems = new List<ToolStripItem>() { this.menuAdministration, this.menuAdministrationArticles, this.menuAdministrationCategories, 
                this.menuAdministrationItemTypes, this.menuAdministrationUsers, this.menuProcesses, this.menuProcessesClinicReception, 
                this.menuProcessesClinicShipping, this.menuProcessesInternalShipping, this.menuProcessesItemCreation, this.menuProcessesItemRemoval, 
                this.menuProcessesLaundryReception, this.menuProcessesLaundryShipping, this.menuProcessesRoadMap, this.menuReports, this.menuReports, 
                this.menuReportsMovements, this.menuReportsShippings, this.menuLanguage, this.menuLogout, this.menuLanguageManage };

            PopulateLanguageMenu();
            Translate();
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

            this.Tag = "MainMenu";
            this.menuAdministration.Tag = "Administration";
            this.menuProcesses.Tag = "Processes";
            this.menuReports.Tag = "Reports";
            this.menuAdministrationUsers.Tag = "Users";
            this.menuAdministrationArticles.Tag = "Articles";
            this.menuAdministrationCategories.Tag = "Categories";
            this.menuAdministrationItemTypes.Tag = "ItemTypes";
            this.menuProcessesClinicReception.Tag = "ClinicReception";
            this.menuProcessesClinicShipping.Tag = "ClinicShipping";
            this.menuProcessesInternalShipping.Tag = "InternalShipping";
            this.menuProcessesItemCreation.Tag = "ItemCreation";
            this.menuProcessesItemRemoval.Tag = "ItemRemoval";
            this.menuProcessesLaundryReception.Tag = "LaundryReception";
            this.menuProcessesLaundryShipping.Tag = "LaundryShipping";
            this.menuProcessesRoadMap.Tag = "RoadMap";
            this.menuReportsMovements.Tag = "Movements";
            this.menuReportsShippings.Tag = "Shippings";
            this.menuLanguage.Tag = "Language";
            this.menuLogout.Tag = "Logout";
            this.menuLanguageManage.Tag = "Administration";
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
            foreach(var form in this.MdiChildren)
            {
                form.Close();
            }
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
        private void menuLanguageManage_Click(object sender, EventArgs e)
        {
            var frm = new frmTranslations();
            frm.MdiParent = this;   
            frm.Show();
        }

        private void languageItem_Click(object sender, EventArgs e)
        {
            var language = (Language)((ToolStripMenuItem)sender).Tag;

            Session.SetTranslations(translatorBLL.GetTranslations(language));
            Session.ChangeLanguage(language);
            
            userBLL.Save((UserDTO)Session.Instance.User);
            CheckLanguage(Session.Instance.User.Language);
        }

        private void PopulateLanguageMenu()
        {
            this.menuLanguage.DropDownItems.Clear();
            menuLanguage.DropDownItems.Add(menuLanguageManage);
            menuLanguage.DropDownItems.Add(new ToolStripSeparator());

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

        private void CheckLanguage(ILanguage language)
        {
            foreach (var item in this.menuLanguage.DropDownItems)
            {
                if(item is ToolStripMenuItem)
                {
                    if((item as ToolStripMenuItem).Tag is ILanguage)
                        (item as ToolStripMenuItem).Checked = language.Id.Equals(((ILanguage)(item as ToolStripMenuItem).Tag).Id);
                }
            }
        }

        private void Translate()
        {
            FormValidation.Translate(Session.Translations, controls);
            FormValidation.Translate(Session.Translations, toolStripItems);
        }

        public void UpdateLanguage(ILanguage language)
        {
            Translate();
            CheckLanguage(language);
        }

        private void frmMain_Load(object sender, EventArgs e) => Session.SubsribeObserver(this);

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubsribeObserver(this);

    }
}
