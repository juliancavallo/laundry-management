using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Reception
{
    public partial class frmReceptionReport : Form, ILanguageObserver
    {
        private ReceptionBLL receptionBLL;
        private IList<Control> controls;
        public frmReceptionReport()
        {
            receptionBLL = new ReceptionBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnViewDetail, this.btnSearch, this.lblDateFrom, this.lblDateTo };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridReceptions.AllowUserToAddRows = false;
            this.gridReceptions.AllowUserToDeleteRows = false;
            this.gridReceptions.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridReceptions.MultiSelect = false;
            this.gridReceptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridReceptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


            this.Tag = "Receptions";
            this.btnViewDetail.Tag = "ViewDetail";
            this.btnSearch.Tag = "Search";
            this.lblDateTo.Tag = "DateTo";
            this.lblDateFrom.Tag = "DateFrom";

            this.dateFrom.Value = DateTime.Now.AddDays(-7);
            this.dateTo.Value = DateTime.Now;
        }

        private void ReloadGridEvent(ReceptionFilter filter)
        {
            this.gridReceptions.DataSource = null;
            this.gridReceptions.DataSource = receptionBLL.GetAllForView(filter);
            this.gridReceptions.Columns["Id"].Visible = false;
        }

        private void frmReceptionReport_Load(object sender, EventArgs e)
        {
            try
            {
                var filter = new ReceptionFilter();
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

        private void frmReceptionReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridReceptions);
                var selectedRow = (ReceptionViewDTO)this.gridReceptions.CurrentRow.DataBoundItem;
                var frm = new frmReceptionDetailReport(selectedRow.Id);
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
                var filter = new ReceptionFilter();
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
