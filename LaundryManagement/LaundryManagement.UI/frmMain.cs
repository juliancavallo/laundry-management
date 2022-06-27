using LaundryManagement.BLL;
using LaundryManagement.Domain;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using LaundryManagement.UI.Forms.Roles;
using LaundryManagement.UI.Forms.Shipping;
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
                this.menuReportsMovements, this.menuReportsLaundryShippings, this.menuLanguage, this.menuLogout, this.menuLanguageManage };

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
            this.menuAdministration.Tag = new MenuItemMetadataDTO { TagName = "Administration", Permission = "ADM" };
            this.menuProcesses.Tag = new MenuItemMetadataDTO { TagName = "Processes", Permission = "PRO" };
            this.menuReports.Tag = new MenuItemMetadataDTO { TagName = "Reports", Permission = "REP" };
            this.menuAdministrationUsers.Tag = new MenuItemMetadataDTO { TagName = "Users", Permission = "ADM_USR" };
            this.menuAdministrationLocations.Tag = new MenuItemMetadataDTO { TagName = "Locations", Permission = "ADM_LOC" };
            this.menuAdministrationArticles.Tag = new MenuItemMetadataDTO { TagName = "Articles", Permission = "ADM_ART" };
            this.menuAdministrationSizes.Tag = new MenuItemMetadataDTO { TagName = "Sizes", Permission = "ADM_SIZ" };
            this.menuAdministrationCategories.Tag = new MenuItemMetadataDTO { TagName = "Categories", Permission = "ADM_CAT" };
            this.menuAdministrationItemTypes.Tag = new MenuItemMetadataDTO { TagName = "ItemTypes", Permission = "ADM_TYP" };
            this.menuProcessesClinicReception.Tag = new MenuItemMetadataDTO { TagName = "ClinicReception", Permission = "PRO_REC_CLI" };
            this.menuProcessesClinicShipping.Tag = new MenuItemMetadataDTO { TagName = "ClinicShipping", Permission = "PRO_SHP_CLI" };
            this.menuProcessesInternalShipping.Tag = new MenuItemMetadataDTO { TagName = "InternalShipping", Permission = "PRO_SHP_INT" };
            this.menuProcessesItemCreation.Tag = new MenuItemMetadataDTO { TagName = "ItemCreation", Permission = "PRO_ITM_NEW" };
            this.menuProcessesItemRemoval.Tag = new MenuItemMetadataDTO { TagName = "ItemRemoval", Permission = "PRO_ITM_DEL" };
            this.menuProcessesLaundryReception.Tag = new MenuItemMetadataDTO { TagName = "LaundryReception", Permission = "PRO_REC_LDY" };
            this.menuProcessesLaundryShipping.Tag = new MenuItemMetadataDTO { TagName = "LaundryShipping", Permission = "PRO_SHP_LDY" };
            this.menuProcessesRoadMap.Tag = new MenuItemMetadataDTO { TagName = "RoadMap", Permission = "PRO_ROA" };
            this.menuReportsMovements.Tag = new MenuItemMetadataDTO { TagName = "Movements", Permission = "REP_MOV" };
            this.menuReportsLaundryShippings.Tag = new MenuItemMetadataDTO { TagName = "LaundryShippings", Permission = "REP_SHP_LDY" };
            this.menuLanguage.Tag = new MenuItemMetadataDTO { TagName = "Language", Permission = "" };
            this.menuLogout.Tag = new MenuItemMetadataDTO { TagName = "Logout", Permission = "" };
            this.menuLanguageManage.Tag = new MenuItemMetadataDTO { TagName = "Administration", Permission = "LAN" };
        }

        public void ValidateForm()
        {
            ShowMenus();

            if(!loginBLL.IsLogged())
                ShowLogin();

        }

        private void ShowMenus()
        {
            foreach (ToolStripMenuItem item in this.menuStrip1.Items)
            {
                var permission = ((MenuItemMetadataDTO)item.Tag).Permission;
                item.Visible = loginBLL.IsLogged() && userBLL.HasPermission((UserDTO)Session.Instance.User, permission);

                bool hasChildren = false;

                foreach(var subitem in item.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem && ((ToolStripMenuItem)subitem).Tag is MenuItemMetadataDTO)
                    {
                        var castedSubitem = (ToolStripMenuItem)subitem;
                        var subpermission = ((MenuItemMetadataDTO)(castedSubitem).Tag).Permission;
                        bool visible = loginBLL.IsLogged() && userBLL.HasPermission((UserDTO)Session.Instance.User, subpermission);
                        castedSubitem.Visible = visible;

                        if(visible) 
                            hasChildren = true;
                    }
                }

                item.Visible = hasChildren || permission == "";
            }

            this.menuStrip1.Enabled = loginBLL.IsLogged();
        }

        private void ShowLogin()
        {
            var frmLogin = new frmLogin();
            frmLogin.MdiParent = this;
            frmLogin.Show();
        }

        #region Menus
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

        private void menuAdministrationUsers_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationUsers();
            frm.MdiParent = this;
            frm.FormClosing += new FormClosingEventHandler((sender, e) => ShowMenus());
            frm.Show();
        }
        private void menuLanguageManage_Click(object sender, EventArgs e)
        {
            var frm = new frmTranslations();
            frm.MdiParent = this;   
            frm.Show();
            frm.FormClosing += new FormClosingEventHandler((sender, e) => PopulateLanguageMenu());
        }

        private void languageItem_Click(object sender, EventArgs e)
        {
            var language = (Language)((ToolStripMenuItem)sender).Tag;

            Session.SetTranslations(translatorBLL.GetTranslations(language));
            Session.ChangeLanguage(language);
            
            userBLL.Save((UserDTO)Session.Instance.User);
            CheckLanguage(Session.Instance.User.Language);
        }

        private void menuProcessesLaundryShipping_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationShippings(ShippingTypeEnum.ToLaundry);
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuAdministrationRoles_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationPermissions();
            frm.MdiParent = this;
            frm.FormClosing += new FormClosingEventHandler((sender, e) => ShowMenus());
            frm.Show();
        }

        private void menuReportsLaundryShippings_Click(object sender, EventArgs e)
        {
            var frm = new frmShippingReport(ShippingTypeEnum.ToLaundry);
            frm.MdiParent = this;
            frm.Show();
        }

        #endregion

        #region Language

        private void PopulateLanguageMenu()
        {
            try
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
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void CheckLanguage(ILanguage language)
        {
            try
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
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void Translate()
        {
            FormValidation.Translate(Session.Translations, controls);
            FormValidation.Translate(Session.Translations, toolStripItems);
        }

        public void UpdateLanguage(ILanguage language)
        {
            try
            {
                Translate();
                CheckLanguage(language);
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }
        #endregion
        private void frmMain_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

    }
}
