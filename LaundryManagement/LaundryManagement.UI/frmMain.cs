using LaundryManagement.BLL;
using LaundryManagement.Domain;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using LaundryManagement.UI.Forms.Backups;
using LaundryManagement.UI.Forms.Logs;
using LaundryManagement.UI.Forms.Reception;
using LaundryManagement.UI.Forms.Roadmap;
using LaundryManagement.UI.Forms.Roles;
using LaundryManagement.UI.Forms.Shipping;
using LaundryManagement.UI.Forms.Stock;
using LaundryManagement.UI.Forms.Traceability;
using LaundryManagement.UI.Forms.Translations;
using Microsoft.Extensions.Configuration;
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
        private UserPermissionBLL userPermissionBLL;

        private IList<Control> controls;
        private IList<ToolStripItem> toolStripItems;
        public frmMain()
        {
            loginBLL = new LoginBLL();
            translatorBLL = new TranslatorBLL();
            userBLL = new UserBLL();
            userPermissionBLL = new UserPermissionBLL();

            InitializeComponent();
            ApplySetup();
            ValidateForm();

            controls = new List<Control>() { this };
            toolStripItems = new List<ToolStripItem>() { this.menuAdministration, this.menuAdministrationArticles, this.menuAdministrationCategories, 
                this.menuAdministrationItemTypes, this.menuAdministrationUsers, this.menuProcesses, this.menuProcessesReception, 
                this.menuProcessesClinicShipping, this.menuProcessesInternalShipping, this.menuProcessesItemCreation, this.menuProcessesItemRemoval, 
                this.menuProcessesReception, this.menuProcessesLaundryShipping, this.menuProcessesRoadMap, this.menuReports, this.menuReports, 
                this.menuReportsLaundryShippings, this.menuLanguage, this.menuLogout, this.menuLanguageManage, 
                this.menuReportsTraceability, this.menuReportsClinicShippings, this.menuReportsStock, this.menuAdministrationBackups, this.menuReportsRoadmaps,
                this.menuReportsLogs, this.menuReportsReceptions, this.menuAdministrationStockImport};

            PopulateLanguageMenu();
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;

            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var imagePath = Program.Configuration.GetSection("imagePath").Value.ToString();
            this.BackgroundImage = Image.FromFile(Path.Combine(appPath, imagePath));
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
            this.menuProcessesReception.Tag = new MenuItemMetadataDTO { TagName = "Reception", Permission = "PRO_REC" };
            this.menuProcessesClinicShipping.Tag = new MenuItemMetadataDTO { TagName = "ClinicShipping", Permission = "PRO_SHP_CLI" };
            this.menuProcessesInternalShipping.Tag = new MenuItemMetadataDTO { TagName = "InternalShipping", Permission = "PRO_SHP_INT" };
            this.menuProcessesItemCreation.Tag = new MenuItemMetadataDTO { TagName = "ItemCreation", Permission = "PRO_ITM_NEW" };
            this.menuProcessesItemRemoval.Tag = new MenuItemMetadataDTO { TagName = "ItemRemoval", Permission = "PRO_ITM_DEL" };
            this.menuProcessesLaundryShipping.Tag = new MenuItemMetadataDTO { TagName = "LaundryShipping", Permission = "PRO_SHP_LDY" };
            this.menuProcessesRoadMap.Tag = new MenuItemMetadataDTO { TagName = "RoadMap", Permission = "PRO_ROA" };
            this.menuReportsTraceability.Tag = new MenuItemMetadataDTO { TagName = "Traceability", Permission = "REP_TRA" };
            this.menuReportsLaundryShippings.Tag = new MenuItemMetadataDTO { TagName = "LaundryShippings", Permission = "REP_SHP_LDY" };
            this.menuReportsClinicShippings.Tag = new MenuItemMetadataDTO { TagName = "ClinicShippings", Permission = "REP_SHP_CLI" };
            this.menuReportsStock.Tag = new MenuItemMetadataDTO { TagName = "Stock", Permission = "REP_STK" };
            this.menuLanguage.Tag = new MenuItemMetadataDTO { TagName = "Language", Permission = "" };
            this.menuLogout.Tag = new MenuItemMetadataDTO { TagName = "Logout", Permission = "" };
            this.menuLanguageManage.Tag = new MenuItemMetadataDTO { TagName = "Administration", Permission = "LAN" };
            this.menuAdministrationBackups.Tag = new MenuItemMetadataDTO { TagName = "Backups", Permission = "ADM_BCK" };
            this.menuReportsRoadmaps.Tag = new MenuItemMetadataDTO { TagName = "RoadMap", Permission = "REP_ROA" };
            this.menuReportsLogs.Tag = new MenuItemMetadataDTO { TagName = "Logs", Permission = "REP_LOGS" };
            this.menuReportsReceptions.Tag = new MenuItemMetadataDTO { TagName = "Reception", Permission = "REP_REC" };
            this.menuAdministrationStockImport.Tag = new MenuItemMetadataDTO { TagName = "StockImport", Permission = "REP_STK" };
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
                item.Visible = loginBLL.IsLogged() && userPermissionBLL.HasPermission((UserDTO)Session.Instance.User, permission);

                bool hasChildren = false;

                foreach(var subitem in item.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem && ((ToolStripMenuItem)subitem).Tag is MenuItemMetadataDTO)
                    {
                        var castedSubitem = (ToolStripMenuItem)subitem;
                        var subpermission = ((MenuItemMetadataDTO)(castedSubitem).Tag).Permission;
                        bool visible = loginBLL.IsLogged() && userPermissionBLL.HasPermission((UserDTO)Session.Instance.User, subpermission);
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
                    if (item is ToolStripMenuItem)
                    {
                        if ((item as ToolStripMenuItem).Tag is ILanguage)
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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) 
        {
            loginBLL.Logout();
            Session.UnsubscribeObserver(this); 
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

        private void menuReportsTraceability_Click(object sender, EventArgs e)
        {
            var frm = new frmTraceabilityReport();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuProcessesClinicShipping_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationShippings(ShippingTypeEnum.ToClinic);
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuProcessesInternalShipping_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationShippings(ShippingTypeEnum.Internal);
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuReportsClinicShippings_Click(object sender, EventArgs e)
        {
            var frm = new frmShippingReport(ShippingTypeEnum.ToClinic);
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuReportsStock_Click(object sender, EventArgs e)
        {
            var frm = new frmStockReport();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuProcessesRoadMap_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationRoadmaps();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuReportsLogs_Click(object sender, EventArgs e)
        {
            var frm = new frmLogs();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuAdministrationBackups_Click(object sender, EventArgs e)
        {
            var frm = new frmBackupRestore();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuReportsRoadmaps_Click(object sender, EventArgs e)
        {
            var frm = new frmRoadmapReport();
            frm.MdiParent = this;
            frm.Show();
        }


        private void menuProcessesReception_Click(object sender, EventArgs e)
        {
            var frm = new frmAdministrationReceptions();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuReportsReceptions_Click(object sender, EventArgs e)
        {
            var frm = new frmReceptionReport();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuAdministrationStockImport_Click(object sender, EventArgs e)
        {
            var frm = new frmStockImport();
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion

    }
}
