using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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
    public partial class frmRoadmapReport : Form, ILanguageObserver
    {
        private RoadmapBLL roadmapBLL;
        private IList<Control> controls;
        public frmRoadmapReport()
        {
            roadmapBLL = new RoadmapBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnViewDetail, this.btnSearch, this.lblDateFrom, this.lblDateTo };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridRoadmaps.AllowUserToAddRows = false;
            this.gridRoadmaps.AllowUserToDeleteRows = false;
            this.gridRoadmaps.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridRoadmaps.MultiSelect = false;
            this.gridRoadmaps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridRoadmaps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


            this.Tag = "Roadmaps";
            this.btnViewDetail.Tag = "ViewDetail";
            this.btnSearch.Tag = "Search";
            this.lblDateTo.Tag = "DateTo";
            this.lblDateFrom.Tag = "DateFrom";

            this.dateFrom.Value = DateTime.Now.AddDays(-7);
            this.dateTo.Value = DateTime.Now;
        }

        private void ReloadGridEvent(RoadmapFilter filter)
        {
            filter.IdLocationOrigin = Session.Instance.User.Location.Id;

            this.gridRoadmaps.DataSource = null;
            this.gridRoadmaps.DataSource = roadmapBLL.GetAllForView(filter);
            this.gridRoadmaps.Columns["Id"].Visible = false;
        }

        private void frmRoadmapReport_Load(object sender, EventArgs e)
        {
            try
            {
                var filter = new RoadmapFilter();
                filter.DateFrom = this.dateFrom.Value;
                filter.DateTo = this.dateTo.Value;

                this.ReloadGridEvent(filter);
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

        private void frmRoadmapReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridRoadmaps);
                var selectedRow = (RoadmapViewDTO)this.gridRoadmaps.CurrentRow.DataBoundItem;
                var frm = new frmRoadmapDetailReport(selectedRow.Id);
                frm.Show();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var filter = new RoadmapFilter();
                filter.DateFrom = this.dateFrom.Value;
                filter.DateTo = this.dateTo.Value;

                this.ReloadGridEvent(filter);
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
    }
}
