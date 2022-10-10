using LaundryManagement.BLL;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Roadmap
{
    public partial class frmAdministrationRoadmaps : Form, ILanguageObserver
    {
        RoadmapBLL roadmapBLL;
        private IList<Control> controls;

        public frmAdministrationRoadmaps()
        {
            roadmapBLL = new RoadmapBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnNewRoadmap };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridRoadmap.AllowUserToAddRows = false;
            this.gridRoadmap.AllowUserToDeleteRows = false;
            this.gridRoadmap.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridRoadmap.MultiSelect = false;
            this.gridRoadmap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridRoadmap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.btnNewRoadmap.TabIndex = 0;

            this.Tag = "Roadmaps";
            this.btnNewRoadmap.Tag = "Create";

        }

        private void ReloadGridEvent()
        {
            try
            {
                this.gridRoadmap.DataSource = null;
                this.gridRoadmap.DataSource = roadmapBLL.GetAllForView(new RoadmapFilter());
                this.gridRoadmap.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNewRoadmap_Click(object sender, EventArgs e)
        {
            var frm = new frmNewRoadmap();
            frm.FormClosing += new FormClosingEventHandler((sender, e) => ReloadGridEvent());
            frm.Show();
        }

        private void frmAdministrationRoadmaps_Load(object sender, EventArgs e)
        {
            try
            {
                this.ReloadGridEvent();
                Session.SubscribeObserver(this);
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

        private void frmAdministrationRoadmaps_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);
    }
}
