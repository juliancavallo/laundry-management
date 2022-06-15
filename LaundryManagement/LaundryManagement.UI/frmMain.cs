using LaundryManagement.BLL;
using LaundryManagement.Domain;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmMain : Form, ILanguageObserver
    {
        private LoginBLL loginBLL;
        private TranslatorBLL translatorBLL;
        private Configuration configuration;
        private UserBLL userBLL;
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
                this.menuReportsMovements, this.menuReportsShippings, this.menuLanguage, this.menuLogout };


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

        private void Translate(ILanguage language = null)
        {
            var translations = translatorBLL.GetTranslations(language);

            FormValidation.Translate(translations, controls);
            FormValidation.Translate(translations, toolStripItems);
        }

        public void UpdateLanguage(ILanguage language)
        {
            Translate(language);
            CheckLanguage(language);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Session.SubsribeObserver(this);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Session.UnsubsribeObserver(this);
        }
    }
}
